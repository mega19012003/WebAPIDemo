namespace WebAPI.Services;

public class FileService : IFileService
{
    public async Task<List<string>> SaveFileAsync(List<IFormFile> files, string folderPath)
    {
        var filePaths = new List<string>();

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                filePaths.Add(Path.Combine("images", uniqueFileName));
            }
        }

        return filePaths;
    }

    public async Task<List<string>> UpdateFileAsync(List<IFormFile> files, string folderPath, List<string> oldFiles)
    {
        var filePaths = new List<string>();
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                filePaths.Add(Path.Combine("images", uniqueFileName));
            }
        }
        foreach (var oldFile in oldFiles)
        {
            var fullPath = Path.Combine(folderPath, oldFile);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
        return filePaths;
    }

}

