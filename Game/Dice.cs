using System;

namespace BoardGame
{
    // Базовый интерфейс для сущностей игры
    public interface IGameEntity
    {
        void Update();
        void Render();
    }

    // Класс кубика для случайного броска
    public class Dice
    {
        private Random random;

        public Dice()
        {
            random = new Random();
        }

        // Метод броска кубика
        public int Roll()
        {
            return random.Next(1, 7); // Возвращает число от 1 до 6
        }
    }
}
