using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace ChessGame
{

    public class BoardDrawManager
    {
        private void DrawGameOver(Player wP, Player bP)
        {
            if (wP.Turn)
            {
                SplashKit.FillRectangle(Color.Red, 195, 315, 310, 110);
                SplashKit.FillRectangle(Color.Black, 200, 320, 300, 100);
                SplashKit.DrawText("Click Anywhere to go back.....", Color.White, SplashKit.FontNamed("Calibri"), 200, 225, 400);
                SplashKit.DrawText("You Won!\n by CheckMate", Color.White, SplashKit.FontNamed("Arial"), 100, 225, 350);
            }
            else
            {
                SplashKit.FillRectangle(Color.Red, 195, 315, 310, 110);
                SplashKit.FillRectangle(Color.White, 200, 320, 300, 100);
                SplashKit.DrawText("You Won!\n by CheckMate", Color.Black, SplashKit.FontNamed("Calibri"), 100, 225, 350);
                SplashKit.DrawText("Click Anywhere to go back.....", Color.Black, SplashKit.FontNamed("Arial"), 200, 225, 400);
            }
        }

        private void DrawClock(Player wP, Player bP)
        {
            SplashKit.FillRectangle(Color.Black, 330, 20, Constants.Instance.OffsetValue, 15);
            SplashKit.DrawText(bP.Clock, Color.White, SplashKit.FontNamed("Arial"), 100, 340, 24);
            SplashKit.FillRectangle(Color.White, 330, 705, Constants.Instance.OffsetValue, 15);
            SplashKit.DrawText(wP.Clock, Color.Black, SplashKit.FontNamed("Arial"), 100, 340, 709);
        }

        public void Draw(bool gameOver, PieceType promotionPiece, Player wP, Player bP)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                            SplashKit.FillRectangle(Color.White, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                        else
                            SplashKit.FillRectangle(Color.Black, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                    }
                    else
                    {
                        if (j % 2 == 0)
                            SplashKit.FillRectangle(Color.Black, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                        else
                            SplashKit.FillRectangle(Color.White, i * Constants.Instance.Width + 50, j * Constants.Instance.Width + 50, Constants.Instance.Width, Constants.Instance.Width);
                    }

                }
            }
            if(wP != null && bP != null)
            {
                DrawClock(wP, bP);
                if (gameOver)
                    DrawGameOver(wP, bP);
                SplashKit.DrawText("Promotion Piece : " + promotionPiece.ToString() + ". Press C to Change", Color.Red, 10, 10);
            }
        }
    }
}
