using System.Windows;

namespace Resolver
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
            InitializeComponent();
        }
    }
}
