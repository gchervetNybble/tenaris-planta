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
        private EmailService _emailService;

        public EmailController() { }

        // GET api/emails

        [Route("api/email/getall")]
        [AllowAnonymous]
        [HttpGet]
        public List<object> Get()
        {
            _emailService = new EmailService();

            return _emailService.Get();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}