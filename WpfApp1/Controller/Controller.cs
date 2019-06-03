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
            this.list = new ObservableCollection<IView> ( );

            this.Main_Window_Event_subscription ( );

            this.visibleFile = false;
        }

        

        private void Main_Window_Event_subscription()
        {
            this.window.Event_File_List_Item_Selected += Window_list_Item_Selected;

            this.window.EventPressButtonBack += Window_pressButtonBack;

            this.window.EventPressButtonMenuItemListViewOpen += this.Window_pressButtonMenuItemListViewOpen;

            this.window.EventPressButtonMenuItemListViewProperty += this.Window_pressButtonMenuItemListViewProperty;

            this.window.EventPressButtonSearch += this.Window_pressButtonSearch;

            this.window.Closed += this.Window_Closed;

            this.window.EventEditCurrentPositionMediaPlayer += this.Window_eventEditCurrentPositionMediaPlayer;

            this.window.PropertyExpanderExpanded += this.Window_PropertyExpanderExpanded;
        }

        private void Window_PropertyExpanderExpanded( object sender , RoutedEventArgs e )
        {
            if (this.media != null)
            {
                if (this.media.MediaIsPlay)
                {
                    this.window.mediaPlayerWindowinExpander.Visibility = Visibility.Visible;

                    Thread thread = new Thread(() =>
                    {
                        bool flag = false;

                        Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                            (Action) (() =>
                            {
                                flag = this.media.MediaIsPlay;
                                this.window.mediaProgressBar.Maximum = this.media.Duration.TotalSeconds;
                            }));

                        while (flag)
                        {
                            try
                            {
                                Application.Current.Dispatcher.Invoke ( DispatcherPriority.Background ,
                                    ( Action ) ( () =>
                                    {
                                        this.window.mediaProgressBar.Value = this.media.CurrentPosition.TotalSeconds;
                                    } ) );
                            }
                            catch (Exception exception)
                            {
                                return;
                            }
                        }
                    });
                    thread.Start();
                }
            }

        }

        private void Window_eventEditCurrentPositionMediaPlayer( object sender , System.Windows.Controls.Primitives.DragCompletedEventArgs e )
        {
            this.window.MessageBoxShow ( "Hello World" );
        }

        private void Window_Closed( object sender , EventArgs e )
        {
            if ( media != null )
            {
                if ( media.MediaIsPlay )
                {
                    media.Stop ( );
                }
            }
        }


        #region Event

        private void Window_pressButtonMenuItemListViewOpen( object sender , RoutedEventArgs e )
        {
            this.OpenFile_or_Directory ( this.getListViewSelectedItemFromContextMenu ( sender ) );
        }


        private void Window_list_Item_Selected( object sender , System.Windows.Input.MouseButtonEventArgs e )
        {
            var item = ( ( FrameworkElement ) e.OriginalSource ).DataContext as IView;

            if ( item != null )
            {
                this.OpenFile_or_Directory ( item );
            }
        }


        private void Window_pressButtonSearch( object sender , RoutedEventArgs e )
        {


        }


        private void Window_pressButtonBack( object sender , RoutedEventArgs e )
        {
            this.printFile ( this.back ( ) );
        }


        private void Window_pressButtonMenuItemListViewProperty( object sender , RoutedEventArgs e )
        {
            new Property ( this.getPath ( ) + this.getListViewSelectedItemFromContextMenu ( sender ).Title ).ShowDialog ( );
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
        

        private void OpenFile_or_Directory(IView view)
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
                        this.RunMediaPlayer ( path );
                    }
                }
            }
        }


        

        private void RunMediaPlayer( string path )
        {
            if ( this.media != null )
            {
                this.media.Stop ( );
                this.media = null;
            }


            this.media = new Media_Player.Media_Player ( path );

            this.media.Play ();
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

                    this.list.Add(vi);
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

                        this.list.Add ( vi );
                    }
                }
            }

            this.window.file_list.ItemsSource = this.list;
            
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
