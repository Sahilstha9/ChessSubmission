using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Pawn : Piece
    {
        private bool _firstmove;
        public Pawn(bool colour, int posX, int posY, List<Piece> board)
        {
            Colour = colour;
            PosX = posX;
            PosY = posY;
            _firstmove = true;
            _board = board;
        }

        public override bool Move(int posX, int posY)
        {
            IHavePosition ToMove = new EmptySquare(posX, posY);
            foreach (IHavePosition sq in AvailableMove())
            {
                if(sq.PosX == ToMove.PosX && sq.PosY == ToMove.PosY)
                {
                    if (sq is Piece && (sq as Piece).Colour == Colour)
                        return false;
                    else
                    {
                        RemovePieceFromBoard(posX, posY);
                        if(Colour && PosY == 1)
                        {
                            _board.Add(new Queen(Colour, posX, 0, _board));
                            _board.Remove(this);
                            return true;
                        }
                        else if(!Colour && PosY == 6)
                        {
                            _board.Add(new Queen(Colour, posX, 7, _board));
                            _board.Remove(this);
                            return true;
                        }
                        PosX = posX;
                        PosY = posY;
                        _firstmove = false;
                        return true;
                    }
                }
            }
            return false;
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            foreach (Piece p in _board)
            {
                if (p.Colour != Colour)
                {
                    if (p.Checker)
                        return BlockingPath(path);
                }
            }
            IHavePosition onestep, twostep, leftstep, rightstep;
            if(Colour)
            {
                onestep = new EmptySquare(PosX, PosY - 1);
                twostep = new EmptySquare(PosX, PosY - 2);
                leftstep = new EmptySquare(PosX - 1, PosY - 1);
                rightstep = new EmptySquare(PosX + 1, PosY - 1);
            }
            else
            {
                onestep = new EmptySquare(PosX, PosY + 1);
                twostep = new EmptySquare(PosX, PosY + 2);
                leftstep = new EmptySquare(PosX - 1, PosY + 1);
                rightstep = new EmptySquare(PosX + 1, PosY + 1);
            }
            path.Add(onestep);
            if (_firstmove)
                path.Add(twostep);
            foreach (Piece p in _board)
            {
                if (p.IsEqual(onestep))
                {
                    path.Remove(onestep);
                    path.Remove(twostep);
                }
                else if (p.IsEqual(twostep))
                    path.Remove(twostep);
                else if (p.IsEqual(leftstep))
                    path.Add(p);
                else if (p.IsEqual(rightstep))
                    path.Add(p);
            }
            if(_ispinned)
            {
                path = new List<IHavePosition>();
                if (path.Contains(_pinner))
                    path.Add(_pinner);
                return path;
            }
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
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wPawnImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_pawn_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bPawnImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_pawn_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
