using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Bishop : Piece
    {
        private MoveDiagonal _moveDiagonal;
        private List<IHavePosition> leftup, rightdown, leftdown, rightup;
        private Return _return;
        public Bishop(bool colour, int posX, int posY, List<Piece> board)
        {
            Colour = colour;
            PosX = posX;
            PosY = posY;
            _board = board;
            _moveDiagonal = new MoveDiagonal(board, this);
            _return = new Return();
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            leftup = _moveDiagonal.MoveLeftUp();
            leftdown = _moveDiagonal.MoveLeftDown();
            rightup = _moveDiagonal.MoveRightUp();
            rightdown = _moveDiagonal.MoveRightDown();
            if (!_ispinned)
            {
                path.AddRange(_return.ReturnPath(rightup));
                path.AddRange(_return.ReturnPath(rightdown));
                path.AddRange(_return.ReturnPath(leftup));
                path.AddRange(_return.ReturnPath(leftdown));
            }
            else
            {
                if (leftup.Contains(_pinner))
                {
                    path.AddRange(leftup.GetRange(0, leftup.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(rightdown));
                }
                else if (rightdown.Contains(_pinner))
                {
                    path.AddRange(rightdown.GetRange(0, rightdown.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(leftup));
                }
                else if (rightup.Contains(_pinner))
                {
                    path.AddRange(rightup.GetRange(0, rightup.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(leftdown));
                }
                else if (leftdown.Contains(_pinner))
                {
                    path.AddRange(leftdown.GetRange(0, leftdown.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(rightup));
                }
            }
            foreach (Piece p in _board)
            {
                if (p.Colour != Colour)
                {
                    if (p.Checker)
                        return BlockingPath(path);
                }
            }
            return path;
        }

        public override List<IHavePosition> CheckPath(IHavePosition king)
        {
            List<IHavePosition> path = new List<IHavePosition>();
            leftup = _moveDiagonal.MoveLeftUp();
            leftdown = _moveDiagonal.MoveLeftDown();
            rightup = _moveDiagonal.MoveRightUp();
            rightdown = _moveDiagonal.MoveRightDown();
            if (leftdown.Contains(king))
                path.AddRange(leftdown.GetRange(0, leftdown.IndexOf(king)));
            else if (leftup.Contains(king))
                path.AddRange(leftup.GetRange(0, leftup.IndexOf(king)));
            else if (rightdown.Contains(king))
                path.AddRange(rightdown.GetRange(0, rightdown.IndexOf(king)));
            else if (rightup.Contains(king))
                path.AddRange(rightup.GetRange(0, rightup.IndexOf(king)));
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
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wBishopImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_bishop_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bBishopImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_bishop_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
