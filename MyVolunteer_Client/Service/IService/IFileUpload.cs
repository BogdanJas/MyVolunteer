using Microsoft.AspNetCore.Components.Forms;

namespace MyVolunteer_Client.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string filePath);
    }
}
