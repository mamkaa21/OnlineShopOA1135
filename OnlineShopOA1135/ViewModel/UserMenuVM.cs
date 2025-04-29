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
using System.Windows.Threading;
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

        private List<Category> categoryList { get; set; }
        public List<Category> CategoryList
        {
            get => categoryList;
            set
            {
                categoryList = value;
                Signal(nameof(CategoryList));
            }
        }
        public Command BasketWinOpen { get; }
        public Command UserWinOpen { get; }
        public ICommand DoubleClickCommand { get; private set; }

        private DispatcherTimer timer = null;


        public UserMenuVM()
        {
            GetCategories();
            GetGoods();
            //timerStart();
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
            DoubleClickCommand = new RelayCommand(DoubleClickExecute);
        }

        public void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Start();
        }
        private void timerTick(object sender, EventArgs e) //к таймеру относится 
        {
            Thread thread1 = new Thread(GetCategories);
            thread1.Start();
            Thread thread2 = new Thread(GetGoods);
            thread2.Start();         
        }


        private void DoubleClickExecute(object parameter)
        {
            if (parameter is Good selectedItem)
            {
               GoodWin goodWin = new GoodWin();
                goodWin.Show();
                Signal();
            }
        }


        public async void GetCategories()
        {
            string arg = JsonSerializer.Serialize(Category);
            var responce = await HttpClientS.HttpClient.GetAsync($"Admin/GetCategories");

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CategoryList = await responce.Content.ReadFromJsonAsync<List<Category>>();
                return;
            }
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
                GoodList = await responce.Content.ReadFromJsonAsync<List<Good>>();
                return;
            }
        }

        internal async void ListCategoryClick()
        {
            var categories = CategoryList.Where(s=>s.Check==true).Select(s=>s.Id).ToList();
            string json = JsonSerializer.Serialize<List<int>>(categories);
            var responce = await HttpClientS.HttpClient.PostAsync($"User/FindGoods",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await responce.Content.ReadAsStringAsync();
                MessageBox.Show(result);
                return;
            }
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                GoodList = await responce.Content.ReadFromJsonAsync<List<Good>>();
                return;
            }
        }
    }
}
