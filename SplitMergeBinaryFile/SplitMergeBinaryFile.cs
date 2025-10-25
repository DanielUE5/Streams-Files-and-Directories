namespace SplitMergeBinaryFile
{
    using System;
    using System.IO;

    public class SplitMergeBinaryFile
    {
        static void Main()
        {
            string sourceFilePath = @"..\..\..\Files\example.png";
            string joinedFilePath = @"..\..\..\Files\example-joined.png";
            string partOnePath = @"..\..\..\Files\part-1.bin";
            string partTwoPath = @"..\..\..\Files\part-2.bin";

            SplitBinaryFile(sourceFilePath, partOnePath, partTwoPath);
            MergeBinaryFiles(partOnePath, partTwoPath, joinedFilePath);
        }

        public static void SplitBinaryFile(string sourceFilePath, string partOneFilePath, string partTwoFilePath)
        {
            byte[] fileData = File.ReadAllBytes(sourceFilePath);
            int totalBytes = fileData.Length;

            int partOneSize = (totalBytes + 1) / 2; 
            int partTwoSize = totalBytes / 2;

            using (FileStream partOneStream = new FileStream(partOneFilePath, FileMode.Create, FileAccess.Write))
            {
                partOneStream.Write(fileData, 0, partOneSize);
            }

            using (FileStream partTwoStream = new FileStream(partTwoFilePath, FileMode.Create, FileAccess.Write))
            {
                partTwoStream.Write(fileData, partOneSize, partTwoSize);
            }
        }

        public static void MergeBinaryFiles(string partOneFilePath, string partTwoFilePath, string joinedFilePath)
        {
            using (FileStream joinedStream = new FileStream(joinedFilePath, FileMode.Create, FileAccess.Write))
            {
                using (FileStream partOneStream = new FileStream(partOneFilePath, FileMode.Open, FileAccess.Read))
                {
                    partOneStream.CopyTo(joinedStream);
                }

                using (FileStream partTwoStream = new FileStream(partTwoFilePath, FileMode.Open, FileAccess.Read))
                {
                    partTwoStream.CopyTo(joinedStream);
                }
            }
        }
    }
}
