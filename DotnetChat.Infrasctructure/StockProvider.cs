using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DotnetChat.Infrasctructure
{
    public interface IStockProvider
    {
        string GetStocksInfo(string stock);
    }
    public class StockProvider : IStockProvider
    {
        private const string STOCK_BASE_URL = "https://stooq.com";
        private string _queryParams;
        private string _fullStockUrl;
        private readonly ILogger _logger;
        public StockProvider( ILogger<StockProvider> logger)
        {
            _logger = logger;
        }
        private void SetUrl(string stock)
        {
            _queryParams = $"/q/l/?s={stock}&f=sd2t2ohlcv&h&e=csv";
            _fullStockUrl = $"{STOCK_BASE_URL}{_queryParams}";
        }
        //Exception will be catch by the globa handler middleware
        public string GetStocksInfo(string stock)
        {
            _logger.LogInformation($"getting stock info for {stock}");
            SetUrl(stock);
            var stockInfo = String.Empty;

            var req = (HttpWebRequest)HttpWebRequest.Create(_fullStockUrl);
            using (var resp = req.GetResponse())
            {
                using (var stream = resp.GetResponseStream())
                {
                    char[] seperators = { ',' };
                    var reader = new StreamReader(stream);
                    var data = reader.ReadLine();
                    data = reader.ReadLine();
                    var read = data.Split(seperators, StringSplitOptions.None);
                    stockInfo = $"{read[0]} quote is {read[3]}";
                }
            }
            return stockInfo;
        }
    }
}
