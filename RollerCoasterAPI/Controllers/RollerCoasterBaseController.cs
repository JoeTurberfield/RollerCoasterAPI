using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RollerCoasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RollerCoasterBaseController : ControllerBase
    {
        protected readonly string _keyClientPlatformId = "ClientPlatformID";

        protected int ClientPlatformId
        {
            get
            {
                var request = HttpContext.Request;
                if (request != null)
                {
                    if (request.Headers[_keyClientPlatformId] != string.Empty)
                    {
                        return Convert.ToInt32(request.Headers[_keyClientPlatformId]);
                    }
                }
                return -1;
            }
        }
    }
}
