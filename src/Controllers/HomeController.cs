using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerING.Interfaces;
using ServerING.Mocks;
using ServerING.Models;
using ServerING.Services;
using ServerING.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerING.Controllers {
    public class HomeController : Controller {

        private readonly ILogger<HomeController> _logger;

        private IServerService serverService;
        private IUserService userService;

        public HomeController(IServerService serverService, IUserService userService, ILogger<HomeController> logger) {
            this.serverService = serverService;
            this.userService = userService;
            this._logger = logger;
        }

        public IActionResult Index(string name, int? platformId, int page = 1, ServerSortState sortOrder = ServerSortState.NameAsc) {
            var servers = serverService.GetAllServers();
            var viewModel = serverService.ParseServers(servers, name, platformId, page, sortOrder);

            if (User.Identity.IsAuthenticated) {

                User user = userService.GetUserByLogin(User.Identity.Name); // в сервисс ??? 
                var userFavoriteServerIds = serverService.GetUserFavoriteServersIds(user.Id); // в сервисс ???

                viewModel.UserFavoriteServerIds = userFavoriteServerIds.ToList();
            }

            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}",
                User.Identity.Name,
                MethodBase.GetCurrentMethod().Name);

            return View(viewModel);
        }

        public IActionResult Detail(int? id) {
            if (id != null) {
                var detailViewModel = serverService.DetailServer((int)id);

                if (detailViewModel != null) {
                    return View(detailViewModel);
                }
            }

            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}, ServerDetailId = {2}",
                User.Identity.Name,
                MethodBase.GetCurrentMethod().Name,
                id);

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
