using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OnlineShopOA1135.Model;
using OnlineShopOA1135.View;

namespace OnlineShopOA1135.ViewModel
{
    public class MainWinVM : BaseVM
    {
        public Command EnterWinOpen { get; }
        public Command RegisterWinOpen { get; }
        public MainWinVM() 
        {
            EnterWinOpen = new Command(() =>
            {
                EnterWin enterWin = new EnterWin();
                enterWin.Show();
                Signal();
            });
            RegisterWinOpen = new Command(() =>
            {
                RegisterWin registerWin = new RegisterWin();
                registerWin.Show();
                Signal();
            });
            
        } 
        MainWindow mainWindow;
        internal void SetWindow(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }
        internal void CloseWindow(MainWindow mainWindow) 
        { 
            this.mainWindow.Close();
        }
    }
}
