using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RollerCoasterAPI.Models;
using RollerCoasterAPI.Models.Response;
using RollerCoasterAPI.Models.Request;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System;
using RollerCoasterAPI.Data;
using System.Net;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace RollerCoasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttractionController : RollerCoasterBaseController
    {
        /// <summary>
        /// Add a new Attraction
        /// </summary>
        /// <param name="rollerCoaster"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ResponseRollerCoaster> AddAttraction([Required] RollerCoaster rollerCoaster)
        {
            if (rollerCoaster == null)
            {
                return NotFound();
            }

            var response = new ResponseRollerCoaster();

            var ds = DBAccess.ExecuteDataSet("spAttractionSaveUpdate", new
            {
                rollerCoaster.Id,
                rollerCoaster.TypeId,
                rollerCoaster.Name
            });

            if (ds != null)
            {
                response.ResponseCode = 0;
                response.ResponseMessage = "This is a test!";

                return response;
            }


            return NotFound();
        }

        /// <summary>
        /// Get all current Attractions as Roller Coaster array
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<RollerCoaster[]> Attractions()
        {
            DataSet ds = DBAccess.ExecuteDataSet("spRollerCoastersGet", null);

            bool isError = true;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Error"]) == 0)
                {
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        isError = false;
                        string json = ds.Tables[1].Rows[0]["JSON"].ToString();
                        string incomingETag = string.Empty;
                    }
                }
            }

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
