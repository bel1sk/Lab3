namespace BoardGame
{
    // Ячейка, перемещающая игрока назад
    public class BackwardCell : Cell
    {
        private int stepsBackward;

        public BackwardCell(int position, int steps) : base(position, "Backward")
        {
            stepsBackward = steps;
        }

        public override void Trigger(Player player)
        {
            Console.WriteLine($"Игрок {player.Name} попал на поле {Position} и перемещается назад на {stepsBackward} шагов!");
            player.Position -= stepsBackward; // Передвигаем игрока назад

            if (player.Position < 0)
                player.Position = 0; // Предотвращаем выход за границу поля
        }
    }
}
