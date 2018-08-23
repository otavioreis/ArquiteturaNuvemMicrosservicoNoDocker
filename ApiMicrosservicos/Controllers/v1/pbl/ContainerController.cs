using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livraria.Api.ObjectModel.Swagger.Examples;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace Livraria.Api.Controllers.v1.pbl
{
    [Route("v1/public/[controller]")]
    public class ContainerController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("Request processado pelo container id: " + System.Environment.MachineName);
        }
    }
}