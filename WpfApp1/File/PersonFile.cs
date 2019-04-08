using System;
using System . Collections . Generic;
using System.IO;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace WpfApp1 . File
{
    public class PersonFile:IData
    {
        public string[] getFile(string path)
        {
            string [] files=Directory.GetDirectories(path);
            string [] files1=Directory.GetFiles(path);

            string[] temp=new string[files.Length+files1.Length];

            files.CopyTo(temp,0);
            files1.CopyTo(temp,files.Length);

            return temp;
        }
    }
}
