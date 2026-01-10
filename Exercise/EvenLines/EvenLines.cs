namespace EvenLines
{
    using System;
    using System.IO;
    using System.Text;

    public class EvenLines
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";

            Console.WriteLine(ProcessLines(inputFilePath));
        }

        public static string ProcessLines(string inputFilePath)
        {
             string[] allLines = File.ReadAllLines(inputFilePath);

            StringBuilder resultBuilder = new StringBuilder();
            for (int i = 0; i < allLines.Length; i += 2)
                {
                    string sanitizedLine = SanitizeLine(allLines[i]);
                    resultBuilder.Append(sanitizedLine);

                }

                return resultBuilder.ToString();
            }

        private const string CharactersToReplace = "-,.!?";

        private static string SanitizeLine(string text)
        {
            foreach (char specialSymbol in CharactersToReplace)
                text = text.Replace(specialSymbol, '@');

            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(words);

            return string.Join(' ', words);
        }
    }
}
        
    

