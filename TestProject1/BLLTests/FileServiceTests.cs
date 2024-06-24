using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Library.Tests;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.BLLTests
{
    public class FileServiceTests
    {

        [Test]
        public async Task FileService_GetAll_ReturnsAll()
        {
            //arrange
            var expected = GetTestsFileModels;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.FileInformationRepository.GetAllAsync())
                .ReturnsAsync(GetTestsFileInformation.AsEnumerable());

            var fileService = new FileService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await fileService.GetAllAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FileService_GetAllPublic_ReturnsRightList()
        {
            //arrange
            var expected = new List<FileModel>() { GetTestsFileModels[0] };
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(x => x.FileInformationRepository.GetAllAsync())
                .ReturnsAsync(GetTestsFileInformation.AsEnumerable());

            var fileService = new FileService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            var actual = await fileService.GetAllPublicAsync();

            //assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase("483625f0-ab5c-4868-a766-1e8cce646874")]
        [TestCase("d0a30097-020c-4b5d-a674-a299b9681cc8")]
        public async Task FileService_DeleteAsync_DeletesFileType(string id)
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(m => m.FileInformationRepository.DeleteByIdAsync(It.IsAny<Guid>()));
            mockUnitOfWork.Setup(m => m.FileInformationRepository.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new FileInformation()
                {
                    Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f")
                });
            mockUnitOfWork.Setup(m => m.FileManager.DeleteFile(It.IsAny<string>()));
            var fileService = new FileService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //act
            await fileService.DeleteAsync(Guid.Parse(id));

            //assert
            mockUnitOfWork.Verify(x => x.FileInformationRepository.DeleteByIdAsync(Guid.Parse(id)), Times.Once());
            mockUnitOfWork.Verify(x => x.FileManager.DeleteFile(It.IsAny<string>()), Times.Once());
            mockUnitOfWork.Verify(x => x.SaveAllAsync(), Times.Once());
        }


        #region TestData

        public List<FileModel> GetTestsFileModels =>
            new List<FileModel>()
            {
                new FileModel { Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"), CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", Name = "file1", AccessLevel = AccessLevel.Public, Description = "descryption1", CreationDate = new DateTime(2022, 10, 8), Size = 2000, FileTypeExtension = "txt", ContentType = "text/plain", CreatorUserName = "username1" },
                new FileModel { Id = new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file2", AccessLevel = AccessLevel.OnlyByLink, Description = null, CreationDate = new DateTime(2021, 10, 19), Size = 100, FileTypeExtension = "png", ContentType = "image/png", CreatorUserName = "username2" },
                new FileModel { Id = new Guid("5ce6a3d6-d8c6-4d0c-9d5e-0a9748b75646"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file3", AccessLevel = AccessLevel.OnlyByLink, Description = "descryption3", CreationDate = new DateTime(2021, 5, 19), Size = 50, FileTypeExtension = "png", ContentType = "image/png", CreatorUserName = "username2" },
                new FileModel { Id = new Guid("e51de004-27b0-4f83-80c0-58dd2944e17d"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file4", AccessLevel = AccessLevel.Private, Description = "descryption4", CreationDate = new DateTime(2021, 2, 19), Size = 10, FileTypeExtension = "png", ContentType = "image/png", CreatorUserName = "username2" }
            };

        public List<FileInformation> GetTestsFileInformation =>
            new List<FileInformation>()
            {
                new FileInformation { Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"), CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", Name = "file1", AccessLevel = AccessLevel.Public, Description = "descryption1", Path = "C:\\file1", 
                    CreationDate = new DateTime(2022, 10, 8), Size = 2000, FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), FileType = new FileType { Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), Extension = "txt", MIMEType = "text/plain" }, Creator = GetTestsAppUsers[0] },

                new FileInformation { Id = new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file2", AccessLevel = AccessLevel.OnlyByLink, Description = null, Path = "C:\\file2", 
                    CreationDate = new DateTime(2021, 10, 19), Size = 100, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), FileType = new FileType { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png" }, Creator = GetTestsAppUsers[1] },

                new FileInformation { Id = new Guid("5ce6a3d6-d8c6-4d0c-9d5e-0a9748b75646"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file3", AccessLevel = AccessLevel.OnlyByLink, Description = "descryption3", Path = "C:\\file3", 
                    CreationDate = new DateTime(2021, 5, 19), Size = 50, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), FileType = new FileType { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png" }, Creator = GetTestsAppUsers[1] },

                new FileInformation { Id = new Guid("e51de004-27b0-4f83-80c0-58dd2944e17d"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file4", AccessLevel = AccessLevel.Private, Description = "descryption4", Path = "C:\\file4", 
                    CreationDate = new DateTime(2021, 2, 19), Size = 10, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), FileType = new FileType { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png" }, Creator = GetTestsAppUsers[1] }
            };

        public List<FileType> GetTestFileTypes =>
           new List<FileType>()
           {
                new FileType { Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), Extension = "txt", MIMEType = "text/plain",
                FileInformation = new List<FileInformation>() {new FileInformation { Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"), CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", Name = "file1", AccessLevel = AccessLevel.Public, Description = "descryption1", Path = "C:\\file2", CreationDate = new DateTime(2022, 10, 8), Size = 2000, FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874") } }
                },
                new FileType { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png",
                FileInformation = new List<FileInformation>() { new FileInformation { Id = new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file1", AccessLevel = AccessLevel.OnlyByLink, Description = null, Path = "C:\\file2", CreationDate = new DateTime(2021, 10, 19), Size = 100, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8") },
                    new FileInformation { Id = new Guid("5ce6a3d6-d8c6-4d0c-9d5e-0a9748b75646"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file3", AccessLevel = AccessLevel.OnlyByLink, Description = "descryption3", Path = "C:\\file3", CreationDate = new DateTime(2021, 5, 19), Size = 50, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8") },
                    new FileInformation { Id = new Guid("e51de004-27b0-4f83-80c0-58dd2944e17d"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file4", AccessLevel = AccessLevel.Private, Description = "descryption4", Path = "C:\\file4", CreationDate = new DateTime(2021, 2, 19), Size = 10, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8") }
                    }
                }
           };

        public List<FileTypeModel> GetTestFileTypeModels =>
           new List<FileTypeModel>()
           {
                new FileTypeModel { Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), Extension = "txt", MIMEType = "text/plain",
                FileInformationIds = new List<Guid>() { new Guid("d513e26a-ff82-4aea-9693-fd29057c445f") } },
                new FileTypeModel { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png",
                FileInformationIds = new List<Guid>() { new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"),
                    new Guid("5ce6a3d6-d8c6-4d0c-9d5e-0a9748b75646"),
                    new Guid("e51de004-27b0-4f83-80c0-58dd2944e17d")
                } }
           };

        public List<AppUser> GetTestsAppUsers =>
            new List<AppUser>()
            {
                new AppUser { Id = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", UserName = "username1", Email = "username1@gmail.com", PasswordHash = "", CreationTime = new DateTime(1980, 5, 25), StoredFiles = new List<FileInformation>(){ GetTestsFileInformation[0] } },
                new AppUser { Id = "595631f2-ded2-4104-8b02-970bb91fdb8d", UserName = "username2", Email = "username2@gmail.com", PasswordHash = "", CreationTime = new DateTime(1984, 10, 19), StoredFiles = new List<FileInformation>(){ GetTestsFileInformation[1], GetTestsFileInformation[2], GetTestsFileInformation[3] } }
            };


        #endregion
    }
}
