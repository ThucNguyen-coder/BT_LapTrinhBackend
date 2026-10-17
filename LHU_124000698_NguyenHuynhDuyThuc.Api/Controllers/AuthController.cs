using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
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
    }
}
