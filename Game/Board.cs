using System.Collections.Generic;

namespace BoardGame
{
    public class Board
    {
        public List<Cell> Cells { get; private set; }
        public int TotalCells => Cells.Count;

        public Board(int cellCount)
        {
            Cells = new List<Cell>();

            for (int i = 0; i < cellCount; i++)
            {
                if (i == 5) Cells.Add(new ForwardCell(i, 3));         // Перемещение на 3 шага вперёд
                else if (i == 10) Cells.Add(new BackwardCell(i, 2));  // Перемещение на 2 шага назад
                else if (i == 12) Cells.Add(new BonusTurnCell(i));    // Дополнительный ход
                else if (i == 15) Cells.Add(new SkipTurnCell(i));     // Пропуск хода
                else if (i == 16) Cells.Add(new SkipTurnCell(i));     // Пропуск хода
                else if (i == 17) Cells.Add(new SkipTurnCell(i));     // Пропуск хода
                else Cells.Add(new Cell(i)); // Обычная ячейка
            }
        }

        public Cell GetCell(int position)
        {
            if (position >= 0 && position < Cells.Count)
                return Cells[position];
            else
                return null;
        }

        public void Render()
        {
            foreach (var cell in Cells)
            {
                Console.Write(cell.Type == "Regular" ? "[ ]" : $"[{cell.Type.Substring(0, 1)}]");
            }
            Console.WriteLine();
        }
    }
}
