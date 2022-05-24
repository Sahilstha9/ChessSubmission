using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Return
    {
        public List<IHavePosition> ReturnPath(List<IHavePosition> path)
        {
            List<IHavePosition> returningPath = new List<IHavePosition>();
            foreach (IHavePosition p in path)
            {
                if (p is King)
                    break;
                returningPath.Add(p);
                if (p is Piece)
                    break;
            }
            return returningPath;
        }
    }
}
