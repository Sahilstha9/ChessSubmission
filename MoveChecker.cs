using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class MoveCheckerPiece
    {
        private List<PieceManager> _board;
        private PieceManager _piece;

        public MoveCheckerPiece(List<PieceManager> board, PieceManager p)
        {
            _piece = p;
            _board = board;
        }

        public List<IHavePosition> MoveLeftUp(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (PieceManager p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _piece.Colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX >= 0 && posY >= 0)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX < 0 || posY < 0)
                return path;
            if (i > 2)
                path.AddRange(MoveRightUp(i + 1, posX + 1, posY - 1, _hasPiece));
            path.AddRange(MoveLeftUp(i + 1, posX - 1, posY - 1, _hasPiece));
            return path;
        }

        public List<IHavePosition> MoveRightUp(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (PieceManager p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _piece.Colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX < 8 && posY >= 0)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX > 7 || posY < 0)
                return path;
            path.AddRange(MoveRightUp(i + 1, posX + 1, posY - 1, _hasPiece));
            if (i > 2)
                path.AddRange(MoveLeftUp(i + 1, posX - 1, posY + 1, _hasPiece));
            return path;
        }

        public List<IHavePosition> MoveLeftDown(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (PieceManager p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _piece.Colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX >= 0 && posY < 8)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX < 0 || posY > 7)
                return path;
            if (i > 2)
                path.AddRange(MoveRightDown(i + 1, posX + 1, posY + 1, _hasPiece));
            path.AddRange(MoveLeftDown(i + 1, posX - 1, posY + 1, _hasPiece));
            return path;
        }

        public List<IHavePosition> MoveRightDown(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (PieceManager p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _piece.Colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX < 8 && posY < 8)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX > 7 || posY > 7)
                return path;
            if (i > 2)
                path.AddRange(MoveLeftDown(i + 1, posX - 1, posY + 1, _hasPiece));
            path.AddRange(MoveRightDown(i + 1, posX + 1, posY + 1, _hasPiece));
            return path;
        }

        public void RemovePiece(int posX, int posY)
        {
            List<IHavePosition> toRemove = new List<IHavePosition>();
            if (posY == _piece.PosY + 2)
            {
                if (posX == _piece.PosX + 2)
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY + 1));
                else if (posX == _piece.PosX - 2)
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY + 1));
            }
            else if (posY == _piece.PosY - 2)
            {
                if (posX == _piece.PosX + 2)
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY - 1));
                else if (posX == _piece.PosX - 2)
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY - 1));
            }
            else if (posY == _piece.PosY + 4)
            {
                if (posX == _piece.PosX + 4)
                {
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY + 1));
                    toRemove.Add(new EmptySquare(_piece.PosX + 3, _piece.PosY + 3));
                }
                else if (posX == _piece.PosX - 4)
                {
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY + 1));
                    toRemove.Add(new EmptySquare(_piece.PosX - 3, _piece.PosY + 3));
                }
                else if(posX == _piece.PosX)
                {
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY + 1));
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY + 3));
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY + 1));
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY + 3));
                }
            }
            else if(posY == _piece.PosY - 4)
            {
                if (posX == _piece.PosX + 4)
                {
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY - 1));
                    toRemove.Add(new EmptySquare(_piece.PosX + 3, _piece.PosY - 3));
                }
                else if (posX == _piece.PosX - 4)
                {
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY - 1));
                    toRemove.Add(new EmptySquare(_piece.PosX - 3, _piece.PosY - 3));
                }
                else if (posX == _piece.PosX)
                {
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY - 1));
                    toRemove.Add(new EmptySquare(_piece.PosX - 1, _piece.PosY - 3));
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY - 1));
                    toRemove.Add(new EmptySquare(_piece.PosX + 1, _piece.PosY - 3));
                }
            }
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(_board);
            foreach (PieceManager p in tempList)
            {
                foreach (IHavePosition sq in toRemove)
                {
                    if (p.IsEqual(sq))
                        _board.Remove(p);
                }
            }
        }
    }
}
