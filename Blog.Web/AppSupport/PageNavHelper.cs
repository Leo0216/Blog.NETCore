namespace Blog.Web.AppSupport
{
    public class PageNavHelper
    {
        public static string PageUrl(string urlFormat, int pageIndex)
        {
            if (pageIndex == -1)
                return urlFormat.Replace("{pageIndex}", "");
            return urlFormat.Replace("{pageIndex}", pageIndex.ToString());
        }
    }
}
