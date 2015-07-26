using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10D04VHW
{
    class FolderUtility
    {
        public string[] PictureFilesExtensions { get; set; }
        public string[] MoviesFilesExtensions { get; set; }
        public string[] MusicFilesExtensions { get; set; }
        public string[] TextFilesExtensions { get; set; }
        public DirectoryInfo Dir { get; set; }
        public FolderUtility(string folderPath)
        {
            this.TextFilesExtensions = new string[] { "*.doc", "*.docx", "*.log", "*.msg", "*.odt", "*.pages", "*.tex", "*.txt", "*.wpd", "*.wps", "*.pdf" };
            this.MusicFilesExtensions = new string[] { "*.aif", "*.iff", "*.m3u", "*.m4a", "*.mid", "*.mp3", "*.mpa", "*.ra", "*.wav", "*.wma"};
            this.MoviesFilesExtensions = new string[] { "*.3g2", "*.3gp", "*.asf", "*.asx", "*.avi", "*.flv", "*.m4v", "*.mov", "*.mp4", "*.mpg", "*.rm", "*.srt", "*.swf", "*.vob", "*.wmv", "*.mkv" };
            this.PictureFilesExtensions = new string[] { "*.bmp", "*.dds", "*.gif", "*.jpg", "*.png", "*.psd", "*.pspimage", "*.tga", "*.thm", "*.tif", "*.tiff", "*.yuv", "*.obj", "*.max", "*.3ds", "*.3dm", "*.ai", "*.eps", "*.ps", "*.svg" };
            this.Dir = new DirectoryInfo(folderPath);
        }
        public void DeleteEmptyFolder()
        {
            DeleteEmptyFolder(Dir.FullName);
        }
        /// <summary>
        /// this method is deleting empty folders via recursion
        /// </summary>
        /// <param name="folderPath"></param>
        private void DeleteEmptyFolder(string folderPath)
        {
            var listOfFolders = Directory.GetDirectories(folderPath);
            if (listOfFolders.Length > 0)
            {
                foreach (var item in listOfFolders)
                {
                    DeleteEmptyFolder(item);
                }
            }
            if (Directory.GetDirectories(folderPath).Length == 0 && Directory.GetFiles(folderPath).Length == 0)
            {
                Directory.Delete(folderPath);
            }
        }
        /// <summary>
        /// this method is printing log to the txt file in root folder
        /// </summary>
        public void PrintLogTxtInFolder()
        {
            var fileList = Directory.GetFiles(Dir.FullName,"*" ,SearchOption.AllDirectories);
            using(StreamWriter sw = new StreamWriter(Dir.FullName+"\\log.txt"))
            {
                foreach(var item in fileList)
                {
                    sw.WriteLine(item);
                }

            }
        }

    }
}
