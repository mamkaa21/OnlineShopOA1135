using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;
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

        public Command OrderWinOpen { get; }
        public Command CreateWinOpen { get; }
        public Command EditWinOpen { get; }
        public Command DeleteGoods { get; }
        public Command FindGoodsByTitle { get; }
        public Command UserWinOpen { get; }
        public AdminWinVM()
        {
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
                //проверка что обьект не пустой и только тогда открыть окно
            });
            DeleteGoods = new Command(async () =>
            {
                //запрос на удаление товара + проверка, что товар не пустой
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

        AdminWin adminWindow;
        internal void SetWindow(AdminWin adminWindow)
        {
            this.adminWindow = adminWindow;
        }
    }
}
