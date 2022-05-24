using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Clock
    {
        private Counter _mins = new Counter("Minutes", 9);
        private Counter _seconds = new Counter("Seconds", 55);

        public Clock()
        {

        }

        private void MinsDecrement()
        {
            _mins.Decrement();
        }

        public void ClockDecrement()
        {
            _seconds.Decrement();
            if (_seconds.Ticks < 0)
            {
                _seconds.Reset();
            }
        }

        public void Reset()
        {
            _seconds.Reset();
        }

        public string Display()
        {
            string toReturn = String.Format("{0}:{1}", _mins.Ticks.ToString("D2"), _seconds.Ticks.ToString("D2"));
            return toReturn;
        }
    }
}
