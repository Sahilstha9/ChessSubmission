using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class MoveStraight
    {
        private List<PieceManager> _board;
        private int _index;
        private bool _haspiece;
        private PieceManager _piece;
        public MoveStraight(List<PieceManager> board, PieceManager p)
        {
            _board = board;
            _piece = p;
        }

        public List<IHavePosition> MoveUp()
        {
            _index = -1;
            _haspiece = false;
            int i = 0;
            List<IHavePosition> path = new List<IHavePosition>();
            int y = _piece.PosY - 1;
            while (y >= 0)
            {
                IHavePosition x = new EmptySquare(_piece.PosX, y);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(x))
                    {
                        x = p;
                        Pin(path, p, i);
                    }
                }
                path.Add(x);
                y--;
                i++;
            }
            return path;
        }

        public List<IHavePosition> MoveDown()
        {
            _haspiece = false;
            _index = -1;
            int i = 0;
            List<IHavePosition> path = new List<IHavePosition>();
            int y = _piece.PosY + 1;
            while (y <= 7)
            {
                IHavePosition x = new EmptySquare(_piece.PosX, y);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(x))
                    {
                        x = p;
                        Pin(path, p, i);
                    }
                }
                path.Add(x);
                i++;
                y++;
            }
            return path;
        }

        public List<IHavePosition> MoveLeft()
        {
            _haspiece = false;
            _index = -1;
            int i = 0;
            List<IHavePosition> path = new List<IHavePosition>();
            int x = _piece.PosX - 1;
            while (x >= 0)
            {
                IHavePosition w = new EmptySquare(x, _piece.PosY);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                    {
                        w = p;
                        Pin(path, p, i);
                    }
                }
                path.Add(w);
                i++;
                x--;
            }
            return path;
        }

        public List<IHavePosition> MoveRight()
        {
            _haspiece = false;
            _index = -1;
            int i = 0;
            List<IHavePosition> path = new List<IHavePosition>();
            int x = _piece.PosX + 1;
            while (x <= 7)
            {
                IHavePosition w = new EmptySquare(x, _piece.PosY);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                    {
                        w = p;
                        Pin(path, p, i);
                    }
                }
                path.Add(w);
                i++;
                x++;
            }
            return path;
        }

        public void Pin(List<IHavePosition> path, PieceManager x, int i)
        {
            if (!_haspiece)
            {
                _haspiece = true;
                _index = i;
            }
            else
            {
                if (x.Piece is King && x.Colour != _piece.Colour)
                {
                    if ((path[_index] as PieceManager).Colour != _piece.Colour && (path[_index] as PieceManager).Piece is not King)
                    {
                        (path[_index] as PieceManager).Pinned = true;
                        (path[_index] as PieceManager).Pinner = _piece;
                    }
                }
            }
        }
    }
}
