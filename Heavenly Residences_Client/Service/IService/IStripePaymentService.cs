using System.Threading.Tasks;
using Models;

namespace Heavenly_Residences_Client.Service.IService
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDTO model);
    }
}
