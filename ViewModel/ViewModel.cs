using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Resolver
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public EquationModel Equation { get; set; } = new EquationModel();

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

        private CommandModel _copy_command;
        public CommandModel CopyCommand
        {
            get => _copy_command ??= new CommandModel(
                (action) =>
                {
                    Clipboard.SetText(
                        $"{Equation.Solution.FirstRoot} {Equation.Solution.SecondRoot}"
                    );
                }, (lambda) =>
                {
                    return (Equation.Solution.FirstRoot.HasValue
                    || Equation.Solution.SecondRoot.HasValue)
                    && Equation.Solution != null;
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
