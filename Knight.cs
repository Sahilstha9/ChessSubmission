using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Knight : Piece, IPieceStrategy
    {
        public Knight(PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
        }

        public override bool Move(int posX, int posY)
        {
            Console.Write(posY);
            foreach (IHavePosition sq in AvailableMove())
            {
                if (sq.PosX == posX && sq.PosY == posY)
                {
                    if (sq is EmptySquare || (sq is Piece && (sq as Piece).Colour != Colour))
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

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (PieceManager sq in Board.Instance.GameBoard)
            {
                if (sq.Colour != Colour)
                {
                    if (sq.Checker)
                        return BlockingPath(path);
                }
            }
            for (int i = _controller.PosX - 2; i <= _controller.PosX + 2; i++)
            {
                for (int j = _controller.PosY - 2; j <= _controller.PosY + 2; j++)
                {
                    if (i == _controller.PosX + 2 || i == _controller.PosX - 2)
                    {
                        if (j == _controller.PosY - 1 || j == _controller.PosY + 1)
                        {
                            if (i >= 0 && i < 8 && j >= 0 && j < 8)
                                path.Add(new EmptySquare(i, j));
                        }
                    }
                    else if (j == _controller.PosY + 2 || j == _controller.PosY - 2)
                    {
                        if (i == _controller.PosX - 1 || i == _controller.PosX + 1)
                        {
                            if (i >= 0 && i < 8 && j >= 0 && j < 8)
                                path.Add(new EmptySquare(i, j));
                        }
                    }
                }
            }
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(path);
            foreach(PieceManager p in Board.Instance.GameBoard)
            {
                foreach(IHavePosition sq in path)
                {
                    if(p.IsEqual(sq))
                    {
                        if (p.Colour == Colour)
                            tempList.Remove(sq);
                    }
                }
            }
            path = tempList;
            if (_controller.Pinned)
                path = new List<IHavePosition>();
            return path;
        }

        public override void Draw()
        {
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wKinghtImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_knight_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bKnightImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_knight_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
