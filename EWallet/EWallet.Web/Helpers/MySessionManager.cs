using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWallet.Data.Enums;
using EWallet.Utilities.Extensions;
using EWallet.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EWallet.Web.Helpers
{
    public class MySessionManager
    {
        private const string WebInfoSessionKey = "WebInfoData";

        private static IHttpContextAccessor _httpContextAccessor;

        public MySessionManager(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public WebInfo WebInfoData
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Session.Get<WebInfo>(WebInfoSessionKey) == null)
                {
                    LoadSessionWebInfo();
                }

                return _httpContextAccessor.HttpContext.Session.Get<WebInfo>(WebInfoSessionKey) == null
                    ? null
                    : (_httpContextAccessor.HttpContext.Session.Get<WebInfo>(WebInfoSessionKey) as WebInfo);
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.Set<WebInfo>(WebInfoSessionKey, value);
            }
        }

        private void LoadSessionWebInfo(string clientData = null)
        {
            var obj = new WebInfo
            {
                Language = LanguageEnum.En.Value(),
                ECurrency = ECurrencyEnum.Btc.Value()
            };
            WebInfoData = obj;
        }
    }

    [Serializable]
    public class WebInfo
    {
        public int Language { get; set; }
        public int Country { get; set; }
        public int ECurrency { get; set; }
    }
}