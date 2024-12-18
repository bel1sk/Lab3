using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGame;

namespace BoardGameTests
{
    [TestClass]
    public class CellTests
    {
        // Проверка корректного перемещения игрока вперёд при попадании на ForwardCell
        [TestMethod]
        public void ForwardCell_ShouldMovePlayerForward()
        {
            var player = new Player("Test");
            var forwardCell = new ForwardCell(5, 3);

            forwardCell.Trigger(player);

            Assert.AreEqual(3, player.Position);
        }

        // Проверка корректного перемещения игрока назад при попадании на BackwardCell
        [TestMethod]
        public void BackwardCell_ShouldMovePlayerBackward()
        {
            var player = new Player("Test", 5);
            var backwardCell = new BackwardCell(5, 2);

            backwardCell.Trigger(player);

            Assert.AreEqual(3, player.Position);
        }

        // Проверка установки флага дополнительного хода на BonusTurnCell
        [TestMethod]
        public void BonusTurnCell_ShouldGrantBonusTurn()
        {
            var player = new Player("Test");
            var bonusTurnCell = new BonusTurnCell(5);

            bonusTurnCell.Trigger(player);

            Assert.IsTrue(player.HasBonusTurn);
        }

        // Проверка установки пропуска хода на SkipTurnCell
        [TestMethod]
        public void SkipTurnCell_ShouldSetSkipTurn()
        {
            var player = new Player("Test");
            var skipTurnCell = new SkipTurnCell(5);

            skipTurnCell.Trigger(player);

            Assert.AreEqual(1, player.SkipTurns);
        }
    }
}
