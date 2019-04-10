using System . Collections . Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using WpfApp1.File;
using WpfApp1.View;

namespace WpfApp1 . Controller
{
    public class Controller
    {
        private List<string> pathList;

        private bool visibleFile;

        private IData person_file;
        private MainWindow window;
        private IView view;

        public Dictionary<Image, string> ImageArray { set; get; }

        public Controller(MainWindow window, IData person_file, IView view)
        {
            this.window = window;
            this.person_file = person_file;
            this.view = view;

            this.pathList = new List<string>();
            this.ImageArray=new Dictionary<Image, string>();

            this.window.list_Item_Selected += Window_list_Item_Selected;
            this . window . pressButtonBack += Window_pressButtonBack;

            visibleFile = false;
        }

        private void Window_pressButtonBack( object sender , RoutedEventArgs e )
        {
           this.printFile( this . back ( ) );
        }

        public string getPath()
        {
            string temp = string . Empty;

            foreach ( var VARIABLE in this . pathList )
            {
                temp += VARIABLE;
            }

            return temp;
        }

        private void Window_list_Item_Selected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement) e.OriginalSource).DataContext as View.View;

            if (item != null)
            {
                this.pathList.Add(item.Title + "\\");

                printFile(this.getPath());
            }
        }

        

        public void printFile(string path)
        {
            ObservableCollection<IView> list = new ObservableCollection<IView>();

            string[] file = person_file.getFile(path);

            if (path == "\\")
            {
                foreach (var VARIABLE in file)
                {
                    IView vi = this.view.getNewObject();
                    vi.Title = VARIABLE;
                    vi.Image = this.ImageArray[Image.hard_drive];

                    list.Add(vi);
                }
            }
            else
            {
                foreach (var VARIABLE in file)
                {
                    IView vi = this.view.getNewObject();

                    FileInfo info = new FileInfo(VARIABLE);

                    FileAttributes attr = System.IO.File.GetAttributes(VARIABLE);

                    if (this.visibleFile || (info.Attributes & FileAttributes.Hidden) == 0)
                    {
                        vi.Title=info.Name;

                        if (attr.HasFlag(FileAttributes.Directory))
                        {
                            vi.Image = this.ImageArray[Image.folder];
                        }

                        else if ( info.Extension.Contains("txt") )
                        {
                            vi.Image = this.ImageArray [ Image.textFile ];
                        }

                        else if ( info.Extension.Contains ( "mp3" ) )
                        {
                            vi.Image = this.ImageArray [ Image.music ];
                        }

                        list.Add ( vi );
                    }
                }
            }

            window.file_list.ItemsSource = list;
        }

        public string back()
        {
            if (this.pathList.Count == 0)
            {
                return "\\";
            }

            this.pathList.Remove(this.pathList[this.pathList.Count - 1]);

            string temp = string.Empty;

            foreach (var VARIABLE in this.pathList)
            {
                temp += VARIABLE;
            }

            if (temp == string.Empty)
            {
                return "\\";
            }

            return temp;

        }

    }
}
