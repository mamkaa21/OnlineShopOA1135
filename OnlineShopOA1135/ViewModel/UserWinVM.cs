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
    public class UserWinVM : BaseVM
    {
        private User user;
        public User User
        {
            get => user;
            set
            {
                user = value;
                Signal(nameof(User));
            }
        }
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

        private bool listBoxEnabled = false; // Изначально отключен
        public bool ListBoxEnabled
        {
            get => listBoxEnabled;
            set
            {
                if (listBoxEnabled != value)
                {
                    listBoxEnabled = value;
                    Signal();
                }
            }
        }

        public ICommand EditUserWin { get; }
        public Command SaveChangedByUserWin { get; }

        public UserWinVM()
        {
            GetUserData();
          
            EditUserWin = new RelayCommand(EnableListBox);
            SaveChangedByUserWin = new Command( async() =>
            {
                string arg = JsonSerializer.Serialize(User);
                var responce = await HttpClientS.HttpClient.PutAsync($"User/SaveChangedByUserWin", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("error");
                    return;
                }
                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("ok");
                }
            });

        }
        public void EnableListBox(object j)
        {
            ListBoxEnabled = true;
        }
        public async void GetUserData()
        {
            string arg = JsonSerializer.Serialize(User);
            var responce = await HttpClientS.HttpClient.GetAsync($"User");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                User = await responce.Content.ReadFromJsonAsync<User>();
                GetOrderActive(User.Id);
                GetOrderDontActive(User.Id);
                return;
            }
        }
        public async void GetOrderActive(int userId)
        {
            string arg = JsonSerializer.Serialize(CrossActive);
            var responce = await HttpClientS.HttpClient.GetAsync($"User/GetOrderActive/{userId}");

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

        public async void GetOrderDontActive(int userId)
        {
            string arg = JsonSerializer.Serialize(CrossDontActive);
            var responce = await HttpClientS.HttpClient.GetAsync($"User/GetOrderDontActive/{userId}");

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

        UserWin userWin;
        internal void SetWindow(UserWin userWin)
        {
            this.userWin = userWin;
        }
        internal void CloseWindow(UserWin userWin)
        {
            this.userWin.Close();
        }
    }
}
