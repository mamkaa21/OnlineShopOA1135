using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopOA1135.Model;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Net.Http.Json;

namespace OnlineShopOA1135.ViewModel
{
    public class RegisterWnVM : BaseVM
    {
        public User User { get; set;  } = new ();
        public Command EnterWinOpen { get; }
        public RegisterWnVM() 
        {
            EnterWinOpen = new Command(async() =>
            {
                string arg = JsonSerializer.Serialize(User);
                var responce = await HttpClientS.HttpClient.PostAsync($"Auth/AddNewUser", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Пароль или логин не может быть пустым");
                    return;
                }
                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    EnterWin enterWin = new EnterWin();
                    enterWin.Show();
                    Signal();
                }
            });
        }
        RegisterWin registerWindow;
        internal void SetWindow(RegisterWin registerWindow)
        {
            this.registerWindow = registerWindow;
        }
        internal void CloseWindow(RegisterWin registerWindow)
        {
            this.registerWindow.Close();
        }

    }
}
