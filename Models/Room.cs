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
        public int Floor { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime DayOfReservation { get; set; }
        public string StartInterval { get; set; }
        public string EndInterval { get; set; }
        [JsonIgnore]
        public int? TenantId { get; set; }
        [JsonIgnore]
        public Tenant? Tenant { get; set; }
        [JsonIgnore]
        public int? LayoutId { get; set; }
        [JsonIgnore]
        public Layout? Layout { get; set; }
    }
}
