using LHU_124000698_NguyenHuynhDuyThuc.Api.Database;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Database.Entities;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Controllers
{
    [Route("api/sinh-vien")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        DatabaseContext db;

        public SinhVienController(DatabaseContext context)
        {
            db = context;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<SinhVienDto>>> GetAll()
        {
            List<SinhVienDto> danhSachSinhVien = await db.sinhVien
                .Select(s => new SinhVienDto()
                {
                    SinhVienId = s.SinhVienID,
                    MaSV = s.MaSV,
                    HoTen = s.HoTen,
                    Email = s.Email,
                    NgaySinh = s.NgaySinh,
                    LopSinhHoat = s.LopSinhHoat
                })
                .ToListAsync();

            return Ok(danhSachSinhVien);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(SinhVienAddDto req)
        {
            SinhVienEntity newSinhVien = new SinhVienEntity()
            {
                MaSV = req.MaSV,
                HoTen = req.HoTen,
                Email = req.Email,
                NgaySinh = req.NgaySinh,
                LopSinhHoat = req.LopSinhHoat
            };

            await db.sinhVien.AddAsync(newSinhVien);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> edit(SinhVienEditDto req)
        {
            var sinhVien = await db.sinhVien.Where(s => s.SinhVienID == req.SinhVienId).FirstOrDefaultAsync();
            if (sinhVien == null)
            {
                return NotFound();
            }

            sinhVien.MaSV = req.MaSV;
            sinhVien.HoTen = req.HoTen;
            sinhVien.Email = req.Email;
            sinhVien.NgaySinh = req.NgaySinh;
            sinhVien.LopSinhHoat = req.LopSinhHoat;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> delete(int id)
        {
            var sinhVien = await db.sinhVien.Where(s => s.SinhVienID == id).FirstOrDefaultAsync();
            if (sinhVien == null)
            {
                return NotFound();
            }

            db.sinhVien.Remove(sinhVien);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}