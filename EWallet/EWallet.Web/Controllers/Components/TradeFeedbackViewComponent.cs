using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWallet.Web.Controllers.Components
{
    public class TradeFeedbackViewComponent : ViewComponent
    {
        public TradeFeedbackViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //TODO: Load data here and return to viewComponent
            return View();
        }
    }
}
