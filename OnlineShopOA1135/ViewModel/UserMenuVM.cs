using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopOA1135.Model;


namespace OnlineShopOA1135.ViewModel
{
    public class UserMenuVM : BaseVM
    {
      public Command BasketWinOpen { get; }
        public UserMenuVM() 
        {
            BasketWinOpen = new Command (() =>
            { 
                
            });


        }
    }
}
