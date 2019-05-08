using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Property.xaml
    /// </summary>

    class CustomFileInfo
    {
        public static Dictionary<string,string> GetFileInfo(string path)
        {
            var dir = Path.GetDirectoryName ( path );
            var file = Path.GetFileName ( path );

            var shell = new Shell32.Shell ( );
            var folder = shell.NameSpace ( dir );
            var folderItem = folder.ParseName ( file );

            var names =
                ( from idx in Enumerable.Range ( 0 , short.MaxValue )
                    let key = folder.GetDetailsOf ( null , idx )
                    where !string.IsNullOrEmpty ( key )
                    select new KeyValuePair<int , string> ( idx , key ) ).ToDictionary ( p => p.Key , p => p.Value );

            var properties =
                ( from idx in names.Keys
                    orderby idx
                    let value = folder.GetDetailsOf ( folderItem , idx )
                    where !string.IsNullOrEmpty ( value )
                    select new KeyValuePair<int , string> ( idx , value ) ).ToDictionary ( p => p.Key , p => p.Value );

            Dictionary<string, string> temp=new Dictionary<string, string>();

            foreach (var VARIABLE in properties)
            {
                temp.Add(
                    names[VARIABLE.Key],
                    properties[VARIABLE.Key]
                    );

            }

            properties = null;
            names = null;

            GC.Collect();

            return temp;
        }
        
    }


    public partial class Property : Window
    {
        public Property(string path)
        {
            InitializeComponent ( );

            var temp = CustomFileInfo.GetFileInfo ( path );  

            foreach (var VARIABLE in temp)
            {
                this.InfoLabel.Text += $"{VARIABLE.Key}\n";
                this.Info.Text += $"{VARIABLE.Value}\n";
            }

        }
    }
}
