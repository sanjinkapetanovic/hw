using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grep
{
    public static class Indexing
    {
        static public DirectoryInfo IndexFolder { get; set; }
        public static void InitializeIndexing(string folderPath)
        {
            IndexFolder = Directory.CreateDirectory(folderPath + "\\Indexing");
        }
        /// <summary>
        /// This method is converting array of strings into one string which is file name after
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static string GetFileNameFromCommand(string[] command)
        {
            string fileName = "";
            switch (command[1])
            {
                case "*.*":
                    fileName = "all_" + command[0];
                    break;
                case "*.txt":
                    fileName = "txt_" + command[0];
                    break;
                case "*.html":
                    fileName = "html_" + command[0];
                    break;
                default:
                    fileName = command[1] + "_" + command[0];
                    break;
            }
            return IndexFolder.FullName + "\\" + fileName + ".txt";
        }
        /// <summary>
        /// This method is searching if index file exists an if it is older than one day
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IndexFileValid(string fileName)
        {
            FileInfo IndexFile = new FileInfo(fileName);
            if (File.Exists(IndexFile.FullName) && (System.DateTime.Now.Day - File.GetCreationTime(IndexFile.FullName).Day) < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
