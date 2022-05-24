using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public interface IHavePosition
    {
        public int PosX {
            get { return PosX; }
            set { PosX = value; }
        }
        public int PosY
        {
            get { return PosY; }
            set { PosY = value; }
        }

        public bool IsEqual(IHavePosition x);

        public bool IsAt(Point2D pt);
    }
}
