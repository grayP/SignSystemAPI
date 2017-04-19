using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignSystem.API.Entities;
using SignSystem.API.Models.Signs;
using SignSystem.API.Models.Stores;
using SignSystem.API.Services.Signs;

namespace SignSystem.API.Controllers
{
    [Route("api/sign")]
    public class SignController : Controller
    {
        private ISignRepository _signRepository;
        public SignController(SignRepository signRepository)
        {
            _signRepository = signRepository;
        }

        [HttpGet()]
        public IActionResult GetSigns()
        {
            var signEntities = _signRepository.GetSigns();
            var results = Mapper.Map<IEnumerable<SignDto>>(signEntities);

            return Ok(results);
        }


        [HttpGet("{id}")]
        public IActionResult GetSign(int signId)
        {
            var signEntity = _signRepository.GetSign(signId);
            var result = Mapper.Map<SignDto>(signEntity);
            return Ok(result);
        }
    }
}
