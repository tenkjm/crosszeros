using CrossZeros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossZeros
{
    interface IGameProcessor
    {
        IEnumerable<CrossZeroUser> GetAllUsers();
        void StartNewGameWithUser(CrossZeroUser oponent);
        void CheckWin(Game game);
        bool MakeMove(int row, int column);
    }
}
