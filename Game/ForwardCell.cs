namespace BoardGame
{
    // Ячейка, перемещающая игрока вперёд
    public class ForwardCell : Cell
    {
        private int stepsForward;

        public ForwardCell(int position, int steps) : base(position, "Forward")
        {
            stepsForward = steps;
        }

        public override void Trigger(Player player)
        {
            Console.WriteLine($"{player.Name} попал на поле {Position} и перемещается вперёд на {stepsForward} шагов!");
            player.Move(stepsForward, int.MaxValue); // Передвигаем игрока вперёд
        }
    }
}
