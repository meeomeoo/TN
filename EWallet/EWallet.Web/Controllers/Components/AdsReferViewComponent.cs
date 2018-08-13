using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWallet.Web.Controllers.Components
{
    public class AdsReferViewComponent : ViewComponent
    {
        public AdsReferViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //TODO
            return View();
        }
    }
}
