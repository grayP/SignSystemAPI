using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignSystem.API.Entities;

namespace SignSystem.API.Controllers
{
    public class DummyController : Controller
    {
        private SignSystemInfoContext _ctx;

        public DummyController(SignSystemInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
