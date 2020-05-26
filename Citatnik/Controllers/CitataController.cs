using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Citatnik.DataBase;
using Citatnik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Citatnik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitataController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CitataRepository citataRepository = CitataRepository.getInstance();

            Citata citata = citataRepository.Select(id);

            if (citata == null)
            {
                return NotFound();
            }

            return new ObjectResult(citata);
        }
    }
}