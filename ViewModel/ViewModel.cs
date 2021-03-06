using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Resolver
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public EquationModel Equation { get; set; } = new EquationModel();

        private bool _is_light_theme_selected = Properties.Settings.Default.app_theme == 1;
        public bool IsLightThemeSelected
        {
            get => _is_light_theme_selected;
            set { _is_light_theme_selected = value; OnPropertyChanged(); }
        }

        private bool _is_dark_theme_selected = Properties.Settings.Default.app_theme == 0;
        public bool IsDarkThemeSelected
        {
            get => _is_dark_theme_selected;
            set { _is_dark_theme_selected = value; OnPropertyChanged(); }
        }

        private bool _is_equation_copied;
        public bool IsEquationCopied
        {
            get => _is_equation_copied;
            set { _is_equation_copied = value; OnPropertyChanged(); }
        }

        private CommandModel _refresh_command;
        public CommandModel RefreshCommand
        {
            get => _refresh_command ??= new CommandModel(
                (action) =>
                {
                    Equation.Solution = default;
                    Equation.A = Equation.B = Equation.C = null;
                }, (lambda) =>
                {
                    return Equation.A.HasValue
                    || Equation.B.HasValue
                    || Equation.C.HasValue;
                }
            );
        }

        private CommandModel _to_light_command;
        public CommandModel ToLightCommand
        {
            get => _to_light_command ??= new CommandModel(
                (action) =>
                {
                    Properties.Settings.Default.app_theme = 1;
                    Properties.Settings.Default.Save();

                    IsLightThemeSelected = true;
                    IsDarkThemeSelected = false;

                    MainWindow.InitializeTheme();
                }
            );
        }

        private CommandModel _to_dark_command;
        public CommandModel ToDarkCommand
        {
            get => _to_dark_command ??= new CommandModel(
                (action) =>
                {
                    Properties.Settings.Default.app_theme = 0;
                    Properties.Settings.Default.Save();

                    IsLightThemeSelected = false;
                    IsDarkThemeSelected = true;

                    MainWindow.InitializeTheme();
                }
            );
        }

        private CommandModel _copy_command;
        public CommandModel CopyCommand
        {
            get => _copy_command ??= new CommandModel(
                (action) =>
                {
                    Clipboard.SetText(
                        $"{Equation.Solution.FirstRoot} {Equation.Solution.SecondRoot}"
                    );

                    // animation states
                    IsEquationCopied = true;
                    IsEquationCopied = !IsEquationCopied;
                }, (lambda) =>
                {
                    return Equation.Solution != null &&
                    (Equation.Solution.FirstRoot.HasValue || Equation.Solution.SecondRoot.HasValue);
                }
            );
        }

        private CommandModel _solve_command;
        public CommandModel SolveCommand
        {
            get => _solve_command ??= new CommandModel(
                (action) =>
                {
                    Equation.SolveEquation();
                }, (lambda) =>
                {
                    return Equation.A.HasValue
                    && Equation.B.HasValue
                    && Equation.C.HasValue;
                }
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
