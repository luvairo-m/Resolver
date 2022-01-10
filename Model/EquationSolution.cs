namespace Resolver.Model
{
    internal class EquationSolution
    {
        public string MainEquationViewLine { get; set; }
        public string TopLine { get; set; } = "1) Descriminant method:";
        public string DescriminantLine { get; set; }
        public double Descriminant { get; set; }
        public string FirstRootLine { get; set; }
        public double? FirstRoot { get; set; }
        public string SecondRootLine { get; set; }
        public double? SecondRoot { get; set; }
        public string Answer { get; set; }
        public string BottomLine { get; set; } = "2) Vieta's theorem method: ";
        public string FirstEquationLine { get; set; }
        public string SecondEquationLine { get; set; }
    }
}
