using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.IServices
{
    public interface IJwtTokenService
    {
        string GenerateToken(NguoiDungEntity user, string? hoTen = null);
    }
}
