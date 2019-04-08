using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace WpfApp1 . View
{
    interface IView
    {
        string Title { get; set; }

        IView getNewObject();
    } 
}
