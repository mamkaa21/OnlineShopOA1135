using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;


namespace OnlineShopOA1135.ViewModel
{
    public class UserMenuVM : BaseVM
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

        private List<Good> goodList { get; set; }
        public List<Good> GoodList
        {
            get => goodList;
            set
            {
                goodList = value;
                Signal(nameof(GoodList));
            }
        }

        public Command BasketWinOpen { get; }
        public Command UserWinOpen { get; }
        public UserMenuVM()
        {
            GetGoods();
            BasketWinOpen = new Command(() =>
            {
                BasketWin basketWin = new BasketWin();
                basketWin.Show();
                Signal();
            });
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
        }

        public async void GetGoods()
        {
            string arg = JsonSerializer.Serialize(Good);
            var responce = await HttpClientS.HttpClient.GetAsync($"Admin/getGoods");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show("succes");
                return;
            }
        }

    }
}
