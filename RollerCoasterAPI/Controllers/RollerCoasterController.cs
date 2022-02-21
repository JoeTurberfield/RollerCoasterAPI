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
using System.Threading.Tasks;
using System.IO;
using RestSharp;

namespace RollerCoasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollerCoasterController : RollerCoasterBaseController
    {
        /// <summary>
        /// Add a new roller coaster
        /// </summary>
        /// <param name="rollerCoaster"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ResponseRollerCoaster> AddRollerCoaster([Required] RollerCoaster rollerCoaster)
        {
            if (rollerCoaster == null)
            {
                return NotFound();
            }

            var response = new ResponseRollerCoaster();

            var ds = DBAccess.ExecuteDataSet("spRollerCoasterSaveUpdate", new
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
        /// Get all current roller coasters as Roller Coaster array
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<RollerCoaster[]> RollerCoasters()
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

        /// <summary>
        /// Get all current roller coasters as json using etag header to check if data is modifed
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public ActionResult RollerCoastersJson()
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

        [HttpPost]
        public ActionResult<ResponsePOV> UploadPOV([Required] string fileName, [Required] string filePath, int durationFrames, decimal frameRate, int resolutionX, int resolutionY)
        {
            ResponsePOV response = new ResponsePOV();

            DataSet ds = DBAccess.ExecuteDataSet("sp_RollerCoasterPOVUpload", new {
                user = 12,
                fileName,
                filePath,
                durationFrames,
                frameRate,
                resolutionX,
                resolutionY
            });

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow rowTbl0 = ds.Tables[0].Rows[0];

                response.ResponseCode = Convert.ToInt32(rowTbl0["ResponseCode"]);
                response.ResponseMessage = rowTbl0["ResponseMessage"].ToString();

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

                DataSet dsGuid = DBAccess.ExecuteDataSet("sp_POVFileGUIDSet", new
                {
                    //FileID = fileId,
                    //GUID = video.Guid
                });

                string guid = ""; // NOT YET SET
                string url = ""; // NOT YET SET

                if (dsGuid != null && dsGuid.Tables.Count > 0 && dsGuid.Tables[0].Rows.Count > 0)
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
    }
}
