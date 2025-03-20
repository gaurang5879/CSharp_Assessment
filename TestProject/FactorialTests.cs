using FactorialApp;

namespace TestProject
{
    [TestFixture]
    public class FactorialTests
    {
        [Test]
        public void FactorialOfZero_ShouldReturnOne()
        {
            long result = FactorialCalculator.CalculateFactorial(0);
            Assert.AreEqual(1, result, TestMessages.FactorialZero);
        }

        [Test]
        public void FactorialOfOne_ShouldReturnOne()
        {
            long result = FactorialCalculator.CalculateFactorial(1);
            Assert.AreEqual(1, result, TestMessages.FactorialOne);
        }

        [Test]
        public void FactorialOfFive_ShouldReturnCorrectValue()
        {
            long result = FactorialCalculator.CalculateFactorial(5);
            Assert.AreEqual(120, result, TestMessages.FactorialFive);
        }

        [Test]
        public void FactorialOfTwenty_ShouldReturnCorrectValue()
        {
            long result = FactorialCalculator.CalculateFactorial(20);
            Assert.AreEqual(2432902008176640000, result, TestMessages.FactorialTwenty);
        }

        [Test]
        public void FactorialOfNegativeNumber_ShouldThrowArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => FactorialCalculator.CalculateFactorial(-1));
            Assert.That(ex.Message, Is.EqualTo(TestMessages.FactorialNegativeError));
        }

        [Test]
        public void FactorialOfTwentyOne_ShouldThrowOverflowException()
        {
            var ex = Assert.Throws<OverflowException>(() => FactorialCalculator.CalculateFactorial(21));
            Assert.That(ex.Message, Is.EqualTo(TestMessages.FactorialOverflowError));
        }
    }
}
