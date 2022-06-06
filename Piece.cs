using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SplashKitSDK;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Piece
    {
        private int _posX, _posY;
        private bool _colour, _ispinned, _checked;
        private Square[,] _board;
        private Piece _thisPiece, _pinner;
        private Player _player;
        public Pieces(bool colour, int posX, int posY, Square[,] board, PieceType piece)
        {
            _colour = colour;
            _posX = posX;
            _posY = posY;
            _board = board;
            _thisPiece = ReuturnPiece(piece);
        }

        public List<Square> AvailableMove()
        {
            return _thisPiece.AvailableMove();
        }

        public bool Move(int posX, int posY)
        {
            return _thisPiece.Move(posX, posY);
        }

        public void Draw()
        {
            _thisPiece.Draw();
        }

        public Piece Pinner
        {
            set { _pinner = value; }
        }

        public bool Pinned
        {
            set { _ispinned = value; }
        }

        public Player Player
        {
            set { _player = value; }
        }

        public bool Checker
        {
            get { return _checke; }
            set { _checker = value; }
        }

        protected void DrawAvailableMove()
        {
            foreach (Square p in AvailableMove())
            {
                if (!(p is EmptySquare))
                {
                    if (p.Colour != Colour)
                        SplashKit.FillCircle(Color.Red, p.PosX * 80 + 88, p.PosY * 80 + 88, 20);
                }
                else
                    SplashKit.FillCircle(Color.Red, p.PosX * 80 + 88, p.PosY * 80 + 88, 20);
            }
        }

        private Piece ReuturnPiece(PieceType p)
        {
            switch(p)
            {
                case PieceType.Pawn:
                    return new Pawn(_colour, _posX, _posY, _board);
                    break;
                case PieceType.Rook:
                    return new Rook(_colour, _posX, _posY, _board);
                    break;
                case PieceType.Bishop:
                    return new Bishop(_colour, _posX, _posY, _board);
                    break;
                case PieceType.Knight:
                    return new Knight(_colour, _posX, _posY, _board);
                    break;
                case PieceType.Queen:
                    return new Queen(_colour, _posX, _posY, _board);
                    break;
                default:
                    return new Pawn(_colour, _posX, _posY, _board); ;
            }
        }
    }
}
