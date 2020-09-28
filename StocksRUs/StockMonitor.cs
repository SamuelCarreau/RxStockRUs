using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StocksRUs
{
    public class StockMonitor : IDisposable
    {
        private const decimal maxChangeRatio = 0.1m;
        private readonly StockTicker _ticker;
        private Dictionary<string, StockInfo> _stockInfos = new Dictionary<string, StockInfo>();
        private object _stockTickLocker = new object();

        public StockMonitor(StockTicker ticker)
        {
            _ticker = ticker;
            _ticker.StockTick += OnStockTick;
        }

        private void OnStockTick(object sender, StockTick stockTick)
        {
            Debug.WriteLine("{0}: {1}", stockTick.QuoteSymbol, stockTick.Price);
            var quoteSymbol = stockTick.QuoteSymbol;
            lock (_stockTickLocker)
            {
                var stockInfoExists = _stockInfos.TryGetValue(quoteSymbol, out StockInfo stockInfo);
                if (stockInfoExists)
                {
                    var priceDiff = stockTick.Price - stockInfo.PrevPrice;
                    var changeRatio = Math.Abs(priceDiff / stockInfo.PrevPrice);
                    if (changeRatio > maxChangeRatio)
                    {
                        //Do SOmething with the stock - notify user or display on screen
                        Console.WriteLine("Stock:{0} has changed with {1:N2} ratio, Old Price:{2} New Price:{3}",
                            quoteSymbol,
                            changeRatio,
                            stockInfo.PrevPrice,
                            stockTick.Price);
                    }
                    _stockInfos[quoteSymbol].PrevPrice = stockTick.Price;
                }
                else
                {
                    _stockInfos[quoteSymbol] = new StockInfo(quoteSymbol, stockTick.Price);
                }
            }
        }

        public void Dispose()
        {
            _ticker.StockTick -= OnStockTick;
            _stockInfos.Clear();
        }
    }
}
