using System;
using System.IO;
using System . Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.File;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event MouseButtonEventHandler list_Item_Selected;
        public event RoutedEventHandler pressButtonBack;

        public event RoutedEventHandler pressButtonMenuItemListViewOpen;
        //public event RoutedEventHandler pressButtonMenuItemListViewCopy;
        //public event RoutedEventHandler pressButtonMenuItemListViewMove;
        //public event RoutedEventHandler pressButtonMenuItemListViewDelete;
        public event RoutedEventHandler pressButtonMenuItemListViewProperty;

        public event RoutedEventHandler pressButtonSearch;

        public ProgressBar mediaProgressBar;

        private StackPanel mediaPlayerPanel;

        public MainWindow()
        {
            InitializeComponent ( );
            
            IData personFile=new PersonFile();

            IView view=new View.View();
                
            Controller.Controller controller = new Controller.Controller(window:this,person_file:personFile, view:view);

            controller.ImageArray.Add ( ImageKey.folder , "../Image/folder.png" );
            controller.ImageArray.Add ( ImageKey.music , "../Image/music.png" );
            controller.ImageArray.Add ( ImageKey.textFile , "../Image/document.png" );
            controller.ImageArray.Add ( ImageKey.hard_drive , "../Image/drive.png" );

            controller.printFile("\\");

            ButtonSearch.Click += this.pressButtonSearch;
            
            file_list.MouseDoubleClick+=this.list_Item_Selected;

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
            StackPanel mediaInfo = new StackPanel();

            TextBlock mediaName = new TextBlock ( );

            mediaName.Text = name;
            mediaName.TextAlignment = TextAlignment.Center;

            this.mediaProgressBar = new ProgressBar ( );
            this.mediaProgressBar.Minimum = 0;
            this.mediaProgressBar.Height = 10;

            mediaInfo.Children.Add ( mediaName );
            mediaInfo.Children.Add ( this.mediaProgressBar );

            Grid grid = new Grid ( );

            Image imagePause = new Image ( );
            BitmapImage bitmapImage = new BitmapImage (new Uri (
                Path.Combine(Environment.CurrentDirectory,"Image","folder.png" )));
            imagePause.Source = bitmapImage;
            imagePause.Height = 20;
            imagePause.Width = 20;

            Button pause = new Button ( );
            pause.Content = imagePause;
            pause.Height = imagePause.Height;
            pause.Width = imagePause.Width;
            pause.HorizontalAlignment = HorizontalAlignment.Center;

            this.mediaPlayerPanel = new StackPanel ( );
            mediaPlayerPanel.Children.Add ( mediaInfo );
            mediaPlayerPanel.Children.Add ( pause );
            mediaPlayerPanel.Background = Brushes.AliceBlue;

            this.stackPanel.Children.Add ( mediaPlayerPanel );

            mediaPlayerPanel.Height = 50;

            this.file_list.Height = this.file_list.Height - 50;
        }

       public void CloseMediaPlayer()
        {
            this.file_list.Height = this.file_list.Height + 50;

            this.stackPanel.Children.Remove ( this.mediaPlayerPanel );
        }

        public void MessageBoxShow(string message)
        {
            MessageBox.Show ( message );
        }
        
    }
}
