using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class EmptySquare : IHavePosition
    {

        private int _posX, _posY;
        public EmptySquare(int x, int y)
        {
            PosX = x;
            PosY = y;
        }

        public int PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }

        public int PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }

        public bool IsEqual(IHavePosition x)
        {
            if (PosX == x.PosX && PosY == x.PosY)
                return true;
            return false;
        }

        public bool IsAt(Point2D pt)
        {
            if (pt.X >= PosX * Constants.Instance.Width + 50 && pt.Y >= PosY * Constants.Instance.Width + 50 && pt.X <= (PosX * Constants.Instance.Width + 130) && pt.Y <= (PosY * Constants.Instance.Width + 130))
                return true;
            else
                return false;
        }
    }
}
