using AutoMapper;
using BuisnessLogicLayer.Exceptions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(AppUserModel appUserModel)
        {
            if(appUserModel.Email == null ||
                appUserModel.Password == null ||
                appUserModel.UserName == null)
            {
                throw new BLLException();
            }

            await _unitOfWork.AppUserRepository.AddAsync(_mapper.Map<AppUser>(appUserModel));
            await _unitOfWork.SaveAllAsync();
        }

        public async Task DeleteAsync(string modelId)
        {
            if(modelId == null)
            {
                throw new BLLException();
            }

            _unitOfWork.AppUserRepository.DeleteById(modelId);
            await _unitOfWork.SaveAllAsync();
        }

        public async Task<IEnumerable<AppUserModel>> GetAllAsync()
        {
            var appUsers = await _unitOfWork.AppUserRepository.GetAllAsync();

            if(appUsers == null)
            {
                throw new BLLException();
            }

            return _mapper.Map<IEnumerable<AppUserModel>>(appUsers);
        }

        public AppUserModel GetById(string id)
        {
            if (id == null)
            {
                throw new BLLException();
            }

            var appUser = _unitOfWork.AppUserRepository.GetByIdAsync(id);

            return _mapper.Map<AppUserModel>(appUser);
        }

        public Task UpdateAsync(AppUserModel appUser)
        {
            throw new NotImplementedException();
        }
    }
}
