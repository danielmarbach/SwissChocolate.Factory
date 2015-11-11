using System.Threading.Tasks;

namespace Blending
{
    public class VanillaService : IVanillaService
    {
        public async Task<Vanilla> GetVanilla()
        {
            await Task.Delay(5000).ConfigureAwait(false);
            return new Vanilla();
        }
    }
}