namespace SimpleBank.Domain.Common
{
    internal static class AlphaNumericGenerator
    {
        private static readonly Random _rGenerator = new();
        private static readonly string _digits = "0123456789";
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GetRandomNumbers(int length)
        { 
            var randomNumbers = new string(
                    Enumerable.Repeat(_digits, length)
                    .Select(d => d[_rGenerator.Next(d.Length)])
                    .ToArray()
                );

            return randomNumbers;
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
