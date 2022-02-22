using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RestSharp;
using RollerCoasterAPI.Data;
using RollerCoasterAPI.Models;
using RollerCoasterAPI.Models.Request;
using RollerCoasterAPI.Models.Response;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RollerCoasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollerCoasterController : RollerCoasterBaseController
    {
        /// <summary>
        /// Returns all drop down lists for the Roller Coaster inputs
        /// </summary>
        /// <returns></returns>
        [HttpGet("RollerCoasterDropDownLists")]
        public ActionResult<ResponseDropDownList> RollerCoasterDropDownLists()
        {
            var dbRes = DBHelper.ExecuteDataSet("sp_RollerCoasterDatabaseGetDDLs");

            if (dbRes.IsSuccess)
            {
                ResponseDropDownList responseDropDownList = new ResponseDropDownList();

                foreach (DataTable tbl in dbRes.Ds.Tables)
                {
                    DataColumnCollection columns = tbl.Columns;
                    if (!columns.Contains("Value") || !columns.Contains("Name"))
                    {
                        continue;
                    }

                    DropDownList ddl = new DropDownList();

                    foreach (DataRow row in tbl.Rows)
                    {
                        ddl._DropDownList.Add( new DropDownItem {
                            Value = Convert.ToInt32(row["Value"]),
                            Name = row["Name"].ToString()
                        });
                    }

                    responseDropDownList.DropDownLists.Add(ddl);
                }

                return Ok(responseDropDownList);
            }

            return NotFound();
        }

        /// <summary>
        /// Add a new roller coaster
        /// </summary>
        /// <param name="rollerCoaster"></param>
        /// <returns></returns>
        [HttpPost("AddRollerCoaster")]
        public ActionResult<ResponseRollerCoaster> AddRollerCoaster([Required] RollerCoaster rollerCoaster)
        {
            if (rollerCoaster == null)
            {
                return NotFound();
            }

            var dbRes = DBHelper.ExecuteDataSet("sp_RollerCoasterSaveUpdate", new
            {
                RollerCoasterID = rollerCoaster.Id,
                RollerCoasterTypeID = rollerCoaster.TypeId,
                RollerCoasterName = rollerCoaster.Name,
                ThemeParkID = rollerCoaster.ThemeParkId,
                ManufacturerID = rollerCoaster.ManufacturerId,
                YearOpened = rollerCoaster.YearOpened,
                Height = rollerCoaster.Height,
                TrackLength = rollerCoaster.TrackLength,
                MaxSpeed = rollerCoaster.MaxSpeed,
                OperatingStatusID = rollerCoaster.OperatingStatusId,
                Cost = rollerCoaster.Cost,
                TrainTypeID = rollerCoaster.TrainTypeId,
                IsDeleted = false
            });

            return Ok(new ResponseRollerCoaster(dbRes));
        }

        /// <summary>
        /// Get all current roller coasters as Roller Coaster array
        /// </summary>
        /// <returns></returns>
        [HttpGet("RollerCoasters")]
        public ActionResult<RollerCoaster[]> RollerCoasters()
        {
            var dbRes = DBHelper.ExecuteDataSet("sp_RollerCoastersGet");

            if (dbRes.IsSuccess)
            {
                DataSet ds = dbRes.Ds;
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    //string json = dbRes.Tables[1].Rows[0]["JSON"].ToString();
                    //string incomingETag = string.Empty;
                }
            }

            return StatusCode((int)HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get all current roller coasters as json using etag header to check if data is modifed
        /// </summary>
        /// <returns></returns>
        [HttpGet("RollerCoastersJson")]
        [Produces("application/json")]
        public ActionResult RollerCoastersJson()
        {
            var dbRes = DBHelper.ExecuteDataSet("spRollerCoastersGet", null).Ds;

            bool isError = true;
            if (dbRes.Tables.Count > 0 && dbRes.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(dbRes.Tables[0].Rows[0]["Error"]) == 0)
                {
                    if (dbRes.Tables.Count > 1 && dbRes.Tables[1].Rows.Count > 0)
                    {
                        isError = false;

                        string json = dbRes.Tables[1].Rows[0]["JSON"].ToString();
                        string incomingETag = string.Empty;

                        if (HttpContext.Request.Headers.ContainsKey(HeaderNames.IfNoneMatch))
                        {
                            incomingETag = HttpContext.Request.Headers[HeaderNames.IfNoneMatch].ToString();
                        }

                        StringBuilder Sb = new StringBuilder();
                        using (var hash2 = System.Security.Cryptography.SHA256.Create())
                        {
                            Encoding enc = Encoding.UTF8;
                            byte[] result = hash2.ComputeHash(enc.GetBytes(json));

                            foreach (byte b in result)
                            {
                                Sb.Append(b.ToString("x2"));
                            }
                        }
                        var eTag = Sb.ToString();

                        if (incomingETag.Equals(eTag))
                        {
                            return StatusCode((int)HttpStatusCode.NotModified);
                        }
                        else
                        {
                            HttpContext.Response.Headers.Add(HeaderNames.ETag, new[] { eTag });
                            return Content(json, "application/json");
                        }
                    }
                }
            }

            return NotFound();
        }

        #region POV Videos
        /// <summary>
        /// Downloads an .mp4 video file for Roller Coasters POVs
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet("DownloadFile")]
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DownloadPOVFile([Required] string filename)
        {
            string filePath = Path.Combine(RollerCoaster.POV_dir, filename);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var result = File(bytes, "video/mp4", Path.GetFileName(filePath));

            return result;
        }

        [HttpPost("UploadPOV")]
        public ActionResult<ResponsePOV> UploadPOV([Required] string fileName, [Required] string filePath, int durationFrames, decimal frameRate, int resolutionX, int resolutionY)
        {
            ResponsePOV response = new ResponsePOV();

            var dbRes = DBHelper.ExecuteDataSet("sp_RollerCoasterPOVUpload", new {
                user = 12,
                fileName,
                filePath,
                durationFrames,
                frameRate,
                resolutionX,
                resolutionY
            });

            response.ResponseCode = dbRes.ResponseCode;
            response.ResponseMessage = dbRes.ResponseMessage;

            if (dbRes.IsSuccess)
            {
                DataSet ds = dbRes.Ds;

                if (response.ResponseCode == 0 && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    DataRow rowTbl1 = ds.Tables[1].Rows[0];

                    POV newPOV = new POV();

                    newPOV.FildId = Convert.ToInt32(rowTbl1["FileID"]);
                    newPOV.FileName = $"{rowTbl1["BunnyCDNFileName"]}.mp4";
                }
            }

            string streamLibraryId = ""; // NOT SET YET
            string streamApiKey = ""; // NOT SET YET

            // Send to BunnyCDN API
            var client = new RestClient("https://video.bunnycdn.com");
            var request = new RestRequest($"library/{streamLibraryId}/videos", Method.Post);
            request.AddHeader("AccessKey", streamApiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new CreateNewVideoRequest { Title = fileName });
            var bunnyResponse = client.ExecuteAsync<POV>(request);

            if (bunnyResponse.IsCompleted)
            {
                #region API paths to download video file to be uploaded to BunnyCDN               
                // Local - Does not work with BunnyCDN
                // string url = $"https://localhost:44343/api/VideoOnDemand/DownloadFile?filename={filename}";
                #endregion

                POV video = response.Pov;

                var dbResGuid = DBHelper.ExecuteDataSet("sp_POVFileGUIDSet", new
                {
                    //FileID = fileId,
                    //GUID = video.Guid
                });

                string guid = ""; // NOT YET SET
                string url = ""; // NOT YET SET

                if (dbResGuid.IsSuccess)
                {
                    // Fetch video and upload
                    var requestUpload = new RestRequest($"library/{streamLibraryId}/videos/{guid}/fetch", Method.Post);
                    requestUpload.AddJsonBody(new FetchVideoRequest { URL = url });
                    requestUpload.AddHeader("Content-Type", "application/json");
                    requestUpload.AddHeader("AccessKey", streamApiKey);
                    var uploadResponse = client.ExecuteAsync<FetchVideoResponse>(requestUpload);

                    if (uploadResponse.IsCompleted)
                    {
                        if (uploadResponse.Result.StatusCode == HttpStatusCode.OK)
                        {
                            response.ResponseCode = 0;
                            response.ResponseMessage = $"{fileName} Upload to BunnyCDN successful";
                        }
                        else
                        {
                            response.ResponseCode = -1;
                            response.ResponseMessage = $"{fileName} Upload to BunnyCDN unsuccessful. Error - Status Code: {uploadResponse.Result.StatusCode}, Message: {uploadResponse.Result.ErrorMessage}.";
                        }
                    }
                    else
                    {
                        response.ResponseCode = -1;
                        response.ResponseMessage = $"{fileName} Upload to BunnyCDN unsuccessful. Error - Status Code: {uploadResponse.Result.StatusCode}, Message: {uploadResponse.Result.ErrorMessage}.";
                    }
                }
            }
            else
            {
                response.ResponseCode = -1;
                response.ResponseMessage = $"{fileName} Upload to BunnyCDN unsuccessful. Error - Message: {response.ResponseMessage}.";
            }

            return response;
        }

        #endregion
    }
}
