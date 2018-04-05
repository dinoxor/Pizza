using System.Web;
using System.Web.Mvc;

namespace The_Ace_of_Spades_Pizza
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
