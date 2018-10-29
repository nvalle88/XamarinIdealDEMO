using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.ViewModels
{
    public class MainViewModel
    {
        //public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }
        #endregion
        #region Constructor
        public MainViewModel()
        {
           
            //this.Login = new LoginViewModel();
        }

       
        #endregion
    }
}
