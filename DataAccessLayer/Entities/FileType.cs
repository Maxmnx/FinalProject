using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class FileType : BaseEntity
    {
        public string Extension { get; set; }

        public string MIMEType { get; set; }

        public ICollection<FileInformation> FileInformation { get; set; }

    }
}
