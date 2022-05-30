using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class MoveDiagonal
    {
        private List<PieceManager> _board;
        private int _index;
        private bool _haspiece;
        private PieceManager _piece;
        public MoveDiagonal(List<PieceManager> board, PieceManager p)
        {
            _board = board;
            _piece = p;
        }

        public List<IHavePosition> MoveLeftUp()
        {
            int j = 0;
            _haspiece = false;
            _index = -1;
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX - i >= 0 && _piece.PosY - i >= 0)
            {
                IHavePosition w = new EmptySquare(_piece.PosX - i, _piece.PosY - i);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                    {
                        w = p;
                        Pin(path, p, j);
                    }
                }
                path.Add(w);
                i++;
                j++;
            }
            return path;
        }

        public List<IHavePosition> MoveLeftDown()
        {
            int j = 0;
            _haspiece = false;
            _index = -1;
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX - i >= 0 && _piece.PosY + i < 8)
            {
                IHavePosition w = new EmptySquare(_piece.PosX - i, _piece.PosY + i);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                    {
                        w = p;
                        Pin(path, p, j);
                    }
                }
                path.Add(w);
                i++;
                j++;
            }
            return path;
        }

        public List<IHavePosition> MoveRightUp()
        {
            int j = 0;
            _haspiece = false;
            _index = -1;
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX + i < 8 && _piece.PosY - i >= 0)
            {
                IHavePosition w = new EmptySquare(_piece.PosX + i, _piece.PosY - i);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                    {
                        w = p;
                        Pin(path, p, j);
                    }
                }
                path.Add(w);
                i++;
                j++;
            }
            return path;
        }

        public List<IHavePosition> MoveRightDown()
        {
            int j = 0;
            _haspiece = false;
            _index = -1;
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX + i < 8 && _piece.PosY + i < 8)
            {
                IHavePosition w = new EmptySquare(_piece.PosX + i, _piece.PosY + i);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                    {
                        w = p;
                        Pin(path, p, j);
                    }
                }
                path.Add(w);
                i++;
                j++;
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
