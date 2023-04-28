using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RollerCoasterAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RollerCoasterAPI.Models.Classes.Request;

namespace RollerCoasterAPI.Models.Classes
{
    public class RollerCoaster 
    {
        private RollerCoaster rollerCoaster;

        public RollerCoaster() 
        {
        }

        public RollerCoaster(RollerCoasterRequest rollerCoasterRequest)
        {
            this.Attraction = new Attraction(rollerCoasterRequest.Attraction);
            this.RollerCoasterType = rollerCoasterRequest.RollerCoasterType;
            this.RollerCoasterDesign = rollerCoasterRequest.RollerCoasterDesign;
            this.Height = rollerCoasterRequest.Height;
            this.TrackLength = rollerCoasterRequest.TrackLength;
            this.MaxSpeed = rollerCoasterRequest.MaxSpeed;
        }

        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [JsonIgnore]
        public virtual Attraction Attraction { get; set; } = null!;  
        [Required]
        public RollerCoasterType RollerCoasterType { get; set; } = null!;  
        [Required]
        public RollerCoasterDesign RollerCoasterDesign { get; set; } = null!;  
        [Column(TypeName = "decimal(3, 2)")]        
        public decimal Height { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TrackLength { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal MaxSpeed { get; set; }
    }
}