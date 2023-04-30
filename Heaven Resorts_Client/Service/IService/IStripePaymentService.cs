using System.Threading.Tasks;
using Models;

namespace Heaven_Resorts_Client.Service.IService
{
    public interface IStripePaymentService
    {
        public Task<SuccessModel> CheckOut(StripePaymentDTO model);
    }
}
