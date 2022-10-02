using PostgreSQL.Demo.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace PostgreSQL.Demo.API.Models.Authors
{
    public class CreateAuthorRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
