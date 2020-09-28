namespace StocksRUsRX
{
    internal class DrasticChange
    {
        public DrasticChange()
        {
        }

        public string Symbol { get; set; }
        public decimal ChangeRatio { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
    }
}