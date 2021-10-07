using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Resolver
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public EquationModel Equation { get; set; } = new EquationModel();

        private CommandModel _refresh_command;
        public CommandModel RefreshCommand
        {
            get => _refresh_command ??= new CommandModel(
                (obj) =>
                {
                    Equation.ClearFields();
                }, (lambda) =>
                {
                    return Equation.A.HasValue
                    || Equation.B.HasValue
                    || Equation.C.HasValue;
                }
            );
        }

        private CommandModel _solve_command;
        public CommandModel SolveCommand
        {
            get => _solve_command ??= new CommandModel(
                (obj) =>
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
