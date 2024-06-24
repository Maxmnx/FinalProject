using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<FileInformation> FileInformation { get; set; }

        public DbSet<FileType> FileTypes { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             if (!optionsBuilder.IsConfigured)
             {
                 optionsBuilder.UseSqlServer(@"Server=DESKTOP-E1CPJHN;Database=MyStorageDB;Trusted_Connection=True;");
             }
         }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FileInformation>()
                .HasOne(s => s.Creator)
                .WithMany(u => u.StoredFiles)
                .HasForeignKey(s => s.CreatorId);

            

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.StoredFiles)
                .WithOne(s => s.Creator)
                .HasForeignKey(s => s.CreatorId);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.BannedAppUsers)
                .WithMany(u => u.BannedByAppUsers);





            base.OnModelCreating(modelBuilder);
        }
    }
}
