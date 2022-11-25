using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.Services;
using System.Linq;

namespace ServerING.Controllers {
    [ApiController]
    [Route("/api/v1/server")]
    public class ServerController : Controller {
        private IServerService serverService;

        public ServerController(IServerService serverService) {
            this.serverService = serverService;
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] ServerFilterDto filter,
            [FromQuery] ServerSortState? sortState,
            [FromQuery] int? page
        ) {
            return Ok(serverService.GetAllServers(filter, sortState, page));
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(ServerAddDto server) {
            try {
                var addedServer = serverService.AddServer(server);
                return Ok(addedServer);
            }
            catch (ServerConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Server), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var server = serverService.GetServerByID(id);

            if (server != null) {
                return Ok(server);
            }
            else {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Server), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, ServerUpdateDto server) {
            try {
                var updatedServer = serverService.PutServer(id, server);
                return updatedServer != null ? Ok(updatedServer) : NotFound();
            }
            catch (ServerConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Server), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, ServerUpdateDto server) {
            try {
                var updatedServer = serverService.PatchServer(id, server);
                return updatedServer != null ? Ok(updatedServer) : NotFound();
            }
            catch (ServerConflictException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Server), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedServer = serverService.DeleteServer(id);

            if (deletedServer != null) {
                return Ok(deletedServer);
            }
            else {
                return NotFound();
            }
        }

        [HttpGet("{serverId}/players")]
        [ProducesResponseType(typeof(IEnumerable<Player>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetServerPlayers(int serverId)
        {
            try
            {
                var players = serverService.GetServerPlayers(serverId);
                return players != null && players.Any() ? Ok(players) : NoContent();
            }
            catch (UserNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
