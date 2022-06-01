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
        private PieceManager _toPin;
        private PieceManager _piece;
        public MoveDiagonal(List<PieceManager> board, PieceManager p)
        {
            _board = board;
            _piece = p;
        }

        public List<IHavePosition> MoveLeftUp()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX - i >= 0 && _piece.PosY - i >= 0)
            {
                IHavePosition w = new EmptySquare(_piece.PosX - i, _piece.PosY - i);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                i++;
            }
            Pin(path);
            return path;
        }

        public List<IHavePosition> MoveLeftDown()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX - i >= 0 && _piece.PosY + i < 8)
            {
                IHavePosition w = new EmptySquare(_piece.PosX - i, _piece.PosY + i);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                i++;
            }
            Pin(path);
            return path;
        }

        public List<IHavePosition> MoveRightUp()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX + i < 8 && _piece.PosY - i >= 0)
            {
                IHavePosition w = new EmptySquare(_piece.PosX + i, _piece.PosY - i);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                i++;
            }
            Pin(path);
            return path;
        }

        public List<IHavePosition> MoveRightDown()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int i = 1;
            while (_piece.PosX + i < 8 && _piece.PosY + i < 8)
            {
                IHavePosition w = new EmptySquare(_piece.PosX + i, _piece.PosY + i);
                foreach(PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                i++;
            }
            Pin(path);
            return path;
        }

        public void Pin(List<IHavePosition> path)
        {
            if (_piece.Piece is not Pawn && _piece.Piece is not King)
            {
                _index = 0;
                foreach (IHavePosition sq in path)
                {
                    if (sq is PieceManager)
                    {
                        PieceManager p = sq as PieceManager;
                        if (p.Colour != _piece.Colour)
                        {
                            if (p.Piece is King)
                            {
                                if (_index == 1)
                                {
                                    _toPin.Pinned = true;
                                    _toPin.Pinner = _piece;
                                }
                            }
                            else if (p.Piece is not King)
                            {
                                _toPin = p;
                                _index++;
                            }
                        }
                    }
                }
            }
        }
    }
}
