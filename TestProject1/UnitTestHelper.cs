

using AutoMapper;
using BuisnessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<ApplicationDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static void SeedData(ApplicationDbContext context)
        {
            context.Users.AddRange(
                new AppUser { Id = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", UserName = "username1", Email = "username1@gmail.com", PasswordHash = "", CreationTime = new DateTime(1980, 5, 25) },
                new AppUser { Id = "595631f2-ded2-4104-8b02-970bb91fdb8d", UserName = "username2", Email = "username2@gmail.com", PasswordHash = "", CreationTime = new DateTime(1984, 10, 19) });
            context.FileInformation.AddRange(
                new FileInformation { Id = new Guid("d513e26a-ff82-4aea-9693-fd29057c445f"), CreatorId = "6d5d7ecb-9629-41be-91b7-0ef3d049af6c", Name = "file1", AccessLevel = AccessLevel.Public, Description = "descryption1", Path = "C:\\file2", CreationDate = new DateTime(2022, 10, 8), Size = 2000, FileTypeId = new Guid("483625f0-ab5c-4868-a766-1e8cce646874") },
                new FileInformation { Id = new Guid("03cf8d6d-2e96-4170-9872-50fa518a476a"), CreatorId = "595631f2-ded2-4104-8b02-970bb91fdb8d", Name = "file1", AccessLevel = AccessLevel.OnlyByLink, Description = null, Path = "C:\\file2", CreationDate = new DateTime(2021, 10, 19), Size = 100, FileTypeId = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8") });
            context.FileTypes.AddRange(
                new FileType { Id = new Guid("483625f0-ab5c-4868-a766-1e8cce646874"), Extension = "txt", MIMEType = "text/plain" },
                new FileType { Id = new Guid("d0a30097-020c-4b5d-a674-a299b9681cc8"), Extension = "png", MIMEType = "image/png" });

            context.SaveChanges();
        }
    }
}