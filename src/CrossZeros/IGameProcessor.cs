using CrossZeros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossZeros
{
    public interface IGameProcessor
    {
        IEnumerable<CrossZeroUser> GetAllUsers();
        bool StartNewGameWithUser(CrossZeroUser me, CrossZeroUser oponent);
        WhoWins CheckWin(int gameId);
        bool MakeMove(int row, int column, int GameId, CrossZeroUser user);
        bool IsFieldValid(int fildNum);
        IEnumerable<Game> GetAllMyGames(CrossZeroUser user);
    }
}
