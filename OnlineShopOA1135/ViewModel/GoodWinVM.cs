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

        private Good review { get; set; }
        public Good Review
        {
            get => review;
            set
            {
                review = value;
                Signal(nameof(Review));
            }
        }

        private List<Good> reviewGood {  get; set; }
        public List<Good> ReviewGood
        {
            get => reviewGood;
            set
            {
                reviewGood = value;
                Signal(nameof(ReviewGood));
            }
        }
        public Command UserWinOpen { get; }
        public Command CloseGoodWin { get; }

      public GoodWinVM() 
      {
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
        }



        GoodWin goodWin;
        internal void SetWindow(GoodWin goodWin)
        {
            this.goodWin = goodWin;
        }

        internal void CloseWindow(GoodWin goodWin)
        {
           goodWin.Close();
        }
    }
}
