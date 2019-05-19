using System;
using System . Collections . Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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

        public Dictionary<ImageKey, string> ImageArray { set; get; }
        ObservableCollection<IView> list;
        Media_Player.Media_Player media;




        public Controller(MainWindow window, IData person_file, IView view)
        {
            this.window = window;
            this.person_file = person_file;
            this.view = view;

            this.pathList = new List<string>();
            this.ImageArray=new Dictionary<ImageKey, string>();
            list = new ObservableCollection<IView> ( );

            this.Main_Window_Event_subscription ( );

            this.media = new Media_Player.Media_Player ( );

            visibleFile = false;
        }




        private void Main_Window_Event_subscription()
        {
            this.window.list_Item_Selected += Window_list_Item_Selected;

            this.window.pressButtonBack += Window_pressButtonBack;

            this.window.pressButtonMenuItemListViewOpen += this.Window_pressButtonMenuItemListViewOpen;

            this.window.pressButtonMenuItemListViewProperty += this.Window_pressButtonMenuItemListViewProperty;

            this.window.pressButtonSearch += this.Window_pressButtonSearch;

            this.window.Unloaded += this.Window_Unloaded;
        }




        private void Window_Unloaded( object sender , RoutedEventArgs e )
        {
            if ( media != null )
            {
                if ( media.MediaIsPlay )
                {
                    media.Close ( );
                }
            }
        }




        private void Window_pressButtonSearch( object sender , RoutedEventArgs e )
        {
            
            
        }




        private void Window_pressButtonMenuItemListViewProperty( object sender , RoutedEventArgs e )
        {
            new Property(this.getPath()+this.getListViewSelectedItemFromContextMenu(sender).Title).ShowDialog();
        }


        #region Event

        #region ContextMenuEvent
        private void Window_pressButtonMenuItemListViewOpen( object sender , RoutedEventArgs e )
        {
            this.Open ( this.getListViewSelectedItemFromContextMenu ( sender ) );
        }


        #endregion


        private void Window_list_Item_Selected( object sender , System.Windows.Input.MouseButtonEventArgs e )
        {
            var item = ( ( FrameworkElement ) e.OriginalSource ).DataContext as IView;

            if ( item != null )
            {
                this.Open ( item );
            }
        }




        private void Window_pressButtonBack( object sender , RoutedEventArgs e )
        {
            for ( int a = 0 ; a < this.list.Count ; ++a )
            {
                this.list [ a ] = null;
            }

            GC.Collect ( );

            this.printFile ( this.back ( ) );
        }



        #endregion

        private IView getListViewSelectedItemFromContextMenu(object sender)
        {
            MenuItem menu = sender as MenuItem;

            ListView lvi = ( ( ContextMenu ) menu.Parent ).PlacementTarget as ListView;

            return lvi.SelectedItem as IView;
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
        

        private void Open(IView view)
        {
            string path = this.getPath ( ) + (view.Title);

            FileAttributes attr = System.IO.File.GetAttributes ( path );

            if ( attr.HasFlag ( FileAttributes.Directory ) )
            {
                this.pathList.Add ( view.Title + "\\" );

                this.printFile ( this.getPath ( ) );
            }
            else
            {

                FileInfo info = new FileInfo ( path );

                foreach ( string format in Media_Player.Media_Player.supportMediaFormat )
                {
                    if ( info.Extension.ToLower ( ).Contains ( format.ToLower ( ) ) )
                    {
                        RunMediaPlayer ( path );
                    }
                }
            }
        }

        private void RunMediaPlayer( string path )
        {

            if ( this.media.MediaIsPlay )
            {
                media.Stop ( );
                this.window.CloseMediaPlayer();
            }

            this.media.Play ( path );

            Thread.Sleep ( 500 );

            this.window.ShowMediaPlayer ( Path.GetFileName ( path ) );

            this.window.mediaProgressBar.Maximum = this.media.Duration;

            Thread thread = new Thread ( () =>
            {
                while ( this.media.MediaIsPlay )
                {
                    Application.Current.Dispatcher.BeginInvoke (DispatcherPriority.Background, ( Action ) ( () =>
                              {
                                  this.window.mediaProgressBar.Value = this.media.CurrentPosition;
                              } ) );
                }
                      });
            thread.Start ( );

            
        }



        public void printFile(string path)
        {
            this.list.Clear ( );

            string [ ] file = person_file.getFile(path);

            if (path == "\\")
            {
                foreach (var VARIABLE in file)
                {
                    IView vi = this.view.getNewObject();
                    vi.Title = VARIABLE;
                    vi.Image = this.ImageArray[ImageKey.hard_drive];

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
                            vi.Image = this.ImageArray[ImageKey.folder];
                        }

                        else if ( info.Extension.Contains("txt") )
                        {
                            vi.Image = this.ImageArray [ ImageKey.textFile ];
                        }

                        else if ( info.Extension.Contains ( "mp3" ) )
                        {
                            vi.Image = this.ImageArray [ ImageKey.music ];
                        }

                        else if (info.Extension.Contains("jpg")
                                 || info.Extension.Contains("png"))
                        {
                            vi.Image = VARIABLE;
                        }

                        info = null;

                        list.Add ( vi );
                    }
                }
            }

            window.file_list.ItemsSource = list;
            
        }




        public string back()
        {
            this.list.Clear();

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
