using Resolver.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Resolver
{
    internal class EquationModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private double? _a, _b, _c;
        private EquationSolution _solution;

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

        public EquationSolution Solution
        {
            get => _solution;
            set { _solution = value; OnPropertyChanged(); }
        }

        public string Error { get; set; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "A":
                        Error = A == 0 ? "A value cannot be null" : string.Empty;
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }

        private string MakeEquationViewLine(double a, double b, double c)
        {
            if (b == 0 && c != 0)
            {
                return $"{A.Value} * x^2 {(C.Value < 0 ? "-" : "+")} {Math.Abs(C.Value)} = 0";
            }
            else if (b == 0 && c == 0)
            {
                return $"{A.Value} * x^2 = 0";
            }
            else if (b != 0 && c == 0)
            {
                return $"{A.Value} * x^2 {(B.Value < 0 ? "-" : "+")} {Math.Abs(B.Value)} * x = 0";
            }
            else
            {
                return $"{A.Value} * x^2 {(B.Value < 0 ? "-" : "+")} {Math.Abs(B.Value)} * x {(C.Value < 0 ? "-" : "+")} {Math.Abs(C.Value)} = 0";
            }
        }

        public void SolveEquation()
        {
            double desc = (B.Value * B.Value) - (4 * A.Value * C.Value);

            double? first_root = Math.Round((-B.Value + Math.Sqrt(desc)) / (2 * A.Value), 2);
            double? second_root = Math.Round((-B.Value - Math.Sqrt(desc)) / (2 * A.Value), 2);

            Solution = new EquationSolutionBuilder()
                .SetMainEquationViewLine(MakeEquationViewLine(A.Value, B.Value, C.Value))
                .SetDescriminantLine($"D = {B.Value}^2 - 4 * {A.Value} * {C.Value} = ")
                .SetDescriminant(desc)
                .SetFirstRootLine(desc < 0 ? "x1 doesn't exist" : $"x1 = (-{B.Value} + {desc}^0.5) / (2 * {A.Value}) = ")
                .SetFirstRoot(double.IsNaN(first_root.Value) ? null : first_root)
                .SetSecondRootLine(desc <= 0 ? "x2 doesn't exist" : $"x2 = (-{B.Value} - {desc}^0.5) / (2 * {A.Value}) = ")
                .SetSecondRoot(double.IsNaN(second_root.Value) || second_root.Value == first_root.Value ? null : second_root)
                .SetAnswer(desc < 0 ? "Answer: empty set (no solutions)" : desc == 0 ? $"Answer: {first_root}" : $"Answer: {first_root}; {second_root}")
                .SetFirstEquationLine(desc < 0 ? "x1 doesn't exist" : $"    x1 + x2 = -{B.Value}")
                .SetSecondEquationLine(desc < 0 ? "x2 doesn't exist" : $"    x1 * x2 = {C.Value}")
                .BuildSolution();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
