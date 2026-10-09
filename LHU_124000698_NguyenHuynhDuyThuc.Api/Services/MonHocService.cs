using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using LHU_124000698_NguyenHuynhDuyThuc.Api.IServices;
using Microsoft.EntityFrameworkCore;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Services
{
    public class MonHocService : IMonHocService
    {
        DatabaseContext db;

        public MonHocService(DatabaseContext context)
        {
            db = context;
        }

        public async Task<List<MonHocDto>> GetAllAsync()
        {
            List<MonHocDto> danhSachMonHoc = await db.MonHoc
                .Select(m => new MonHocDto()
                {
                    MonHocID = m.MonHocID,
                    MaMon = m.MaMon,
                    TenMon = m.TenMon,
                    SoTinChi = m.SoTinChi,
                    SoTietLyThuyet = m.SoTietLyThuyet
                })
                .ToListAsync();

            return danhSachMonHoc;
        }

        public async Task<bool> AddAsync(MonHocAddDto req)
        {
            bool tonTaiMonHoc = await db.MonHoc
                .Where(m => m.MaMon == req.MaMon)
                .AnyAsync();

            if (tonTaiMonHoc == true)
            {
                return false;
            }

            MonHocEntity newMonHoc = new MonHocEntity()
            {
                MaMon = req.MaMon,
                TenMon = req.TenMon,
                SoTinChi = req.SoTinChi,
                SoTietLyThuyet = req.SoTietLyThuyet
            };

            await db.MonHoc.AddAsync(newMonHoc);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditAsync(MonHocEditDto req)
        {
            var monHoc = await db.MonHoc.Where(m => m.MonHocID == req.MonHocID).FirstOrDefaultAsync();
            if (monHoc == null)
            {
                return false;
            }

            monHoc.MaMon = req.MaMon;
            monHoc.TenMon = req.TenMon;
            monHoc.SoTinChi = req.SoTinChi;
            monHoc.SoTietLyThuyet = req.SoTietLyThuyet;

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int monHocId)
        {
            var monHoc = await db.MonHoc.Where(m => m.MonHocID == monHocId).FirstOrDefaultAsync();
            if (monHoc == null)
            {
                return false;
            }

            db.MonHoc.Remove(monHoc);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
