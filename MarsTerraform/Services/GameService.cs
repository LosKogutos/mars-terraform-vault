using MarsTerraform.Services.Interfaces;
using MarsTerraform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarsTerraform.Services
{
    public class GameService : IGameService
    {
        public bool SaveGame(NewGameVM newGame)
        {
            return true;
        }
    }
}