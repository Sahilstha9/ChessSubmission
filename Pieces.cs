using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public abstract class Piece : IHavePosition
    {
        private int _posX, _posY;
        private bool _colour;
        private bool _selected;
        protected bool _ispinned;
        protected Piece _pinner;
        protected Player _player;
        protected List<Piece> _board;
        private bool _checker;

        public virtual bool Move(int posX, int posY)
        { 
            foreach (IHavePosition p in AvailableMove())
            {
                if(p.PosX == posX && p.PosY == posY)
                {
                    if(p is EmptySquare || (p as Piece).Colour != Colour)
                    {
                        RemovePieceFromBoard(posX, posY);
                        PosX = posX;
                        PosY = posY;
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract List<IHavePosition> AvailableMove();

        public abstract void Draw();

        protected virtual List<IHavePosition> BlockingPath(List<IHavePosition> blockpath)
        {
            Piece p = null;
            List<IHavePosition> blockingpath = new List<IHavePosition>();
            foreach(Piece piece in _board)
            {
                if (piece.Checker)
                    p = piece ;
            }
            foreach (IHavePosition blocker in blockpath)
            {
                foreach(IHavePosition attacker in p.CheckPath(_player.KingPiece))
                {
                    if (blocker.IsEqual(attacker))
                        blockingpath.Add(blocker);
                }
            }
            return blockingpath;
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
            set {_player = value ; }
        }

        protected void RemovePieceFromBoard(int x, int y)
        {
            Piece toRemove = null;
            foreach(Piece p in _board)
            {
                if (p.PosX == x && p.PosY == y)
                    toRemove = p;
            }
            _board.Remove(toRemove);
        }

        protected void DrawAvailableMove()
        {
            foreach(IHavePosition p in AvailableMove())
            {
                if(!(p is EmptySquare))
                {
                    if((p as Piece).Colour != Colour)
                        SplashKit.FillCircle(Color.Red, p.PosX * Constants.Instance.Width + 88, p.PosY * Constants.Instance.Width + 88, 20);
                }
                else
                    SplashKit.FillCircle(Color.Red, p.PosX * Constants.Instance.Width + 88, p.PosY * Constants.Instance.Width + 88, 20);
            }
        }

        public bool IsEqual(IHavePosition x)
        {
            if (PosX == x.PosX && PosY == x.PosY)
                return true;
            return false;
        }

        public virtual List<IHavePosition> CheckPath(IHavePosition king)
        {
            List<IHavePosition> path = new List<IHavePosition>();
            path.Add(this);
            return path;
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

        protected void DrawOutline()
        {
            SplashKit.FillRectangle(Color.RGBAColor(255, 150, 255, 90), PosX * Constants.Instance.Width + 50, PosY * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
        }
    }
}
