using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelpersController : ControllerBase
    {
        [HttpGet]
        public IActionResult ListEnvironmentVariables()
        {
            var envVars = Environment.GetEnvironmentVariables();

            var list = new List<EnvVars>();

            foreach (DictionaryEntry e in envVars)
            {
                list.Add(new EnvVars { Name = e.Key.ToString(), Value = e.Value.ToString() });
            }

            return Ok(list);
        }


        private class EnvVars
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}