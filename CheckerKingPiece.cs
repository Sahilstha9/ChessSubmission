using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class CheckerKingPiece : Piece, IPieceStrategy
    {
        private MoveCheckerPiece _move;
        public CheckerKingPiece(PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
            _move = new MoveCheckerPiece(_controller);
        }

        public override bool Move(int posX, int posY)
        {
            IHavePosition toMove = new EmptySquare(posX, posY);
            foreach (PieceManager p in Board.Instance.GameBoard)
            {
                if (p.IsEqual(toMove))
                    return false;
            }
            foreach (IHavePosition p in AvailableMove())
            {
                if (p.IsEqual(toMove))
                {
                    Board.Instance.Store();
                    foreach (IHavePosition sq in CheckMustMove())
                    {
                        if (sq.IsEqual(p))
                            _move.RemovePiece(posX, posY);
                    }
                    _controller.PosX = posX;
                    _controller.PosY = posY;
                    return true;
                }
            }
            return false;
        }



        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            if (CheckMustMove().Count > 0)
                return (CheckMustMove());
            if (_controller.PosX - 1 >= 0 && _controller.PosY - 1 >= 0)
                path.Add(new EmptySquare(_controller.PosX - 1, _controller.PosY - 1));
            if (_controller.PosX + 1 < 8 && _controller.PosY - 1 >= 0)
                path.Add(new EmptySquare(_controller.PosX + 1, _controller.PosY - 1));
            if (_controller.PosX - 1 >= 0 && _controller.PosY + 1 < 8)
                path.Add(new EmptySquare(_controller.PosX - 1, _controller.PosY + 1));
            if (_controller.PosX + 1 < 8 && _controller.PosY + 1 < 8)
                path.Add(new EmptySquare(_controller.PosX + 1, _controller.PosY + 1));
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(path);
            foreach (IHavePosition sq in tempList)
            {
                foreach (PieceManager p in Board.Instance.GameBoard)
                {
                    if (p.IsEqual(sq))
                        path.Remove(sq);
                }
            }
            return path;
        }

        public List<IHavePosition> CheckMustMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            path.AddRange(_move.MoveLeftUp(1, _controller.PosX - 1, _controller.PosY - 1, false));
            path.AddRange(_move.MoveRightUp(1, _controller.PosX + 1, _controller.PosY - 1, false));
            path.AddRange(_move.MoveLeftDown(1, _controller.PosX - 1, _controller.PosY + 1, false));
            path.AddRange(_move.MoveRightDown(1, _controller.PosX + 1, _controller.PosY + 1, false));
            return path;
        }

        public override void Draw()
        {
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/WhiteKing.png"), _controller.PosX * Constants.Instance.Width + 50, _controller.PosY * Constants.Instance.Width + 50);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/KingCheckerPiece.png"), _controller.PosX * Constants.Instance.Width + 50, _controller.PosY * Constants.Instance.Width + 50);
        }
    }
}
