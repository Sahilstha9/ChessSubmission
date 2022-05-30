using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
     public class Queen : Piece, IPieceStrategy
    {
        private Lazy<MoveDiagonal> _moveDiagonal;
        private Lazy<MoveStraight> _moveStraight;
        private List<IHavePosition> up, down, left, right, leftup, rightdown, leftdown, rightup;
        private Return _return;

        public Queen(List<PieceManager> board, PieceManager controller)
        {
            _controller = controller;
            Colour = controller.Colour;
            _board = board;
            _moveDiagonal = new Lazy<MoveDiagonal>(() => new MoveDiagonal(_board, _controller));
            _moveStraight = new Lazy<MoveStraight>(() => new MoveStraight(_board, _controller));
            _return = new Return();
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            up = _moveStraight.Value.MoveUp();
            down = _moveStraight.Value.MoveDown();
            left = _moveStraight.Value.MoveLeft();
            right = _moveStraight.Value.MoveRight();
            leftup = _moveDiagonal.Value.MoveLeftUp();
            leftdown = _moveDiagonal.Value.MoveLeftDown();
            rightup = _moveDiagonal.Value.MoveRightUp();
            rightdown = _moveDiagonal.Value.MoveRightDown();
            if (!_controller.Pinned)
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
                if (up.Contains(_controller.Pinner))
                {
                    path.AddRange(up.GetRange(0, up.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(down));
                }
                else if (down.Contains(_controller.Pinner))
                {
                    path.AddRange(down.GetRange(0, down.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(up));
                }
                else if (right.Contains(_controller.Pinner))
                {
                    path.AddRange(right.GetRange(0, right.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(left));
                }
                else if (left.Contains(_controller.Pinner))
                {
                    path.AddRange(left.GetRange(0, left.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(right));
                }
                else if (leftup.Contains(_controller.Pinner))
                {
                    path.AddRange(leftup.GetRange(0, leftup.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(rightdown));
                }
                else if (rightdown.Contains(_controller.Pinner))
                {
                    path.AddRange(rightdown.GetRange(0, rightdown.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(leftup));
                }
                else if (rightup.Contains(_controller.Pinner))
                {
                    path.AddRange(rightup.GetRange(0, rightup.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(leftdown));
                }
                else if (leftdown.Contains(_controller.Pinner))
                {
                    path.AddRange(leftdown.GetRange(0, leftdown.IndexOf(_controller.Pinner) + 1));
                    path.AddRange(_return.ReturnPath(rightup));
                }
                path.AddRange(_return.ReturnPath(right));
            }
            foreach (PieceManager p in _board)
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
            up = _moveStraight.Value.MoveUp();
            down = _moveStraight.Value.MoveDown();
            left = _moveStraight.Value.MoveLeft();
            right = _moveStraight.Value.MoveRight();
            leftup = _moveDiagonal.Value.MoveLeftUp();
            leftdown = _moveDiagonal.Value.MoveLeftDown();
            rightup = _moveDiagonal.Value.MoveRightUp();
            rightdown = _moveDiagonal.Value.MoveRightDown();
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
            path.Add(_controller);
            return path;
        }

        public override void Draw()
        {
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wQueenImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_queen_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bQueenImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_queen_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
