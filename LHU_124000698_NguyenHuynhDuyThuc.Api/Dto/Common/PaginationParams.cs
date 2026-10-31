namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common
{
    public class PaginationParams
    {
        // Giới hạn kích thước trang tối đa chống cạn kiệt tài nguyên máy chủ (DoS/OOM)
        private const int MaxPageSize = 50;
        private int _pageIndex = 1;
        private int _pageSize = 10;

        /// <summary>
        /// Trang hiện tại (mặc định là 1, tự động ép về 1 nếu truyền số <= 0)
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        /// <summary>
        /// Số lượng bản ghi trên một trang (mặc định 10, tối đa 50)
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1) _pageSize = 10;
                else if (value > MaxPageSize) _pageSize = MaxPageSize;
                else _pageSize = value;
            }
        }
    }
}
