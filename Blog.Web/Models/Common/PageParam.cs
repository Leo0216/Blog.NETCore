namespace Blog.Web.Models
{
    public class PageParam
    {
        public PageParam(int pageIndex, int pageSize, int totalCount, int totalPage)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPage = totalPage;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
    }
}
