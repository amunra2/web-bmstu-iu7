using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServerING.Interfaces;
using ServerING.Mocks;
using ServerING.Models;
using ServerING.Services;
using ServerING.ViewModels;
using System;
using System.Reflection;

namespace ServerING.Controllers {
    public class AdminController : Controller {

        private readonly ILogger<AdminController> _logger;

        private IServerService serverService;
        private IPlatformService platformService;
        private IWebHostingService webHostingService;
        private IUserService userService;

        public AdminController(IServerService serverService, IPlatformService platformService, 
                                IWebHostingService webHostingService, IUserService userService, ILogger<AdminController> logger) {
            this.serverService = serverService;
            this.platformService = platformService;
            this.webHostingService = webHostingService;
            this.userService = userService;
            this._logger = logger;
        }

        public IActionResult Control() {
            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name);

            return View();
        }

        [Authorize(Roles="Admin")]
        public IActionResult ControlServer(string name, int? platformId, int page = 1, ServerSortState sortOrder = ServerSortState.NameAsc) {

            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name);

            var servers = serverService.GetAllServers();
            var viewModel = serverService.ParseServers(servers, name, platformId, page, sortOrder);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddServer() { // для принятия данных из формы

            ViewBag.Platforms = new SelectList(platformService.GetAllPlatforms(), "Id", "Name");
            ViewBag.WebHostings = new SelectList(webHostingService.GetAllWebHostings(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public IActionResult AddServer(InfoServerViewModel model) {
            if (ModelState.IsValid) {
                Server server = new Server {
                    Name = model.Name,
                    Ip = model.Ip,
                    GameVersion = model.GameVersion,
                    HostingID = model.WebHostingId,
                    PlatformID = model.PlatformId
                };

                if (serverService.IsServerExists(server)) {
                    ModelState.AddModelError("", "Name or IP have already exist");

                    _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; Exception: {2}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        "Name or IP have already exist");
                } else {
                    serverService.AddServer(server);

                    _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; Added Server: Name = {2}, IP = {3}, GameVersion = {4}",
                            User.Identity.Name,
                            MethodBase.GetCurrentMethod().Name,
                            server.Name,
                            server.Ip,
                            server.GameVersion);

                    return RedirectToAction("ControlServer");
                }
            } else {
                _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; Exception: {2}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        "Incorrect data");
            }

            ViewBag.Platforms = new SelectList(platformService.GetAllPlatforms(), "Id", "Name");
            ViewBag.WebHostings = new SelectList(webHostingService.GetAllWebHostings(), "Id", "Name");

            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateServer(int id) {

            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; ServerId = {2} - Try",
                    User.Identity.Name,
                    MethodBase.GetCurrentMethod().Name,
                    id);

            ViewBag.Platforms = new SelectList(platformService.GetAllPlatforms(), "Id", "Name");
            ViewBag.WebHostings = new SelectList(webHostingService.GetAllWebHostings(), "Id", "Name");

            Server server = serverService.GetServerByID(id);

            if (server != null) {
                InfoServerViewModel model = new InfoServerViewModel {
                    Id = server.Id,
                    Name = server.Name,
                    Ip = server.Ip,
                    GameVersion = server.GameVersion,
                    WebHostingId = server.HostingID,
                    PlatformId = server.PlatformID
                };

                return View(model);
            }

            return NotFound();
        }


        [HttpPost]
        public IActionResult UpdateServer(InfoServerViewModel model) {
            if (ModelState.IsValid) {
                Server server = new Server {
                    Id = model.Id,
                    Name = model.Name,
                    Ip = model.Ip,
                    GameVersion = model.GameVersion,
                    HostingID = model.WebHostingId,
                    PlatformID = model.PlatformId
                };

                if (serverService.IsServerExists(server)) {
                    ModelState.AddModelError("", "Name or IP have already exist");

                    _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; Exception: {2}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        "Name or IP have already exist");
                } else {
                    serverService.UpdateServer(server);

                    _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; Updated Server: Id = {2}, Name = {3}, IP = {4}, GameVersion = {5}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        server.Id,
                        server.Name,
                        server.Ip,
                        server.GameVersion);

                    return RedirectToAction("ControlServer");
                }
            } else {
                _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; Exception: {2}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        "Incorrect data");
            }

            ViewBag.Platforms = new SelectList(platformService.GetAllPlatforms(), "Id", "Name");
            ViewBag.WebHostings = new SelectList(webHostingService.GetAllWebHostings(), "Id", "Name");

            return View(model);
        }

        public IActionResult DeleteServer(int id) {
            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; ServerId = {2} - Try",
                    User.Identity.Name,
                    MethodBase.GetCurrentMethod().Name,
                    id);

            Server server = serverService.GetServerByID(id);

            if (server != null) { // Delete FavoriteServers and ServerPlayers == Delete On Cascade???
                serverService.DeleteServer(server);

                _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}; ServerId = {2} - Deleted",
                    User.Identity.Name,
                    MethodBase.GetCurrentMethod().Name,
                    id);

                return RedirectToAction("ControlServer");
            }

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ControlUser(string login, int page = 1, UserSortState sortOrder = UserSortState.LoginAsc) {

            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name);

            var users = userService.GetAllUsers();
            var viewModel = userService.ParseUsers(users, login, page, sortOrder);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult UserDetail(int id) {
            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}, UserId = {2}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        id);

            User user = userService.GetUserByID(id);

            if (user != null) {
                return View(user);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult UserDetail(int id, string role) {
            _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}, UserId = {2} - Change Role",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        id);

            User user = userService.GetUserByID(id);

            if (ModelState.IsValid) {
                user.Role = role;
                userService.UpdateUser(user);

                _logger.Log(LogLevel.Information, "User: login = {0}; in method = {1}, UserId = {2} - New Role = {3}",
                        User.Identity.Name,
                        MethodBase.GetCurrentMethod().Name,
                        id,
                        role);

                return RedirectToAction("ControlUser");
            }

            return View(user);
        }
    }
}
