using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{
    public class Board
    {
        private List<PieceManager> _board;
        private PieceManager _checkingPiece;
        private Player wP, bP;
        private bool _gameOver;
        private Game _gameType;
        private static Board _instance;
        private PieceType _promotionPiece;
        private BoardDrawManager _drawManager;
        private TextGame _text;

        //BoardClass takes an string gametype and depending on the string initialises
        //either chess or checkers game
        private Board()
        {
            _gameOver = false;
            _checkingPiece = null;
            _board = new List<PieceManager>();
            _promotionPiece = PieceType.Queen;
            _drawManager = new BoardDrawManager();
            _text = new TextGame();
        }

        public static Board Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Board();
                return _instance;
            }
        }

        public Game SetType
        {
            set
            {
                _board = new List<PieceManager>();
                new PieceManagerFactory(value);
                List<PieceManager> whitepiece = new List<PieceManager>();
                List<PieceManager> blackpiece = new List<PieceManager>();
                foreach (PieceManager p in _board)
                {
                    if (p.Colour == false)
                        blackpiece.Add(p);
                    else
                        whitepiece.Add(p);
                }
                wP = new Player(true, true, whitepiece);
                bP = new Player(false, false, blackpiece);
                _gameType = value;
            }
        }

        public void Update()
        {
            _gameOver = TimeMonitor();
            switch(_gameType)
            {
                case Game.Checker:
                    GameCheckerOver();
                    break;
                case Game.Mix:
                    GameCheckerOver();
                    break;
                case Game.Chess:
                    foreach (PieceManager p in _board)
                        p.Pinned = false;
                    foreach (PieceManager p in _board)
                    {
                        p.Checker = false;
                        p.AvailableMove();
                    }
                    Player player;
                    _checkingPiece = MonitorCheck(wP, bP);
                    if (wP.Turn)
                        player = wP;
                    else
                        player = bP;
                    if (_checkingPiece != null)
                    {
                        player.KingPiece.CheckMate(_checkingPiece);
                        _gameOver = player.KingPiece.GameOver;
                    }
                    break;
            }
            wP.UpdatePieces(_board);
            bP.UpdatePieces(_board);
        }

        public void Input(string s) => _text.Move(s, wP, bP);

        public void ChangePromotionPiece()
        {
            int i = (int)_promotionPiece;
            i++;
            if (i > 7)
                i = 1;
            _promotionPiece = (PieceType)i;
        }

        public void PrintBoard() => _text.PrintBoard();
        public void GameCheckerOver()
        {
            if (wP.Pieces.Count == 0)
                _gameOver = true;
            else if (bP.Pieces.Count == 0)
                _gameOver = true;
            else
                _gameOver = false;
        }

        //MonitorCheck checks for any checks on the board and returns the piece
        //checking the king
        public PieceManager MonitorCheck(Player white, Player black)
        {
            if (white.Turn)
            {
                foreach (PieceManager p in black.Pieces)
                {
                    if (p.AvailableMove().Contains(white.KingPiece) && p.Piece is not King)
                    {
                        p.Checker = true;
                        return p;
                    }
                }
            }
            else
            {
                foreach (PieceManager p in white.Pieces)
                {
                    if (p.AvailableMove().Contains(black.KingPiece) && p.Piece is not King)
                    {
                        p.Checker = true;
                        return p;
                    }
                }
            }
            return null;
        }

        public void LeftClick(IHavePosition p)
        {
            foreach (PieceManager piece in _board)
            {
                if (piece.Selected)
                {
                    Player player;
                    if (piece.Colour)
                        player = wP;
                    else
                        player = bP;
                    if (player.MovePiece(piece, p.PosX, p.PosY))
                    {
                        wP.UpdatePieces(_board);
                        bP.UpdatePieces(_board);
                        piece.Selected = false;
                        wP.Turn = !wP.Turn;
                        bP.Turn = !bP.Turn;
                    }
                    else
                    {
                        if(p is PieceManager)
                            (p as PieceManager).Selected = true;
                        piece.Selected = false;
                    }
                    return;
                }
            }    
            if(p is PieceManager)
                (p as PieceManager).Selected = true;
        }

        public List<PieceManager> GameBoard
        {
            get { return _board; }
        }

        //Draws board on the screen
        public void Draw() => _drawManager.Draw(_gameOver, _promotionPiece, wP, bP);

        private bool TimeMonitor()
        {
            if (wP.Clock == "00:00")
                return true;
            else if (bP.Clock == "00:00")
                return true;
            return false;
        }

        public bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }

        public TextGame TextGame
        {
            get { return _text; }
        }

        public PieceType PromotionPiece
        {
            get { return _promotionPiece; }
        }
    }
}
