using Microsoft.AspNetCore.WebUtilities;

namespace First_Task.Interfaces;
public interface IFileUploadService
{
    public Task<bool> UploadFile(MultipartReader reader, MultipartSection section);
}

