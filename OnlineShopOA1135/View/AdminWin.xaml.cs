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
    /// Логика взаимодействия для AdminWin.xaml
    /// </summary>
    public partial class AdminWin : Window
    {
        public AdminWin()
        {
            InitializeComponent();
            (DataContext as AdminWinVM).SetWindow(this);
        }
        private void IsCheckedClick(object sender, RoutedEventArgs e)
        {
            (DataContext as AdminWinVM).ListCategoryClick();

        }
    }
}
