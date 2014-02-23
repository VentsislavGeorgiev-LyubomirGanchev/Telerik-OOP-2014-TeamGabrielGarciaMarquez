using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolePlayingGame.UI
{
    public class Tile : Sprite
    {
        //default constructor
        public Tile()
            :base(new Core.GameState(new System.Drawing.SizeF()),12,12,new System.Drawing.Bitmap("blq"),new System.Drawing.Rectangle(),2)
        {
        }
    }
}
