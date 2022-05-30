using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Pawn : Piece, IPieceStrategy
    {
        private bool _firstmove;
        public Pawn(List<PieceManager> board, PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
            _firstmove = true;
            _board = board;
        }

        public override bool Move(int posX, int posY)
        {
            IHavePosition ToMove = new EmptySquare(posX, posY);
            foreach (IHavePosition sq in AvailableMove())
            {
                if(sq.IsEqual(ToMove))
                {
                    if (sq is PieceManager && (sq as PieceManager).Colour == Colour)
                        return false;
                    else
                    {
                        RemovePieceFromBoard(posX, posY);
                        if (Colour && _controller.PosY == 1)
                            _controller.Promotion();

                        else if (!Colour && _controller.PosY == 6)
                            _controller.Promotion();

                        _controller.PosX = posX;
                        _controller.PosY = posY;
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
            IHavePosition onestep, twostep, leftstep, rightstep;
            if(Colour)
            {
                onestep = new EmptySquare(_controller.PosX, _controller.PosY - 1);
                twostep = new EmptySquare(_controller.PosX, _controller.PosY - 2);
                leftstep = new EmptySquare(_controller.PosX - 1, _controller.PosY - 1);
                rightstep = new EmptySquare(_controller.PosX + 1, _controller.PosY - 1);
            }
            else
            {
                onestep = new EmptySquare(_controller.PosX, _controller.PosY + 1);
                twostep = new EmptySquare(_controller.PosX, _controller.PosY + 2);
                leftstep = new EmptySquare(_controller.PosX - 1, _controller.PosY + 1);
                rightstep = new EmptySquare(_controller.PosX + 1, _controller.PosY + 1);
            }
            path.Add(onestep);
            if (_firstmove)
                path.Add(twostep);
            foreach (PieceManager p in _board)
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
            if(_controller.Pinned)
            {
                if (path.Contains(_controller.Pinner))
                {
                    path = new List<IHavePosition>();
                    path.Add(_controller.Pinner);
                }
                else
                    path = new List<IHavePosition>();
                return path;
            }
            foreach(PieceManager p in _board)
            {
                if (p.Checker && p.Colour != Colour)
                    return BlockingPath(path);
            }
             return path;
        }

        public override void Draw()
        {
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wPawnImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_pawn_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bPawnImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_pawn_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
