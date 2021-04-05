using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Runtime.InteropServices;

using Physarum.Models;

using PhysarumCore;

namespace Physarum.ViewModels
{
    public class MainViewModel
    {
        TKWindow window;

        public ICommand StartCmd
        {
            get { return new Command((o) => Start()); }
        }

        public ICommand PropertyChangedCmd
        {
            get { return new Command((o) => PropertyChanged((ValueChangedArgs)o)); }
        }

        public MainViewModel()
        {
        }

        private void Start()
        {
            if (window == null)
            {
                AllocConsole();
                window = new TKWindow();
                window.Run();
            }
        }


        private void PropertyChanged(ValueChangedArgs args)
        {
            if (args.PropertyType == Enums.PropertyType.AgentSpeed)
                window.AgentSettings.Speed = args.Value;
            else if (args.PropertyType == Enums.PropertyType.AgentSteerStrength)
                window.AgentSettings.SteerStrength = args.Value;
            else if (args.PropertyType == Enums.PropertyType.AgentJitter)
                window.AgentSettings.Jitter = args.Value;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
