namespace Resolver.Model
{
    internal class EquationSolution
    {
        public string DescriminantLine { get; set; }
        public double Descriminant { get; set; }
        public string FirstRootLine { get; set; }
        public double? FirstRoot { get; set; }
        public string SecondRootLine { get; set; }
        public double? SecondRoot { get; set; }
        public string Answer { get; set; }
    }
}
