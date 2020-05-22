using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BookShop.Extensions
{
    public static class UploadFileExtensions
	{
        public static string UploadFile(IFormFile image, string folder)
        {
            if (image == null) return string.Empty;

            try
            {
                var fileName = Path.GetFileNameWithoutExtension(image.FileName).ToLower();

                if(File.Exists(fileName)) File.Delete(fileName);

                var fileNameCleaned = Regex.Replace(fileName, @"[^a-z0-9]", "") + $"{DateTime.Now.Ticks}{Path.GetExtension(image.FileName)}";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folder, fileNameCleaned);

                using (var file = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(file);
                }

                return fileNameCleaned;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
