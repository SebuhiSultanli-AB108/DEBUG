using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace DEBUG.BL.Extensions;

public static class FormFileExtension
{
    public static async Task<string> UploadAsync(this IFormFile file, IWebHostEnvironment _wwwRoot, string folder, string? customName = null)
    {
        string fileName = string.Empty;

        if (customName.IsNullOrEmpty())
            fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.Now:MM-dd-yyyy}{Path.GetExtension(file.FileName)}";
        else
            fileName = customName + Path.GetExtension(file.FileName);

        string directory = Path.Combine(_wwwRoot.WebRootPath, $"images/{folder}");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string path = Path.Combine(directory, fileName);
        using (Stream stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return $"../images/{folder}/{fileName}";
    }
}