using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossZeros.ViewModels
{
    public class GameViewModel
    {
        public System.DateTime Created { get; set; }
        public int Id { get; set; }
        public string UserZeroId { get; set; }
        public bool isFinished { get; set; }
        public bool isCrossTurn { get; set; }
    }
}
