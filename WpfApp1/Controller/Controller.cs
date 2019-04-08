﻿using System . Collections . Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using WpfApp1.File;
using WpfApp1.View;

namespace WpfApp1 . Controller
{
    class Controller
    {

        private string path;

        private IData person_file;
        private MainWindow window;
        private IView view;

        public Controller(MainWindow window, IData person_file,IView view)
        {
            this.window = window;
            this.person_file = person_file;
            this.view = view;

            this . window . list_Item_Selected += Window_list_Item_Selected; ; 
        }

        private void Window_list_Item_Selected( object sender , System . Windows . Input . MouseButtonEventArgs e )
        {
            var item = ( ( FrameworkElement ) e . OriginalSource ) . DataContext as View.View;

            if ( item != null )
            {
                path += item.Title + "\\";
                printFile (path);
            }
        }

        public void printFile(string path)
        {
            List<IView> list = new List<IView>();


            string[] file = person_file.getFile(path);

            if (path=="\\")
            {
                foreach (var VARIABLE in file)
                {
                    IView vi = this.view.getNewObject();
                    vi.Title = VARIABLE;

                    list.Add(vi);
                }
            }
            else
            {
                foreach ( var VARIABLE in file )
                {
                    IView vi = this . view . getNewObject ( );

                    vi . Title = new FileInfo ( VARIABLE ) . Name;

                    list . Add ( vi );
                }
            }

            window.file_list.ItemsSource = list;
        }

    }
}
