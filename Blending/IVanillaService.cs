using System.ServiceModel;
using System.Threading.Tasks;

namespace Blending
{
    [ServiceContract]
    public interface IVanillaService
    {
        [OperationContract]
        Task<Vanilla> GetVanilla();
    }
}