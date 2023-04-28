using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RollerCoasterAPI.Models.Classes
{
    public class Manufacturer
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ManufacturerName { get; set; } = null!;
        [StringLength(05)]
        public string City { get; set; } = null!;
        [StringLength(50)]
        public string County { get; set; } = null!;
        [StringLength(50)]
        public string Country { get; set; } = null!;
    }
}