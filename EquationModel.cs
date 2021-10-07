using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Resolver
{
    internal class EquationModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private double? _a, _b, _c, _descriminant, _first_root, _second_root;
        private string _descriminant_line, _first_root_line, _second_root_line, _answer_line;

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

        public string DescriminantLine
        {
            get => _descriminant_line;
            set { _descriminant_line = value; OnPropertyChanged(); }
        }
        public double? Descriminant
        {
            get => _descriminant;
            set { _descriminant = value; OnPropertyChanged(); }
        }
        public string FirstRootLine
        {
            get => _first_root_line;
            set { _first_root_line = value; OnPropertyChanged(); }
        }
        public double? FirstRoot
        {
            get => _first_root;
            set { _first_root = value; OnPropertyChanged(); }
        }
        public string SecondRootLine
        {
            get => _second_root_line;
            set { _second_root_line = value; OnPropertyChanged(); }
        }
        public double? SecondRoot
        {
            get => _second_root;
            set { _second_root = value; OnPropertyChanged(); }
        }

        public string AnswerLine
        {
            get => _answer_line;
            set { _answer_line = value; OnPropertyChanged(); }
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

        public void SolveEquation()
        {
            double desc = (B.Value * B.Value) - (4 * A.Value * C.Value);

            double? first_root = Math.Round((-B.Value + Math.Sqrt(desc)) / (2 * A.Value), 2);
            double? second_root = Math.Round((-B.Value - Math.Sqrt(desc)) / (2 * A.Value), 2);

            DescriminantLine = $"D = {B.Value}^2 - 4 * {A.Value} * {C.Value} = ";
            Descriminant = desc;
            FirstRootLine = desc < 0 ? $"x(1) doesn't exist" : $"x(1) = (-{B.Value} + sqrt({desc})) / (2 * {A.Value}) = ";
            FirstRoot = double.IsNaN(first_root.Value) ? null : first_root;
            SecondRootLine = desc <= 0 ? "x(2) doesn't exist" : $"x(2) = (-{B.Value} - sqrt({desc})) / (2 * {A.Value}) = ";
            SecondRoot = double.IsNaN(second_root.Value) ? null : second_root;
            AnswerLine = desc < 0 ? "Answer: empty set (no sulutions)" : desc == 0 ? $"Answer: {first_root}" : $"Answer: {first_root}; {second_root}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public void ClearFields()
        {
            A = B = C = null;
            FirstRootLine = SecondRootLine = DescriminantLine = AnswerLine = null;
            FirstRoot = SecondRoot = Descriminant = null;
        }
    }
}
