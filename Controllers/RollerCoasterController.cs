using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RollerCoasterAPI.Models.Classes;
using RollerCoasterAPI.Models.Classes.Request;
using RollerCoasterAPI.Models.Classes.Response;
using RollerCoasterAPI.Models.Data;

namespace RollerCoasterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RollerCoasterController : ControllerBase
    {
        [HttpGet]
        [Route("RollerCoaster")]
        public ActionResult<RollerCoaster> RollerCoaster(int rollerCoasterId) 
        {
            // Find by id

            return Ok();
        }

        [HttpGet]
        [Route("RollerCoasters")]
        public ActionResult<RollerCoaster[]> RollerCoasters() 
        {
            // Find all 

            return Ok();
        }

        [HttpPost]
        [Route("AddRollerCoaster")]
        public ActionResult<RollerCoasterResponse[]> AddRollerCoaster(RollerCoasterRequest rollerCoasterRequest) 
        {
            // Find all by id
            using (var context = new RollerCoasterContext())
            {
                var std = new RollerCoaster(rollerCoasterRequest);
                context.RollerCoasters.Add(std);

                // or
                // context.Add<Student>(std);

                context.SaveChanges();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("RemoveRollerCoaster")]
        public ActionResult<RollerCoaster[]> RemoveRollerCoaster() 
        {
            // delete by id

            return Ok();
        }
    }
}