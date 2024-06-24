using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Provides saving, updating, getting, deleting operations for <see cref="FileInformation"/>
    /// </summary>
    public class FileInformationRepository : IFileInformationRepository
    {
        private readonly ApplicationDbContext _db;


        /// <summary>
        /// Initializes an instanse of <see cref="FileInformationRepository"/>
        /// </summary>
        /// <param name="db">DbContext of application</param>
        public FileInformationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Saves new entity of type <see cref="FileInformation"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="entity">Entity of type <see cref="FileInformation"/> to save</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task AddAsync(FileInformation information)
        {
            if(information == null)
            {
                throw new DALException();
            }
            await _db.AddAsync(information);

        }

        /// <summary>
        /// Deletes entity of type <see cref="FileInformation"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="entity">Entity of type <see cref="FileInformation"/> to delete</param>
        public void Delete(FileInformation entity)
        {
            _db.Remove(entity);
        }

        /// <summary>
        /// Deletes entity of type <see cref="FileInformation"/> at <see cref="ApplicationDbContext"/> using given id
        /// </summary>
        /// <param name="id">Id of entity of type <see cref="FileInformation"/> to delete</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task DeleteByIdAsync(Guid id)
        {
            FileInformation fileInformation = await _db.FileInformation.FindAsync(id);

            if (fileInformation != null)
            {
                _db.Remove(fileInformation);
            }
        }

        /// <summary>
        /// Gets all entities of type <see cref="FileInformation"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <returns>The task that represents asynchronous operation. The task result contains <see cref="List{FileType}"/></returns>
        public async Task<IEnumerable<FileInformation>> GetAllAsync()
        {
            var fileInformation = await _db.FileInformation.
                Include(x => x.Creator).Include(x => x.FileType).ToListAsync();

            return fileInformation;
        }

        /// <summary>
        /// Gets entity of type <see cref="FileInformation"/> at <see cref="ApplicationDbContext"/> by given id
        /// </summary>
        /// <param name="id">Id of entity of type <see cref="FileInformation"/> to get</param>
        /// <returns>The task that represents asynchronous operation. The task result contains <see cref="FileInformation"/></returns>
        public async Task<FileInformation> GetByIdAsync(Guid id)
        {
            var fileInformation = await _db.FileInformation.
                Include(x => x.Creator).Include(x => x.FileType).FirstOrDefaultAsync(x => x.Id == id);

            return fileInformation;
        }

        /// <summary>
        /// Updates given entity of type <see cref="FileInformation"/> at <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="entity">Entity of type <see cref="FileInformation"/> to update</param>
        /// <exception cref="DALException">Throws  <see cref="FileInformation"/> if some of the entities are  <see langword="null" /></exception>
        public void Update(FileInformation entity)
        {
            if (entity != null)
            {
                FileInformation fileInformation = _db.FileInformation.Find(entity.Id);

                if (fileInformation != null && entity != null
                    && entity.Name != null && entity.Path != null)
                {
                    fileInformation.Name = entity.Name;
                    fileInformation.Description = entity.Description;
                    fileInformation.Path = entity.Path;
                    fileInformation.AccessLevel = entity.AccessLevel;
                    fileInformation.CreationDate = fileInformation.CreationDate;
                    fileInformation.Size = fileInformation.Size;
                }
                else
                {
                    throw new DALException();
                }
            }

        }
    }
}
