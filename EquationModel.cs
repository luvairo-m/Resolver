using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Resolver
{
    internal class EquationModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private double? _a, _b, _c;
        private string _solution;

        public double? A
        {
            get => _a;
            set { _a = value; OnPropertyChanged(); }
        }

        public double? B
        {
            get => _b;
            set { _b = value; OnPropertyChanged(); }
        }

        public double? C
        {
            get => _c;
            set { _c = value; OnPropertyChanged(); }
        }

        public string Solution
        {
            get => _solution;
            set { _solution = value; OnPropertyChanged(); }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "A":
                        if (A == 0) { error = "A value cannot be null"; }
                        break;
                    default:
                        break;
                }
                return error;
            }
        }

        public void ProcessSolution(QuadraticEquation equation)
        {
            //StringBuilder text = new StringBuilder();

            //switch (equation.HasRoots)
            //{
            //    case true:
            //        _ = text.AppendLine($"1. D = {equation.Descriminant} => {(equation.Descriminant != 0 ? "2 roots" : "1 root")}");
            //        _ = text.AppendLine($"2. x(1) = {equation.FirstRoot}");
            //        _ = text.AppendLine($"{(equation.Descriminant != 0 ? $"3. x(2) = {equation.SecondRoot}" : "3. x(2) doesn't exist")}");
            //        _ = text.AppendLine($"Answer: {equation.FirstRoot} {(equation.Descriminant != 0 ? $"; {equation.SecondRoot}" : string.Empty)}");
            //        break;

            //    default:
            //        _ = text.AppendLine($"D = {equation.Descriminant} => no solutions");
            //        _ = text.AppendLine($"Answer: empty set");
            //        break;
            //}

            //Solution = text.ToString();

        }

        public void Solve()
        {
            double desc = (B.Value * B.Value) - (4 * A.Value * C.Value);

            QuadraticEquation equation = new QuadraticEquation
            {
                HasRoots = desc >= 0,
                Descriminant = desc,
                FirstRoot = Math.Round((-B.Value - Math.Sqrt(desc)) / (2 * A.Value), 2),
                SecondRoot = Math.Round((-B.Value + Math.Sqrt(desc)) / (2 * A.Value), 2),
                FirstCoefficent = A.Value,
                SecondCoefficent = B.Value,
                FreeMember = C.Value
            };

            ProcessSolution(equation);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public struct QuadraticEquation
        {
            public bool HasRoots { get; set; }
            public double Descriminant { get; set; }
            public double? FirstRoot { get; set; }
            public double? SecondRoot { get; set; }
            public double FirstCoefficent { get; set; }
            public double SecondCoefficent { get; set; }
            public double FreeMember { get; set; }
        }
    }
}
