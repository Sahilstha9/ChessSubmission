using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class CheckerPiece : Piece
    {
        private MoveCheckerPiece _move;
        public CheckerPiece(bool colour, int posX, int posY, List<Piece> board)
        {
            Colour = colour;
            PosX = posX;
            PosY = posY;
            _board = board;
            _move = new MoveCheckerPiece(_board, this);
        }

        public override bool Move(int posX, int posY)
        {
            IHavePosition toMove = new EmptySquare(posX, posY);
            foreach(Piece p in _board)
            {
                if (p.IsEqual(toMove))
                    return false;
            }
            foreach (IHavePosition p in AvailableMove())
            {
                if (p.IsEqual(toMove))
                {
                    foreach(IHavePosition sq in CheckMustMove())
                    {
                        if (sq.IsEqual(p))
                            _move.RemovePiece(posX, posY);
                    }
                    if (Colour && posY == 0)
                    {
                        _board.Add(new CheckerKingPiece(Colour, posX, posY, _board));
                        _board.Remove(this);
                    }
                    else if (!Colour && posY == 7)
                    {
                        _board.Add(new CheckerKingPiece(Colour, posX, posY, _board));
                        _board.Remove(this);
                    }
                    else
                    {
                        PosX = posX;
                        PosY = posY;
                    }
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
                if (PosX - 1 >= 0 && PosY - 1 >= 0)
                    path.Add(new EmptySquare(PosX - 1, PosY - 1));
                if(PosX  + 1 < 8 && PosY - 1 >= 0)
                    path.Add(new EmptySquare(PosX + 1, PosY - 1));
            }
            else
            {
                if (PosX - 1 >= 0 && PosY + 1 < 8)
                    path.Add(new EmptySquare(PosX - 1, PosY + 1));
                if(PosX + 1 < 8 && PosY + 1 < 8)
                    path.Add(new EmptySquare(PosX + 1, PosY + 1));
            }
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(path);
            foreach(IHavePosition sq in tempList)
            {
                foreach(Piece p in _board)
                {
                    if (p.IsEqual(sq))
                        path.Remove(sq);
                }
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
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wCheckerImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/WhitePiece.png"), PosX * Constants.Instance.Width + 50, PosY * Constants.Instance.Width + 50);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bCheckerImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/BlackCheckerPiece.png"), PosX * Constants.Instance.Width + 50, PosY * Constants.Instance.Width + 50);
        }

        private List<IHavePosition> CheckMustMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            if (Colour)
            {
                path.AddRange(_move.MoveLeftUp(1, PosX -1, PosY - 1, false));
                path.AddRange(_move.MoveRightUp(1, PosX + 1, PosY - 1, false));
            }
            else
            {
                path.AddRange(_move.MoveLeftDown(1, PosX - 1, PosY + 1, false));
                path.AddRange(_move.MoveRightDown(1, PosX + 1, PosY + 1, false));
            }
            return path;
        }

        
    }
}
