﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignSystem.API.Models;
using SignSystem.API.Services;

namespace SignSystem.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ISignSystemInfoRepository _signSystemInfoRepository;


        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ISignSystemInfoRepository signSystemInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _signSystemInfoRepository = signSystemInfoRepository;
        }

        //[HttpGet("{cityId}/pointsofinterest")]
        //public IActionResult GetPointsOfInterest(int cityId)
        //{
        //    try
        //    {
        //        if (!_signSystemInfoRepository.SignExists(cityId))
        //        {
        //            _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
        //            return NotFound();
        //        }

        //        var pointsOfInterestForCity = _signSystemInfoRepository.GetPointsOfInterestForCity(cityId);
        //        var pointsOfInterestForCityResults =
        //                           Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);

        //        return Ok(pointsOfInterestForCityResults);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
        //        return StatusCode(500, "A problem happened while handling your request.");
        //    }
        //}

        //[HttpGet("{cityId}/pointsofinterest/{id}", Model = "GetPointOfInterest")]
        //public IActionResult GetPointOfInterest(int cityId, int id)
        //{
        //    if (!_signSystemInfoRepository.CityExists(cityId))
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterest = _signSystemInfoRepository.GetPointOfInterestForCity(cityId, id);

        //    if (pointOfInterest == null)
        //    {
        //        return NotFound();
        //    }

        //    var pointOfInterestResult = Mapper.Map<PointOfInterestDto>(pointOfInterest);
        //    return Ok(pointOfInterestResult);             
        //}

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_signSystemInfoRepository.SignExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _signSystemInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_signSystemInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPointOfInterestToReturn = Mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_signSystemInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _signSystemInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterest, pointOfInterestEntity);

            if (!_signSystemInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }


        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_signSystemInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _signSystemInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (!_signSystemInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_signSystemInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _signSystemInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _signSystemInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

            if (!_signSystemInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            _mailService.Send("Point of interest deleted.",
                    $"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");
            
            return NoContent();
        }
    }
}
