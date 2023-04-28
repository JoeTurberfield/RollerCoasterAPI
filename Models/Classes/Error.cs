using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RollerCoasterAPI.Models.Classes
{
    public class Error
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string ErrorName { get; set; } = null!;
        [StringLength(255)]
        public string ErrorMessage { get; set; } = null!;
        public DateTime DateCreated { get; set; }
    }
}