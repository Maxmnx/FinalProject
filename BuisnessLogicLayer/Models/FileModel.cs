using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class FileModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }
 
        public AccessLevel AccessLevel { get; set; }

        public string FileTypeExtension { get; set; }

        public string ContentType { get; set; }

        public string? Description { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUserName { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
