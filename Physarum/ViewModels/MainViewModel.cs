using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Runtime.InteropServices;

using Physarum.Models;

using PhysarumCore;

namespace Physarum.ViewModels
{
    public class MainViewModel : Changeable
    {
        public static AgentSettings AgentSettings
        {
            get { return window.AgentSettings; }
            set { window.AgentSettings = value; }
        }

        public static FadeSettings FadeSettings
        {
            get { return window.FadeSettings; }
            set { window.FadeSettings = value; }
        }

        static TKWindow window;

        bool running = false;

        public ICommand StartCmd
        {
            get { return new Command((o) => Start()); }
        }
        public MainViewModel()
        {
            window = new TKWindow();
            window.AgentSettings = AgentSettings.Default();
            window.FadeSettings = FadeSettings.Default();
        }

        private void Start()
        {
            if (!running)
            {
                running = true;
                AllocConsole();
                window.Run();
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
