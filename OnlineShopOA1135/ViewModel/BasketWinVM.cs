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
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Data.Common;
using System.Windows.Threading;

namespace OnlineShopOA1135.ViewModel
{
   public  class BasketWinVM : BaseVM
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
        //private int quantity;
        //public int Quantity
        //{
        //    get => quantity;
        //    set
        //    {
        //        if (quantity != value)
        //        {
        //            quantity = value;
        //            OnPropertyChanged();
        //        Signal(nameof(Quantity));
        //    }
        //}
        private User user { get; set; }
        public User User
        {
            get => user;
            set
            {
                user = value;
                Signal(nameof(User));
            }
        }
   
        public Command Update { get; }
        public Command UserWinOpen { get; }   
        public Command StatusOrderFromActive { get; }
        public Command AddQuantity { get; }

        private DispatcherTimer timer = null;
        public BasketWinVM()
        {           
            GetUserData();
            Update = new Command(() =>
            {
                GetUserData();
            });
            StatusOrderFromActive = new Command(() =>
            {
                if (CrossList.Count > 0)
                {
                    GetUserData();
                    OrderFromActive(User.Id);
                }
                else
                {
                    // Показать сообщение (через MessageBox или сервис)
                    MessageBox.Show("дистбокс пустой");
                }
            });
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
            //timerStart();
            //AddQuantity = new Command(async() =>
            //{
            //    string arg = JsonSerializer.Serialize(CrossList);
            //    var responce = await HttpClientS.HttpClient.PutAsync($"User/AddQuantity", new StringContent(arg, Encoding.UTF8, "application/json"));

            //    if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            //    {
            //        var result = await responce.Content.ReadAsStringAsync();
            //        MessageBox.Show(result);
            //        return;
            //    }
            //    if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            //    {
            //        //GetGoodByOrder(User.Id);
            //        MessageBox.Show("ok");
            //    }
            //});
        }

        public void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e) //к таймеру относится 
        {
            Thread thread1 = new Thread(GetUserData);
            thread1.Start();
            
        }


        private async void OrderFromActive(int userId)
        {         
               string arg = JsonSerializer.Serialize(CrossList);
                var responce = await HttpClientS.HttpClient.PutAsync($"User/StatusOrderFromActive/{userId}", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show(result);
                    return;
                }
                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //GetGoodByOrder(User.Id);
                    MessageBox.Show("ok");
                }                   
        }
        public async void GetGoodByOrder(int userId)
        {
            string arg = JsonSerializer.Serialize(Cross);
            var responce = await HttpClientS.HttpClient.GetAsync($"User/GetGoodByOrder/{userId}");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CrossList = await responce.Content.ReadFromJsonAsync<List<OrderGoodsCross>>();

                return;
            }
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

                GetGoodByOrder(User.Id);
                return;
            }
        }
       
        //public void AddCount(object j)
        //{
        //    if (CrossList != null)
        //    {
        //          ++Cross.Quantity;
        //          Signal();
        //    }
         
        //}

        BasketWin basketWin;
        internal void SetWindow(BasketWin basketWin)
        {
            this.basketWin = basketWin;
        }
        internal void CloseWindow(BasketWin basketWin)
        {
            this.basketWin.Close();
        }
    }
}
