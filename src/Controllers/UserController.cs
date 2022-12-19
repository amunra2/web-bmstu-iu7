using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.ModelConverters;
using ServerING.Models;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.Controllers {
    [ApiController]   
    [Route("/api/v1/users")]
    public class UserController : Controller {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly UserConverters userConverters;

        public UserController(IUserService userService,
            IMapper mapper, UserConverters userConverters) {
            this.userService = userService;
            this.mapper = mapper;
            this.userConverters = userConverters;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public IActionResult GetAll() {
            return Ok(mapper.Map<IEnumerable<UserDto>>(userService.GetAllUsers()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Add(UserBaseDto user) {
            try {
                var addedUser = userService
                    .AddUser(mapper.Map<UserBL>(user));
                return Ok(mapper.Map<UserDto>(addedUser));
            }
            catch (UserAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id) {
            var user = mapper.Map<UserDto>(userService.GetUserByID(id));
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Put(int id, UserBaseDto user) {
            try {
                var updatedUser = userService
                    .UpdateUser(id, mapper.Map<UserBL>(user,
                        o => o.AfterMap((src, dest) => dest.Id = id)));

                return updatedUser != null ? Ok(mapper.Map<UserDto>(updatedUser)) : NotFound();
            }
            catch (UserAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch (UserException ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public IActionResult Patch(int id, UserBaseDto user) {
            try {
                var updatedUser = userService
                    .UpdateUser(id, userConverters.convertPatch(id, user));
                return updatedUser != null ? Ok(mapper.Map<UserDto>(updatedUser)) : NotFound();
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id) {
            var deletedUser = userService
                .DeleteUser(id);
            return deletedUser != null ? Ok(mapper.Map<UserDto>(deletedUser)) : NotFound();
        }

        [HttpGet("{userId}/favorites")]
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

        [HttpPost("{userId}/favorites/{serverId}")]
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

        [HttpDelete("{userId}/favorites/{serverId}")]
        [ProducesResponseType(typeof(FavoriteServer), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult DeleteFavorite(int userId, int serverId)
        {
            var deletedFavorite = userService.DeleteFavoriteServer(userId, serverId);
            return deletedFavorite != null ? Ok(deletedFavorite) : NotFound();
        }
    }
}