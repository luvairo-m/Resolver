using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Resolver
{
    class ViewModel : INotifyPropertyChanged
    {
        public EquationModel Equation { get; set; } = new EquationModel();


        private CommandModel _refresh_command;
        public CommandModel RefreshCommand
        {
            get => _refresh_command ??= new CommandModel(
                (obj) =>
                {
                    Equation.Solution = string.Empty;
                    Equation.A = 1;
                    Equation.B = Equation.C = 0;
                }
            );
        }

        private CommandModel _solve_command;
        public CommandModel SolveCommand
        {
            get => _solve_command ??= new CommandModel(
                (obj) =>
                {
                    Equation.Solve();
                }
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
