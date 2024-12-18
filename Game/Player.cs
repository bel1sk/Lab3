public class Player
{
    public string Name { get; set; }
    public int Position { get; set; }
    public bool HasWon { get; set; }
    public bool HasBonusTurn { get; set; } // Новый флаг для дополнительного хода

    public int SkipTurns { get; set; } // Количество пропущенных ходов

    public Player(string name, int position = 0, bool hasWon = false)
    {
        Name = name;
        Position = position;
        HasWon = hasWon;
        HasBonusTurn = false;
    }

    public void Move(int steps, int boardSize)
    {
        Position += steps;

        if (Position >= boardSize)
        {
            HasWon = true;
        }
    }

    public void ResetBonusTurn()
    {
        HasBonusTurn = false;
    }

    public void ApplySkipTurn()
    {
        SkipTurns = 1; // Игрок пропускает один цикл
    }

    public void DecrementSkipTurn()
    {
        if (SkipTurns > 0)
        {
            SkipTurns--;
        }
    }

    public bool ShouldSkipTurn()
    {
        return SkipTurns > 0;
    }

    public void Render()
    {
        Console.WriteLine($"{Name} находится на позиции {Position}");
    }
}
