using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Locus.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LayoutId { get; set; }
        [JsonIgnore]
        public Layout? Layout { get; set; }
        [JsonIgnore]
        public int? TenantId { get; set; }
        [JsonIgnore]
        public Tenant? Tenant { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<Reservation> Reservations { get; set; }
        public Room()
        {
            Reservations = new List<Reservation>();
        }
    }
}
