using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Tests;
using DataAccessLayer.Interfaces;
using System.Reflection;
using DataAccessLayer.Entities;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using FluentAssertions;
using BuisnessLogicLayer.Exceptions;

namespace Tests.BLLTests
{
    public class FileTypeServiceTests
    {

        [Test]
        public async Task FileTypeService_GetAll_ReturnsAll()
        {
            //arrange
            var expected = GetTestFileTypeModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(GetTestFileTypes.AsEnumerable());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await fileTypeService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FileTypeService_GetById_ReturnsFileTypeModel()
        {
            //arrange
            var expected = GetTestFileTypeModels.First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.FileTypeRepository.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(GetTestFileTypes.First());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await fileTypeService.GetByIdAsync(Guid.Parse("483625f0-ab5c-4868-a766-1e8cce646874"));

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FileTypeService_AddAsync_AddsModel()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.AddAsync(It.IsAny<FileType>()));
            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(new List<FileType>());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var fileTypeModel = GetTestFileTypeModels.First();

            //act
            await fileTypeService.AddAsync(fileTypeModel);

            //assert
            mockUnitOfWork.Verify(x => x.FileTypeRepository.AddAsync(It.Is<FileType>(x =>
                            x.Id == fileTypeModel.Id && x.Extension == fileTypeModel.Extension &&
                            x.MIMEType == fileTypeModel.MIMEType)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAllAsync(), Times.Once);
        }

        [Test]
        public async Task FileTypeService_AddAsync_ThrowsBLLExceptionWithEmptyExtension()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.AddAsync(It.IsAny<FileType>()));
            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(new List<FileType>());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var fileType = GetTestFileTypeModels.First();
            fileType.Extension = string.Empty;

            //act
            Func<Task> act = async () => await fileTypeService.AddAsync(fileType);

            //assert
            await act.Should().ThrowAsync<BLLException>();
        }


        [Test]
        public async Task FileTypeService_AddAsync_ThrowsBLLExceptionWithNullObject()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.AddAsync(It.IsAny<FileType>()));
            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(new List<FileType>());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            Func<Task> act = async () => await fileTypeService.AddAsync(null);

            //assert
            await act.Should().ThrowAsync<BLLException>();
        }

        [TestCase("483625f0-ab5c-4868-a766-1e8cce646874")]
        [TestCase("d0a30097-020c-4b5d-a674-a299b9681cc8")]
        public async Task FileTypeService_DeleteAsync_DeletesFileType(string id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.DeleteByIdAsync(It.IsAny<Guid>()));
            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await fileTypeService.DeleteAsync(Guid.Parse(id));

            //assert
            mockUnitOfWork.Verify(x => x.FileTypeRepository.DeleteByIdAsync(Guid.Parse(id)), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAllAsync(), Times.Once());
        }

        
        [Test]
        public async Task FileTypeService_AddAsync_ThrowsBLLExceptionWithExsistingMIMEType()
        {
            //arrange 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.AddAsync(It.IsAny<FileType>()));
            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(GetTestFileTypes.AsEnumerable());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var fileType = GetTestFileTypeModels.First();
            fileType.MIMEType = "text/plain";

            //act
            Func<Task> act = async () => await fileTypeService.AddAsync(fileType);

            //assert
            await act.Should().ThrowAsync<BLLException>();
        }

        [Test]
        public async Task FileTypeService_UpdateAsync_UpdatesFileType()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.AddAsync(It.IsAny<FileType>()));
            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(GetTestFileTypes.AsEnumerable());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var fileType = GetTestFileTypeModels.First();

            //act
            await fileTypeService.UpdateAsync(fileType);

            //assert
            mockUnitOfWork.Verify(x => x.FileTypeRepository.Update(It.Is<FileType>(x =>
                x.Id == fileType.Id && x.Extension == fileType.Extension &&
                x.MIMEType == fileType.MIMEType)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAllAsync(), Times.Once);
        }

        [Test]
        public async Task FileTypeService_UpdateAsync_ThrowsBLLExceptionWithEmptyExtension()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.Update(It.IsAny<FileType>()));
            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(GetTestFileTypes.AsEnumerable());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var fileType = GetTestFileTypeModels.Last();
            fileType.Extension = null;

            //act
            Func<Task> act = async () => await fileTypeService.UpdateAsync(fileType);

            //assert
            await act.Should().ThrowAsync<BLLException>();
        }

        [Test]
        public async Task FileTypeService_UpdateAsync_ThrowsBLLExceptionWithMIMEType()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.FileTypeRepository.Update(It.IsAny<FileType>()));
            mockUnitOfWork
                .Setup(x => x.FileTypeRepository.GetAllAsync())
                .ReturnsAsync(GetTestFileTypes.AsEnumerable());

            var fileTypeService = new FileTypeService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var fileTypeModel = GetTestFileTypeModels.First();
            fileTypeModel.MIMEType = "image/png";

            //act
            Func<Task> act = async () => await fileTypeService.UpdateAsync(fileTypeModel);

            //assert
            await act.Should().ThrowAsync<BLLException>();
        }

        
        

        #region TestData
        public List<FileInformation> GetTestsFileInformation =>
            new List<FileInformation>()
            {
                new FileInformation { Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"), CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", Name = "file1", AccessLevel = AccessLevel.Public, Description = "descryption1", Path = "C:\\file2", CreationDate = new DateTime(2022, 10, 8), Size = 2000, FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874") },
                new FileInformation { Id = new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file1", AccessLevel = AccessLevel.OnlyByLink, Description = null, Path = "C:\\file2", CreationDate = new DateTime(2021, 10, 19), Size = 100, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8") }
            };

        public List<FileType> GetTestFileTypes =>
           new List<FileType>()
           {
                new FileType { Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), Extension = "txt", MIMEType = "text/plain",
                FileInformation = new List<FileInformation>() {new FileInformation { Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"), CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", Name = "file1", AccessLevel = AccessLevel.Public, Description = "descryption1", Path = "C:\\file2", CreationDate = new DateTime(2022, 10, 8), Size = 2000, FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874") } }
                },
                new FileType { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png",
                FileInformation = new List<FileInformation>() { new FileInformation { Id = new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file1", AccessLevel = AccessLevel.OnlyByLink, Description = null, Path = "C:\\file2", CreationDate = new DateTime(2021, 10, 19), Size = 100, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8") } }
                }
           };

        public List<FileTypeModel> GetTestFileTypeModels =>
           new List<FileTypeModel>()
           {
                new FileTypeModel { Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), Extension = "txt", MIMEType = "text/plain",
                FileInformationIds = new List<Guid>() { new Guid("d513e26a-ff82-4aea-9693-fd29057c445f") } },
                new FileTypeModel { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png",
                FileInformationIds = new List<Guid>() { new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a") } }
           };


    }
    #endregion
}
