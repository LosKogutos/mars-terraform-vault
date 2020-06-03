using MarsTerraform.Services.Interfaces;
using MarsTerraform.ViewModels;
using System.Web.Mvc;

namespace MarsTerraform.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Join()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

    }
}
