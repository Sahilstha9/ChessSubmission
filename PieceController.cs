using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class PieceManager : IHavePosition
    {
        private int _posX, _posY;
        private bool _ispinned, _selected, _colour, _gameOver, _checker;
        private Lazy<PieceManager> _pinner;
        private Player _player;
        private List<PieceManager> _board;
        private IPieceStrategy _piece;
        private PieceFactory _pieceCreator;

        public PieceManager(int posX, int posY, bool colour, List<PieceManager> board, PieceType piece)
        {
            _posX = posX;
            _posY = posY;
            _colour = colour;
            _board = board;
            _gameOver = false;
            _pinner = new Lazy<PieceManager>();
            _pieceCreator = new PieceFactory();
            _piece = _pieceCreator.CreatePiece(piece, _board, this);
        }

        public bool Move(int posX, int posY) => _piece.Move(posX, posY);

        public List<IHavePosition> AvailableMove() => _piece.AvailableMove();

        public void Draw()
        {
            if(_selected)
            {
                DrawAvailableMove();
                DrawOutline();
            }
            _piece.Draw();
        }

        private void DrawOutline()
        {
            SplashKit.FillRectangle(Color.RGBAColor(255, 150, 255, 90), PosX * Constants.Instance.Width + 50, PosY * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
        }

        private void DrawAvailableMove()
        {
            foreach (IHavePosition p in AvailableMove())
            {
                if (!(p is EmptySquare))
                {
                    if ((p as PieceManager).Colour != Colour)
                        SplashKit.FillCircle(Color.Red, p.PosX * Constants.Instance.Width + 88, p.PosY * Constants.Instance.Width + 88, 20);
                }
                else
                    SplashKit.FillCircle(Color.Red, p.PosX * Constants.Instance.Width + 88, p.PosY * Constants.Instance.Width + 88, 20);
            }
        }

        public List<IHavePosition> CheckPath(PieceManager king) => _piece.CheckPath(king);

        public void CheckMate(PieceManager _checkingPiece)
        {
            if(_piece is King)
            {
                _gameOver = true;
                if (AvailableMove().Count > 0)
                    _gameOver = false;
                else
                {
                    List<IHavePosition> blockablePath = new List<IHavePosition>();
                    foreach (PieceManager p in _player.Pieces)
                        blockablePath.AddRange(p.AvailableMove());
                    foreach (IHavePosition attackingPath in _checkingPiece.CheckPath(_player.KingPiece))
                    {
                        foreach (IHavePosition blockingPath in blockablePath)
                        {
                            if (!(blockingPath is King))
                            {
                                if (attackingPath.IsEqual(blockingPath))
                                {
                                    _gameOver = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public IPieceStrategy Piece
        {
            get { return _piece; }
        }

        public void Promotion()
        {
            _piece = _pieceCreator.CreatePiece(Board.Instance.PromotionPiece, _board, this);
        }

        public PieceManager Pinner
        {
            set { _pinner = new Lazy<PieceManager>(() => value) ; }
            get { return _pinner.Value; }
        }

        public bool Pinned
        {
            set { _ispinned = value; }
            get { return _ispinned; }
        }

        public Player Player
        {
            set { _player = value; }
            get { return _player; }
        }

        public bool IsEqual(IHavePosition x)
        {
            if (PosX == x.PosX && PosY == x.PosY)
                return true;
            return false;
        }

        public int PosX
        {
            get { return _posX; }
            set { _posX = value; }
        }

        public int PosY
        {
            get { return _posY; }
            set { _posY = value; }
        }

        public bool Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public bool Checker
        {
            get { return _checker; }
            set { _checker = value; }
        }

        public bool IsAt(Point2D pt)
        {
            if (pt.X >= PosX * Constants.Instance.Width + 50 && pt.Y >= PosY * Constants.Instance.Width + 50 && pt.X <= (PosX * Constants.Instance.Width + 130) && pt.Y <= (PosY * Constants.Instance.Width + 130))
                return true;
            else
                return false;
        }

        public bool GameOver
        {
            get { return _gameOver; }
        }
    }
}
