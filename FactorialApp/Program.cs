namespace FactorialApp
{
    internal class Program
    {
        static readonly object _lock = new();

        static void Main(string[] args)
        {
            int[] numbers = [1, 2, 3, 4, 5];

            List<Thread> threads = [];

            foreach (var num in numbers)
            {
                Thread thread = new(() => {
                    try
                    {
                        long result = CalculateFactorial(num);
                        lock (_lock) // Ensures only one thread writes to console at a time
                        {
                            Console.WriteLine($"Factorial of {num} is {result}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error calculating factorial of {num}: {ex.Message}");
                    }
                });
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        /// <summary>
        /// Calculate factorial recursive
        /// </summary>
        /// <param name="number">The number for which the factorial is to be calculated.</param>
        /// <returns>The factorial of the given number</returns>
        /// <exception cref="ArgumentException">Thrown if the number is negative.</exception>
        public static long CalculateFactorial(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Number must be non-negative.");
            }

            // Base case: factorial of 0 or 1 is 1
            if (number == 0 || number == 1)
            {
                return 1;
            }

            // Recursive call
            return number * CalculateFactorial(number - 1);
        }
    }
}
