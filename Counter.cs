using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Counter
    {
        private int _count;
        private string _name;
        private int _startVal;

        public Counter(string name, int value)
        {
            _name = name;
            _count = value;
            _startVal = value;
        }

        public int Ticks
        {
            get { return _count; }
        }

        public void Decrement()
        {
            _count--;
        }

        public void Reset()
        {
            _count = _startVal;
        }
    }
}
