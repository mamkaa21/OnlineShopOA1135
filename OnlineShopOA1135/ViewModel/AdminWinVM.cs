using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;

namespace OnlineShopOA1135.ViewModel
{
    public class AdminWinVM : BaseVM
    {
        public Command OrderWinOpen { get; }
        public AdminWinVM() 
        {
            OrderWinOpen = new Command(() =>
            {

            });
        
        
        }
    }
}
