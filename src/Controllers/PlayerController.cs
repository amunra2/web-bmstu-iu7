using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Models;
using ServerING.Services;

namespace ServerING.Controllers
{
    [ApiController]   
    [Route("/api/v1/players")]
    public class PlayerController : Controller
    {
        private IPlayerService playerService;

        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Player>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            var countries = playerService.GetAllPlayers();
            return countries.Any() ? Ok(countries) : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Player), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(PlayerDto playerDto)
        {
            try
            {
                return Ok(playerService.AddPlayer(playerDto));
            }
            catch (PlayerAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var player = playerService.GetPlayerByID(id);
            return player != null ? Ok(player) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, PlayerDto playerDto) {
            try
            {
                var playerUpdateDto = new PlayerUpdateDto()
                {
                    Id = id,
                    Nickname = playerDto.Nickname,
                    LastPlayed = playerDto.LastPlayed,
                    HoursPlayed = playerDto.HoursPlayed
                };

                return Ok(playerService.UpdatePlayer(playerUpdateDto));
            }
            catch (PlayerAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (PlayerNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, PlayerSparceDto playerDto) {
            try
            {
                var playerUpdateDto = new PlayerUpdateSparceDto()
                {
                    Id = id,
                    Nickname = playerDto.Nickname,
                    LastPlayed = playerDto.LastPlayed,
                    HoursPlayed = playerDto.HoursPlayed
                };

                return Ok(playerService.PatchUpdatePlayer(playerUpdateDto));
            }
            catch (PlayerAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (PlayerNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletedPlayer = playerService.DeletePlayer(id);
            return deletedPlayer != null ? Ok(deletedPlayer) : NotFound();
        }

    }
}