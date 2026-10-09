using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.IServices
{
    public interface IMonHocService
    {
        Task<List<MonHocDto>> GetAllAsync();

        Task<bool> AddAsync(MonHocAddDto req);

        Task<bool> EditAsync(MonHocEditDto req);

        Task<bool> DeleteAsync(int monHocId);
    }
}
