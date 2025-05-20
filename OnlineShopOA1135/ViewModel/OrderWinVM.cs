using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

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


      public  OrderWinVM() 
      {
        
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
