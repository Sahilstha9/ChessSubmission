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
        private PieceManager _piece, _toPin;
        public MoveStraight(List<PieceManager> board, PieceManager p)
        {
            _board = board;
            _piece = p;
        }

        public List<IHavePosition> MoveUp()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int y = _piece.PosY - 1;
            while (y >= 0)
            {
                IHavePosition w = new EmptySquare(_piece.PosX, y);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                y--;
            }
            Pin(path);
            return path;
        }

        public List<IHavePosition> MoveDown()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int y = _piece.PosY + 1;
            while (y <= 7)
            {
                IHavePosition w = new EmptySquare(_piece.PosX, y);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                y++;
            }
            Pin(path);
            return path;
        }

        public List<IHavePosition> MoveLeft()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int x = _piece.PosX - 1;
            while (x >= 0)
            {
                IHavePosition w = new EmptySquare(x, _piece.PosY);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                x--;
            }
            Pin(path);
            return path;
        }

        public List<IHavePosition> MoveRight()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            int x = _piece.PosX + 1;
            while (x <= 7)
            {
                IHavePosition w = new EmptySquare(x, _piece.PosY);
                foreach (PieceManager p in _board)
                {
                    if (p.IsEqual(w))
                        w = p;
                }
                path.Add(w);
                x++;
            }
            Pin(path);
            return path;
        }

        public void Pin(List<IHavePosition> path)
        {
            if(_piece.Piece is not Pawn && _piece.Piece is not King)
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
