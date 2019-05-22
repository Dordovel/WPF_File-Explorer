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
        public event MouseButtonEventHandler list_Item_Selected;
        public event RoutedEventHandler pressButtonBack;

        public event RoutedEventHandler pressButtonMenuItemListViewOpen;
        //public event RoutedEventHandler pressButtonMenuItemListViewCopy;
        //public event RoutedEventHandler pressButtonMenuItemListViewMove;
        //public event RoutedEventHandler pressButtonMenuItemListViewDelete;
        public event RoutedEventHandler pressButtonMenuItemListViewProperty;

        public event RoutedEventHandler pressButtonSearch;

        public event DragCompletedEventHandler eventEditCurrentPositionMediaPlayer;

        #endregion



        public Slider mediaProgressBar;

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
            ButtonSearch.Click += this.pressButtonSearch;

            file_list.MouseDoubleClick += this.list_Item_Selected;

            ButtonBack.Click += this.pressButtonBack;

            MenuItemOpen.Click += this.pressButtonMenuItemListViewOpen;

            /*MenuItemCopy.Click += pressButtonMenuItemListViewCopy;

            MenuItemDelete.Click += pressButtonMenuItemListViewDelete;

            MenuItemMove.Click += pressButtonMenuItemListViewMove;
            */
            MenuItemProperty.Click += this.pressButtonMenuItemListViewProperty;
        }


        public void ShowMediaPlayer(string name)
        {
            StackPanel mediaInfo = new StackPanel ( );

            TextBlock mediaName = new TextBlock ( );

            mediaName.Text = name;
            mediaName.TextAlignment = TextAlignment.Center;

            this.mediaProgressBar = new Slider ( );
            Style style = this.FindResource ( "SliderStyle1" ) as Style;
            mediaProgressBar.Style = style;
            this.mediaProgressBar.Minimum = 0;
            this.mediaProgressBar.Height = 20;

            mediaInfo.Children.Add ( mediaName );
            mediaInfo.Children.Add ( this.mediaProgressBar );

            Grid grid = new Grid ( );

            this.mediaPlayerPanel = new StackPanel ( );
            mediaPlayerPanel.Children.Add ( mediaInfo );

            grid.Children.Add ( this.Create_Media_Button(ImageKey.music_play,HorizontalAlignment.Center,new Thickness(0,0,0,0) ));
            grid.Children.Add ( this.Create_Media_Button ( ImageKey.music_pause , HorizontalAlignment.Right, new Thickness ( 0 , 0 , 80 , 0 ) ) );
            grid.Children.Add ( this.Create_Media_Button ( ImageKey.music_stop , HorizontalAlignment.Left, new Thickness ( 80 , 0 , 0 , 0 ) ) );

            mediaPlayerPanel.Background = Brushes.AliceBlue;

            mediaPlayerPanel.Children.Add ( grid );

            this.stackPanel.Children.Add ( mediaPlayerPanel );

            mediaPlayerPanel.Height = 70;

            this.file_list.Height = this.file_list.Height - 80;
        }

        private Button Create_Media_Button(ImageKey key,HorizontalAlignment horizontalAlignment, Thickness thickness)
        {
            Image imagePause = new Image ( );
            BitmapImage bitmapImage = new BitmapImage ( new Uri (
                Path.Combine ( Environment.CurrentDirectory , "Image" , this.controller.ImageArray [ key ] ) ) );
            imagePause.Source = bitmapImage;
            imagePause.Height = 20;
            imagePause.Width = 20;
            

            Button pause = new Button ( );
            pause.Content = imagePause;
            pause.Height = 25;
            pause.Width = 25;
            pause.Background = Brushes.AliceBlue;
            pause.BorderBrush = Brushes.AliceBlue;
            pause.HorizontalAlignment = HorizontalAlignment;
            pause.Margin = thickness;

            return pause;
        }

        private void Thumb_DragCompleted( object sender , DragCompletedEventArgs e )
        {
            this.MessageBoxShow ( "Hello" );
        }

        public void CloseMediaPlayer()
        {
            this.file_list.Height = this.file_list.Height + 80;

            this.stackPanel.Children.Remove ( this.mediaPlayerPanel );
        }



        public void MessageBoxShow(string message)
        {
            MessageBox.Show ( message );
        }
    }
}
