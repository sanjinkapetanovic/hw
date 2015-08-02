using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grep
{
    class GrepClass
    {
        /// <summary>
        /// Prints lists of finded lines in one file
        /// </summary>
        /// <param name="list"></param>
        public static void PrintResults(List<string> list)
        {
            foreach (string str in list)
            {
                Console.WriteLine(str);
            }
        }
        /// <summary>
        /// Prints results from indexed file
        /// </summary>
        /// <param name="filePath"></param>
        public static void PrintResultFromFile(string filePath)
        {
            string line = "";
            using (StreamReader sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        /// <summary>
        /// This method is called when the program starts. It initializes Indexing folder, searchs if it is there valid index file and opens a stream  to write new index files
        /// </summary>
        /// <param name="commands"></param>
        public static void ApplyTheCommand(string[] commands)
        {
            string folderPath = Directory.GetCurrentDirectory();
            Indexing.InitializeIndexing(folderPath);
            string fileName = Indexing.GetFileNameFromCommand(commands);
            if (Indexing.IndexFileValid(fileName))
            {
                PrintResultFromFile(fileName);
                return;
            }
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                List<FileInfo> list = GetFiles(commands[1], folderPath);
                if (list.Count > 0)
                {
                    foreach (var file in list)
                    {
                        PrintResults(SearchFile(file, commands[0], sw));
                    }
                }
                else
                {
                    Console.WriteLine("There is no such files");
                }
            }
        }
        /// <summary>
        /// This method is returning a list of files with searched extension. There are some tasks for better visual expirience. They are bad for program performance
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<FileInfo> GetFiles(string searchPattern, string folderPath)
        {
            List<string> fileListString = new List<string>();
            Task getFilesTask = new Task(() =>
                {
                    fileListString = Directory.GetFiles(folderPath, searchPattern, SearchOption.AllDirectories).Where(x => !x.StartsWith(folderPath + "\\Indexing")).ToList();
                });
            getFilesTask.Start();
            while (!getFilesTask.IsCompleted)
            {
                Console.WriteLine("Searching files...");
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(30);
                    DrawProgressBar(i, 100, 100, '|');
                }
            }
            List<FileInfo> fileListInfo = new List<FileInfo>();
            foreach (string file in fileListString)
            {
                fileListInfo.Add(new FileInfo(file));
            }


            return fileListInfo;
        }
        /// <summary>
        /// This method is searching file and for search pattern, it also has task for progress bar
        /// </summary>
        /// <param name="file"></param>
        /// <param name="searchTerm"></param>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static List<string> SearchFile(FileInfo file, string searchTerm, StreamWriter sw)
        {
            List<string> containingLines = new List<string>();
            string line = "";
            int lineNum = 1;
            Task searchFileTask = new Task(() =>
            { 
            using (StreamReader sr = new StreamReader(file.FullName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(searchTerm))
                    {
                        string resultLine = line + " from file: " + file.FullName + " on line: " + lineNum;
                        containingLines.Add(resultLine);
                        sw.WriteLine(resultLine);
                    }
                    lineNum++;
                }
            }
            });
            searchFileTask.Start();
            while (!searchFileTask.IsCompleted)
            {
                Console.WriteLine("Searching file " + file.FullName);
                for (int i = 0; i <= 100; i++)
                {
                    Thread.Sleep(20);
                    DrawProgressBar(i, 100, 100, '|');
                }
            }
            return containingLines;
            }
        /// <summary>
        /// This is method for drawing progress bar
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="maxVal"></param>
        /// <param name="barSize"></param>
        /// <param name="progressCharacter"></param>
        private static void DrawProgressBar(int complete, int maxVal, int barSize, char progressCharacter)
        {
            Console.CursorVisible = false;
            int left = Console.CursorLeft;
            decimal perc = (decimal)complete / (decimal)maxVal;
            int chars = (int)Math.Floor(perc / ((decimal)1 / (decimal)barSize));
            string p1 = String.Empty, p2 = String.Empty;
            for (int i = 0; i < chars; i++) p1 += progressCharacter;
            for (int i = 0; i < barSize - chars; i++) p2 += progressCharacter;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(p1);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.ResetColor();
            Console.Write(" {0}%", (perc * 100).ToString("N2"));
            Console.CursorLeft = left;
        }


    }
}
