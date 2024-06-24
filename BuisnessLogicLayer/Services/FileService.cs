using AutoMapper;
using BuisnessLogicLayer.Exceptions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Services
{
    /// <summary>
    /// Represents service for working with file information
    /// </summary>
    public class FileService : IFileService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _fileStoragePath =
            @"C:\Users\Максим\MyFiles\EPAM\FinalTask\FileStorage";

        /// <summary>
        /// Initializes instanse of <see cref="FileService"/> by given <see cref="IUnitOfWork"/>
        /// </summary>
        /// <param name="unitOfWork">Instanse of <see cref="IUnitOfWork"/> of aplication</param>
        /// <param name="mapper">Instanse of <see cref="IMapper"/> of aplication</param>
        public FileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds new file to the storage
        /// </summary>
        /// <param name="addFileModel">Model of file to add</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task AddAsync(AddFileModel addFileModel)
        {
            if (addFileModel == null
                || addFileModel.Name == null
                || addFileModel.Name == ""
                || addFileModel.AccessLevel < 0
                || addFileModel.AccessLevel >= 4 
                || addFileModel.CreatorId == null
                || addFileModel.CreatorId == "")
            {
                throw new BLLException();
            }

            FileInformation fileInformation = new FileInformation();

            fileInformation.Name = addFileModel.Name;
            fileInformation.CreatorId = addFileModel.CreatorId;
            fileInformation.Description = addFileModel.Description;
            fileInformation.Size = addFileModel.File.Length;

            var fileTypes = await _unitOfWork.FileTypeRepository.GetAllAsync();

            FileType type = fileTypes.FirstOrDefault(x => x.MIMEType == addFileModel.File.ContentType);

            if (fileTypes == null)
            {
                throw new BLLException();
            }

            fileInformation.FileType = type;
            fileInformation.FileTypeId = type.Id;
            fileInformation.AccessLevel = (AccessLevel)addFileModel.AccessLevel;
            fileInformation.CreationDate = DateTime.Now;
            fileInformation.Path = Path.Combine(_fileStoragePath, Guid.NewGuid().ToString() + '.' + type.Extension);


            var fileInformations = await _unitOfWork.FileInformationRepository.GetAllAsync();

            if (fileInformations.Any(x => x.CreatorId == addFileModel.CreatorId && x.Name == addFileModel.Name))
            {
                throw new BLLException();
            }
            
            await _unitOfWork.FileInformationRepository.AddAsync(fileInformation);
            await _unitOfWork.FileManager.SaveFileAsync(addFileModel.File, fileInformation.Path);
            await _unitOfWork.SaveAllAsync();
            
        }

        /// <summary>
        /// Gets all <see cref="FileModel"/> presented in database with <see cref="AccessLevel.Public"/> by filter
        /// </summary>
        /// <param name="filter">Model for filtering file models</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously</returns>
        public async Task DeleteAsync(Guid modelId)
        {
            var fileInformation = await _unitOfWork.FileInformationRepository.GetByIdAsync(modelId);

            if(fileInformation == null)
            {
                throw new BLLException();
            }

            _unitOfWork.FileManager.DeleteFile(fileInformation.Path);
            await _unitOfWork.FileInformationRepository.DeleteByIdAsync(modelId);
            await _unitOfWork.SaveAllAsync();
        }

        /// <summary>
        /// Gets all <see cref="FileModel"/> presented in database
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously</returns>
        public async Task<IEnumerable<FileModel>> GetAllAsync()
        {
            var fileInformation = await _unitOfWork.FileInformationRepository.GetAllAsync();

            var fileModels = _mapper.Map<IEnumerable<FileModel>>(fileInformation);

            return fileModels;
        }

        /// <summary>
        /// Gets all <see cref="FileModel"/> presented in database
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously with <see cref="AccessLevel.Public"/></returns>
        public async Task<IEnumerable<FileModel>> GetAllPublicAsync()
        {
            var fileInformation = await _unitOfWork.FileInformationRepository.GetAllAsync();

            fileInformation = fileInformation.Where(x => x.AccessLevel == AccessLevel.Public);

            var fileModels = _mapper.Map<IEnumerable<FileModel>>(fileInformation);

            return fileModels;
        }

        /// <summary>
        /// Gets list of files by user id
        /// </summary>
        /// <param name="id">Id of the <see cref="AppUser"/></param>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously</returns>
        public IEnumerable<FileModel> GetFilesByUserId(string id)
        {
            var user = _unitOfWork.AppUserRepository.GetByIdAsync(id);

            if(user == null || user.StoredFiles == null)
            {
                throw new BLLException();
            }

            var fileModels = _mapper.Map<IEnumerable<FileModel>>(user.StoredFiles);

            return fileModels;
        }

        /// <summary>
        /// Gets FileModel by it`s id
        /// </summary>
        /// <param name="id">Id of the seeked <see cref="FileModel"/></param>
        /// <returns><see cref="FileModel"/> asynchronously</returns>
        public async Task<FileModel> GetByIdAsync(Guid id)
        {
            var fileInformation = await _unitOfWork.FileInformationRepository.GetByIdAsync(id);

            if(fileInformation == null)
            {
                throw new BLLException();
            }

            return _mapper.Map<FileModel>(fileInformation);
        }

        /// <summary>
        /// Loads file by given id
        /// </summary>
        /// <param name="id">Id of the file to search</param>
        /// <returns><see cref="FileStream"/> of the found file</returns>
        public async Task<FileStream> GetFileAsync(Guid id)
        {
            FileInformation fileInformation = await _unitOfWork.FileInformationRepository.GetByIdAsync(id);
            
            var file = _unitOfWork.FileManager.LoadFile(fileInformation.Path);

            return file;
        }

        /// <summary>
        /// Updates <see cref="FileInformation"/> of some file
        /// </summary>
        /// <param name="model">Information of file to update</param>
        /// <returns>The task that represents asynchronous operation</returns>
        public async Task UpdateAsync(FileModel model)
        {
            var fileInformation = _mapper.Map<FileInformation>(model);

            _unitOfWork.FileInformationRepository.Update(fileInformation);
            await _unitOfWork.SaveAllAsync();
        }

        /// <summary>
        /// Gets all <see cref="FileModel"/> presented in database with <see cref="AccessLevel.Public"/> by filter
        /// </summary>
        /// <param name="filter">Model for filtering file models</param>
        /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="FileModel"/> asynchronously</returns>
        public async Task<IEnumerable<FileModel>> GetPublicByFileterAsync(FileFilterModel filter)
        {
            var fileInformation = await _unitOfWork.FileInformationRepository.GetAllAsync();

            fileInformation = fileInformation.Where(x => x.AccessLevel == AccessLevel.Public);

            if(filter.StartDate != default)
            {
                fileInformation = fileInformation.Where(x => x.CreationDate >= filter.StartDate);
            }
            if (filter.EndDate != default)
            {
                fileInformation = fileInformation.Where(x => x.CreationDate <= filter.EndDate);
            }
            if (filter.FileType != default)
            {
                fileInformation = fileInformation.Where(x => x.FileType.Extension == filter.FileType);
            }
            if (filter.MinSize != default)
            {
                fileInformation = fileInformation.Where(x => x.Size >= filter.MinSize);
            }
            if (filter.MaxSize != default)
            {
                fileInformation = fileInformation.Where(x => x.Size <= filter.MaxSize);
            }
            if (filter.CreatorUserName != default)
            {
                fileInformation = fileInformation.Where(x => x.Creator.UserName == filter.CreatorUserName);
            }

            return _mapper.Map<IEnumerable<FileModel>>(fileInformation);
        }

        /// <summary>
        /// Gets <see cref="FileModel"/> by it's creator userName and Name
        /// </summary>
        /// <param name="userName">Username of the creator</param>
        /// <param name="fileName">Name of the file</param>
        /// <returns><see cref="FileStream"/> of the found file</returns>
        public async Task<FileModel> GetFileModelByUserAndNameAsync(string userName, string fileName)
        {
            if(userName == null 
                || userName == ""
                || fileName == null
                || fileName == "")
            {
                throw new BLLException();
            }
            var fileInformation = await _unitOfWork.FileInformationRepository.GetAllAsync();

            var resultedFileInformation = fileInformation.FirstOrDefault(x => x.AccessLevel != AccessLevel.Private
            && x.Creator.UserName == userName && x.Name == fileName);

            return _mapper.Map<FileModel>(resultedFileInformation);

        }


    }
}
