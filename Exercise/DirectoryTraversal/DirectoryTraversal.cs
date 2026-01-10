namespace DirectoryTraversal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DirectoryTraversal
    {
        static void Main()
        {
            Console.WriteLine("Enter the directory path:");
            string path = Console.ReadLine();
            string reportFileName = @"\report.txt";

            string reportContent = TraverseDirectory(path);
            Console.WriteLine(reportContent);

            WriteReportToDesktop(reportContent, reportFileName);
        }

        public static string TraverseDirectory(string inputFolderPath)
        {
            var filesByExtension = new Dictionary<string, List<FileInfo>>();

            try
            {
                var files = new DirectoryInfo(inputFolderPath).GetFiles();

                foreach (var file in files)
                {
                    if (!filesByExtension.ContainsKey(file.Extension))
                    {
                        filesByExtension[file.Extension] = new List<FileInfo>();
                    }
                    filesByExtension[file.Extension].Add(file);
                }

                var reportLines = new List<string>();

                var orderedExtensions = filesByExtension
                    .OrderByDescending(entry => entry.Value.Count)
                    .ThenBy(entry => entry.Key);

                foreach (var extensionGroup in orderedExtensions)
                {
                    reportLines.Add(extensionGroup.Key);

                    var orderedFiles = extensionGroup.Value.OrderBy(file => file.Length);

                    foreach (var file in orderedFiles)
                    {
                        reportLines.Add($"    {file.Name} - {file.Length} bytes");
                    }
                }

                return string.Join(Environment.NewLine, reportLines);
            }
            catch (Exception ex)
            {
                return $"Error traversing directory: {ex.Message}";
            }
        }

        public static void WriteReportToDesktop(string textContent, string reportFileName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullPath = Path.Combine(desktopPath, reportFileName.TrimStart('\\'));

            try
            {
                File.WriteAllText(fullPath, textContent);
                Console.WriteLine($"Report saved to: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing report: {ex.Message}");
            }
        }
    }
}
