using System;

namespace 文字版OXGame
{
    public class OXGameEngine
    {
        private char[,] gameMarkers;        //二維陣列放遊戲空格狀況

        public OXGameEngine()
        {
            gameMarkers = new char[3, 3];   //二維陣列規格3X3
            ResetGame();
        }

        public void SetMarker(int x, int y, char player)
        {
            if (IsValidMove(x, y))
            {
                gameMarkers[x, y] = player;  //玩家在[x,y]下標記
            }
            else
            {
                throw new ArgumentException("Invalid move!");  //無效的話，丟出Invalid move!
            }
        }

        public void ResetGame()
        {
            gameMarkers = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameMarkers[i, j] = ' ';       //將場上的標記清空
                }
            }
        }

        public char IsWinner()   //確認是否有贏家(連成一線)
        {
            // 檢查橫向
            for (int i = 0; i < 3; i++)
            {
                if (gameMarkers[i, 0] != ' ' && gameMarkers[i, 0] == gameMarkers[i, 1] && gameMarkers[i, 1] == gameMarkers[i, 2])
                {
                    return gameMarkers[i, 0];
                }
            }

            // 檢查縱向
            for (int j = 0; j < 3; j++)
            {
                if (gameMarkers[0, j] != ' ' && gameMarkers[0, j] == gameMarkers[1, j] && gameMarkers[1, j] == gameMarkers[2, j])
                {
                    return gameMarkers[0, j];
                }
            }

            // 檢查對角線
            if (gameMarkers[0, 0] != ' ' && gameMarkers[0, 0] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 2])
            {
                return gameMarkers[0, 0];
            }

            if (gameMarkers[0, 2] != ' ' && gameMarkers[0, 2] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 0])
            {
                return gameMarkers[0, 2];
            }

            return ' '; // 沒有贏家出現
        }

        public bool IsValidMove(int x, int y)
        {
            if (x < 0 || x >= 3 || y < 0 || y >= 3)
            {
                return false;    //檢查是否在合理範圍
            }

            if (gameMarkers[x, y] != ' ')
            {
                return false;  //檢查是否以下過標記
            }

            return true;
        }

        public char GetMarker(int x, int y)
        {
            return gameMarkers[x, y];    //取得[x,y]裡的標記
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            OXGameEngine engine = new OXGameEngine();
            char player = 'X'; //當前玩家設置為x
            Console.WriteLine("遊戲開始!\n玩家請輸入想下的位置[0~2]:(行 列)");

            while (true)  //遊戲開始前，秀出棋盤
            {
                //畫出遊戲棋盤
                Console.WriteLine("-------------");

                for (int row = 0; row < 3; row++)
                {
                    Console.Write("| ");
                    for (int col = 0; col < 3; col++)
                    {
                        // 設置XO棋子顏色
                        Console.ForegroundColor = (engine.GetMarker(row, col) == 'X') ? ConsoleColor.Yellow : ConsoleColor.Blue;

                        // 顯示指定位置的標記
                        Console.Write(engine.GetMarker(row, col));

                        // 恢復文字顏色為預設值
                        Console.ResetColor();
                        Console.Write(" | ");
                    }
                    Console.WriteLine("\n-------------");
                }

               


                // 檢查是否有贏家
                char winner = engine.IsWinner();
                if (winner != ' ')
                {
                    Console.WriteLine($"贏家為: {winner}，遊戲結束。");
                    break;
                }

                //無贏家，繼續下棋
                Console.WriteLine($"輪到 {player} 。\n請輸入位置[0~2] (行 列)");   //換人下棋
                string[] input = Console.ReadLine().Split(' ');
                if (input.Length != 2)  //除錯
                {
                    Console.WriteLine("輸入格式錯誤，請重新輸入。");
                    continue;
                }
                int x, y;
                if (!int.TryParse(input[0], out x) || !int.TryParse(input[1], out y))
                {
                    Console.WriteLine("輸入格式錯誤，請重新輸入。");
                    continue;
                }

                // 設置玩家的標記
                try
                {
                    engine.SetMarker(x, y, player);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // 切換玩家
                player = (player == 'X') ? 'O' : 'X';
            }

            Console.ReadLine();


        }

    }

}
