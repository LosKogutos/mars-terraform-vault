using System.Collections.Generic;
using MarsTerraform.ViewModels;

namespace MarsTerraform.Services.Interfaces
{
    public interface IGameService
    {
        bool SaveGame(NewGameVM newGame);
        List<GameVM> GetAvailableGames();
        bool JoinGame(int gameId);
    }
}
