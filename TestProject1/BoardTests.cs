using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGame;

namespace BoardGameTests
{
    [TestClass]
    public class BoardTests
    {
        // Проверка получения корректной ячейки по позиции
        [TestMethod]
        public void GetCell_ShouldReturnCorrectCell()
        {
            var board = new Board(20);
            var cell = board.GetCell(5);

            Assert.IsNotNull(cell);
            Assert.AreEqual(5, cell.Position);
        }

        // Проверка корректной обработки некорректной позиции (возвращает null)
        [TestMethod]
        public void GetCell_ShouldReturnNullForInvalidPosition()
        {
            var board = new Board(20);
            var cell = board.GetCell(-1);

            Assert.IsNull(cell);
        }
    }
}
