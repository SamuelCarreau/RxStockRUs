using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;

namespace StocksRUsRX
{
    public class StockMonitor : IDisposable
    {      
        private IDisposable _subscription;
        public StockMonitor(StockTicker ticker)
        {
            _subscription = SubcribeToDrasticChangesOfTickerTicks(ticker, 0.1m);
        }
        private IDisposable SubcribeToDrasticChangesOfTickerTicks(StockTicker ticker, decimal maxChangeRatio)
        {
            var ticks = Observable.FromEventPattern<EventHandler<StockTick>, StockTick>(
                h => ticker.StockTick += h,
                h => ticker.StockTick -= h)
                .Select(tickEvent => tickEvent.EventArgs)
                .Synchronize();
            var drasticChanges =
                from tick in ticks
                group tick by tick.QuoteSymbol
                into company
                from tickPair in company.Buffer(2, 1)
                let changeRatio = Math.Abs((tickPair[1].Price - tickPair[0].Price) / tickPair[0].Price)
                where changeRatio > maxChangeRatio
                select new DrasticChange()
                {
                    Symbol = company.Key,
                    ChangeRatio = changeRatio,
                    OldPrice = tickPair[0].Price,
                    NewPrice = tickPair[1].Price
                };
            var subscription = drasticChanges.Subscribe(change =>
            {
                Console.WriteLine($"Stock:{change.Symbol} has changed with" +
                                    $"{change.ChangeRatio:N2} ratio," +
                                    $"Old Price:{change.OldPrice} " +
                                    $"New Price:{change.NewPrice}");
            },
            ex => {/* code that handle errors*/},
            () => {/* code that handles the observable completeness*/ });
            return subscription;
        }
        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
