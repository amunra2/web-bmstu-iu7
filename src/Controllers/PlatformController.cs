using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.Services;
using ServerING.ModelConverters;

namespace ServerING.Controllers {
    [ApiController]
    [Route("/api/v1/platforms")]
    public class PlatformController : Controller {
        private readonly IPlatformService platformService;
        private readonly PlatformConverters platformConverters;

        public PlatformController(IPlatformService platformService, PlatformConverters platformConverters) {
            this.platformService = platformService;
            this.platformConverters = platformConverters;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(platformService.GetAllPlatforms());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Platform), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(PlatformFormDto platform) {
            try {
                var addedPlatform = platformService.AddPlatform(platformConverters.convertPost(platform));
                return Ok(addedPlatform);
            }
            catch (PlatformConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Platform), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var platform = platformService.GetPlatformByID(id);
            return platform != null ? Ok(platform) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Platform), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, PlatformFormDto platform) {
            try {
                var updatedPlatform = platformService.UpdatePlatform(id, platformConverters.convertPut(id, platform));
                return updatedPlatform != null ? Ok(updatedPlatform) : NotFound();
            }
            catch (PlatformConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Platform), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, PlatformFormDto platform) {
            try {
                var updatedPlatform = platformService.UpdatePlatform(id, platformConverters.convertPatch(id, platform));
                return updatedPlatform != null ? Ok(updatedPlatform) : NotFound();
            }
            catch (PlatformConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Platform), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedPlatform = platformService.DeletePlatform(id);
            return deletedPlatform != null ? Ok(deletedPlatform) : NotFound();
        }
    }
}
