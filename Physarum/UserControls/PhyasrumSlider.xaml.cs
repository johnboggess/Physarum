using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Physarum.Enums;
using Physarum.Models;
namespace Physarum.UserControls
{
    /// <summary>
    /// Interaction logic for PhyasrumSlider.xaml
    /// </summary>
    public partial class PhyasrumSlider : UserControl
    {

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); lbl_Name.Content = value; }
        }
        public PropertyType PropertyType
        {
            get { return (PropertyType)GetValue(PropertyTypeProperty); }
            set { SetValue(PropertyTypeProperty, value); }
        }
        public float Min
        {
            get { return (float)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); slider.Minimum = value; }
        }
        public float Max
        {
            get { return (float)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); slider.Maximum = value; }
        }
        public float Value
        {
            get { return (float)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); slider.Value = value; }
        }
        public ICommand ValueChanged
        {
            get { return (ICommand)GetValue(ValueChangedProperty); }
            set { SetValue(ValueChangedProperty, value); }
        }

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(nameof(PropertyName), typeof(string), typeof(PhyasrumSlider),
                                  new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnPropertyNameChanged)));
        public static readonly DependencyProperty PropertyTypeProperty = DependencyProperty.Register(nameof(PropertyType), typeof(PropertyType), typeof(PhyasrumSlider),
                                  new FrameworkPropertyMetadata(PropertyType.AgentSpeed, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnPropertyTypeChanged)));
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(float), typeof(PhyasrumSlider),
                                  new FrameworkPropertyMetadata(default(float), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnMinChanged)));
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof(Max), typeof(float), typeof(PhyasrumSlider),
                                  new FrameworkPropertyMetadata(default(float), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnMaxChanged)));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(float), typeof(PhyasrumSlider),
                                  new FrameworkPropertyMetadata(default(float), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnValueChanged)));
        public static readonly DependencyProperty ValueChangedProperty = DependencyProperty.Register(nameof(ValueChanged), typeof(ICommand), typeof(PhyasrumSlider),
                                  new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(OnValueChangedChanged)));

        private static void OnPropertyNameChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PhyasrumSlider control = (PhyasrumSlider)source;
            control.PropertyName = (string)e.NewValue;
        }
        private static void OnPropertyTypeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PhyasrumSlider control = (PhyasrumSlider)source;
            control.PropertyType = (PropertyType)e.NewValue;
        }
        private static void OnMinChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PhyasrumSlider control = (PhyasrumSlider)source;
            control.Min = (float)e.NewValue;
        }
        private static void OnMaxChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PhyasrumSlider control = (PhyasrumSlider)source;
            control.Max = (float)e.NewValue;
        }
        private static void OnValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PhyasrumSlider control = (PhyasrumSlider)source;
            control.Value = (float)e.NewValue;
        }
        private static void OnValueChangedChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            PhyasrumSlider control = (PhyasrumSlider)source;
            control.ValueChanged = (ICommand)e.NewValue;
        }

        public PhyasrumSlider()
        {
            InitializeComponent();

            slider.ValueChanged += Slider_ValueChanged;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ValueChanged?.Execute(new ValueChangedArgs() { PropertyType = PropertyType, Value = (float)e.NewValue });
        }
    }
}
