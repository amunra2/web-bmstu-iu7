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
    [Route("/api/v1/user")]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            var countries = userService.GetAllUsers();
            return countries.Any() ? Ok(countries) : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(UserDto userDto)
        {
            try
            {
                return Ok(userService.AddUser(userDto));
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var user = userService.GetUserByID(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, UserDto userDto) {
            try
            {
                var userUpdateDto = new UserUpdateDto()
                {
                    Id = id,
                    Login = userDto.Login,
                    Password = userDto.Password,
                    Role = userDto.Role
                };

                return Ok(userService.UpdateUser(userUpdateDto));
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (UserNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, UserDto userDto) {
            try
            {
                var userUpdateDto = new UserUpdateDto()
                {
                    Id = id,
                    Login = userDto.Login,
                    Password = userDto.Password,
                    Role = userDto.Role
                };

                return Ok(userService.PatchUpdateUser(userUpdateDto));
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (UserNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var deletedUser = userService.DeleteUser(id);
            return deletedUser != null ? Ok(deletedUser) : NotFound();
        }

        [HttpGet("{userId}/favorite")]
        [ProducesResponseType(typeof(IEnumerable<Server>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetFavorites(int userId)
        {
            try
            {
                var servers = userService.GetUserFavoriteServers(userId);
                return servers != null && servers.Any() ? Ok(servers) : NoContent();
            }
            catch (UserNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{userId}/favorite/{serverId}")]
        [ProducesResponseType(typeof(FavoriteServer), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult AddFavorite(int userId, int serverId)
        {
            try
            {
                return Ok(userService.AddFavoriteServer(userId, serverId));
            }
            catch (UserFavoriteAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{userId}/favorite/{serverId}")]
        [ProducesResponseType(typeof(FavoriteServer), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult DeleteFavorite(int userId, int serverId)
        {
            var deletedFavorite = userService.DeleteFavoriteServer(userId, serverId);
            return deletedFavorite != null ? Ok(deletedFavorite) : NotFound();
        }
    }
}