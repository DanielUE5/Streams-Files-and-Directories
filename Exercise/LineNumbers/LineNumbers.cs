namespace LineNumbers
{
    using System;
    using System.IO;
    using System.Linq;

    public class LineNumbers
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";
            string outputFilePath = @"..\..\..\output.txt";

            ProcessLines(inputFilePath, outputFilePath);
        }

        public static void ProcessLines(string inputFilePath, string outputFilePath)
        {
            string[] lines = File.ReadAllLines(inputFilePath);

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    int letterCount = line.Count(char.IsLetter);
                    int punctuationCount = line.Count(char.IsPunctuation);

                    string outputLine = $"Line {i + 1}: {line} ({letterCount})({punctuationCount})";

                    writer.WriteLine(outputLine);
                }
            }
        }
    }
}
