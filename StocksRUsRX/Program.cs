using System;

namespace StocksRUsRX
{
    class Program
    {
        static void Main(string[] args)
        {
            StockTicker stockTicker = new StockTicker();
            StockMonitor stockMonitor = new StockMonitor(stockTicker);

            stockTicker.Tick(new StockTick { QuoteSymbol = "MSFT", Price = 100 });
            stockTicker.Tick(new StockTick { QuoteSymbol = "INTC", Price = 150 });
            stockTicker.Tick(new StockTick { QuoteSymbol = "MSFT", Price = 170 });
            stockTicker.Tick(new StockTick { QuoteSymbol = "MSFT", Price = 195 });
        }
    }
}
