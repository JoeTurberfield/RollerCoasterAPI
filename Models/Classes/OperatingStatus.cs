using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RollerCoasterAPI.Models.Classes
{
    public class OperatingStatus
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = null!;
    }
}