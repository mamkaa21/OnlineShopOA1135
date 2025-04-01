using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOA1135.ViewModel
{
    class BasketWinVM : BaseVM
    {
        public Command UserMenuOpen { get; }

        public Command UserWinOpen { get; }
        public BasketWinVM()
        {
            UserMenuOpen = new Command(() =>
            {
                UserMenu userMenu = new UserMenu();
                userMenu.Show();
                Signal();
            });
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
        }
        BasketWin basketWin;
        internal void SetWindow(BasketWin basketWin)
        {
            this.basketWin = basketWin;
        }
        internal void CloseWindow(EnterWin basketWin)
        {
            this.basketWin.Close();
        }
    }
}
