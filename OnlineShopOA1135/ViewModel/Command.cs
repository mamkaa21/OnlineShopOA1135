using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlineShopOA1135.ViewModel
{
    public class Command : ICommand      
    {
        Action  action;
        public Command(Action action) 
        {
            this.action = action;
        }
        public event EventHandler? CanExecuteChanged;
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            action();
        }
    }
}
