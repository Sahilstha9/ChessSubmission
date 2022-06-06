using System;
using SplashKitSDK;

namespace ChessGame // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Window("Shape Drawer", 750, 750);
            Screen gamechoice = Screen.Home;
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.RGBColor(50, 50, 0));
                Board.Instance.Draw();
                switch(gamechoice)
                {
                    case Screen.Home:
                        Game choice = DrawHomeScreen();
                        if (choice != Game.NotSet)
                        {
                            gamechoice = Screen.Game;
                            Board.Instance.SetType = choice;
                        }
                        break;
                    case Screen.Game:
                        foreach (Piece p in Board.Instance.GameBoard)
                            p.Draw();
                        Board.Instance.Update();

                        if (SplashKit.MouseClicked(MouseButton.LeftButton))
                        {
                            if (!Board.Instance.GameOver)
                            {
                                IHavePosition s = null;
                                foreach(Piece p in Board.Instance.GameBoard)
                                {
                                    if (p.IsAt(SplashKit.MousePosition()))
                                        s = p;
                                }
                                if (s != null)
                                    Board.Instance.LeftClick(s);
                                else
                                    Board.Instance.LeftClick(EmptySquareIsAt(SplashKit.MousePosition()));
                            }
                            else
                                gamechoice = Screen.Home;
                        }
                        break;
                }
                SplashKit.RefreshScreen();
            } while (!SplashKit.WindowCloseRequested("Shape Drawer"));
        }

        static IHavePosition EmptySquareIsAt(Point2D pt)
        {
            double x, y;
            x = Math.Floor(((pt.X - 50) / Constants.Instance.Width));
            y = Math.Floor(((pt.Y - 50) / Constants.Instance.Width));
            x = Convert.ToInt32(x);
            y = Convert.ToInt32(y);
            return new EmptySquare(Convert.ToInt32(x), Convert.ToInt32(y));
        }

        static Game DrawHomeScreen()
        {
            Point2D pt = SplashKit.MousePosition();
            SplashKit.DrawText("Welcome to Board Game", Color.Red, SplashKit.FontNamed("Arial"), 100, 250, 125);
            if (pt.X >= 75 && pt.X <= 325 && pt.Y >= 320 && pt.Y <= 420)
                SplashKit.FillRectangle(Color.Red, 70, 315, 260, 110);
            SplashKit.FillRectangle(Color.Black, 75, 320, 250, 100);
            SplashKit.DrawText("Play Chess", Color.White, SplashKit.FontNamed("Calibri"), 200, 100, 350);
            if (pt.X >= 425 && pt.X <= 675 && pt.Y >= 320 && pt.Y <= 420)
                SplashKit.FillRectangle(Color.Red, 420, 315, 260, 110);
            SplashKit.FillRectangle(Color.Black, 425, 320, 250, 100);
            SplashKit.DrawText("Play Checker", Color.White, SplashKit.FontNamed("Calibri"), 200, 500, 350);
            if (pt.X >= 250 && pt.X <= 500 && pt.Y >= 480 && pt.Y <= 580)
                SplashKit.FillRectangle(Color.Red, 245, 475, 260, 110);
            SplashKit.FillRectangle(Color.Black, 250, 480, 250, 100);
            SplashKit.DrawText("Play Mix", Color.White, SplashKit.FontNamed("Calibri"), 200, 315, 510);
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                if (pt.X >= 75 && pt.X <= 325 && pt.Y >= 320 && pt.Y <= 420)
                    return Game.Chess;
                if (pt.X >= 425 && pt.X <= 675 && pt.Y >= 320 && pt.Y <= 420)
                    return Game.Checker;
                if (pt.X >= 250 && pt.X <= 500 && pt.Y >= 480 && pt.Y <= 580)
                    return Game.Mix;
            }
            return Game.NotSet;
        }
    }
}