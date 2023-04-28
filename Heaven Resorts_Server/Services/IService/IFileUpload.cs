using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Heaven_Resorts_Server.Services.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string fileName);
    }
}
