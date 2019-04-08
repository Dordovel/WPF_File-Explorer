using System;
using System.IO;

namespace WpfApp1 . File
{
    public class PersonFile:IData
    {
        public string[] getFile(string path)
        {
            if (path == "\\")
            {
                return Environment.GetLogicalDrives();
            }

            string[] files = Directory.GetDirectories(path);
            string[] files1 = Directory.GetFiles(path);

            string[] temp = new string[files.Length + files1.Length];

            files.CopyTo(temp, 0);
            files1.CopyTo(temp, files.Length);

            return temp;
        }
    }
}
