using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class BoardStorage
    {
        private List<List<PieceManager>> _boardList;
        private int i;

        public BoardStorage()
        {
            _boardList = new List<List<PieceManager>>();
            i = 0;
        }

        public void Store()
        {
            List<PieceManager> _board = new List<PieceManager>();
            foreach(PieceManager p in Board.Instance.GameBoard)
            {
                _board.Add(p.Clone());
            }
            if(_boardList.Count > 4)
                _boardList.Remove(_boardList.First());
            _boardList.Add(_board);
            i = _boardList.Count;
        }

        public List<PieceManager> ReturnUndoBoard(Player white, Player black)
        {
            if(i > 0)
            {
                i--;
                white.Turn = !white.Turn;
                black.Turn = !black.Turn;
                return _boardList[i];
            }
            return Board.Instance.GameBoard;
        }

        public List<PieceManager> ReturnRedoBoard(Player white, Player black)
        {
            if (i < _boardList.Count)
            {
                i++;
                white.Turn = !white.Turn;
                black.Turn = !black.Turn;
                return _boardList[i];
            }
            return Board.Instance.GameBoard;
        }
    }
}
