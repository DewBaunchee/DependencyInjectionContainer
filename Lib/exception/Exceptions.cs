namespace Lib.Exception
{
    public class CyclingDependencyException : System.Exception
    {
        public CyclingDependencyException(string message) : base(message)
        {
        }
    }
    
    public class CannotResolveSuchTypeException : System.Exception
    {
        public CannotResolveSuchTypeException(string message) : base(message)
        {
        }
    }
    
    public class CannotFindSuitableConstructorException : System.Exception
    {
        public CannotFindSuitableConstructorException(string message) : base(message)
        {
        }
    }
}