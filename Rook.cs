using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Rook : Piece, IPieceStrategy
    {
        private Lazy<MoveStraight> _moveStraight;
        List<IHavePosition> up, down, left, right;
        private Return _return;
        public Rook(List<PieceManager> board, PieceManager controller)
        {
            _controller = controller;
            Colour = _controller.Colour;
            _board = board;
            _moveStraight= new Lazy<MoveStraight>(() => new MoveStraight(_board, _controller));
            _return = new Return();
        }

        public override List<IHavePosition> AvailableMove()
        {
            List<IHavePosition> path = new List<IHavePosition>();
            up = _moveStraight.Value.MoveUp();
            down = _moveStraight.Value.MoveDown();
            left = _moveStraight.Value.MoveLeft();
            right = _moveStraight.Value.MoveRight();
            if (!_controller.Pinned)
            {
                path.AddRange(_return.ReturnPath(up));
                path.AddRange(_return.ReturnPath(left));
                path.AddRange(_return.ReturnPath(right));
                path.AddRange(_return.ReturnPath(down));
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
            if (up.Contains(king))
                path.AddRange(up.GetRange(0, up.IndexOf(king)));
            else if (down.Contains(king))
                path.AddRange(down.GetRange(0, down.IndexOf(king)));
            else if (left.Contains(king))
                path.AddRange(left.GetRange(0, left.IndexOf(king)));
            else if (right.Contains(king))
                path.AddRange(right.GetRange(0, right.IndexOf(king)));
            path.Add(_controller);
            return path;
        }

        public override void Draw()
        {
            if (Colour)
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("wRookImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/w_rook_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
            else
                SplashKit.DrawBitmap(SplashKit.LoadBitmap("bRookImage", "E:/C#/cs/ChessGame/ChessGame/Resources/Images/b_rook_png_shadow_128px.png"), _controller.PosX * Constants.Instance.Width + Constants.Instance.OffsetValue, _controller.PosY * Constants.Instance.Width + Constants.Instance.OffsetValue);
        }
    }
}
