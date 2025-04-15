using Microsoft.Win32;
using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineShopOA1135.ViewModel
{
    internal class CreateEditWinVM : BaseVM
    {
        public Good Good { get; set; } = new Good();

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


        public Command GetImageByFile { get; }
        public Command AddGoods { get; }


        public CreateEditWinVM()
        {
            GetCategories();

            GetImageByFile = new Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    var filepath = openFileDialog.FileName;
                    var info = new FileInfo(filepath);
                    if (!info.Exists || info.Length > 2 * 1024 * 1024)
                    {
                        MessageBox.Show("Файл имеет неверный размер");
                        return;
                    }
                    Good.Image = File.ReadAllBytes(filepath);
                    Signal(nameof(Good));
                }
            });
            AddGoods = new Command(async () =>
            {
                string arg = JsonSerializer.Serialize(Good);
                var responce = await HttpClientS.HttpClient.PostAsync($"Admin/CreateGoods", new StringContent(arg, Encoding.UTF8, "application/json"));

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
    }
}
