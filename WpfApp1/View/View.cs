using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace WpfApp1 . View
{
    class View:IView
    {
        public string Title { get; set; }

        public IView getNewObject()
        {
            return new View();
        }
    }
}
