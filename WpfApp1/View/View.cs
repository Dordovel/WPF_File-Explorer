﻿using System;


namespace WpfApp1 . View
{
    class View:IView
    {
        public string Title { get; set; }

        public string Image { get; set; }

        public IView getNewObject()
        {
            return new View();
        }
    }
}
