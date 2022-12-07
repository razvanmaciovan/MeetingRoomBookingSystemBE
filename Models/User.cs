using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Locus.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(16)]
        [SwaggerSchema(WriteOnly = true)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int? TenantId { get; set; }
        [JsonIgnore]
        public Tenant? Tenant { get; set; }

    }
}