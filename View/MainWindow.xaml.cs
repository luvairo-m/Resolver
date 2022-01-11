using System;
using System.Windows;

namespace Resolver
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;

            InitializeTheme();
            InitializeComponent();
        }

        internal static void InitializeTheme()
        {
            byte style = Properties.Settings.Default.app_theme;

            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary { Source = 
                    new Uri(@$"Assets\Themes\{(style == 0 ? "DarkTheme.xaml" : "LightTheme.xaml")}", UriKind.Relative)  
                }
            );
        }
    }
}
