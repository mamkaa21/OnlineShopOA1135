﻿using OnlineShopOA1135.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOA1135.ViewModel
{
    public class EnterWinVM
    {
        public Command Enter {  get; }
        public EnterWinVM()
        {
            Enter = new Command(() =>
            {
                //запрос для входа + проверка на роль юзера? если роль 2 - окно юзера открывается
                // если роль 1 - то окно админа
            });
            
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
