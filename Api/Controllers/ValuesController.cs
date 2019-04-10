using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger _logger;

        public ValuesController(
            ILogger<ValuesController> logger)
        {
            this._logger = logger;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogTrace("Get Values Request");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            _logger.LogTrace("Get Value Request");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }

        [HttpGet("Do")]
        public string Run()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = "images",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            var process = new Process { StartInfo = psi };
            process.Start();
            var output = string.Empty;
            var error = string.Empty;
            using(var sr = process.StandardOutput)
            {
                output += sr.ReadToEnd();
            }
            using(var sr = process.StandardError)
            {
                error += sr.ReadToEnd();
            }
            return output;
        }
    }
}