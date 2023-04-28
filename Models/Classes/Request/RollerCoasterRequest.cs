using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Classes.Request
{
    public class RollerCoasterRequest
    {
        private RollerCoasterRequest rollerCoasterRequest;

        public RollerCoasterRequest(RollerCoasterRequest rollerCoasterRequest)
        {
            this.rollerCoasterRequest = rollerCoasterRequest;
        }

        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public virtual AttractionRequest Attraction { get; set; } = null!;  
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