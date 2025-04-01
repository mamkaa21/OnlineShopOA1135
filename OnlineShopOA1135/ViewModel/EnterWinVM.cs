using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOA1135.ViewModel
{
    public class EnterWinVM
    {
        public EnterWinVM()
        {

        }
        EnterWin enterWindow;
        internal void SetWindow(EnterWin enterWindow)
        {
            this.enterWindow = enterWindow;
        }
        internal void CloseWindow(EnterWin enterWindow)
        {
            this.enterWindow.Close();
        }


    }
}
