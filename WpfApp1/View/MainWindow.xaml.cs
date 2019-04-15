using System;
using System . Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public event RoutedEventHandler pressButtonMenuItemListViewCopy;
        public event RoutedEventHandler pressButtonMenuItemListViewMove;
        public event RoutedEventHandler pressButtonMenuItemListViewDelete;
        public event RoutedEventHandler pressButtonMenuItemListViewProperty;

        public MainWindow()
        {
            InitializeComponent ( );
            
            IData personFile=new PersonFile();

            IView view=new View.View();
                
            Controller.Controller controller = new Controller.Controller(window:this,person_file:personFile, view:view);

            controller.ImageArray.Add ( Image.folder , "../Image/folder.png" );
            controller.ImageArray.Add ( Image.music , "../Image/music.png" );
            controller.ImageArray.Add ( Image.textFile , "../Image/document.png" );
            controller.ImageArray.Add ( Image.hard_drive , "../Image/drive.png" );

            controller.printFile("\\");
            
            file_list.MouseDoubleClick+=list_Item_Selected;

            ButtonBack.Click += pressButtonBack;

            MenuItemOpen.Click += pressButtonMenuItemListViewOpen;

            /*MenuItemCopy.Click += pressButtonMenuItemListViewCopy;

            MenuItemDelete.Click += pressButtonMenuItemListViewDelete;

            MenuItemMove.Click += pressButtonMenuItemListViewMove;

            MenuItemProperty.Click += pressButtonMenuItemListViewProperty;
            */
        }
    }
}
