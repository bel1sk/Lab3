using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGame;

namespace BoardGameTests
{
    [TestClass]
    public class PlayerTests
    {
        // Проверка корректного перемещения игрока на указанное количество шагов
        [TestMethod]
        public void Move_ShouldMovePlayerCorrectly()
        {
            var player = new Player("Test");
            player.Move(5, 20);
            Assert.AreEqual(5, player.Position);
        }

        // Проверка установки флага победы, если игрок достиг конца поля
        [TestMethod]
        public void Move_ShouldSetHasWonWhenReachingEnd()
        {
            var player = new Player("Test");
            player.Move(20, 20);
            Assert.IsTrue(player.HasWon);
            Assert.AreEqual(20, player.Position);
        }

        // Проверка установки пропуска хода
        [TestMethod]
        public void ApplySkipTurn_ShouldSetSkipTurns()
        {
            var player = new Player("Test");
            player.ApplySkipTurn();
            Assert.AreEqual(1, player.SkipTurns);
        }

        // Проверка, что метод ShouldSkipTurn возвращает true, если игрок должен пропустить ход
        [TestMethod]
        public void ShouldSkipTurn_ShouldReturnTrueWhenSkipTurnsIsPositive()
        {
            var player = new Player("Test");
            player.ApplySkipTurn();
            Assert.IsTrue(player.ShouldSkipTurn());
        }

        // Проверка уменьшения количества пропущенных ходов
        [TestMethod]
        public void DecrementSkipTurn_ShouldReduceSkipTurns()
        {
            var player = new Player("Test");
            player.ApplySkipTurn();
            player.DecrementSkipTurn();
            Assert.AreEqual(0, player.SkipTurns);
        }
    }
}
