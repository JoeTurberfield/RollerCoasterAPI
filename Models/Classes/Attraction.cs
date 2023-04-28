using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using RollerCoasterAPI.Models.Classes.Request;

namespace RollerCoasterAPI.Models.Classes
{
    public class Attraction 
    {
        public Attraction() 
        {
        }

        public Attraction (AttractionRequest attractionRequest) 
        {
            this.Name = attractionRequest.Name;
            this.Description = attractionRequest.Description;
            this.Cost = attractionRequest.Cost;
            this.ThemePark = attractionRequest.ThemePark;
            this.Manufacturer = attractionRequest.Manufacturer;
            this.YearOpened = attractionRequest.YearOpened;
            this.OperatingStatus = attractionRequest.OperatingStatus;
            this.Location = attractionRequest.Location;
            this.Deleted = attractionRequest.Deleted;
        }

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
        public virtual ThemePark ThemePark { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public DateTime YearOpened { get; set; }
        public virtual OperatingStatus OperatingStatus { get; set; }  = null!;
        [StringLength(50)]
        public string Location { get; set; } = null!;
        public bool Deleted { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}