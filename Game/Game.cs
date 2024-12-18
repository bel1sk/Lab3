using System.Text.Json;
using System.IO;

namespace BoardGame
{
    public class Game
    {
        private List<Player> players;
        private Board board;
        private Dice dice;
        private int currentPlayerIndex;

        public Game(int playerCount, int boardSize)
        {
            InitializeGame(playerCount, boardSize);
        }

        // Конструктор для загрузки сохранённого состояния
        private Game(List<Player> savedPlayers, int boardSize, int savedCurrentPlayer)
        {
            players = savedPlayers;
            board = new Board(boardSize);
            dice = new Dice();
            currentPlayerIndex = savedCurrentPlayer;
        }

        // Инициализация новой игры
        private void InitializeGame(int playerCount, int boardSize)
        {
            players = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player($"Игрок {i + 1}"));
            }

            board = new Board(boardSize);
            dice = new Dice();
            currentPlayerIndex = 0;
        }

        public void Start()
        {
            Console.WriteLine("Игра началась!");

            while (!IsGameOver())
            {
                var currentPlayer = players[currentPlayerIndex];

                // Проверка на пропуск хода
                if (currentPlayer.ShouldSkipTurn())
                {
                    Console.WriteLine($"\nИгрок {currentPlayer.Name} пропускает ход!");
                    currentPlayer.DecrementSkipTurn();
                    SwitchToNextPlayer();
                    continue;
                }

                // Игрок выполняет свой ход
                PlayTurn();

                // Переход к следующему игроку
                SwitchToNextPlayer();
            }

            AnnounceWinner();
        }


        // Метод выполнения хода текущего игрока
        private void PlayTurn()
        {
            var currentPlayer = players[currentPlayerIndex];

            do
            {
                // Отображаем статус перед броском кубика
                Console.WriteLine($"\nБросает {currentPlayer.Name}. Находится на позиции {currentPlayer.Position}. Нажмите Enter, чтобы бросить кубик или M для выхода из игры.");

                string input = Console.ReadLine()?.ToUpper();
                if (input == "M")
                {
                    HandleExitRequest();
                    return;
                }

                // Бросок кубика
                int diceResult = dice.Roll();
                Console.WriteLine($"{currentPlayer.Name} выбросил {diceResult}");

                // Перемещение игрока
                currentPlayer.Move(diceResult, board.TotalCells);
                Console.WriteLine($"{currentPlayer.Name} находится на позиции {currentPlayer.Position}");

                // Обрабатываем специальное поле
                var triggeredCell = board.GetCell(currentPlayer.Position);
                if (triggeredCell != null && triggeredCell.Type != "Regular")
                {
                    triggeredCell.Trigger(currentPlayer);

                    // Выводим статус после срабатывания специального поля
                    Console.WriteLine($"{currentPlayer.Name} находится на позиции {currentPlayer.Position}");
                }

                // Проверка на победу
                if (currentPlayer.HasWon)
                {
                    Console.WriteLine($"{currentPlayer.Name} достиг финиша и победил!");
                    return;
                }

                // Проверка на дополнительный ход
                if (currentPlayer.HasBonusTurn)
                {
                    Console.WriteLine($"{currentPlayer.Name} выполняет дополнительный ход!");
                    currentPlayer.ResetBonusTurn(); // Сбрасываем флаг дополнительного хода
                }
                else
                {
                    break; // Завершаем ход, если бонусного хода нет
                }

            } while (true); // Повторяем ход, если есть бонусный ход
        }






        private void HandleExitRequest()
        {
            Console.WriteLine("\nВы хотите выйти из игры. Как вы хотите это сделать?");
            Console.WriteLine("1. Сохранить игру и выйти.");
            Console.WriteLine("2. Выйти без сохранения.");
            Console.WriteLine("3. Отменить выход и продолжить игру.");

            while (true)
            {
                Console.Write("Выберите вариант (1, 2 или 3): ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    SaveGame();
                    Console.WriteLine("Игра сохранена. Выход из программы.");
                    Environment.Exit(0);
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Выход из программы без сохранения.");
                    Environment.Exit(0);
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Продолжаем игру!");
                    break; // Возвращаемся к текущему ходу
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }
            }
        }


        private void SwitchToNextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        private bool IsGameOver()
        {
            foreach (var player in players)
            {
                if (player.HasWon)
                    return true;
            }
            return false;
        }

        private void AnnounceWinner()
        {
            foreach (var player in players)
            {
                if (player.HasWon)
                {
                    Console.WriteLine($"\nПоздравляем, {player.Name} победил!");
                    break;
                }
            }

            // Предложение начать новую игру
            Console.WriteLine("\nХотите сыграть ещё раз? (да/нет): ");
            string input = Console.ReadLine()?.ToLower();

            if (input == "да")
            {
                RestartGame();
            }
            else
            {
                Console.WriteLine("Спасибо за игру! До свидания!");
                Environment.Exit(0);
            }
        }

        private void RestartGame()
        {
            Console.Write("Введите количество игроков (от 2 до 4): ");
            int playerCount;

            while (!int.TryParse(Console.ReadLine(), out playerCount) || playerCount < 2 || playerCount > 4)
            {
                Console.WriteLine("Некорректный ввод. Введите число от 2 до 4.");
            }

            // Заново инициализируем игру
            InitializeGame(playerCount, board.TotalCells);
            Console.WriteLine("\nНачинаем новую игру!");
            Start();
        }


        // Метод сохранения игры в JSON файл
        public void SaveGame()
        {
            var gameState = new
            {
                Players = players,
                BoardSize = board.TotalCells,
                CurrentPlayerIndex = currentPlayerIndex
            };

            string json = JsonSerializer.Serialize(gameState, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("game_save.json", json);
        }

        // Статический метод загрузки игры из JSON файла
        public static Game LoadGame()
        {
            if (File.Exists("game_save.json"))
            {
                string json = File.ReadAllText("game_save.json");
                var gameState = JsonSerializer.Deserialize<GameState>(json);

                return new Game(gameState.Players, gameState.BoardSize, gameState.CurrentPlayerIndex);
            }
            else
            {
                throw new FileNotFoundException("Сохранение не найдено!");
            }
        }

        // Класс для десериализации состояния игры
        private class GameState
        {
            public List<Player> Players { get; set; }
            public int BoardSize { get; set; }
            public int CurrentPlayerIndex { get; set; }
        }
    }
}
