using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignSystem.API.Models.Stores;
using SignSystem.API.Services.Stores;
using System.Collections.Generic;

namespace SignSystem.API.Controllers
{
    [Route("api/store")]
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet()]
        public IActionResult GetStores()
        {
            var storeEntities = _storeRepository.GetStores();
            var results = Mapper.Map<IEnumerable<StoreDto>>(storeEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetStore(int id)
        {
            var store = _storeRepository.GetStore(id);

            if (store == null)
            {
                return NotFound();
            }

            var storeResult = Mapper.Map<StoreDto>(store);
            return Ok(storeResult);
        }
    }
}
