using Entities;
using MarsTerraform.Models;
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
            var username = HttpContext.Current.User.Identity.Name;
            if(IsGameMember(username, gameId))
            {
                return true;
            }

            using (var context = new MarsdbEntities())
            {
                try
                {
                    var game = context.Games.Where(g => g.Id == gameId).FirstOrDefault();
                    game.AspNetUsers.Add(
                        context.AspNetUsers.First(u => u.UserName.Equals(username))
                    );
                    context.Productions.Add(new Production()
                    {
                        GameId = gameId,
                        Owner = username,
                        Money = 0,
                        Steel = 0,
                        Titan = 0,
                        Flora = 0,
                        Energy = 0,
                        Heat = 0
                    });
                    context.Vaults.Add(new Vault()
                    {
                        GameId = gameId,
                        Owner = username,
                        Money = 20,
                        Steel = 0,
                        Titan = 0,
                        Flora = 0,
                        Energy = 0,
                        Heat = 0
                    });
                    context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }
        }

        public bool IsGameMember(string username, int gameId)
        {
            using(var context = new MarsdbEntities())
            {
                var count = context.AspNetUsers
                    .Count(u => u.UserName.Equals(username) &&
                        u.Games.Count(g => g.Id == gameId) > 0);
                return count > 0;
            }
        }

        public HandVM GetUserHand(string username, int gameId)
        {
            var hand = new HandVM()
            {
                GameId = gameId,
                Owner = username
            };
            using (var context = new MarsdbEntities())
            {
                hand.Production = context.Productions
                    .Where(p => p.GameId == gameId && p.Owner.Equals(username))
                    .Select(p => new ProductionVM
                    {
                        Id = p.Id,
                        Money = p.Money,
                        Steel = p.Steel,
                        Titan = p.Titan,
                        Flora = p.Flora,
                        Energy = p.Energy,
                        Heat = p.Heat
                    }).First();

                hand.Vault = context.Vaults
                    .Where(p => p.GameId == gameId && p.Owner.Equals(username))
                    .Select(p => new VaultVM
                    {
                        Id = p.Id,
                        Money = p.Money,
                        Steel = p.Steel,
                        Titan = p.Titan,
                        Flora = p.Flora,
                        Energy = p.Energy,
                        Heat = p.Heat
                    }).First();
                return hand;
            }
        }

        public ChangeValueResponse Add(string area, string field, string username, int gameId)
        {
            var response = new ChangeValueResponse();
            using (var context = new MarsdbEntities())
            {
                if(area.Equals("production"))
                {
                    var production = context.Productions
                        .Where(p => p.Owner.Equals(username) && p.GameId == gameId)
                        .First();
                }
                else if (area.Equals("vault"))
                {
                    var vault = context.Vaults
                        .Where(p => p.Owner.Equals(username) && p.GameId == gameId)
                        .First();
                }
            }
            return response;
        }
    }
}