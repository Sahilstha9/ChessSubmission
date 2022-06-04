using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public abstract class Piece : IPieceStrategy
    {
        private bool _colour;
        protected PieceManager _controller;

        public virtual bool Move(int posX, int posY)
        {
            foreach (IHavePosition p in AvailableMove())
            {
                if(p.PosX == posX && p.PosY == posY)
                {
                    if(p is EmptySquare || (p as PieceManager).Colour != _controller.Colour)
                    {
                        Board.Instance.Store();
                        RemovePieceFromBoard(posX, posY);
                        _controller.PosX = posX;
                        _controller.PosY = posY;
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
            PieceManager p = null;
            List<IHavePosition> blockingpath = new List<IHavePosition>();
            foreach (PieceManager piece in Board.Instance.GameBoard)
            {
                if (piece.Checker)
                    p = piece ;
            }
            foreach (IHavePosition blocker in blockpath)
            {
                foreach(IHavePosition attacker in p.CheckPath(_controller.Player.KingPiece))
                {
                    if (blocker.IsEqual(attacker))
                        blockingpath.Add(blocker);
                }
            }
            return blockingpath;
        }

        protected void RemovePieceFromBoard(int x, int y)
        {
            PieceManager toRemove = null;
            foreach(PieceManager p in Board.Instance.GameBoard)
            {
                if (p.PosX == x && p.PosY == y)
                    toRemove = p;
            }
            Board.Instance.GameBoard.Remove(toRemove);
        }

        public virtual List<IHavePosition> CheckPath(IHavePosition king)
        {
            List<IHavePosition> path = new List<IHavePosition>();
            path.Add(_controller);
            return path;
        }

        public bool Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }
    }
}
