using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOA1135.ViewModel
{
    public class RegisterWnVM
    {
        RegisterWnVM() 
        {
        
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
