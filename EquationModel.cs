using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Resolver
{
    class EquationModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private double _a = 1;
        private double _b;
        private double _c;

        private string _solution;

        public double A
        {
            get => _a;
            set { _a = value; OnPropertyChanged(); }
        }

        public double B
        {
            get => _b;
            set { _b = value; OnPropertyChanged(); }
        }

        public double C
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
                }
                return error;
            }
        }

        public void ProcessInformation(QuadraticEquation equation)
        {
            StringBuilder text = new StringBuilder();

            switch(equation.HasRoots)
            {
                case true:
                    if(equation.Descriminant != 0)
                    {
                        text.AppendLine($"D = {equation.Descriminant} => 2 roots");
                        text.AppendLine($"\t=> x(1) = {equation.FirstRoot}");
                        text.AppendLine($"\t=> x(2) = {equation.SecondRoot}");
                        text.Append($"Answer: {equation.FirstRoot}; {equation.SecondRoot}");
                    } else
                    {
                        text.AppendLine($"D = {equation.Descriminant} => 1 root");
                        text.AppendLine($"\t=> x(1) = {equation.FirstRoot}");
                        text.AppendLine($"Answer: {equation.FirstRoot}");
                    }
                    break;

                default:
                    text.AppendLine($"D = {equation.Descriminant} => no solutions");
                    text.Append($"Answer: empty set");
                    break;
            }

            Solution = text.ToString();
        }

        public void Solve()
        {
            double desc = B * B - 4 * A * C;

            QuadraticEquation equation = new QuadraticEquation
            {
                HasRoots = desc >= 0,
                Descriminant = desc,
                FirstRoot = Math.Round((-B - Math.Sqrt(desc)) / (2 * A), 2),
                SecondRoot = Math.Round((-B + Math.Sqrt(desc)) / (2 * A), 2)
            };

            ProcessInformation(equation);
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
        }
    }
}
