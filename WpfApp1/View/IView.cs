﻿using System;

namespace WpfApp1 . View
{
    public interface IView
    {
        string Title { get; set; }
        string Image { get; set; }

        IView getNewObject();
    }
}
