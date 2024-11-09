
using AFF_BE.Core.Models.File;

namespace AFF_BE.Api.Services.Interfaces
{
    public interface IFileService
    {

        Task<string> UploadFileAsync(IFormFile? file, string folder);

        Task<bool> DeleteFileAsync(string fileUrl);

        Task<List<string>> DeleteFilesAsync(List<string> fileUrls);
         
        Task<List<string>> UploadMultipleFilesAsync(UploadMultipleFileRequest request, string folder);



    }
}
