using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public DateTime CreationTime { get; set; }

        public ICollection<FileInformation> StoredFiles { get; set; }

        public ICollection<AppUser> BannedAppUsers { get; set; }

        public ICollection<AppUser> BannedByAppUsers { get; set; }
    }
}
