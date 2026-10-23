using System.ComponentModel.DataAnnotations;

namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto
{
    public class ChangePasswordDto
    {
        /// <summary>
        /// Tên đăng nhập (tùy chọn, ưu tiên xác thực qua JWT token)
        /// </summary>
        public string? TenDangNhap { get; set; }

        private string _matKhauHienTai = string.Empty;

        /// <summary>
        /// Mật khẩu hiện tại cần xác thực
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu hiện tại không được để trống.")]
        public string MatKhauHienTai
        {
            get => _matKhauHienTai;
            set => _matKhauHienTai = value;
        }

        /// <summary>
        /// Alias hỗ trợ trường MatKhauCu nếu client gửi
        /// </summary>
        public string? MatKhauCu
        {
            get => _matKhauHienTai;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _matKhauHienTai = value;
            }
        }

        /// <summary>
        /// Mật khẩu mới cần cập nhật
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
        public string MatKhauMoi { get; set; } = string.Empty;

        private string? _xacNhanMatKhauMoi;

        /// <summary>
        /// Xác nhận lại mật khẩu mới (tùy chọn)
        /// </summary>
        public string? XacNhanMatKhauMoi
        {
            get => _xacNhanMatKhauMoi;
            set => _xacNhanMatKhauMoi = value;
        }

        /// <summary>
        /// Alias hỗ trợ trường XacNhanMatKhau nếu client gửi
        /// </summary>
        public string? XacNhanMatKhau
        {
            get => _xacNhanMatKhauMoi;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _xacNhanMatKhauMoi = value;
            }
        }
    }
}
