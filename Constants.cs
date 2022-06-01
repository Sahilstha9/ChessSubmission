using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Constants
    {
        private static Constants _instance;
        private Constants() { }

        public static Constants Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Constants();
                }
                return _instance;
            }
        }

        public int Width
        {
            get { return 80; }
        }

        public int OffsetValue
        {
            get { return 60; }
        }
    }
}
