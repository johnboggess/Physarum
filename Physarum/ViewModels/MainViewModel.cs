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
        TKWindow window;

        float _agentSpeed;
        public float AgentSpeed { get { return _agentSpeed; } set { _agentSpeed = value; OnPropertyChanged(); } }
        float _steerStrength;
        public float SteerStrength { get { return _steerStrength; } set { _steerStrength = value; OnPropertyChanged(); } }
        float _jitter;
        public float Jitter { get { return _jitter; } set { _jitter = value; OnPropertyChanged(); } }
        float _fadeRate;
        public float FadeRate { get { return _fadeRate; } set { _fadeRate = value; OnPropertyChanged(); } }
        bool _additiveFade;
        public bool AdditiveFade { get { return _additiveFade; } set { _additiveFade = value; OnPropertyChanged(); } }
        float _diffusionRate;
        public float DiffusionRate { get { return _diffusionRate; } set { _diffusionRate = value; OnPropertyChanged(); } }

        public ICommand StartCmd
        {
            get { return new Command((o) => Start()); }
        }

        public ICommand PropertyChangedCmd
        {
            get { return new Command((o) => PhysarumPropertyChanged((ValueChangedArgs)o)); }
        }

        public ICommand AdditiveFadeValueChanged { get { return new Command((o) => window.FadeSettings.AdditiveFade = AdditiveFade); } }

        public MainViewModel()
        {
            AgentSettings agentSettings = AgentSettings.Default();
            FadeSettings fadeSetting = FadeSettings.Default();

            AgentSpeed = agentSettings.Speed;
            SteerStrength = agentSettings.SteerStrength;
            Jitter = agentSettings.Jitter;

            FadeRate = fadeSetting.FadeRate;
            AdditiveFade = fadeSetting.AdditiveFade;
            DiffusionRate = fadeSetting.DiffusionRate;
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


        private void PhysarumPropertyChanged(ValueChangedArgs args)
        {
            if (window == null)
                return;
            if (args.PropertyType == Enums.PropertyType.AgentSpeed)
                window.AgentSettings.Speed = args.Value;
            else if (args.PropertyType == Enums.PropertyType.AgentSteerStrength)
                window.AgentSettings.SteerStrength = args.Value;
            else if (args.PropertyType == Enums.PropertyType.AgentJitter)
                window.AgentSettings.Jitter = args.Value;
            else if (args.PropertyType == Enums.PropertyType.FadeRate)
                window.FadeSettings.FadeRate = args.Value;
            else if (args.PropertyType == Enums.PropertyType.DiffusionRate)
                window.FadeSettings.DiffusionRate = args.Value;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
