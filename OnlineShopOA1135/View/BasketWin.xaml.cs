﻿using OnlineShopOA1135.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OnlineShopOA1135.View
{
    /// <summary>
    /// Логика взаимодействия для BasketWin.xaml
    /// </summary>
    public partial class BasketWin : Window
    {
        public BasketWin()
        {
            InitializeComponent();
            (DataContext as BasketWinVM).SetWindow(this);
        }
    }
}
