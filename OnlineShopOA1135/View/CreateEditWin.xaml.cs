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
using OnlineShopOA1135.Model;
using OnlineShopOA1135.ViewModel;

namespace OnlineShopOA1135.View
{
    /// <summary>
    /// Логика взаимодействия для CreateEditWin.xaml
    /// </summary>
    public partial class CreateEditWin : Window
    {
        public CreateEditWin(Good good)
        {
            InitializeComponent();
            ((CreateEditWinVM)DataContext).Good = good;
            (DataContext as CreateEditWinVM).SetWindow(this);
        }
    }
}
