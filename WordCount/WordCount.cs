namespace WordCount
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class WordCount
    {
        static void Main()
        {
            string wordPath = @"..\..\..\Files\words.txt";
            string textPath = @"..\..\..\Files\text.txt";
            string outputPath = @"..\..\..\Files\output.txt";

            CalculateWordCounts(wordPath, textPath, outputPath);
        }

        public static void CalculateWordCounts(string wordsFilePath, string textFilePath, string outputFilePath)
        {
            using (StreamReader wordsReader = new StreamReader(wordsFilePath))
            {
                using (StreamReader textReader = new StreamReader(textFilePath))
                {
                    using (StreamWriter writer = new StreamWriter(outputFilePath))
                    {
                        List<string> wordsList = wordsReader
                            .ReadToEnd()
                            .ToLower()
                            .Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                            .ToList();

                        string[] textWords = textReader
                            .ReadToEnd()
                            .ToLower()
                            .Split(new char[] { ' ', '\n', '\r', '.', ',', '!', '?', ';', ':', '-', '…' }, StringSplitOptions.RemoveEmptyEntries);

                        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

                        foreach (var word in wordsList)
                        {
                            if (!wordCounts.ContainsKey(word))
                            {
                                wordCounts[word] = 0;
                            }
                        }

                        foreach (var word in textWords)
                        {
                            if (wordCounts.ContainsKey(word))
                            {
                                wordCounts[word]++;
                            }
                        }

                        foreach (var kvp in wordCounts.OrderByDescending(x => x.Value))
                        {
                            writer.WriteLine($"{kvp.Key} - {kvp.Value}");
                        }
                    }
                }
            }
        }
    }
}
