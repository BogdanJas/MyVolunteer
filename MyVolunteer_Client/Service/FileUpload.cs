using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyVolunteer_Client.Service.IService;

namespace MyVolunteer_Client.Service
{
    public class FileUpload:IFileUpload
    {
        private readonly IWebAssemblyHostEnvironment _webAssemblyHostEnvironment;

        public FileUpload(IWebAssemblyHostEnvironment webAssemblyHostEnvironment)
        {
            _webAssemblyHostEnvironment = webAssemblyHostEnvironment;
        }

        public bool DeleteFile(string filePath)
        {
            if (File.Exists(_webAssemblyHostEnvironment.BaseAddress + filePath))
            {
                File.Delete(_webAssemblyHostEnvironment.BaseAddress + filePath);
                return true;
            }
            return false;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
            var folderDirectory = $"{_webAssemblyHostEnvironment.BaseAddress}\\images\\users";
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }
            var filePath = Path.Combine(folderDirectory, fileName);

            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);

            var fullPath = $"/images/users/{fileName}";
            return fullPath;

        }
    }
}
