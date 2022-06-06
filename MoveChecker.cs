using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class MoveCheckerPiece
    {
        private int _posX, _posY;
        private List<Piece> _board;
        private bool _colour;

        public MoveCheckerPiece(List<Piece> board, Piece p)
        {
            _posX = p.PosX;
            _posY = p.PosY;
            _colour = p.Colour;
            _board = board;
        }

        public List<IHavePosition> MoveLeftUp(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (Piece p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX >= 0 && posY >= 0)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX < 0 || posY < 0)
                return path;
            if (i >= 2)
                path.AddRange(MoveRightUp(i + 1, posX + 1, posY - 1, _hasPiece));
            path.AddRange(MoveLeftUp(i + 1, posX - 1, posY - 1, _hasPiece));
            return path;
        }

        public List<IHavePosition> MoveRightUp(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (Piece p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX < 8 && posY >= 0)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX > 7 || posY < 0)
                return path;
            path.AddRange(MoveRightUp(i + 1, posX + 1, posY - 1, _hasPiece));
            if (i >= 2)
                path.AddRange(MoveLeftUp(i + 1, posX - 1, posY + 1, _hasPiece));
            return path;
        }

        public List<IHavePosition> MoveLeftDown(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (Piece p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX >= 0 && posY < 8)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX < 0 || posY > 7)
                return path;
            if (i >= 2)
                path.AddRange(MoveLeftDown(i + 1, posX - 1, posY + 1, _hasPiece));
            path.AddRange(MoveRightDown(i + 1, posX + 1, posY + 1, _hasPiece));
            return path;
        }

        public List<IHavePosition> MoveRightDown(int i, int posX, int posY, bool hasPiece)
        {
            bool _hasPiece = false;
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (Piece p in _board)
            {
                if (p.PosX == posX && p.PosY == posY && p.Colour != _colour)
                    _hasPiece = true;
            }
            if (hasPiece != _hasPiece && i % 2 == 0 && posX < 8 && posY < 8)
                path.Add(new EmptySquare(posX, posY));
            if (i == 4 || _hasPiece == hasPiece || posX > 7 || posY > 7)
                return path;
            if (i >= 2)
                path.AddRange(MoveLeftDown(i + 1, posX - 1, posY + 1, _hasPiece));
            path.AddRange(MoveRightDown(i + 1, posX + 1, posY + 1, _hasPiece));
            return path;
        }

        public void RemovePiece(int posX, int posY)
        {
            List<IHavePosition> toRemove = new List<IHavePosition>();
            if (posY == _posY + 2)
            {
                if (posX == _posX + 2)
                    toRemove.Add(new EmptySquare(_posX + 1, _posY + 1));
                else
                    toRemove.Add(new EmptySquare(_posX - 1, _posY + 1));
            }
            else if (posY == _posY - 2)
            {
                if (posX == _posX + 2)
                    toRemove.Add(new EmptySquare(_posX + 1, _posY - 1));
                else
                    toRemove.Add(new EmptySquare(_posX - 1, _posY - 1));
            }
            else if (posY == _posY + 4)
            {
                if (posX == _posX + 4)
                {
                    toRemove.Add(new EmptySquare(_posX + 1, _posY + 1));
                    toRemove.Add(new EmptySquare(_posX + 3, _posY + 3));
                }
                else if (posX == _posX - 4)
                {
                    toRemove.Add(new EmptySquare(_posX - 1, _posY + 1));
                    toRemove.Add(new EmptySquare(_posX - 3, _posY + 3));
                }
                else
                {
                    toRemove.Add(new EmptySquare(_posX - 1, _posY + 1));
                    toRemove.Add(new EmptySquare(_posX - 1, _posY + 3));
                    toRemove.Add(new EmptySquare(_posX + 1, _posY + 1));
                    toRemove.Add(new EmptySquare(_posX + 1, _posY + 3));
                }
            }
            else
            {
                if (posX == _posX + 4)
                {
                    toRemove.Add(new EmptySquare(_posX + 1, _posY - 1));
                    toRemove.Add(new EmptySquare(_posX + 3, _posY - 3));
                }
                else if (posX == _posX - 4)
                {
                    toRemove.Add(new EmptySquare(_posX - 1, _posY - 1));
                    toRemove.Add(new EmptySquare(_posX - 3, _posY - 3));
                }
                else
                {
                    toRemove.Add(new EmptySquare(_posX - 1, _posY - 1));
                    toRemove.Add(new EmptySquare(_posX - 1, _posY - 3));
                    toRemove.Add(new EmptySquare(_posX + 1, _posY - 1));
                    toRemove.Add(new EmptySquare(_posX + 1, _posY - 3));
                }
            }
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(_board);
            foreach (Piece p in tempList)
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
