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
    public class RideController : ControllerBase
    {
        [HttpGet]
        [Route("/Ride")]
        public ActionResult<Ride> Ride(int rideId) 
        {
            // find by id

            return Ok();
        }

        [HttpGet]
        [Route("/Rides")]
        public ActionResult<Ride[]> Rides() 
        {
            // find by id
            
            return Ok();
        }
    }
}