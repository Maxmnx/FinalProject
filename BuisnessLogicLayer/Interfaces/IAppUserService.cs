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
    public interface IAppUserService
    {
        Task<IEnumerable<AppUserModel>> GetAllAsync();

        AppUserModel GetById(string id);

        Task AddAsync(AppUserModel appUser);

        Task UpdateAsync(AppUserModel appUser);

        Task DeleteAsync(string modelId);
    }
}
