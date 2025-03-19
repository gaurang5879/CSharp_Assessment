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
                Thread thread = new(() => CalculateFactorial(num));
                threads.Add(thread);
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        /// <summary>
        /// Calculate factorial
        /// </summary>
        /// <param name="number"></param>
        static void CalculateFactorial(int number)
        {
            long result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }

            lock (_lock)
            {
                Console.WriteLine($"Factorial of {number} is {result}");
            }
        }
    }
}
