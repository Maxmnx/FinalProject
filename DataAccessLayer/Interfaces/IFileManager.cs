using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IFileManager
    {
        public Task SaveFileAsync(IFormFile formFile, string path);

        public FileStream LoadFile(string path);

        public void DeleteFile(string path);
    }
}
