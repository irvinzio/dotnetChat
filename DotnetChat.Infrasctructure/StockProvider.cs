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

        private void SetUrl(string stock)
        {
            _queryParams = $"/q/l/?s={stock}&f=sd2t2ohlcv&h&e=csv";
            _fullStockUrl = $"{STOCK_BASE_URL}{_queryParams}";
        }
        public string GetStocksInfo(string stock)
        {
            SetUrl(stock);
            var stockInfo = String.Empty;
            var req = (HttpWebRequest)HttpWebRequest.Create(_fullStockUrl);
            using (var resp = req.GetResponse())
            {
                using (var stream = resp.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    stockInfo = reader.ReadToEnd();
                }
            }
            return stockInfo;
        }
    }
}
