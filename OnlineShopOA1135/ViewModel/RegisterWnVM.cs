using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopOA1135.Model;

namespace OnlineShopOA1135.ViewModel
{
    public class RegisterWnVM
    {
        public User User = new User();
        public Command EnterWinOpen { get; }
        public RegisterWnVM() 
        {
            EnterWinOpen = new Command(async() =>
            {

            });
        }
        RegisterWin registerWindow;
        internal void SetWindow(RegisterWin registerWindow)
        {
            this.registerWindow = registerWindow;
        }
        internal void CloseWindow(RegisterWin registerWindow)
        {
            this.registerWindow.Close();
        }

    }
}
