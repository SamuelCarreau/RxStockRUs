using System;
using System.Collections.Generic;
using System.Text;

namespace StocksRUs
{
    public class StockInfo
    {
        public StockInfo(string symbol, decimal price)
        {
            Symbol = symbol;
            PrevPrice = price;
        }

        public string Symbol { get; set; }
        public decimal PrevPrice { get; set; }
    }
}
