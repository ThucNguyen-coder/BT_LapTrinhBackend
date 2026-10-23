using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using LHU_124000698_NguyenHuynhDuyThuc.Api.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _db;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(DatabaseContext db, IJwtTokenService jwtTokenService)
        {
            _db = db;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto req)
        {
            var user = await _db.NguoiDungs
                .Include(u => u.SinhVien)
                .FirstOrDefaultAsync(u => u.TenDangNhap == req.TenDangNhap);

            if (user == null)
            {
                return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không chính xác." });
            }

            // Kiểm tra mật khẩu băm với BCrypt.Verify
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(req.MatKhau, user.MatKhauHash);
            if (!isPasswordValid)
            {
                return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không chính xác." });
            }

            var hoTen = user.SinhVien?.HoTen ?? user.TenDangNhap;
            var token = _jwtTokenService.GenerateToken(user, hoTen);

            return Ok(new LoginResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2),
                HoTen = hoTen,
                VaiTro = user.VaiTro,
                SinhVienId = user.SinhVienId
            });
        }

        [Authorize]
        [HttpGet("me")]
        public ActionResult<UserInfoDto> GetProfile()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            var hoTen = User.FindFirst("HoTen")?.Value ?? "";
            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "";
            var svIdStr = User.FindFirst("SinhVienId")?.Value;

            int.TryParse(userIdStr, out int userId);
            int? sinhVienId = int.TryParse(svIdStr, out int svId) ? svId : null;

            return Ok(new UserInfoDto
            {
                Id = userId,
                TenDangNhap = username,
                HoTen = hoTen,
                VaiTro = role,
                SinhVienId = sinhVienId
            });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto req)
        {
            // Trích xuất định danh người dùng từ Token JWT đã ký số (bảo vệ chống IDOR)
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            NguoiDungEntity? user = null;

            if (!string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int userId))
            {
                user = await _db.NguoiDungs.FirstOrDefaultAsync(u => u.Id == userId);
            }
            else if (!string.IsNullOrEmpty(username))
            {
                user = await _db.NguoiDungs.FirstOrDefaultAsync(u => u.TenDangNhap == username);
            }

            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy thông tin người dùng." });
            }

            // Nếu client có truyền TenDangNhap, kiểm tra để đảm bảo khớp với tài khoản trong Token
            if (!string.IsNullOrWhiteSpace(req.TenDangNhap) &&
                !string.Equals(req.TenDangNhap, user.TenDangNhap, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "Tên đăng nhập không khớp với tài khoản đang đăng nhập." });
            }

            // 1. Kiểm tra mật khẩu hiện tại bằng BCrypt.Verify
            bool isCurrentPasswordValid = BCrypt.Net.BCrypt.Verify(req.MatKhauHienTai, user.MatKhauHash);
            if (!isCurrentPasswordValid)
            {
                return BadRequest(new { message = "Mật khẩu hiện tại không chính xác." });
            }

            // 2. Kiểm tra xác nhận mật khẩu mới (nếu client có truyền)
            if (!string.IsNullOrEmpty(req.XacNhanMatKhauMoi) && req.MatKhauMoi != req.XacNhanMatKhauMoi)
            {
                return BadRequest(new { message = "Xác nhận mật khẩu mới không trùng khớp." });
            }

            // 3. Kiểm tra mật khẩu mới không được trùng với mật khẩu hiện tại
            if (BCrypt.Net.BCrypt.Verify(req.MatKhauMoi, user.MatKhauHash))
            {
                return BadRequest(new { message = "Mật khẩu mới không được trùng với mật khẩu hiện tại." });
            }

            // 4. Băm mật khẩu mới bằng BCrypt.HashPassword và cập nhật vào cơ sở dữ liệu
            user.MatKhauHash = BCrypt.Net.BCrypt.HashPassword(req.MatKhauMoi);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Đổi mật khẩu thành công." });
        }
    }
}

