using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    /// Represents service for working with file information
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Gets all <see cref="FileModel"/> presented in database
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously</returns>
        Task<IEnumerable<FileModel>> GetAllAsync();

        /// <summary>
        /// Gets all <see cref="FileModel"/> presented in database
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously with <see cref="AccessLevel.Public"/></returns>
        Task<IEnumerable<FileModel>> GetAllPublicAsync();

        /// <summary>
        /// Gets list of files by user id
        /// </summary>
        /// <param name="id">Id of the <see cref="AppUser"/></param>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously</returns>
        IEnumerable<FileModel> GetFilesByUserId(string id);

        /// <summary>
        /// Gets FileModel by it`s id
        /// </summary>
        /// <param name="id">Id of the seeked <see cref="FileModel"/></param>
        /// <returns><see cref="FileModel"/> asynchronously</returns>
        Task<FileModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Loads file by given id
        /// </summary>
        /// <param name="id">Id of the file to search</param>
        /// <returns><see cref="FileStream"/> of the found file</returns>
        Task<FileStream> GetFileAsync(Guid id);

        /// <summary>
        /// Adds new file to the storage
        /// </summary>
        /// <param name="addFileModel">Model of file to add</param>
        /// <returns>The task that represents asynchronous operation</returns>
        Task AddAsync(AddFileModel addFileModel);

        /// <summary>
        /// Updates <see cref="FileInformation"/> of some file
        /// </summary>
        /// <param name="model">Information of file to update</param>
        /// <returns>The task that represents asynchronous operation</returns>
        Task UpdateAsync(FileModel model);

        /// <summary>
        /// Deletes file and <see cref="FileInformation"/> by given id
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns>The task that represents asynchronous operation</returns>
        Task DeleteAsync(Guid modelId);

        /// <summary>
        /// Gets all <see cref="FileModel"/> presented in database with <see cref="AccessLevel.Public"/> by filter
        /// </summary>
        /// <param name="filter">Model for filtering file models</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously</returns>
        public Task<IEnumerable<FileModel>> GetPublicByFileterAsync(FileFilterModel filter);

        /// <summary>
        /// Gets <see cref="FileModel"/> by it's creator userName and Name
        /// </summary>
        /// <param name="userName">Username of the creator</param>
        /// <param name="fileName">Name of the file</param>
        /// <returns><see cref="FileStream"/> of the found file</returns>
        public Task<FileModel> GetFileModelByUserAndNameAsync(string userName, string fileName);
    }
}
