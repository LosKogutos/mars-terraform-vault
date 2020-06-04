using Entities;
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
            using(var context  = new MarsdbEntities())
            {
                var existingGamesCount = context.Games.Count(g => g.Name.Equals(newGame.Name));
                if(existingGamesCount == 0)
                {
                    var game = new Game
                    {
                        Name = newGame.Name,
                        IsActive = true,
                        Created = DateTime.Now
                    };

                    game.AspNetUsers.Add(
                        context.AspNetUsers.First(u => u.UserName.Equals(HttpContext.Current.User.Identity.Name))
                    );

                    context.Games.Add(game);
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<GameVM> GetAvailableGames()
        {
            using(var context = new MarsdbEntities())
            {
                var list = context.Games
                    .Where(g => g.IsActive == true)
                    .Select(g => new GameVM
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Created = g.Created,
                        Closed = g.Closed,
                        IsActive = g.IsActive,
                        PlayersCount = g.AspNetUsers.Count()
                    }).ToList();

                return list;
            }
        }

        public bool JoinGame(int gameId)
        {
            using(var context = new MarsdbEntities())
            {
                try
                {
                    var game = context.Games.Where(g => g.Id == gameId).FirstOrDefault();
                    game.AspNetUsers.Add(
                        context.AspNetUsers.First(u => u.UserName.Equals(HttpContext.Current.User.Identity.Name))
                    );
                    context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }
            
        }
    }
}