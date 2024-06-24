using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Library.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace TestProject1.DALTests
{
    [TestFixture]
    public class FileInformationRepositoryTests
    {

        [TestCase("d513e26a-ff82-4aea-9693-fd29057c445f")]
        [TestCase("03cf8d6d-2e96-4170-9872-50fa518a476a")]
        public async Task FileInformationRepositoryy_GetByIdAsync_ReturnsSingleValue(string id)
        {

            Guid guid = Guid.Parse(id);

            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileInformationRepository = new FileInformationRepository(context);
            var fileInformation = await fileInformationRepository.GetByIdAsync(guid);

            var expected = ExpectedProductCategories.FirstOrDefault(x => x.Id == guid);

            Assert.That(fileInformation, Is.EqualTo(expected).Using(new FileInformationEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task FileInformationRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileInformationRepository = new FileInformationRepository(context);
            var fileInformation = await fileInformationRepository.GetAllAsync();

            Assert.That(fileInformation, Is.EqualTo(ExpectedProductCategories).Using(new FileInformationEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task FileInformationRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileInformationRepository = new FileInformationRepository(context);
            var fileInformation = new FileInformation
            {
                Id = Guid.Parse("861ed4ed-bb98-40a8-9a7c-106b830f56cd"),
                Name = "file3",
                Size = 300,
                Description = "This is file3",
                Path = "C\\file3.txt",
                FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"),
                AccessLevel = AccessLevel.Public,
                CreationDate = new DateTime(2022, 11, 11),
                CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c"
            };

            await fileInformationRepository.AddAsync(fileInformation);
            await context.SaveChangesAsync();

            Assert.That(context.FileInformation.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task FileInformationRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileInformationRepository = new FileInformationRepository(context);

            await fileInformationRepository.DeleteByIdAsync(Guid.Parse("d513e26a-ff82-4aea-9693-fd29057c445f"));
            await context.SaveChangesAsync();

            Assert.That(context.FileInformation.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task FileInformationRepository_Update_UpdatesEntity()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileInformationRepository = new FileInformationRepository(context);
            var fileInformation = new FileInformation
            {
                Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"),
                CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c",
                Name = "file10000",
                AccessLevel = AccessLevel.Public,
                Description = "descryption1",
                Path = "C:\\file2",
                CreationDate = new DateTime(2022, 10, 8),
                Size = 2000,
                FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874")
            };

            fileInformationRepository.Update(fileInformation);
            await context.SaveChangesAsync();

            Assert.That(fileInformation, Is.EqualTo(new FileInformation
            {
                Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"),
                CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c",
                Name = "file10000",
                AccessLevel = AccessLevel.Public,
                Description = "descryption1",
                Path = "C:\\file2",
                CreationDate = new DateTime(2022, 10, 8),
                Size = 2000,
                FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874")
            }).Using(new FileInformationEqualityComparer()), message: "Update method works incorrect");
        }

        private static IEnumerable<FileInformation> ExpectedProductCategories =>
            new[]
            {
                new FileInformation { Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"), CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", Name = "file1", AccessLevel = AccessLevel.Public, Description = "descryption1", Path = "C:\\file2", CreationDate = new DateTime(2022, 10, 8), Size = 2000, FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874") },
                new FileInformation { Id = new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file1", AccessLevel = AccessLevel.OnlyByLink, Description = null, Path = "C:\\file2", CreationDate = new DateTime(2021, 10, 19), Size = 100, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8") }
            };
    }
}