using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class FileTypeModel
    {

        public Guid Id { get; set; }
        public string Extension { get; set; }

        public string MIMEType { get; set; }

        public ICollection<Guid>? FileInformationIds { get; set; }
    }
}
