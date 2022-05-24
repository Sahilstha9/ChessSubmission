using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace ChessGame
{
    public class Player
    {
        private bool _colour;
        private bool _myturn;
        private Piece _king;
        private List<Piece> _pieces;
        private Clock _myClock;
        Timer time;

        public Player(bool colour, bool turn, List<Piece> board)
        {
            _myClock = new Clock();
            _pieces = new List<Piece>();
            _colour = colour;
            _pieces = board;
            foreach (Piece p in _pieces)
            {
                if (p is King)
                    _king = p;
                p.Player = this;
            }
            InitTimer();
            _myturn = turn;
        }

        public bool MovePiece(Piece p, int x , int y)
        {
            if (_myturn)
                return p.Move(x, y);
            return false;

        }

        public void InitTimer()
        {
            time = new Timer(1010);
            time.AutoReset = true;
            time.Elapsed += new System.Timers.ElapsedEventHandler(UpdateClock);
            time.Start();
        }

        public void UpdatePieces(List<Piece> board)
        {
            _pieces = new List<Piece>();
            foreach (Piece p in board)
            {
                if (p.Colour == _colour)
                {
                    _pieces.Add(p);
                    p.Player = this;
                }
            }    
        }

        public void UpdateClock(object o, System.Timers.ElapsedEventArgs e)
        {
            if (_myClock.Display() == "00:00")
                return;
            if(_myturn)
                _myClock.ClockDecrement();
        }


        public bool Colour
        {
            get { return _colour; }
        }

        public List<Piece> Pieces
        {
            get { return _pieces; }
        }

        public bool Turn
        {
            get
            {
                return _myturn;
            }
            set
            {
                _myturn = value;
            }
        }

        public string Clock
        {
            get { return _myClock.Display(); }
        }

        public King KingPiece
        {
            get { return (King)_king; }
        }
    }
}
