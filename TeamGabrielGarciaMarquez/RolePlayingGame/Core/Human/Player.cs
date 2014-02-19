using RolePlayingGame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.Core.Human
{
    class Player : Human, IMovable
    {
        //Just base constructor
        //Should be singleton design pattern
        public Player(float row, float col) : base(row, col)
        {
        }
    }
}
