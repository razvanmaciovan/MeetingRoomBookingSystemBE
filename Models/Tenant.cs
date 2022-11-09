using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Locus.Models
{
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<Room> Rooms { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<User> Users { get; set; }
        public Tenant()
        {
            Rooms = new List<Room>();
            Users = new List<User>();
        }
    }
}
