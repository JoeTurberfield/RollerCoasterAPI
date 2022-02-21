using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RollerCoasterAPI.Models;
using RollerCoasterAPI.Models.Response;
using RollerCoasterAPI.Models.Request;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System;

namespace RollerCoasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollerCoasterController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ResponseRollerCoaster> AddRollerCoaster([Required] RollerCoaster rollerCoaster)
        {
            if (rollerCoaster == null)
            {
                return NotFound();
            }

            var response = new ResponseRollerCoaster();

            // SQL Connection
            using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("sp_RollerCoasterSaveUpdate", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RollerCoasterID", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@RollerCoasterTypeID", 1));
                cmd.Parameters.Add(new SqlParameter("@RollerCoasterName", "API Test Roller Coaster"));
                cmd.Parameters.Add(new SqlParameter("@ThemeParkID", 1));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerID", 1));
                cmd.Parameters.Add(new SqlParameter("@YearOpened", "2001"));
                cmd.Parameters.Add(new SqlParameter("@Height", 100));
                cmd.Parameters.Add(new SqlParameter("@TrackLength", 500));
                cmd.Parameters.Add(new SqlParameter("@MaxSpeed", 10));
                cmd.Parameters.Add(new SqlParameter("@OperatingStatusID", 1));
                cmd.Parameters.Add(new SqlParameter("@Cost", 10000000));
                cmd.Parameters.Add(new SqlParameter("@TrainTypeID", 1));
                cmd.Parameters.Add(new SqlParameter("@IsDeleted", 0));

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response.ResponseCode = Convert.ToInt32(ds.Tables[0].Rows[0]["Response"]);
                        response.ResponseMessage = ds.Tables[0].Rows[0]["ResponseMessage"].ToString();
                    }
                }
            }

            return response;




            // Entity Framework

        }
    }
}
