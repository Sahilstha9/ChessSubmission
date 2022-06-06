using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Knight : Piece
    {
        public Knight(bool colour, int posX, int posY, List<Piece> board)
        {
            Colour = colour;
            PosX = posX;
            PosY = posY;
            _board = board;
        }

        public override bool Move(int posX, int posY)
        {
            foreach(IHavePosition sq in AvailableMove())
            {
                if(sq.PosX == posX && sq.PosY == posY)
                {
                    if(sq is EmptySquare || (sq is Piece && (sq as Piece).Colour != Colour))
                    {
                        RemovePieceFromBoard(posX, posY);
                        PosX = posX;
                        PosY = posY;
                        return true;
                    }
                }
            }
            return false;
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (Piece sq in _board)
            {
                if (sq.Colour != Colour)
                {
                    if (sq.Checker)
                        return BlockingPath(path);
                }
            }
            for (int i = PosX - 2; i <= PosX + 2; i++)
            {
                for (int j = PosY - 2; j <= PosY + 2; j++)
                {
                    if (i == PosX + 2 || i == PosX - 2)
                    {
                        if (j == PosY - 1 || j == PosY + 1)
                        {
                            if (i >= 0 && i < 8 && j >= 0 && j < 8)
                                path.Add(new EmptySquare(i, j));
                        }
                    }
                    else if (j == PosY + 2 || j == PosY - 2)
                    {
                        if (i == PosX - 1 || i == PosX + 1)
                        {
                            if (i >= 0 && i < 8 && j >= 0 && j < 8)
                                path.Add(new EmptySquare(i, j));
                        }
                    }
                }
            }
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(path);
            foreach(Piece p in _board)
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
            if (_ispinned)
                path = new List<IHavePosition>();
            return path;
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
                DrawAvailableMove();
            }
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wKinghtImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_knight_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bKnightImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_knight_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
