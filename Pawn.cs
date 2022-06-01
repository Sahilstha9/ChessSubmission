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
        private MoveStraight _moveStraight;
        private MoveDiagonal _moveDiagonal;
        public Pawn(List<PieceManager> board, PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
            _firstmove = true;
            _board = board;
            _moveStraight = new MoveStraight(_board, _controller);
            _moveDiagonal = new MoveDiagonal(_board, _controller);
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
            List<IHavePosition> pathToUse = new List<IHavePosition>();
            List<IHavePosition> capturePath = new List<IHavePosition>();
            if (Colour)
            {
                pathToUse = _moveStraight.MoveUp();
                if(_moveDiagonal.MoveLeftUp().Count > 0)
                    capturePath.Add(_moveDiagonal.MoveLeftUp()[0]);
                if (_moveDiagonal.MoveRightUp().Count > 0)
                    capturePath.Add(_moveDiagonal.MoveRightUp()[0]);
            }
            else
            {
                pathToUse = _moveStraight.MoveDown();
                if (_moveDiagonal.MoveLeftDown().Count > 0)
                    capturePath.Add(_moveDiagonal.MoveLeftDown()[0]);
                if (_moveDiagonal.MoveRightDown().Count > 0)
                    capturePath.Add(_moveDiagonal.MoveRightDown()[0]);
            }
            if (pathToUse[0] is not PieceManager)
                path.Add(pathToUse[0]);
            if (_firstmove && pathToUse[1] is not PieceManager)
                path.Add(pathToUse[1]);
            foreach(IHavePosition p in capturePath)
            {
                if (p is PieceManager && (p as PieceManager).Colour != Colour)
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
                if (pathToUse.Contains(_controller.Pinner))
                    path.Add(pathToUse[0]);
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
