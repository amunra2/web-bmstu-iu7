using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerING.Models;
using ServerING.Services;
using System.Diagnostics;
using System.Linq;

namespace ServerING.Controllers {
    public class HomeController : Controller {
        private IServerService serverService;
        private IUserService userService;

        public HomeController(IServerService serverService, IUserService userService, ILogger<HomeController> logger) {
            this.serverService = serverService;
            this.userService = userService;
        }

        public IActionResult Index(string name, int? platformId, int page = 1, ServerSortState sortOrder = ServerSortState.NameAsc) {
            var servers = serverService.GetAllServers();
            var viewModel = serverService.ParseServers(servers, name, platformId, page, sortOrder);

            if (User.Identity.IsAuthenticated) {

                User user = userService.GetUserByLogin(User.Identity.Name); // в сервисс??? 
                var userFavoriteServerIds = serverService.GetUserFavoriteServersIds(user.Id); // в сервисс ???

                viewModel.UserFavoriteServerIds = userFavoriteServerIds.ToList();
            }

            return View(viewModel);
        }

        public IActionResult Detail(int? id) {
            if (id != null) {
                var detailViewModel = serverService.DetailServer((int)id);

                if (detailViewModel != null) {
                    return View(detailViewModel);
                }
            }

            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
