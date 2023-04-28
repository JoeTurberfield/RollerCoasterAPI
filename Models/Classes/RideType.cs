using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Classes
{
    public class RideType
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = null!;
    }
}