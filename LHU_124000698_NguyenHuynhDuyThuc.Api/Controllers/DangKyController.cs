using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using LHU_124000698_NguyenHuynhDuyThuc.Api.Dto;
using Dapper;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Controllers
{
    [Route("api/dang-ky")]
    [ApiController]
    public class DangKyController : ControllerBase
    {
        SqlConnection cn;

        public DangKyController(SqlConnection connection)
        {
            cn = connection;
        }

        [HttpPost("them-moi")]
        public async Task<IActionResult> ThemMoi(DangKyAddDto req)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@SinhVienId", req.SinhVienId);
            param.Add("@LopHocPhanId", req.LopHocPhanId);
            param.Add("@KetQua", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@ThongBao", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

            await cn.ExecuteAsync("sp_DangKyHocPhan", param, commandType: CommandType.StoredProcedure);

            int ketQua = param.Get<int>("@KetQua");
            string thongBao = param.Get<string>("@ThongBao");

            if (ketQua == 0)
            {
                return BadRequest(thongBao);
            }

            return Ok(thongBao);
        }
    }
}
