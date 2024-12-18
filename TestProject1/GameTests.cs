using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGame;
using System.Collections.Generic;

namespace BoardGameTests
{
    [TestClass]
    public class GameTests
    {
        // Проверка завершения игры, если игрок достиг конца поля и победил
        [TestMethod]
        public void IsGameOver_ShouldReturnTrueWhenPlayerHasWon()
        {
            var players = new List<Player> { new Player("Test", 20, true) };
            var game = new Game(2, 20);

            var result = game.GetType()
                .GetMethod("IsGameOver", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(game, null);

            Assert.IsTrue((bool)result);
        }
    }
}
