using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RollerCoasterAPI.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Classes
{
    public class Ride
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public virtual Attraction Attraction { get; set; } = null!;
        [Required]public RideType Type { get; set; } = null!;
    }
}