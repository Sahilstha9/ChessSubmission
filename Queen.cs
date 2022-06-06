using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
     public class Queen : Piece
     {
        private MoveDiagonal _moveDiagonal;
        private MoveStraight _moveStraight;
        private List<IHavePosition> up, down, left, right, leftup, rightdown, leftdown, rightup;
        private Return _return;

        public Queen(bool colour, int posX, int posY, List<Piece> board)
        {
            Colour = colour;
            PosX = posX;
            PosY = posY;
            _board = board;
            _moveDiagonal = new MoveDiagonal(_board, this);
            _moveStraight = new MoveStraight(_board, this);
            _return = new Return();
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            up = _moveStraight.MoveUp();
            down = _moveStraight.MoveDown();
            left = _moveStraight.MoveLeft();
            right = _moveStraight.MoveRight();
            leftup = _moveDiagonal.MoveLeftUp();
            leftdown = _moveDiagonal.MoveLeftDown();
            rightup = _moveDiagonal.MoveRightUp();
            rightdown = _moveDiagonal.MoveRightDown();
            if (!_ispinned)
            {
                path.AddRange(_return.ReturnPath(up));
                path.AddRange(_return.ReturnPath(left));
                path.AddRange(_return.ReturnPath(right));
                path.AddRange(_return.ReturnPath(down));
                path.AddRange(_return.ReturnPath(rightup));
                path.AddRange(_return.ReturnPath(rightdown));
                path.AddRange(_return.ReturnPath(leftup));
                path.AddRange(_return.ReturnPath(leftdown));
            }
            else
            {
                if (up.Contains(_pinner))
                {
                    path.AddRange(up.GetRange(0, up.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(down));
                }
                else if (down.Contains(_pinner))
                {
                    path.AddRange(down.GetRange(0, down.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(up));
                }
                else if (right.Contains(_pinner))
                {
                    path.AddRange(right.GetRange(0, right.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(left));
                }
                else if (left.Contains(_pinner))
                {
                    path.AddRange(left.GetRange(0, left.IndexOf(_pinner) + 1));
                    path.AddRange(_return.ReturnPath(right));
                }
                else if (leftup.Contains(_pinner))
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
                path.AddRange(_return.ReturnPath(right));
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
            up = _moveStraight.MoveUp();
            down = _moveStraight.MoveDown();
            left = _moveStraight.MoveLeft();
            right = _moveStraight.MoveRight();
            leftup = _moveDiagonal.MoveLeftUp();
            leftdown = _moveDiagonal.MoveLeftDown();
            rightup = _moveDiagonal.MoveRightUp();
            rightdown = _moveDiagonal.MoveRightDown();
            if (up.Contains(king))
                path.AddRange(up.GetRange(0, up.IndexOf(king)));
            else if (down.Contains(king))
                path.AddRange(down.GetRange(0, down.IndexOf(king)));
            else if (left.Contains(king))
                path.AddRange(left.GetRange(0, left.IndexOf(king)));
            else if (right.Contains(king))
                path.AddRange(right.GetRange(0, right.IndexOf(king)));
            else if (leftdown.Contains(king))
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
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wQueenImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_queen_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bQueenImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_queen_png_shadow_128px.png"), PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
