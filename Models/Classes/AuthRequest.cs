using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RollerCoasterAPI.Models.Classes
{
    public class AuthRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}