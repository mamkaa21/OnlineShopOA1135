using OnlineShopOA1135.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OnlineShopOA1135.View;

namespace OnlineShopOA1135.ViewModel
{
    public class GoodWinVM : BaseVM
    {
        private Good good { get; set; }
        public Good Good
        {
            get => good;
            set
            {
                good = value;
                Signal(nameof(Good));
            }
        }

        private Category category { get; set; }
        public Category Category
        {
            get => category;
            set
            {
                category = value;
                Signal(nameof(Category));
            }
        }

        public Command CloseGoodWin { get; }

      public GoodWinVM() 
      {
            CloseGoodWin = new Command(() => 
            {
                GoodWin goodWin = new GoodWin();
                goodWin.Close();
            
            });
      }

    }
}
