using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    internal class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _db;

        public AppUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(AppUser entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                else
                {
                    await _db.AddAsync(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(AppUser entity)
        {
            try
            {
                if (entity != null)
                {
                    _db.Remove(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteById(string id)
        {
            try
            {
                if (id != null)
                {
                    AppUser toDelete = _db.Users.FirstOrDefault(x => x.Id == id);
                    if (toDelete != null)
                    {
                        _db.Remove(toDelete);
                    }   
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _db.Users
                .Include(x => x.StoredFiles)
                .Include(x => x.BannedAppUsers)
                .Include(x => x.BannedByAppUsers)
                .ToListAsync();
        }

        public AppUser GetByIdAsync(string id)
        {
            try
            {
                if (id != null)
                {
                    var appUser = _db.Users.Include(x => x.StoredFiles).ThenInclude(x => x.FileType).FirstOrDefault(x => x.Id == id);
                    if (appUser != null) return appUser;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(AppUser entity)
        {
            try
            {
                if (entity != null)
                {
                    var obj = _db.Update(entity);
                    if (obj != null) _db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
