using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 4824
// Hash 9706
// Hash 1859
// Hash 7058
// Hash 9501
// Hash 5433
// Hash 4811
// Hash 3958
// Hash 6070
// Hash 4147
// Hash 6193
// Hash 2521
// Hash 2668
// Hash 4360
// Hash 7765
// Hash 8168
// Hash 6245
// Hash 8860
// Hash 7443
// Hash 2865
// Hash 5306
// Hash 4809
// Hash 2905
// Hash 6296
// Hash 2272
// Hash 1584
// Hash 3868
// Hash 9971
// Hash 6642
// Hash 8914
// Hash 4119
// Hash 3643
// Hash 8161
// Hash 1488
// Hash 9263
// Hash 6370
// Hash 8080
// Hash 2410
// Hash 8463
// Hash 7007
// Hash 9161
// Hash 3538
// Hash 7848
// Hash 2796
// Hash 5095
// Hash 7945
// Hash 5676
// Hash 5700
// Hash 3031
// Hash 7387
// Hash 9756
// Hash 8961
// Hash 5480
// Hash 4091
// Hash 9271
// Hash 1775
// Hash 5076
// Hash 6358
// Hash 4624
// Hash 6818
// Hash 4421
// Hash 9766
// Hash 3864
// Hash 4715
// Hash 6263
// Hash 7262
// Hash 8778
// Hash 8453
// Hash 1299
// Hash 4744
// Hash 6233
// Hash 1921
// Hash 4361
// Hash 5039
// Hash 8099
// Hash 6621
// Hash 8454
// Hash 8601
// Hash 7875
// Hash 6721
// Hash 6797
// Hash 8702
// Hash 2722