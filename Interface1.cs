using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public interface IPieceStrategy
    {
        public bool Move(int posX, int posY);

        public List<IHavePosition> AvailableMove();

        public void Draw();

        public List<IHavePosition> CheckPath(IHavePosition king);
    }
}
