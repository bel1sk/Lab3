namespace BoardGame
{
    // Базовый класс для ячейки на игровом поле
    public class Cell
    {
        public int Position { get; private set; }  // Позиция ячейки на поле
        public string Type { get; private set; }  // Тип ячейки (обычная, особая и т.д.)

        public Cell(int position, string type = "Regular")
        {
            Position = position;
            Type = type;
        }

        // Метод, который активируется при попадании игрока на ячейку
        public virtual void Trigger(Player player)
        {
            // По умолчанию ничего не делает (обычная ячейка)
        }

        public override string ToString()
        {
            return $"Ячейка {Position} ({Type})";
        }
    }
}
