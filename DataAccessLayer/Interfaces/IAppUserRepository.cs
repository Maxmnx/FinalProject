using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAsync();

        AppUser GetByIdAsync(string id);

        Task AddAsync(AppUser entity);

        void Delete(AppUser entity);

        void DeleteById(string id);

        void Update(AppUser entity);
    }
}
