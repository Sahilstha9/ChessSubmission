using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class King : Piece
    {
        private bool _hasmoved;
        private bool _gameOver;
        public King(bool colour, int posX, int posY, List<Piece> board)
        {
            Colour = colour;
            PosX = posX;
            PosY = posY;
            _board = board;
            _gameOver = false;
            _hasmoved = false;
        }

        public override bool Move(int posX, int posY)
        {
            IHavePosition toMove = new EmptySquare(posX, posY);
            foreach(Piece p in _board)
            {
                if(p.IsEqual(toMove) && p is Rook && (p as Piece).Colour == Colour)
                {
                    if (Castling(p))
                        return true;
                }
            }
            foreach(IHavePosition p in AvailableMove())
            {
                if(p.IsEqual(toMove))
                {
                     if(p is EmptySquare || (p as Piece).Colour != Colour)
                    {
                        PosX = posX;
                        PosY = posY;
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

        private bool Castling(Piece rook)
        {
            if (_hasmoved)
                return false;
            else
            {
                if (rook.PosX == 0)
                {
                    for (int i = 1; i < PosX; i++)
                    {
                        foreach(Piece p in _board)
                        {
                            if (p.PosY == PosY && p.PosX == i)
                                return false;
                            else if((p as Piece).Colour != Colour)
                            {
                                foreach(IHavePosition x in p.AvailableMove())
                                {
                                    if (x.PosX == i && x.PosY == PosY)
                                        return false;
                                }
                            }
                        }
                    }
                    PosX = 2;
                    rook.PosX = 3;
                    return true;
                }
                else if (rook.PosX == 7)
                {
                    for (int i = PosX + 1; i < 7; i++)
                    {
                        foreach (Piece p in _board)
                        {
                            if (p.PosY == PosY && p.PosX == i)
                                return false;
                            else if ((p as Piece).Colour != Colour)
                            {
                                foreach (IHavePosition x in p.AvailableMove())
                                {
                                    if (x.PosX == i && x.PosY == PosY)
                                        return false;
                                }
                            }
                        }
                    }
                    PosX = 2;
                    rook.PosX = 3;
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
            foreach(Piece p in _board)
            {
                if (p == this)
                    continue;
                else if (p is King)
                    path = RemoveSquare(path, ((King)p).AllPath());
                else if (p is Pawn && (p as Piece).Colour != Colour)
                {
                    List<IHavePosition> tempList = new List<IHavePosition>();
                    tempList.AddRange(path);
                    foreach (IHavePosition sq in path)
                    {
                        if ((p as Piece).Colour)
                        {
                            if ((sq.PosX == p.PosX + 1 || sq.PosX == p.PosX - 1) && sq.PosY == p.PosY - 1)
                                tempList.Remove(sq);
                        }
                        else
                        {
                            if ((sq.PosX == p.PosX + 1 || sq.PosX == p.PosX - 1) && sq.PosY == p.PosY + 1)
                                tempList.Remove(sq);
                        }
                    }
                    path = tempList;
                }
                else
                {
                    if ((p as Piece).Colour != Colour)
                        path = RemoveSquare(path, p.AvailableMove());
                }
            }
            foreach (Piece px in _board)
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
            for (int i = PosX - 1; i <= PosX + 1; i++)
            {
                for (int j = PosY - 1; j <= PosY + 1; j++)
                {
                    if (i >= 0 && i < 8 && j >= 0 && j < 8)
                    {
                        IHavePosition square = new EmptySquare(i, j);
                        foreach (Piece p in _board)
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

        public bool GameOver
        {
            get { return _gameOver; }
        }

        public bool HasMoved
        {
            get { return _hasmoved; }
        }

        public void CheckMate(Piece _checkingPiece)
        {
            _gameOver = true;
            if (AvailableMove().Count > 0)
                _gameOver = false;
            else
            {
                List<IHavePosition> blockablePath = new List<IHavePosition>();
                foreach (Piece p in _player.Pieces)
                    blockablePath.AddRange(p.AvailableMove());
                foreach (IHavePosition attackingPath in _checkingPiece.CheckPath(_player.KingPiece))
                {
                    foreach (IHavePosition blockingPath in blockablePath)
                    {
                        if (!(blockingPath is King))
                        {
                            if (attackingPath.IsEqual(blockingPath))
                            {
                                _gameOver = false;
                            }
                        }
                    }
                }
            }
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
                DrawAvailableMove();
            }
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_king_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_king_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
