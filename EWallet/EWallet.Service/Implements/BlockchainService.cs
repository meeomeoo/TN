using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using EWallet.Data.Enums;
using EWallet.Data.Models.MyConfig;
using EWallet.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace EWallet.Service.Implements
{
    public class BlockchainService : IBlockchainService
    {
        #region Variables
        private readonly ILogger _log;
        private readonly IMemoryCache _cache;
        private readonly MyConfiguration _myConfig;

        // Blockchain config
        private const string BlockchainTickerApiUrl = "https://blockchain.info/ticker";
        private const string VietcomBankRateApiUrl = "http://vietcombank.com.vn/ExchangeRates/ExrateXML.aspx";
        //http://dongabank.com.vn/exchange/export

        private const int CacheTime60m = 60; // min
        private const int CacheTime15m = 15; // min
        #endregion

        public BlockchainService(ILogger<BlockchainService> log, IMemoryCache cache, IOptions<MyConfiguration> options)
        {
            _log = log;
            _cache = cache;
            _myConfig = options.Value;
        }

        #region Lay ti gia
        public decimal GetReferenceExchange(ReferenceExchangeEnum referenceExchange)
        {            
            decimal result = 0;

            switch (referenceExchange)
            {
                case ReferenceExchangeEnum.BlockchainInfo:
                    #region BlockchainInfo
                    var keyCache = "ReferenceExchange";

                    // get cache
                    var valueCache = _cache.Get(keyCache);

                    if (valueCache == null)
                    {
                        WebClient webClient = new WebClient();
                        webClient.Encoding = System.Text.Encoding.UTF8; // Fix bug UTF8
                        string json = webClient.DownloadString(BlockchainTickerApiUrl);

                        var convetjson = JObject.Parse(json);
                        var usdLast = convetjson["USD"]["last"];

                        result = Convert.ToDecimal(usdLast);

                        // cache 60min
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(CacheTime15m));
                        _cache.Set(keyCache, result, cacheEntryOptions);
                    }
                    else
                        result = (decimal)valueCache;
                    #endregion
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Ty gia usd
        /// </summary>
        /// <returns></returns>
        public decimal GetUsdRate()
        {
            try
            {
                var keyCache = "UsdRate";

                // get cache
                var valueCache = _cache.Get(keyCache);

                if (valueCache == null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(VietcomBankRateApiUrl);
                    XmlNodeList elemList = doc.GetElementsByTagName("Exrate");

                    if (elemList.Count > 0)
                    {
                        var item = elemList[elemList.Count - 1];
                        if (item != null)
                        {
                            var currencyCode = item.Attributes["CurrencyCode"].Value;
                            if (currencyCode == "USD")
                            {
                                var rate = Convert.ToDecimal(item.Attributes["Transfer"].Value);

                                // cache 60min
                                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                    .SetSlidingExpiration(TimeSpan.FromMinutes(CacheTime60m));
                                _cache.Set(keyCache, rate, cacheEntryOptions);

                                return rate;
                            }
                        }
                    }
                }

                return Convert.ToDecimal(valueCache);
            }
            catch (Exception ex)
            {
                _log.LogError("GetUsdRate:" + ex.ToString());
                return _myConfig.Default.BitUsdPrice;
            }

            return 0;
        }
        #endregion
    }
}
