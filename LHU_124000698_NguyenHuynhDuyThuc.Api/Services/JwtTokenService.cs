using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using LHU_124000698_NguyenHuynhDuyThuc.Api.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(NguoiDungEntity user, string? hoTen = null)
        {
            var secretKey = _config["JwtSettings:SecretKey"]
                ?? throw new InvalidOperationException("JwtSettings:SecretKey chưa được cấu hình.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var displayName = !string.IsNullOrWhiteSpace(hoTen)
                ? hoTen
                : (user.SinhVien?.HoTen ?? user.TenDangNhap);

            // Đóng gói các Claims định danh vào Payload Token
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.TenDangNhap),
                new("HoTen", displayName),
                new(ClaimTypes.Role, user.VaiTro), // Note: Trong hình dùng ClaimTypes.Role hoặc ClaimTypes.NameIdentifier
                new("SinhVienId", user.SinhVienId?.ToString() ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Hạn dùng 2 giờ
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
