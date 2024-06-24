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
    public class FileTypeRepositoryTest
    {

        [TestCase("483625f0-ab5c-4868-a766-1e8cce646874")]
        [TestCase("d0a30097-020c-4b5d-a674-a299b9681cc8")]
        public async Task FileTypeRepository_GetByIdAsync_ReturnsSingleValue(string id)
        {

            Guid guid = Guid.Parse(id);

            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileTypeRepository = new FileTypeRepository(context);
            var fileType = await fileTypeRepository.GetByIdAsync(guid);

            var expected = ExpectedFileTypes.FirstOrDefault(x => x.Id == guid);

            Assert.That(fileType, Is.EqualTo(expected).Using(new FileTypeEqualityComparer()), message: "GetByIdAsync method works incorrect");
        }

        [Test]
        public async Task FileTypeRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileTypeRepository = new FileTypeRepository(context);
            var fileTypes = await fileTypeRepository.GetAllAsync();

            Assert.That(fileTypes, Is.EqualTo(ExpectedFileTypes).Using(new FileTypeEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public async Task FileTypeRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileTypeRepository = new FileTypeRepository(context);
            var fileType = new FileType
            {
                Id = Guid.Parse("861ed4ed-bb98-40a8-9a7c-106b830f56cd"),
                Extension = "jpg",
                MIMEType = "image/jpg"
            };

            await fileTypeRepository.AddAsync(fileType);
            await context.SaveChangesAsync();

            Assert.That(context.FileTypes.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
        }

        [Test]
        public async Task FileTypeRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileTypeRepository = new FileTypeRepository(context);

            await fileTypeRepository.DeleteByIdAsync(Guid.Parse("483625f0-ab5c-4868-a766-1e8cce646874"));
            await context.SaveChangesAsync();

            Assert.That(context.FileTypes.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
        }

        [Test]
        public async Task FileTypeRepository_Update_UpdatesEntity()
        {
            using var context = new ApplicationDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var fileTypeRepository = new FileTypeRepository(context);
            var fileType = new FileType
            {
                Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"),
                Extension = "jpg",
                MIMEType = "image/jpg"
            };

            fileTypeRepository.Update(fileType);
            await context.SaveChangesAsync();

            Assert.That(fileType, Is.EqualTo(new FileType
            {
                Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"),
                Extension = "jpg",
                MIMEType = "image/jpg"
            }).Using(new FileTypeEqualityComparer()), message: "Update method works incorrect");
        }

        private static IEnumerable<FileType> ExpectedFileTypes =>
            new[]
            {
                new FileType { Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), Extension = "txt", MIMEType = "text/plain" },
                new FileType { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png" }
            };
    }
}
