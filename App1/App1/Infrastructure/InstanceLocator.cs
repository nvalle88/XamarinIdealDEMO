using System;
using System.Collections.Generic;
using System.Text;
using App1.ViewModels;

namespace App1.Infrastructure
{
   public class InstanceLocator
    {
        #region MyRegion

        public MainViewModel Main { get; set; }
        #endregion

        #region Construnctores
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
