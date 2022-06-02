using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Bishop : Piece, IPieceStrategy
    {
        private Lazy<MoveDiagonal> _moveDiagonal;
        private List<IHavePosition> leftup, rightdown, leftdown, rightup;
        private Return _return;
        public Bishop(PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
            _moveDiagonal = new Lazy<MoveDiagonal>(() =>new MoveDiagonal(_controller));
            _return = new Return();
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            leftup = _moveDiagonal.Value.MoveLeftUp();
            leftdown = _moveDiagonal.Value.MoveLeftDown();
            rightup = _moveDiagonal.Value.MoveRightUp();
            rightdown = _moveDiagonal.Value.MoveRightDown();
            if (!_controller.Pinned)
            {
                path.AddRange(_return.ReturnPath(rightup));
                path.AddRange(_return.ReturnPath(rightdown));
                path.AddRange(_return.ReturnPath(leftup));
                path.AddRange(_return.ReturnPath(leftdown));
            }
            else
            {
                if (leftup.Contains(_controller.Pinner))
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
            }
            foreach (PieceManager p in Board.Instance.GameBoard)
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
            leftup = _moveDiagonal.Value.MoveLeftUp();
            leftdown = _moveDiagonal.Value.MoveLeftDown();
            rightup = _moveDiagonal.Value.MoveRightUp();
            rightdown = _moveDiagonal.Value.MoveRightDown();
            if (leftdown.Contains(king))
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
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wBishopImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_bishop_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bBishopImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_bishop_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
