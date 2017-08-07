using System.Web;
using System.Web.Mvc;

namespace KnockoutAndTypescript
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
