using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;

namespace OnlineShopOA1135.ViewModel
{
    public class AdminWinVM : BaseVM
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
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                Signal("SearchText");

            }
        }

        public Command OrderWinOpen { get; }
        public Command CreateWinOpen { get; }
        public Command EditWinOpen { get; }
        public Command DeleteGoods { get; }
        public Command FindGoodsByTitle { get; }
        public Command UserWinOpen { get; }

        private DispatcherTimer timer = null;
        public AdminWinVM()
        {
            GetCategories();
            GetGoods();
            OrderWinOpen = new Command(() =>
            {
                OrderWin orderWin = new OrderWin();            
                orderWin.Show();
                Signal();
            });
            CreateWinOpen = new Command(() =>
            {
                CreateEditWin createEditWin = new CreateEditWin(new Good());
                createEditWin.Show();
                Signal();
            });
            EditWinOpen = new Command(() =>
            {
                if (Good != null)
                {
                    CreateEditWin createEditWin = new CreateEditWin(Good);
                    createEditWin.Show();
                    Signal();
                }
                else
                    MessageBox.Show("выберите обьект для редактирования");           
            });
            DeleteGoods = new Command(async () =>
            {             
                if (Good != null)
                {
                    string arg = JsonSerializer.Serialize(Good);
                    var responce = await HttpClientS.HttpClient.PostAsync($"Admin/DeleteGoods", new StringContent(arg, Encoding.UTF8, "application/json"));

                    if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var result = await responce.Content.ReadAsStringAsync();
                        MessageBox.Show("error");
                        return;
                    }
                    if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //мессаджбокс с подтверждением да нет хз как надо думать

                        Signal(nameof(Good));
                    }
                }
                else
                    MessageBox.Show("выберите обьект для удаления");
            });
            FindGoodsByTitle = new Command(async() =>
            {
                if (string.IsNullOrEmpty(SearchText))
                {
                    GetGoods();
                }
                else
                {
                    string arg = JsonSerializer.Serialize(SearchText);
                    var responce = await HttpClientS.HttpClient.PostAsync($"User/FindGoods", new StringContent(arg, Encoding.UTF8, "application/json"));

                    if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var result = await responce.Content.ReadAsStringAsync();
                        MessageBox.Show("error");
                        return;
                    }
                    if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        GoodList = await responce.Content.ReadFromJsonAsync<List<Good>>();
                        return;
                    }
                }
            });
            UserWinOpen = new Command(() =>
            {
                UserWin userWin = new UserWin();
                userWin.Show();
                Signal();
            });
        }

        //public void timerStart()
        //{
        //    timer = new DispatcherTimer();
        //    timer.Tick += new EventHandler(timerTick);
        //    timer.Interval = new TimeSpan(0, 0, 10);
        //    timer.Start();
        //}
        //private void timerTick(object sender, EventArgs e) //к таймеру относится 
        //{
        //    Thread thread1 = new Thread(GetCategories);
        //    thread1.Start();
        //    Thread thread2 = new Thread(GetGoods);
        //    thread2.Start();
        //}


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
            var categories = CategoryList.Where(s => s.Check == true).Select(s => s.Id).ToList();
            string json = JsonSerializer.Serialize<List<int>>(categories);
            var responce = await HttpClientS.HttpClient.PostAsync($"User/FiltGoodsByCat",
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

        AdminWin adminWindow;
        internal void SetWindow(AdminWin adminWindow)
        {
            this.adminWindow = adminWindow;
        }
    }
}
