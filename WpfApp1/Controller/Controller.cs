using System;
using System . Collections . Generic;
using System.IO;
using System . Linq;
using System . Text;
using System . Threading . Tasks;
using System.Windows;
using WpfApp1.File;
using WpfApp1.View;

namespace WpfApp1 . Controller
{
    class Controller
    {

        private IData person_file;
        private MainWindow window;
        private IView view;

        public Controller(MainWindow window, IData person_file,IView view)
        {
            this.window = window;
            this.person_file = person_file;
            this.view = view;
        }


        public void printFile(string path)
        {
            List<IView> list=new List<IView>();

            string[] file = person_file.getFile(path);

            foreach (var VARIABLE in file)
            {
                IView vi = this.view.getNewObject();
                vi.Title=new FileInfo(VARIABLE).Name;

                list.Add(vi);
            }

            window.file_list.ItemsSource = list;
        }

    }
}
