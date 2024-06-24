using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Provides methods for getting, saving and deleting files
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Deletes file by path
        /// </summary>
        /// <param name="path">Path to the file to delete</param>
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// Gets file by path
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <returns><see cref="FileStream"/> of loaded file</returns>
        public FileStream LoadFile(string path)
        {
            FileStream fs = File.Open(path, FileMode.Open);

            return fs;
        }

        /// <summary>
        /// Saves file by given path
        /// </summary>
        /// <param name="formFile">File to save by given path</param>
        /// <param name="path">Path to the location where to save file</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task SaveFileAsync(IFormFile formFile, string path)
        {
            using (var createdFile = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(createdFile);
            }
        }
    }
}
