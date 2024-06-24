using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

    }
}