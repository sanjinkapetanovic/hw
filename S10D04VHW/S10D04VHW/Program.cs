using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10D04VHW
{
    class Program
    {
        /// <summary>
        /// This methos is making object instances and move files to folders
        /// </summary>
        /// <param name="folderPath"></param>
        static void MoveFiles(string folderPath)
        {
            FolderUtility utility = new FolderUtility(folderPath);
            Folder music = new Folder("Music", folderPath, utility.MusicFilesExtensions);
            Folder movies = new Folder("Movies", folderPath, utility.MoviesFilesExtensions);
            Folder pictures = new Folder("Pictures", folderPath, utility.PictureFilesExtensions);
            Folder text = new Folder("Documents", folderPath, utility.TextFilesExtensions);
            music.MoveFilesToThisFolder();
            movies.MoveFilesToThisFolder();
            pictures.MoveFilesToThisFolder();
            text.MoveFilesToThisFolder();
            DeleteOption(utility);
            PrintLogOption(utility);
        }
        /// <summary>
        /// This method is prompting to console to-delete folder question
        /// </summary>
        /// <param name="utility"></param>
        static void DeleteOption(FolderUtility utility)
        {
            Console.WriteLine("Type y if you want to delete empty folders");
            string option = Console.ReadLine();
            if (option=="y")
            {
                utility.DeleteEmptyFolder();
            }
        }
        /// <summary>
        /// This method is prompting to console  question to print log to the file
        /// </summary>
        /// <param name="utility"></param>
        static void PrintLogOption(FolderUtility utility)
        {
            Console.WriteLine("Type y if you want to print log to root folder");
            string option = Console.ReadLine();
            if (option == "y")
            {
                utility.PrintLogTxtInFolder();
            }
        } 
        /// <summary>
        /// This method is checking is the entered path existing and calling other methods for moving files
        /// </summary>
        static void Start()
        {
            string folderPath = " ";
            string notExists = "";
            while (folderPath != "")
            {
                Console.Clear();
                Console.Write(notExists);
                Console.WriteLine("Please enter path of folder to clean or only press enter to leave");
                folderPath = Console.ReadLine();
                if (Directory.Exists(folderPath))
                    MoveFiles(folderPath);
                else
                    notExists = "Typed folder not exists!\n";
            }
        }  
        static void Main(string[] args)
        {
            Start();            
        }
    }
}
