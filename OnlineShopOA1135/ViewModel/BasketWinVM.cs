using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOA1135.ViewModel
{
    class BasketWinVM
    {
        

        BasketWinVM()
        {

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
