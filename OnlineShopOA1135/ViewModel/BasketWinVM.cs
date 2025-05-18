using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using OnlineShopOA1135.Model;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace OnlineShopOA1135.ViewModel
{
    class BasketWinVM : BaseVM
    {
        private Order order {  get; set; }
        public Order Order
        {
            get => order;
            set
            {
                order = value;
                Signal(nameof(Order));
            }
        }

        private List<Order> orderList {  get; set; }
        public List<Order> OrderList
        {
            get => orderList;
            set
            {
                orderList = value;
                Signal(nameof(OrderList));
            }
        }

        private OrderGoodsCross cross { get; set; }
        public OrderGoodsCross Cross
        {
            get => cross;
            set
            {
                cross = value;
                Signal(nameof(Cross));
            }
        }

        private List<OrderGoodsCross> crossList { get; set; }
        public List<OrderGoodsCross> CrossList
        {
            get => crossList;
            set
            {
                crossList = value;
                Signal(nameof(CrossList));
            }
        }

        public Command UserMenuOpen { get; }
        public Command UserWinOpen { get; }
        public BasketWinVM()
        {
            GetOrderBasket();
            UserMenuOpen = new Command(() =>
            {
                UserMenu userMenu = new UserMenu();
                userMenu.Show();
                Signal();
            });
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
        }

        public async void GetOrderBasket()
        {
            string arg = JsonSerializer.Serialize(Order);
            var responce = await HttpClientS.HttpClient.GetAsync($"User/GetOrderBasket");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                OrderList = await responce.Content.ReadFromJsonAsync<List<Order>>();
                return;
            }
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
