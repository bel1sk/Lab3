namespace BoardGame
{
    public class SkipTurnCell : Cell
    {
        public SkipTurnCell(int position) : base(position, "SkipTurn")
        {
        }

        public override void Trigger(Player player)
        {
            Console.WriteLine($"Игрок {player.Name} попал на поле {Position} и пропускает следующий цикл ходов!");
            player.ApplySkipTurn();
        }
    }
}
