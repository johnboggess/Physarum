using System;
using System.Collections.Generic;
using System.Text;

using Physarum.Models;

using PhysarumCore;
namespace Physarum.ViewModels
{
    public class AgentSettingsViewModel : Changeable
    {
        public float Speed
        {
            get { return MainViewModel.AgentSettings.Speed; }
            set
            {
                AgentSettings settings = MainViewModel.AgentSettings;
                settings.Speed = value;
                MainViewModel.AgentSettings = settings;
            }
        }
        public Command SpeedChanged { get { return new Command((o) => Speed = ((ValueChangedArgs)o).Value); } }

        public float SteerStrength
        {
            get { return MainViewModel.AgentSettings.SteerStrength; }
            set
            {
                AgentSettings settings = MainViewModel.AgentSettings;
                settings.SteerStrength = value;
                MainViewModel.AgentSettings = settings;
            }
        }
        public Command SteerStrengthChanged { get { return new Command((o) => SteerStrength = ((ValueChangedArgs)o).Value); } }

        public float Jitter
        {
            get { return MainViewModel.AgentSettings.Jitter; }
            set
            {
                AgentSettings settings = MainViewModel.AgentSettings;
                settings.Jitter = value;
                MainViewModel.AgentSettings = settings;
            }
        }
        public Command JitterChanged { get { return new Command((o) => Jitter = ((ValueChangedArgs)o).Value); } }

        public float SensorDistance
        {
            get { return MainViewModel.AgentSettings.SensorDistance; }
            set
            {
                AgentSettings settings = MainViewModel.AgentSettings;
                settings.SensorDistance = value;
                MainViewModel.AgentSettings = settings;
            }
        }
        public Command SensorDistanceChanged { get { return new Command((o) => SensorDistance = ((ValueChangedArgs)o).Value); } }

        public float SensorAngle
        {
            get { return MainViewModel.AgentSettings.SensorAngle; }
            set
            {
                AgentSettings settings = MainViewModel.AgentSettings;
                settings.SensorAngle = value;
                MainViewModel.AgentSettings = settings;
            }
        }
        public Command SensorAngleChanged { get { return new Command((o) => SensorAngle = ((ValueChangedArgs)o).Value); } }
    }
}
