using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadBookController : ControllerBase
    {
        private readonly ILogger<UploadBookController> _logger;

        public UploadBookController(ILogger<UploadBookController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/uploadAndReturnJSON")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if(file == null)
            {
                return BadRequest();
            }
            var isbns = new List<string>();

            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                var fileContents = await sr.ReadToEndAsync();
                isbns = fileContents.Replace("\r\n", string.Empty).Split(",").ToList();
                var results = await new BookProcessor.ProcessBooks().ByISBNsAsync(isbns, Environment.GetEnvironmentVariable("ApiKey"));

                var serialized = JsonConvert.SerializeObject(results);
                return Ok(serialized);
            }
        }

        [HttpPost]
        [Route("/uploadAndReturnCSVFile")]
        public async Task<FileResult> PostAndReturnFile(IFormFile file)
        {
            var isbns = new List<string>();

            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                var fileContents = await sr.ReadToEndAsync();
                isbns = fileContents.Replace("\r\n", string.Empty).Split(",").ToList();
                var results = await new BookProcessor.ProcessBooks().ByISBNsAsync(isbns, Environment.GetEnvironmentVariable("ApiKey"));

                var fileOut = BookProcessor.WriteOutput.ToFileOutAsCSV(results.OutputDetails, results.NotFound);
                return fileOut;
            }
        }
    }
}
