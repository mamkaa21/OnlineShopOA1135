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
    /// Логика взаимодействия для UserWin.xaml
    /// </summary>
    public partial class UserWin : Window
    {
        public UserWin()
        {
            InitializeComponent();
            (DataContext as UserWinVM).SetWindow(this);
        }
    }
}
