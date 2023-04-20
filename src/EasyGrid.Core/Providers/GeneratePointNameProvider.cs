namespace EasyGrid.Core.Providers
{
    internal class GeneratePointNameProvider
    {
        public string GeneratePointName(int i, int j)
        {
            return $"{GenerateLetterFromIndex(i + 1)}{j + 1}";
        }

        private static string GenerateLetterFromIndex(int index)
        {
            var colLetter = string.Empty;

            while (index > 0)
            {
                var mod = (index - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                index = (index - mod) / 26;
            }

            return colLetter;
        }
    }
}
