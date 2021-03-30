using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using Physarum.Models;
using Physarum.TK;

namespace Physarum.ViewModels
{
    public class MainViewModel
    {
        TKWindow window;

        public ICommand StartCmd
        {
            get { return new Command((o) => Start()); }
        }

        private void Start()
        {
            if (window != null)
            {
                window = new TKWindow();
                window.Run();
            }
        }
    }
}
