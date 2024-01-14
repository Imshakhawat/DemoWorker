using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Worker
{
    public class Tracker
    {

        private const string FilePath = "D:/Codes/VS 2022/Asp Codes/shakhawat_-aspnet-b8/CSEData.Worker/Logs/rotation.txt";

        public static void PrintRotation()
        {
            int rotationCount = ReadRotationCount();
            rotationCount++;

            string rotationText = $"rotation {rotationCount}";

            // Append the new rotation count to the file
            File.AppendAllText(FilePath, rotationText + Environment.NewLine);

            // Print the rotation count to the console
           // Console.WriteLine(rotationText);
        }

        private static int ReadRotationCount()
        {
            if (File.Exists(FilePath))
            {
                // Read the contents of the file and get the last rotation count
                string[] lines = File.ReadAllLines(FilePath);
                if (lines.Length > 0)
                {
                    string lastLine = lines[lines.Length - 1];
                    if (lastLine.StartsWith("rotation "))
                    {
                        int lastRotation = int.Parse(lastLine.Substring(9));
                        return lastRotation;
                    }
                }
            }

            // If the file is empty or not found, start with rotation 0
            return 0;
        }


    }

}
