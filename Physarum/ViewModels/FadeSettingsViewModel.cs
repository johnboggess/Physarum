using System;
using System.Collections.Generic;
using System.Text;

using Physarum.Models;

using PhysarumCore;
namespace Physarum.ViewModels
{
    public class FadeSettingsViewModel : Changeable
    {
        public float FadeRate
        {
            get { return MainViewModel.FadeSettings.FadeRate; }
            set
            {
                FadeSettings fs = MainViewModel.FadeSettings;
                fs.FadeRate = value;
                MainViewModel.FadeSettings = fs;
            }
        }
        public Command FadeRateChanged { get { return new Command((o) => FadeRate = ((ValueChangedArgs)o).Value); } }

        public float DiffusionRate
        {
            get { return MainViewModel.FadeSettings.DiffusionRate; }
            set
            {
                FadeSettings fs = MainViewModel.FadeSettings;
                fs.DiffusionRate = value;
                MainViewModel.FadeSettings = fs;
            }
        }
        public Command DiffusionRateChanged { get { return new Command((o) => DiffusionRate = ((ValueChangedArgs)o).Value); } }

        public bool AdditiveFade
        {
            get { return MainViewModel.FadeSettings.AdditiveFade; }
            set
            {
                FadeSettings fs = MainViewModel.FadeSettings;
                fs.AdditiveFade = value;
                MainViewModel.FadeSettings = fs;
            }
        }
        public Command AdditiveFadeChanged { get { return new Command((o) => AdditiveFade = (bool)o); } }
    }
}
