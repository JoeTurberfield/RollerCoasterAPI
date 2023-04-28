using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RollerCoasterAPI.Models.Classes
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public virtual NoteType NoteType { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; } = null!;
    }
}