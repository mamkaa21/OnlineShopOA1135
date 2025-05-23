using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OnlineShopOA1135.ViewModel
{
    public class OrderWinVM : BaseVM
    {
        private Order order { get; set; }
        public Order Order
        {
            get => order;
            set
            {
                order = value;
                Signal(nameof(Order));
            }
        }

        private List<Order> orderList { get; set; }
        public List<Order> OrderList
        {
            get => orderList;
            set
            {
                orderList = value;
                Signal(nameof(OrderList));
            }
        }

        private OrderGoodsCross crossActive { get; set; }
        public OrderGoodsCross CrossActive
        {
            get => crossActive;
            set
            {
                crossActive = value;
                Signal(nameof(CrossActive));
            }
        }

        private List<OrderGoodsCross> crossListActive { get; set; }
        public List<OrderGoodsCross> CrossListActive
        {
            get => crossListActive;
            set
            {
                crossListActive = value;
                Signal(nameof(CrossListActive));
            }
        }

        private OrderGoodsCross crossDontActive { get; set; }
        public OrderGoodsCross CrossDontActive
        {
            get => crossDontActive;
            set
            {
                crossDontActive = value;
                Signal(nameof(CrossDontActive));
            }
        }

        private List<OrderGoodsCross> crossListDontActive { get; set; }
        public List<OrderGoodsCross> CrossListDontActive
        {
            get => crossListDontActive;
            set
            {
                crossListDontActive = value;
                Signal(nameof(CrossListDontActive));
            }
        }
        public ICommand DoubleClickCommand { get; }
        public Command UserWinOpen { get; }
        public Command StatusOrderFromDontActive { get; }
        public Command Update { get; }
        public OrderWinVM()
        {
            GetOrderActive();
            GetOrderDontActive();
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
            DoubleClickCommand = new RelayCommand(DoubleClickExecute);
            StatusOrderFromDontActive = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(CrossActive);
                var responce = await HttpClientS.HttpClient.PutAsync($"User/StatusOrderFromDontActive", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("error");
                    return;
                }
                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetOrderActive();
                    MessageBox.Show("ok");

                }
            });
            Update = new Command(() =>
            {
                GetOrderActive();
                GetOrderDontActive();
            });
        }

        
        private void DoubleClickExecute(object parameter)
        {

            if (parameter is Good Good)
            {
                GoodWin goodWin = new GoodWin(Good);
                goodWin.Show();
                Signal();
            }
        }

        public async void GetOrderActive()
        {
            string arg = JsonSerializer.Serialize(CrossActive);
            var responce = await HttpClientS.HttpClient.GetAsync($"Admin/GetOrderActive");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CrossListActive = await responce.Content.ReadFromJsonAsync<List<OrderGoodsCross>>();
                return;
            }
        }
        public async void GetOrderDontActive()
        {
            string arg = JsonSerializer.Serialize(CrossDontActive);
            var responce = await HttpClientS.HttpClient.GetAsync($"Admin/GetOrderDontActive");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CrossListDontActive = await responce.Content.ReadFromJsonAsync<List<OrderGoodsCross>>();
                return;
            }
        }

       

        OrderWin orderWin;
        internal void SetWindow(OrderWin orderWin)
        {
            this.orderWin = orderWin;
        }
        internal void CloseWindow(OrderWin orderWin)
        {
            this.orderWin.Close();
        }
    }
}
