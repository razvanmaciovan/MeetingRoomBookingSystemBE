﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Locus.Models
{
    public class Layout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<Room> Rooms { get; set; }
        public int Floor { get; set; }
        public int? TenantId { get; set; }
        [JsonIgnore]
        public Tenant? Tenant { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public IEnumerable<Image> Images { get; set; }
        public Layout()
        {
            Rooms = new List<Room>();
            Images = new List<Image>();
        }
    }
}
