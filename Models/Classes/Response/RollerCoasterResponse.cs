using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RollerCoasterAPI.Models.Classes.Request;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Classes.Response
{
    public class RollerCoasterResponse
    {
        private RollerCoasterResponse rollerCoasterResponse;

        public RollerCoasterResponse(RollerCoasterResponse rollerCoasterResponse)
        {
            this.rollerCoasterResponse = rollerCoasterResponse;
        }

        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public virtual AttractionResponse Attraction { get; set; } = null!;  
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