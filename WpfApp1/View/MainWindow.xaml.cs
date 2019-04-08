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

        public MainWindow()
        {
            InitializeComponent ( );
            
            IData personFile=new PersonFile();

            IView view=new View.View();

            Controller.Controller controller = new Controller.Controller(this, personFile, view);

            controller.printFile("\\");
            
            file_list.MouseDoubleClick+=list_Item_Selected;
        }
    }
}
