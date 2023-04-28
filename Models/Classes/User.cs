using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RollerCoasterAPI.Models.Classes
{
    public class User
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = null!;  
        public DateTime DateCreated { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; } = null!;  
        public bool Member { get; set; }        
    }
}