using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWallet.Web.Controllers.Components
{
    public class SellingAdvertisementViewComponent : ViewComponent
    {
        public SellingAdvertisementViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }

}
