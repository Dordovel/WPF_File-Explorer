using System . Windows;
using WpfApp1.File;
using WpfApp1.View;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent ( );
            
            IData personFile=new PersonFile();

            IView view=new View.View();

            Controller.Controller controller = new Controller.Controller(this, personFile, view);

            controller.printFile("D:\\Test\\");
        }
    }
}
