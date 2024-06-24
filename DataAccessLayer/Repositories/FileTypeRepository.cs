using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Provides saving, updating, getting, deleting operations for <see cref="FileType"/>
    /// </summary>
    public class FileTypeRepository : IFileTypeRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes an instanse of <see cref="FileTypeRepository"/>
        /// </summary>
        /// <param name="db">DbContext of application</param>
        public FileTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Saves new entity of type <see cref="FileType"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="entity">Entity of type <see cref="FileType"/> to save</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task AddAsync(FileType entity)
        {
            await _db.AddAsync(entity);
        }

        /// <summary>
        /// Deletes entity of type <see cref="FileType"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="entity">Entity of type <see cref="FileType"/> to delete</param>
        public void Delete(FileType entity)
        {
            _db.Remove(entity);
        }

        /// <summary>
        /// Deletes entity of type <see cref="FileType"/> at <see cref="ApplicationDbContext"/> using given id
        /// </summary>
        /// <param name="id">Id of entity of type <see cref="FileType"/> to delete</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task DeleteByIdAsync(Guid id)
        {
            FileType fileType = await _db.FileTypes.FindAsync(id);

            if (fileType != null)
            {
                _db.Remove(fileType);
            }

        }

        /// <summary>
        /// Gets all entities of type <see cref="FileType"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <returns>The task that represents asynchronous operation. The task result contains <see cref="List{FileType}"/></returns>
        public async Task<IEnumerable<FileType>> GetAllAsync()
        {
            return await _db.FileTypes.Include(x => x.FileInformation).ToListAsync();
        }

        /// <summary>
        /// Gets entity of type <see cref="FileType"/> at <see cref="ApplicationDbContext"/> by given id
        /// </summary>
        /// <param name="id">Id of entity of type <see cref="FileType"/> to get</param>
        /// <returns>The task that represents asynchronous operation. The task result contains <see cref="FileType"/></returns>
        public async Task<FileType> GetByIdAsync(Guid id)
        {
            return await _db.FileTypes.FindAsync(id);
        }

        /// <summary>
        /// Updates given entity of type <see cref="FileType"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="entity">Entity of type <see cref="FileType"/> to update</param>
        /// <exception cref="DALException">Throws  <see cref="FileType"/> if some of the entities are  <see langword="null" /></exception>
        public void Update(FileType entity)
        {
            if (entity != null)
            {
                FileType fileType = _db.FileTypes.Find(entity.Id);

                if (fileType != null)
                {
                    fileType.Extension = entity.Extension;
                    fileType.MIMEType = entity.MIMEType;
                    fileType.FileInformation = entity.FileInformation;
                }
                else
                {
                    throw new DALException();
                }
            }
        }
    }
}
