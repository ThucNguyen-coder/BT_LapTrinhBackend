using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using LHU_124000698_NguyenHuynhDuyThuc.Api.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Controllers
{
    [Route("api/mon-hoc")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        IMonHocService monHocService;

        public MonHocController(IMonHocService monHocService)
        {
            this.monHocService = monHocService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<MonHocDto>>> GetAll()
        {
            var danhSachMonHoc = await monHocService.GetAllAsync();
            return Ok(danhSachMonHoc);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MonHocAddDto req)
        {
            var result = await monHocService.AddAsync(req);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> edit(MonHocEditDto req)
        {
            var result = await monHocService.EditAsync(req);
            if (result == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> delete(int id)
        {
            var result = await monHocService.DeleteAsync(id);
            if (result == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
