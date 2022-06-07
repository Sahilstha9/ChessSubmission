using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class CheckerPiece : Piece, IPieceStrategy
    {
        private Lazy<MoveCheckerPiece> _move;
        public CheckerPiece(PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
            _move = new Lazy<MoveCheckerPiece>(() => new MoveCheckerPiece(_controller));
        }

        public override bool Move(int posX, int posY)
        {
            IHavePosition toMove = new EmptySquare(posX, posY);
            foreach(PieceManager p in Board.Instance.GameBoard)
            {
                if (p.IsEqual(toMove))
                    return false;
            }
            foreach (IHavePosition p in AvailableMove())
            {
                if (p.IsEqual(toMove))
                {
                    Board.Instance.Store();
                    foreach(IHavePosition sq in CheckMustMove())
                    {
                        if (sq.IsEqual(p))
                            _move.Value.RemovePiece(posX, posY);
                    }
                    if (Colour && posY == 0)
                        _controller.Promotion();

                    else if (!Colour && posY == 7)
                        _controller.Promotion();
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
            if(Colour)
            {
                if (_controller.PosX - 1 >= 0 && _controller.PosY - 1 >= 0)
                    path.Add(new EmptySquare(_controller.PosX - 1, _controller.PosY - 1));
                if(_controller.PosX  + 1 < 8 && _controller.PosY - 1 >= 0)
                    path.Add(new EmptySquare(_controller.PosX + 1, _controller.PosY - 1));
            }
            else
            {
                if (_controller.PosX - 1 >= 0 && _controller.PosY + 1 < 8)
                    path.Add(new EmptySquare(_controller.PosX - 1, _controller.PosY + 1));
                if(_controller.PosX + 1 < 8 && _controller.PosY + 1 < 8)
                    path.Add(new EmptySquare(_controller.PosX + 1, _controller.PosY + 1));
            }
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(path);
            foreach(IHavePosition sq in tempList)
            {
                foreach(PieceManager p in Board.Instance.GameBoard)
                {
                    if (p.IsEqual(sq))
                        path.Remove(sq);
                }
            }
            return path;
        }

        public override void Draw()
        {
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wCheckerImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/WhitePiece.png"), _controller.PosX * Constants.Instance.Width + 50, _controller.PosY * Constants.Instance.Width + 50);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bCheckerImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/BlackCheckerPiece.png"), _controller.PosX * Constants.Instance.Width + 50, _controller.PosY * Constants.Instance.Width + 50);
        }

        private List<IHavePosition> CheckMustMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            if (Colour)
            {
                path.AddRange(_move.Value.MoveLeftUp(1, _controller.PosX -1, _controller.PosY - 1, false));
                path.AddRange(_move.Value.MoveRightUp(1, _controller.PosX + 1, _controller.PosY - 1, false));
            }
            else
            {
                path.AddRange(_move.Value.MoveLeftDown(1, _controller.PosX - 1, _controller.PosY + 1, false));
                path.AddRange(_move.Value.MoveRightDown(1, _controller.PosX + 1, _controller.PosY + 1, false));
            }
            return path;
        }

        
    }
}
