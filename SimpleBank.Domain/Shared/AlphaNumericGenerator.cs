namespace SimpleBank.Domain.Shared
{
    internal static class AlphaNumericGenerator
    {
        private static readonly Random _rGenerator = new();
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GetRandomNumbers(int length)
        {
            var randomNums = _rGenerator.Next(
                (int) Math.Pow(10, length - 1), 
                (int) Math.Pow(10, length) - 1
                ).ToString($"D{length}");

            return randomNums;
        }

        public static string GetRandomAlphabets(int length)
        {
            var randomChars = new string(
                Enumerable.Repeat(_chars, length)
                .Select(c => c[_rGenerator.Next(c.Length)])
                .ToArray()
                );

            return randomChars;
        }

        public static string GetRandomAlphaNumeric(int alphaLength, int numbersLength)
        {
            return GetRandomAlphabets(alphaLength) + GetRandomNumbers(numbersLength);
        }
    }
}
