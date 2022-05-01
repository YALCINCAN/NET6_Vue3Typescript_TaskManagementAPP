using Core.Exceptions;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public static class FileManager
    {
        public static string SaveFile(string foldername, IFormFile file)
        {
            string filename = Guid.NewGuid().ToString().Substring(0, 9) + "_" + file.FileName;
            if (!Directory.Exists(@"Uploads//" + foldername))
                Directory.CreateDirectory(@"Uploads//" + foldername);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads//" + foldername, filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return "Uploads/" + foldername + "/" + filename;
        }

        public static void DeleteFile(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), filename);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}
