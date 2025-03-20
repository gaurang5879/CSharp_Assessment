namespace FactorialApp
{
    public static class Messages
    {
        public const string WarningMaxFactorial = "NOTE: This program supports factorials up to {0}. For larger numbers, BigInteger should be used.";
        public const string EnterNumbers = "Enter numbers separated by spaces: ";
        public const string EnterThreadCount = "Enter the number of threads to use: ";
        public const string InvalidThreadCount = "Invalid input. Using default thread count = {0}.";
        public const string NoValidNumbers = "No valid numbers provided. Please enter numbers between 0 and {0}.";
        public const string InvalidNegativeInput = "Invalid input: {0}. Factorial is only defined for non-negative numbers.";
        public const string InvalidMaxInput = "Invalid input: {0}. Factorial of numbers greater than {1} exceeds 'long' capacity.";
        public const string FactorialResult = "Factorial of {0} is {1}";
        public const string CalculationTimeout = "Calculation for {0} timed out.";
        public const string CalculationCancelled = "Factorial calculation for {0} was canceled.";
        public const string CalculationError = "Error calculating factorial of {0}: {1}";
        public const string AllCalculationsComplete = "All calculations completed.";
        public const string NumberNonNegativeException = "Number must be non-negative.";
        public const string MaxNumberLimitException = "Factorial of {0} exceeds the maximum value of 'long'.";
    }
}
