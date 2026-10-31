namespace LHU_124000698_NguyenHuynhDuyThuc.Api.Dto.Common
{
    public class PagedResult<T>
    {
        // Danh sách dữ liệu của trang hiện tại
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        // Cờ kiểm tra trạng thái trang cho Frontend hiển thị nút Next / Prev
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PagedResult() { }

        public PagedResult(List<T> items, int totalCount,
                          int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;

            // Ép kiểu double để không bị chia nguyên làm tròn sai số trang
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
