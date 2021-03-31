using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Runtime.InteropServices;

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

        public MainViewModel()
        {
            AllocConsole();
        }

        private void Start()
        {
            if (window == null)
            {
                window = new TKWindow();
                window.Run();
            }
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
