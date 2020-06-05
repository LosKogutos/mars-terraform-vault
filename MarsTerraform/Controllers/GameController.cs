using MarsTerraform.Services.Interfaces;
using MarsTerraform.ViewModels;
using System.Web.Mvc;

namespace MarsTerraform.Controllers
{
    [Authorize]
    [RoutePrefix("game")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public ActionResult Create(NewGameVM newGame)
        {
            if (ModelState.IsValid)
            {
                var isSaved = _gameService.SaveGame(newGame);
                if(isSaved)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Unable to add new game because name is already taken. Check your input and try again.";
                    return View();
                }
            }
            return View();
        }

        [Route("join")]
        public ActionResult Join()
        {
            var games = _gameService.GetAvailableGames();
            return View(games);
        }
        
        [Route("joinGame")]
        public ActionResult JoinGame(int gameId)
        {
            if(gameId != 0)
            {
                var isJoined = _gameService.JoinGame(gameId);
                if (isJoined)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Unable to join the game.";
                    return View("Join");
                }
            }
            else
            {
                ViewBag.Error = "Unable to join the game.";
                return View("Join");
            }
        }

        [Route("{gameId}/index")]
        public ActionResult Index(int gameId)
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            if(_gameService.IsGameMember(username, gameId)) {
                //.GetUserHand(username, gameId);
                return View();
            }
            else
            {
                ViewBag.IsNotGameMember = $"You are not yet a member of game #{gameId}. Please join below:";
                return RedirectToAction("Join");
            }
        }
    }
}
