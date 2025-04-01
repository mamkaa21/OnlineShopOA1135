using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;


namespace OnlineShopOA1135.ViewModel
{
    public class UserMenuVM : BaseVM
    {
        public Command BasketWinOpen { get; }
        public Command UserWinOpen { get; }
        public UserMenuVM()
        {
            BasketWinOpen = new Command(() =>
            {
                BasketWin basketWin = new BasketWin();
                basketWin.Show();
                Signal();
            });
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
        }
    }
}
