using System;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в игру 'Бродилка'!");

            Game game;
            if (File.Exists("game_save.json"))
            {
                Console.Write("Обнаружено сохранение. Хотите продолжить игру? (да/нет): ");
                string input = Console.ReadLine()?.ToLower();

                if (input == "да")
                {
                    try
                    {
                        game = Game.LoadGame();
                        Console.WriteLine("Сохранённая игра загружена!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка загрузки: {e.Message}");
                        return;
                    }
                }
                else
                {
                    game = StartNewGame();
                }
            }
            else
            {
                game = StartNewGame();
            }

            game.Start();
        }

        private static Game StartNewGame()
        {
            Console.Write("Введите количество игроков (от 2 до 4): ");
            int playerCount = int.Parse(Console.ReadLine());
            int boardSize = 40;

            return new Game(playerCount, boardSize);
        }
    }
}
