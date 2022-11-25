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
    [Route("/api/v1/hosting")]
    public class HostingController : Controller {
        private IHostingService hostingService;

        public HostingController(IHostingService hostingService) {
            this.hostingService = hostingService;
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(hostingService.GetAllHostings());
        }

        [HttpPost]
        [ProducesResponseType(typeof(WebHosting), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(HostingFormDto hosting) {
            try {
                var addedHosting = hostingService.AddHosting(hosting);
                return Ok(addedHosting);
            }
            catch (HostingConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WebHosting), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var hosting = hostingService.GetHostingByID(id);
            return hosting != null ? Ok(hosting) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(WebHosting), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, HostingFormDto hosting) {
            try {
                var updatedHosting = hostingService.PutHosting(id, hosting);
                return updatedHosting != null ? Ok(updatedHosting) : NotFound();
            }
            catch (HostingConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(WebHosting), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, HostingFormDto hosting) {
            try {
                var updatedHosting = hostingService.PatchHosting(id, hosting);
                return updatedHosting != null ? Ok(updatedHosting) : NotFound();
            }
            catch (HostingConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(WebHosting), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedHosting = hostingService.DeleteHosting(id);
            return deletedHosting != null ? Ok(deletedHosting) : NotFound();
        }
    }
}