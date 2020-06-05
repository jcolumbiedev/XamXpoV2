﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace MainServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public string GetWelcomeMessage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.FullName.Split(",")[0] + ".WelcomeMessage.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        
        Stats stats;
        string osPlatform;
        [HttpGet]
        public ActionResult<string> Get()
        {
            string framework = "";
            try
            {

                framework = Assembly
                    .GetEntryAssembly()?
                    .GetCustomAttribute<TargetFrameworkAttribute>()?
                    .FrameworkName;
                osPlatform = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                stats = new Stats
                {
                    OsPlatform = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                    AspDotnetVersion = framework
                };
            }
            catch (Exception)
            {

                throw;
            }
            return GetWelcomeMessage()+ $"{System.Environment.NewLine}Framework:{framework} platform:{osPlatform}";
            //return new string[] {$"{GetWelcomeMessage()}", $"{System.Environment.NewLine}",
            //    "Hello", "XPO", "Rest" ,$"Framework:{framework} platform:{osPlatform}" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
