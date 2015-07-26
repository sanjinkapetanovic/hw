using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10D04VHW
{
    class Folder
    {
        public string FolderName { get; set; }
        public string[] Extensions { get; set; }
        List<FileInfo> ListOfFiles { get; set; }
        DirectoryInfo Dir { get; set; }
        
        public Folder(string folderName, string folderPath, string[] extensions)
        {
            this.FolderName = folderName;
            this.Extensions = extensions;
            this.Dir = new DirectoryInfo(folderPath);
            this.ListOfFiles = new List<FileInfo>();

        }
        /// <summary>
        /// This method is moving files to newly created folders
        /// </summary>
        public void MoveFilesToThisFolder()
        {
            if (Directory.Exists(Dir + "\\" + FolderName) == false)
                CreateFolder();
            DirectoryInfo dir = new DirectoryInfo(Dir + "\\" + FolderName);
            foreach (var file in GetFilesPath())
            {
                FileInfo fileInf = new FileInfo(file);
                string newFilePath = dir + "\\" + fileInf.Name;
                if (File.Exists(newFilePath) == false)
                {
                    fileInf.MoveTo(newFilePath);
                    ListOfFiles.Add(fileInf);
                }
            }
        }
        private void CreateFolder()
        {
            Directory.CreateDirectory(this.Dir.FullName + "\\" + this.FolderName);
        }
        private List<string> GetFilesPath()
        {
            List<string> filePath = new List<string>();
            foreach (string ext in this.Extensions)
            {
                filePath.AddRange(Directory.GetFiles(Dir.FullName, ext, SearchOption.AllDirectories));
            }
            return filePath;
        }
    }
}
