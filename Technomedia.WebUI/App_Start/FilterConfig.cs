using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Technomedia.WebUI.Classes;

namespace Filter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleAllErrorAttribute());
        }
    }
}