using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerING.Interfaces;
using ServerING.Mocks;
using ServerING.Models;
using ServerING.Repository;
using ServerING.Services;
using System.Collections.Generic;
using System.Reflection;

namespace ServerING.Controllers {
    public class UserController : Controller {
        private IUserService userService;
        private IServerService serverService;

        public UserController(IUserService userService, IServerService serverService) {
            this.userService = userService;
            this.serverService = serverService;
        }

        public IActionResult AddToFavorite(int? id) {
            if (id != null) {
                User user = userService.GetUserByLogin(User.Identity.Name);
                serverService.AddFavoriteServer((int)id, user.Id);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteFromFavorite(int? id, string page) {

            if (id != null) {
                User user = userService.GetUserByLogin(User.Identity.Name);

                serverService.DeleteFavoriteServer((int)id, user.Id);
            }

            if (page == "FavoriteServers") {
                return RedirectToAction("FavoriteServers", "User");
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }


        [Authorize]
        public IActionResult FavoriteServers(string name, int? platformId, 
                int page = 1, ServerSortState sortOrder = ServerSortState.NameAsc) {
            User user = userService.GetUserByLogin(User.Identity.Name);
            IEnumerable<Server> servers = userService.GetUserFavoriteServers(user);

            var viewModel = serverService.ParseServers(servers, name, platformId, page, sortOrder);

            return View(viewModel);
        }
    }
}
