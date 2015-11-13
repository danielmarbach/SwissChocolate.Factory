using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using NServiceBus;

namespace Blending
{
    public class VanillaStatsPoller : IWantToRunWhenBusStartsAndStops
    {
        private readonly VanillaContext context;
        private readonly CancellationTokenSource tokenSource;

        public VanillaStatsPoller(VanillaContext context)
        {
            this.context = context;

            tokenSource = new CancellationTokenSource();
        }

        public void Start()
        {
            SpecialConsole.WriteLine("-----------------------------------------------------------------------------------------");
            SpecialConsole.WriteLine("|                                                                                       |");
            SpecialConsole.WriteLine("-----------------------------------------------------------------------------------------");
            SpecialConsole.WriteLine();
            while (!tokenSource.IsCancellationRequested)
            {
                var recentlyAcquired = context.Usages.OrderByDescending(u => u.Acquired).FirstOrDefault();

                SpecialConsole.WriteAt(0, 1, $"|   ['{recentlyAcquired?.LotNumber}' - Stats] Recently acquired vanilla {recentlyAcquired?.Acquired.ToString(CultureInfo.InvariantCulture) ?? "none"}".PadRight(60));
            }
        }

        public void Stop()
        {
            tokenSource.Cancel();
        }
    }
}