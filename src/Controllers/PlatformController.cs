using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.Services;

namespace ServerING.Controllers {
    [ApiController]
    [Route("/api/v1/platforms")]
    public class PlatformController : Controller {
        private IPlatformService platformService;

        public PlatformController(IPlatformService platformService) {
            this.platformService = platformService;
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
                var addedPlatform = platformService.AddPlatform(platform);
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
                var updatedPlatform = platformService.PutPlatform(id, platform);
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
                var updatedPlatform = platformService.PatchPlatform(id, platform);
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
