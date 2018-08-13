using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWallet.Web.Controllers.Components
{
    public class HomeIntroViewComponent : ViewComponent
    {
        public HomeIntroViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
