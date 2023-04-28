using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RollerCoasterAPI.Models.Classes;

namespace RollerCoasterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class User : ControllerBase
    {
        [HttpGet]
        [Route("Authenticate")]
        public ActionResult UserAuthenticate(AuthRequest authRequest) 
        {
            // Login

            return Ok(); 
        }
    }
}