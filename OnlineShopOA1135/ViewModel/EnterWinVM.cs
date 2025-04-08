using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;


namespace OnlineShopOA1135.ViewModel
{
    public class EnterWinVM : BaseVM
    {
        public User User { get; set; } = new();
        public Command Enter { get; }
        public EnterWinVM()
        {
            Enter = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(User);
                var responce = await HttpClientS.HttpClient.PostAsync($"Auth/CheckAccountIsExist", new StringContent(arg, Encoding.UTF8, "application/json"));

                if (responce.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные. Помните, у вас есть три попытки ввести верный пароль.");
                    return;
                }

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   var d= await responce.Content.ReadFromJsonAsync<UserModel>();
                    if (d.RoleId == 1)
                    {
                        AdminWin adminWin = new AdminWin();
                        adminWin.Show();
                        Signal();
                        enterWindow.Close();
                    }
                    else
                    {
                        UserMenu userMenu = new UserMenu();
                        userMenu.Show();
                        Signal();
                        enterWindow.Close();
                    }
                }
                else
                {
                    var result = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка подключения");
                    return;
                }


            });
            //запрос для входа + проверка на роль юзера? если роль 2 - окно юзера открывается
            // если роль 1 - то окно админа   
        }

        EnterWin enterWindow;
        internal void SetWindow(EnterWin enterWindow)
        {
            this.enterWindow = enterWindow;
        }
        internal void CloseWindow(EnterWin enterWindow)
        {
            this.enterWindow.Close();
        }


    }
}
