using System;
using System.Collections.Generic;
using System.Text;

namespace StocksRUs
{
    public class StockTicker
    {
        public event EventHandler<StockTick> StockTick;

        public void Tick(StockTick stockTick)
        {
            StockTick.Invoke(this, stockTick);
        }
    }
}
