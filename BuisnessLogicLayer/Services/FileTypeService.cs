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
    /// <summary>
    /// Presents CRUD operations for <see cref="FileTypeModel"/>
    /// </summary>
    public class FileTypeService : IFileTypeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes instanse of <see cref="FileTypeService"/> by given <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="unitOfWork">Instanse of <see cref="IUnitOfWork"/> of aplication</param>
        /// <param name="mapper">Instanse of <see cref="IMapper"/> of aplication</param>
        public FileTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        /// <summary>
        /// Return all <see cref="FileTypeModel"/> presented in database
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileTypeModel"/> asynchronously</returns>
        public async Task AddAsync(FileTypeModel model)
        {
            if (model == null ||
                model.Extension == null ||
                model.Extension == "" ||
                model.MIMEType == null ||
                model.MIMEType == "")
            {
                throw new BLLException();
            }

            var fileTypes = await _unitOfWork.FileTypeRepository.GetAllAsync();

            if(fileTypes.Any(x => x.MIMEType == model.MIMEType))
            {
                throw new BLLException();
            }

            
            await _unitOfWork.FileTypeRepository.AddAsync(_mapper.Map<FileType>(model));
            await _unitOfWork.SaveAllAsync();
        }

        /// <summary>
        /// Deletes <see cref="FileType"/> by given id
        /// </summary>
        /// <param name="id">Id of object to delete</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.FileTypeRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAllAsync();
        }

        /// <summary>
        /// Return all <see cref="FileTypeModel"/> presented in database
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileTypeModel"/> asynchronously</returns>
        public async Task<IEnumerable<FileTypeModel>> GetAllAsync()
        {
            var fileTypes = await _unitOfWork.FileTypeRepository.GetAllAsync();

            if(fileTypes == null)
            {
                throw new BLLException();
            }

            return _mapper.Map<IEnumerable<FileTypeModel>>(fileTypes);

        }

        /// <summary>
        /// Gets <see cref="FileTypeModel"/> by given id
        /// </summary>
        /// <param name="id">Id of searched <see cref="FileTypeModel"/></param>
        /// <returns><see cref="FileTypeModel"/> asynchronously</returns>
        public async Task<FileTypeModel> GetByIdAsync(Guid id)
        {
            var fileType = await _unitOfWork.FileTypeRepository.GetByIdAsync(id);

            if(fileType == null)
            {
                throw new BLLException();
            }

            return _mapper.Map<FileTypeModel>(fileType);
        }

        /// <summary>
        /// Updates <see cref="FileType"/> by given <see cref="FileTypeModel"/>
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task UpdateAsync(FileTypeModel model)
        {

            var fileTypes = await _unitOfWork.FileTypeRepository.GetAllAsync();

            if (model == null ||
                model.Extension == null ||
                model.Extension == "" ||
                model.MIMEType == null ||
                model.MIMEType == "" ||
                fileTypes.Any(x => x.MIMEType == model.MIMEType && x.Id != model.Id))
            {
                throw new BLLException();
            }

            _unitOfWork.FileTypeRepository.Update(_mapper.Map<FileType>(model));
            await _unitOfWork.SaveAllAsync();
        }
    }
}
