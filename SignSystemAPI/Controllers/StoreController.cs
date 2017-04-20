using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignSystem.API.Models.Stores;
using SignSystem.API.Services.Stores;
using System.Collections.Generic;
using SignSystem.API.Entities;
using System;

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

        [HttpPost("{create}")]
        public IActionResult AddStore([FromBody] StoreCreateDto store)
        {
            if(store==null)
            { return BadRequest();}
            if (StoreExists(store.Name))
            { ModelState.AddModelError("Description", "This store already exists");}
            if (!ModelState.IsValid)
            { return BadRequest();}

            var storeEntity = Mapper.Map<Entities.Store>(store);
            _storeRepository.Add(storeEntity);
            if (!_storeRepository.Save())
            { return StatusCode(500,"Problem occured adding the store");}

        return CreatedAtRoute("GetStore", new {storeId=storeEntity.Id, Name=storeEntity.Name}, storeEntity);



        }

        private bool StoreExists(string name)
        {
            throw new NotImplementedException();
        }
    }
}
