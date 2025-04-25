using OnlineShopOA1135.Model;
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
        public UserWinVM()
        {
            GetUserData();

        }
        public async void  GetUserData()
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
    }
}
