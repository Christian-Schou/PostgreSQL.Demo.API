using PostgreSQL.Demo.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace PostgreSQL.Demo.API.Models.Authors
{
    public class UpdateAuthorRequest
    {
        public string Name { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
