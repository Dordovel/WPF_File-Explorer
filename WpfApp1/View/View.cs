using System;


namespace WpfApp1 . View
{
    public class Source
    {

    }


    class View:IView
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public IView getNewObject() => new View ( );
    }
}
