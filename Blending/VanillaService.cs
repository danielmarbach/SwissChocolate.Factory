using System;
using System.Threading.Tasks;

namespace Blending
{
    public class VanillaService : IVanillaService
    {
        public async Task<Vanilla> GetVanilla(int lotNumber)
        {
            SpecialConsole.WriteLine($"['{lotNumber}' - Service] Vanilla acquiring takes really long");

            await Task.Delay(5000).ConfigureAwait(false);

            SpecialConsole.WriteLine($"['{lotNumber}' - Service] Vanilla, we have it");
            return new Vanilla();
        }
    }
}