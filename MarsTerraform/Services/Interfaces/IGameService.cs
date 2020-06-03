using System;
using MarsTerraform.ViewModels;

namespace MarsTerraform.Services.Interfaces
{
    public interface IGameService
    {
        bool SaveGame(NewGameVM newGame);
    }
}
