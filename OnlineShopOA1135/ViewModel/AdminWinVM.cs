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
        public Command CreateWinOpen { get; }
        public Command EditWinOpen { get; }
        public Command DeleteGoods {  get; }
        public Command FindGoodsByTitle { get; }
        public Command UserWinOpen { get; }
        public AdminWinVM() 
        {
            OrderWinOpen = new Command(() =>
            {
                OrderWin orderWin = new OrderWin();
                orderWin.Show();
                Signal();
            });
            CreateWinOpen = new Command(() => 
            { 
                CreateEditWin createEditWin = new CreateEditWin();
                createEditWin.Show();
                Signal();
            });
            EditWinOpen = new Command(() => 
            { 
                //проверка что обьект не пустой и только тогда открыть окно
            });
            DeleteGoods = new Command(() =>
            {
                //запрос на удаление товара + проверка, что товар не пустой
            });
            FindGoodsByTitle = new Command(() => 
            {
                //запрос на поиск товара 
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
