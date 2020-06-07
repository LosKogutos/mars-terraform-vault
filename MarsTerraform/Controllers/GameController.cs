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
                    return RedirectToAction("Hand", "Game", new { gameId= gameId });
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

        [Route("{gameId}/hand/{username}")]
        public ActionResult HandOponent(int gameId, string username)
        {
            if (_gameService.IsGameMember(username, gameId))
            {
                var hand = _gameService.GetUserHand(username, gameId);
                return View(hand);
            }
            else
            {
                ViewBag.IsNotGameMember = $"You are not yet a member of game #{gameId}. Please join below:";
                return RedirectToAction("Join");
            }
        }

        [Route("{gameId}/hand")]
        public ActionResult Hand(int gameId)
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;
            if(_gameService.IsGameMember(username, gameId)) {
                var hand = _gameService.GetUserHand(username, gameId);
                TempData["gameId"] = gameId;
                return View(hand);
            }
            else
            {
                ViewBag.IsNotGameMember = $"You are not yet a member of game #{gameId}. Please join below:";
                return RedirectToAction("Join");
            }
        }

        [HttpPost]
        [Route("AddProd")]
        public JsonResult AddProd(HandInputVM input)
        {
            var gameId = (int)TempData["gameId"];
            TempData.Keep();

            var result = _gameService.AddProd(input, gameId);
            return Json(result);
        }

        [HttpPost]
        [Route("SubstractProd")]
        public JsonResult SubstractProd(HandInputVM input)
        {
            var gameId = (int)TempData["gameId"];
            TempData.Keep();

            var result = _gameService.SubstractProd(input, gameId);
            return Json(result);
        }

        [HttpPost]
        [Route("UpdateVault")]
        public JsonResult UpdateVault(HandInputVM input)
        {
            var gameId = (int)TempData["gameId"];
            TempData.Keep();

            var result = _gameService.UpdateVault(input, gameId);
            return Json(result);
        }
    }
}
