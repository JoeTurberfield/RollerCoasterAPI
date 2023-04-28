using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RollerCoasterAPI.Models.Classes.Request
{
    public class AttractionResponse
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(19, 4)")]      
        public decimal Cost { get; set; }    
        public virtual ThemePark ThemePark { get; set; } = null!;
        public virtual Manufacturer Manufacturer { get; set; } = null!;
        public DateTime YearOpened { get; set; }
        public virtual OperatingStatus OperatingStatus { get; set; }  = null!;
        [StringLength(50)]
        public string Location { get; set; } = null!;
        public bool Deleted { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}