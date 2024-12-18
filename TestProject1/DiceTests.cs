using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardGame;

namespace BoardGameTests
{
    [TestClass]
    public class DiceTests
    {
        // Проверка, что метод Roll возвращает число в диапазоне от 1 до 6
        [TestMethod]
        public void Roll_ShouldReturnValueBetween1And6()
        {
            var dice = new Dice();
            for (int i = 0; i < 100; i++)
            {
                int result = dice.Roll();
                Assert.IsTrue(result >= 1 && result <= 6);
            }
        }
    }
}
