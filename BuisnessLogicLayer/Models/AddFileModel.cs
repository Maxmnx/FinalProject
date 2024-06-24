using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogicLayer.Models
{
    public class AddFileModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int AccessLevel { get; set; }

        public string CreatorId { get; set; }

        public IFormFile File { get; set; }
    }
}
