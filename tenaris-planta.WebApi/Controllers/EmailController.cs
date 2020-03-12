using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using tenaris_planta.Services;

namespace tenaris_planta.WebApi.Controllers
{
    [Route("api/email")]
    public class EmailController : ApiController
    {
        public EmailController() { }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/email/get")]
        public object Get(string id = null)
        {
            return EmailService.Get(id);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/email/GetPriority")]
        public object GetPriority()
        {
            return EmailService.GetPriority();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/email/update")]
        public object Update(string id, [FromBody]JObject updateObject)
        {
            return EmailService.Update(id, updateObject);
        }
    }
}