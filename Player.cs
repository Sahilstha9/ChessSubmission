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
        private Lazy<PieceManager> _king;
        private List<PieceManager> _pieces;
        private Lazy<Clock> _myClock;
        Timer time;

        public Player(bool colour, bool turn, List<PieceManager> board)
        {
            _myClock = new Lazy<Clock>(() => new Clock());
            _pieces = new List<PieceManager>();
            _colour = colour;
            _pieces = board;
            InitTimer();
            _myturn = turn;
        }

        public bool MovePiece(PieceManager p, int x , int y)
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

        public void UpdatePieces(List<PieceManager> board)
        {
            _pieces = new List<PieceManager>();
            foreach (PieceManager p in board)
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
            if (_myClock.Value.Display() == "00:00")
                return;
            if(_myturn)
                _myClock.Value.ClockDecrement();
        }


        public bool Colour
        {
            get { return _colour; }
        }

        public List<PieceManager> Pieces
        {
            get { return _pieces; }
        }

        public bool Turn
        {
            get
            { return _myturn; }
            set
            { _myturn = value; }
        }

        public string Clock
        {
            get { return _myClock.Value.Display();}
        }

        public PieceManager KingPiece
        {
            get 
            {
                foreach (PieceManager p in _pieces)
                {
                    if (p.Piece is King)
                        _king = new Lazy<PieceManager>(() => p);
                    p.Player = this;
                }
                return _king.Value;
            }
        }
    }
}
