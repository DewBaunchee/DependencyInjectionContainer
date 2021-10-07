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
    
    public class CannotBeValueTypeException : System.Exception
    {
        public CannotBeValueTypeException(string message) : base(message)
        {
        }
    }
    
    public class ImplementationCannotBeAbstractException : System.Exception
    {
        public ImplementationCannotBeAbstractException(string message) : base(message)
        {
        }
    }
    
    public class ImplementationCannotBeInterfaceException : System.Exception
    {
        public ImplementationCannotBeInterfaceException(string message) : base(message)
        {
        }
    }
    
    public class ImplementationIsNotAssignableException : System.Exception
    {
        public ImplementationIsNotAssignableException(string message) : base(message)
        {
        }
    }
}