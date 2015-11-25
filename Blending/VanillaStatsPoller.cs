using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

namespace Blending
{
    public class VanillaStatsPoller : IWantToRunWhenBusStartsAndStops
    {
        private readonly VanillaContext vanillaContext;
        private readonly CancellationTokenSource tokenSource;
        private Task pollingTask;

        public VanillaStatsPoller(VanillaContext vanillaContext)
        {
            this.vanillaContext = vanillaContext;

            tokenSource = new CancellationTokenSource();
        }

        public Task Start(IBusContext context)
        {
            pollingTask = Task.Run(() =>
            {
                SpecialConsole.WriteLine("-----------------------------------------------------------------------------------------");
                SpecialConsole.WriteLine("|                                                                                       |");
                SpecialConsole.WriteLine("-----------------------------------------------------------------------------------------");
                SpecialConsole.WriteLine();
                while (!tokenSource.IsCancellationRequested)
                {
                    var recentlyAcquired = vanillaContext.Usages.OrderByDescending(u => u.Acquired).FirstOrDefault();

                    SpecialConsole.WriteAt(0, 1, $"|   ['{recentlyAcquired?.LotNumber}' - Stats] Recently acquired vanilla {recentlyAcquired?.Acquired.ToString(CultureInfo.InvariantCulture) ?? "none"}".PadRight(60));
                }
            });
            return Task.FromResult(0);
        }

        public Task Stop(IBusContext context)
        {
            tokenSource.Cancel();
            return pollingTask;
        }
    }
}