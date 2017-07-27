using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossZeros.Models
{
    public class Game
    {
        public System.DateTime Created  { get; set; }
        public int Id { get; set; }
        public int UserCrossId { get; set; }
        public int UserZeroId { get; set; }
        public GameState state { get; set; }
        public bool isFinished { get; set; }
        public bool isCrossTurn { get; set; }
    }
}
