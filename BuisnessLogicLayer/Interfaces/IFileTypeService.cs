using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Interfaces
{
    /// <summary>
    /// Presents CRUD operations for <see cref="FileTypeModel"/>
    /// </summary>
    public interface IFileTypeService
    {
        /// <summary>
        /// Return all <see cref="FileTypeModel"/> presented in database
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileTypeModel"/> asynchronously</returns>
        Task<IEnumerable<FileTypeModel>> GetAllAsync();

        /// <summary>
        /// Gets <see cref="FileTypeModel"/> by given id
        /// </summary>
        /// <param name="id">Id of searched <see cref="FileTypeModel"/></param>
        /// <returns><see cref="FileTypeModel"/> asynchronously</returns>
        Task<FileTypeModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Adds <see cref="FileTypeModel"/> 
        /// </summary>
        /// <param name="addFileModel"></param>
        /// <returns>The task that represents asynchronous operation</returns>
        Task AddAsync(FileTypeModel addFileModel);

        /// <summary>
        /// Updates <see cref="FileType"/> by given <see cref="FileTypeModel"/>
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns>The task that represents asynchronous operation</returns>
        Task UpdateAsync(FileTypeModel model);

        /// <summary>
        /// Deletes <see cref="FileType"/> by given id
        /// </summary>
        /// <param name="id">Id of object to delete</param>
        /// <returns>The task that represents asynchronous operation</returns>
        Task DeleteAsync(Guid id);
    }
}
