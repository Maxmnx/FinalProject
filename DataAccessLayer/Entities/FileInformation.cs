using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{

    public enum AccessLevel
    {
        Public,
        OnlyByLink,
        Private 
    }

    public class FileInformation : BaseEntity
    {

        public string Name { get; set; }

        public Guid FileTypeId { get; set; }

        public FileType FileType { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public string? Description { get; set; }

        public string Path { get; set; }

        public long Size { get; set; }

        public string CreatorId { get; set; }

        public AppUser Creator { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
