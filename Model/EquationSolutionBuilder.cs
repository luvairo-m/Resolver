namespace Resolver.Model
{
    internal class EquationSolutionBuilder
    {
        private readonly EquationSolution _solution;

        internal EquationSolutionBuilder() => _solution = new EquationSolution();

        public EquationSolutionBuilder SetMainEquationViewLine(string line)
        {
            _solution.MainEquationViewLine = line;
            return this;
        }

        public EquationSolutionBuilder SetDescriminantLine(string line)
        {
            _solution.DescriminantLine = line;
            return this;
        }
        public EquationSolutionBuilder SetDescriminant(double number)
        {
            _solution.Descriminant = number;
            return this;
        }
        public EquationSolutionBuilder SetFirstRootLine(string line)
        {
            _solution.FirstRootLine = line;
            return this;
        }
        public EquationSolutionBuilder SetFirstRoot(double? number)
        {
            _solution.FirstRoot = number;
            return this;
        }
        public EquationSolutionBuilder SetSecondRootLine(string line)
        {
            _solution.SecondRootLine = line;
            return this;
        }
        public EquationSolutionBuilder SetSecondRoot(double? number)
        {
            _solution.SecondRoot = number;
            return this;
        }
        public EquationSolutionBuilder SetAnswer(string answer)
        {
            _solution.Answer = answer;
            return this;
        }

        public EquationSolutionBuilder SetFirstEquationLine(string line)
        {
            _solution.FirstEquationLine = line;
            return this;
        }

        public EquationSolutionBuilder SetSecondEquationLine(string line)
        {
            _solution.SecondEquationLine = line;
            return this;
        }

        public EquationSolution BuildSolution() => _solution;
    }
}