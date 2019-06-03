using System;
using System.IO;
using System . Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.File;
using WpfApp1.View;
using System.Windows.Controls.Primitives;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region MainWindowEvent
        public event MouseButtonEventHandler Event_File_List_Item_Selected;
        public event RoutedEventHandler EventPressButtonBack;

        public event RoutedEventHandler EventPressButtonMenuItemListViewOpen;
        //public event RoutedEventHandler pressButtonMenuItemListViewCopy;
        //public event RoutedEventHandler pressButtonMenuItemListViewMove;
        //public event RoutedEventHandler pressButtonMenuItemListViewDelete;
        public event RoutedEventHandler EventPressButtonMenuItemListViewProperty;

        public event RoutedEventHandler EventPressButtonSearch;

        public event DragCompletedEventHandler EventEditCurrentPositionMediaPlayer;

        public event RoutedEventHandler PropertyExpanderExpanded;

        #endregion

        private StackPanel mediaPlayerPanel;

        private IData personFile;

        private IView view;

        Controller.Controller controller;




        public MainWindow()
        {
            InitializeComponent ( );
            
            this.personFile=new PersonFile();

            this.view=new View.View();
                
            this.controller = new Controller.Controller(window:this,person_file:personFile, view:view);

            this.controller.ImageArray.Add ( ImageKey.folder , "../Image/folder.png" );
            this.controller.ImageArray.Add ( ImageKey.music , "../Image/music.png" );
            this.controller.ImageArray.Add ( ImageKey.textFile , "../Image/document.png" );
            this.controller.ImageArray.Add ( ImageKey.hard_drive , "../Image/drive.png" );
            this.controller.ImageArray.Add ( ImageKey.music_play , "../Image/play.png" );
            this.controller.ImageArray.Add ( ImageKey.music_pause , "../Image/pause.png" );
            this.controller.ImageArray.Add ( ImageKey.music_stop , "../Image/stop.png" );

            this.controller.printFile("\\");


            this.event_subscription ( );
            

        }

        private void event_subscription()
        {
            ButtonSearch.Click += this.EventPressButtonSearch;

            file_list.MouseDoubleClick += this.Event_File_List_Item_Selected;

            ButtonBack.Click += this.EventPressButtonBack;

            MenuItemOpen.Click += this.EventPressButtonMenuItemListViewOpen;

            /*MenuItemCopy.Click += pressButtonMenuItemListViewCopy;

            MenuItemDelete.Click += pressButtonMenuItemListViewDelete;

            MenuItemMove.Click += pressButtonMenuItemListViewMove;
            */
            MenuItemProperty.Click += this.EventPressButtonMenuItemListViewProperty;

            propertyExpander.Expanded += this.PropertyExpanderExpanded;
        }



        private void Thumb_DragCompleted( object sender , DragCompletedEventArgs e )
        {
            this.MessageBoxShow ( "Hello" );
        }




        public void MessageBoxShow(string message)
        {
            MessageBox.Show ( message );
        }
    }
}
