using System;

namespace WpfApp1 . View
{
    interface IView
    {
        string Title { get; set; }

        IView getNewObject();
    } 
}
