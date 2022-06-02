using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class King : Piece, IPieceStrategy
    {
        private bool _hasmoved;
        public King(PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
            _hasmoved = false;
        }

        public override bool Move(int posX, int posY)
        {
            IHavePosition toMove = new EmptySquare(posX, posY);
            foreach(PieceManager p in Board.Instance.GameBoard)
            {
                if(p.IsEqual(toMove) && p.Piece is Rook && (p as PieceManager).Colour == Colour)
                {
                    if (Castling(p))
                        return true;
                }
            }
            foreach(IHavePosition p in AvailableMove())
            {
                if(p.IsEqual(toMove))
                {
                     if(p is EmptySquare || (p as PieceManager).Colour != Colour)
                    {
                        RemovePieceFromBoard(posX, posY);
                        _controller.PosX = posX;
                        _controller.PosY = posY;
                        return true;
                    }
                }
            }
            return false;
        }

        public override List<IHavePosition> CheckPath(IHavePosition king)
        {
            return new List<IHavePosition>();
        }

        private bool Castling(PieceManager rook)
        {
            if (_hasmoved)
                return false;
            else
            {
                if (rook.PosX == 0)
                {
                    for (int i = 1; i < _controller.PosX; i++)
                    {
                        foreach(PieceManager p in Board.Instance.GameBoard)
                        {
                            if (p.PosY == _controller.PosY && p.PosX == i)
                                return false;
                            else if((p as PieceManager).Colour != Colour)
                            {
                                foreach(IHavePosition x in p.AvailableMove())
                                {
                                    if (x.PosX == i && x.PosY == _controller.PosY)
                                        return false;
                                }
                            }
                        }
                    }
                    _controller.PosX = 2;
                    rook.PosX = 3;
                    return true;
                }
                else if (rook.PosX == 7)
                {
                    for (int i = _controller.PosX + 1; i < 7; i++)
                    {
                        foreach (PieceManager p in Board.Instance.GameBoard)
                        {
                            if (p.PosY == _controller.PosY && p.PosX == i)
                                return false;
                            else if ((p as PieceManager).Colour != Colour)
                            {
                                foreach (IHavePosition x in p.AvailableMove())
                                {
                                    if (x.PosX == i && x.PosY == _controller.PosY)
                                        return false;
                                }
                            }
                        }
                    }
                    _controller.PosX = 6;
                    rook.PosX = 5;
                    return true;
                }
                else
                    return false;
            }
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            path.AddRange(AllPath());
            foreach(PieceManager p in Board.Instance.GameBoard)
            {
                if (p == _controller)
                    continue;
                else if (p.Piece is King)
                    path = RemoveSquare(path, ((King)p.Piece).AllPath());
                else if (p.Piece is Pawn && (p as PieceManager).Colour != Colour)
                {
                    List<IHavePosition> tempList = new List<IHavePosition>();
                    tempList.AddRange(path);
                    foreach (IHavePosition sq in tempList)
                    {
                        if ((p as PieceManager).Colour)
                        {
                            if ((sq.PosX == p.PosX + 1 || sq.PosX == p.PosX - 1) && sq.PosY == p.PosY - 1)
                                path.Remove(sq);
                        }
                        else
                        {
                            if ((sq.PosX == p.PosX + 1 || sq.PosX == p.PosX - 1) && sq.PosY == p.PosY + 1)
                                path.Remove(sq);
                        }
                    }
                    path = tempList;
                }
                else
                {
                    if ((p as PieceManager).Colour != Colour)
                        path = RemoveSquare(path, p.AvailableMove());
                }
            }
            foreach (PieceManager px in Board.Instance.GameBoard)
            {
                if (px.Colour == Colour)
                    path.Remove(px);
            }
            return path;
        }

        private List<IHavePosition> RemoveSquare(List<IHavePosition> path, List<IHavePosition> removingPath)
        {
            List<IHavePosition> returningPath = new List<IHavePosition>();
            returningPath.AddRange(path);
            foreach(IHavePosition p in removingPath)
            {
                foreach(IHavePosition x in path)
                {
                    if (x.IsEqual(p))
                        returningPath.Remove(x);
                }
            }
            return returningPath;
        }

        public List<IHavePosition> AllPath()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            for (int i = _controller.PosX - 1; i <= _controller.PosX + 1; i++)
            {
                for (int j = _controller.PosY - 1; j <= _controller.PosY + 1; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        IHavePosition square = new EmptySquare(i, j);
                        foreach (PieceManager p in Board.Instance.GameBoard)
                        {
                            if (p.IsEqual(square))
                                square = p;
                        }
                        path.Add(square);
                    }
                }
            }
            return path;
        }

        public bool HasMoved
        {
            get { return _hasmoved; }
        }

        public override void Draw()
        { 
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_king_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_king_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
