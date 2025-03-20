namespace FactorialApp
{
    public static class FactorialCalculator
    {
        /// <summary>
        /// Calculate factorial iterative
        /// </summary>
        /// <param name="number">The number for which the factorial is to be calculated.</param>
        /// <returns>The factorial of the given number</returns>
        /// <exception cref="ArgumentException">Thrown if the number is negative.</exception>
        /// <exception cref="OverflowException">Thrown if the factorial of number exceeds maximum value of long.</exception>
        public static long CalculateFactorial(int number)
        {
            if (number < 0)
                throw new ArgumentException(Messages.NumberNonNegativeException);

            if (number > 20)
                throw new OverflowException(string.Format(Messages.MaxNumberLimitException, number));

            long result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}
