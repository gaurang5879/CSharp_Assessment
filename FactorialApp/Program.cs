namespace FactorialApp
{
    public class Program
    {
        private static readonly object _lock = new();
        private const int MaxFactorialInput = 20;

        static async Task Main(string[] args)
        {
            Console.WriteLine(string.Format(Messages.WarningMaxFactorial, MaxFactorialInput));
            Console.Write(Messages.EnterNumbers);

            string input = Console.ReadLine();
            int[] numbers = ParseNumbers(input);

            if (numbers.Length == 0)
            {
                Console.WriteLine(string.Format(Messages.NoValidNumbers, MaxFactorialInput));
                return;
            }

            Console.Write(Messages.EnterThreadCount);
            if (!int.TryParse(Console.ReadLine(), out int threadCount) || threadCount < 1)
            {
                Console.WriteLine(string.Format(Messages.InvalidThreadCount, 2));
                threadCount = 2;
            }

            List<Task> tasks = new List<Task>();

            foreach (var num in numbers)
            {
                tasks.Add(Task.Run(() => CalculateFactorialWithTimeout(num)));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine(Messages.AllCalculationsComplete);
        }

        /// <summary>
        /// Parse inputs
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int[] ParseNumbers(string input)
        {
            return input
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(num => int.TryParse(num, out int value) ? value : (int?)null)
                .Where(value => value.HasValue)
                .Select(value => value.Value)
                .ToArray();
        }

        /// <summary>
        /// Callculate factorial with timeout of 5 seconds
        /// </summary>
        /// <param name="num"></param>
        private static void CalculateFactorialWithTimeout(int num)
        {
            if (num < 0)
            {
                Console.WriteLine(string.Format(Messages.InvalidNegativeInput, num));
                return;
            }

            if (num > MaxFactorialInput)
            {
                Console.WriteLine(string.Format(Messages.InvalidMaxInput, num, MaxFactorialInput));
                return;
            }

            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                var task = Task.Run(() => FactorialCalculator.CalculateFactorial(num), cts.Token);

                if (task.Wait(5000, cts.Token))
                {
                    lock (_lock)
                    {
                        Console.WriteLine(string.Format(Messages.FactorialResult, num, task.Result));
                    }
                }
                else
                {
                    Console.WriteLine(string.Format(Messages.CalculationTimeout, num));
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(string.Format(Messages.CalculationCancelled, num));
            }
            catch (Exception ex)
            {
                lock (_lock)
                {
                    Console.WriteLine(string.Format(Messages.CalculationError, num, ex.Message));
                }
            }
        }
    }
}
