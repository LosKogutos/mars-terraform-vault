using System.Collections.Generic;
using MarsTerraform.Models;
using MarsTerraform.ViewModels;

namespace MarsTerraform.Services.Interfaces
{
    public interface IGameService
    {
        bool SaveGame(NewGameVM newGame);
        List<GameVM> GetAvailableGames();
        bool JoinGame(int gameId);
        bool IsGameMember(string username, int gameId);
        HandVM GetUserHand(string username, int gameId);
        ChangeValueResponse AddProd(HandInputVM input, int gameId);
        ChangeValueResponse SubstractProd(HandInputVM input, int gameId);
        ChangeValueResponse UpdateVault(HandInputVM input, int gameId);
        bool Finish(int gameId);
    }
}
