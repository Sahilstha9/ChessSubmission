using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class CheckerKingPiece : Piece
    {
        private MoveCheckerPiece _move;
        public CheckerKingPiece(bool colour, int posX, int posY, List<Piece> board)
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
            foreach (Piece p in _board)
            {
                if (p.IsEqual(toMove))
                    return false;
            }
            foreach (IHavePosition p in AvailableMove())
            {
                if (p.IsEqual(toMove))
                {
                    foreach (IHavePosition sq in CheckMustMove())
                    {
                        if (sq.IsEqual(p))
                            _move.RemovePiece(posX, posY);
                    }
                    PosX = posX;
                    PosY = posY;
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
            if (PosX - 1 >= 0 && PosY - 1 >= 0)
                path.Add(new EmptySquare(PosX - 1, PosY - 1));
            if (PosX + 1 < 8 && PosY - 1 >= 0)
                path.Add(new EmptySquare(PosX + 1, PosY - 1));
            if (PosX - 1 >= 0 && PosY + 1 < 8)
                path.Add(new EmptySquare(PosX - 1, PosY + 1));
            if (PosX + 1 < 8 && PosY + 1 < 8)
                path.Add(new EmptySquare(PosX + 1, PosY + 1));
            List<IHavePosition> tempList = new List<IHavePosition>();
            tempList.AddRange(path);
            foreach (IHavePosition sq in tempList)
            {
                foreach (Piece p in _board)
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
            path.AddRange(_move.MoveLeftUp(1, PosX - 1, PosY - 1, false));
            path.AddRange(_move.MoveRightUp(1, PosX + 1, PosY - 1, false));
            path.AddRange(_move.MoveLeftDown(1, PosX - 1, PosY + 1, false));
            path.AddRange(_move.MoveRightDown(1, PosX + 1, PosY + 1, false));
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
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/WhiteKing.png"), PosX * Constants.Instance.Width + 50, PosY * Constants.Instance.Width + 50);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bKingImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/KingCheckerPiece.png"), PosX * Constants.Instance.Width + 50, PosY * Constants.Instance.Width + 50);
        }
    }
}
