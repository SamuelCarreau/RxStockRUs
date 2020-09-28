using System;
using System.Collections.Generic;
using System.Text;

namespace StocksRUsRX
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
