namespace WebAPI.Services
{
    public interface IFileService
    {
        Task<List<String>> SaveFileAsync(List<IFormFile> files, string folderPath);
        Task<List<String>> UpdateFileAsync(List<IFormFile> files, string folderPath, List<string> oldFiles);
    }
}
