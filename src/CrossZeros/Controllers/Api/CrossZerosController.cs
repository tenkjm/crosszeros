using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrossZeros.Controllers.Api
{
   
    public class CrossZerosController: Controller
    {
        [Authorize]
        [Route("api/crosszeros/move")]
        public async Task<JsonResult> MakeMove()
        {
             
            Response.StatusCode = (int)HttpStatusCode.Accepted;
            return Json("Logout is ok");
        }

    }
}
