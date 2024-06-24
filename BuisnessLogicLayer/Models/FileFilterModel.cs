using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class FileFilterModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? FileType { get; set; }

        public int? MinSize { get; set; }

        public int? MaxSize { get; set; }

        public string? CreatorUserName { get; set; }
    }
}
