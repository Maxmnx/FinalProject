using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    /// <summary>
    /// Represents one center for working with repositories
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Initializes a new instanse of <see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="db"></param>
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            FileTypeRepository = new FileTypeRepository(db);
            FileInformationRepository = new FileInformationRepository(db);
            AppUserRepository = new AppUserRepository(db);
            FileManager = new FileManager();
        }

        /// <summary>
        /// Provides saving, updating, getting, deleting operations for <see cref="FileType"/>
        /// </summary>
        public IFileTypeRepository FileTypeRepository { get; }

        /// <summary>
        /// Provides saving, updating, getting, deleting operations for <see cref="FileInformation"/>
        /// </summary>
        public IFileInformationRepository FileInformationRepository { get; }

        /// <summary>
        /// Provides saving, updating, getting, deleting operations for <see cref="AppUser"/>
        /// </summary>
        public IAppUserRepository AppUserRepository { get; }

        /// <summary>
        /// Provides saving, getting, deleting operations for files
        /// </summary>
        public IFileManager FileManager { get; }

        /// <summary>
        /// Saves all changes made in Repositories
        /// </summary>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task SaveAllAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
