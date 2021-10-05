namespace Tests.Entity
{

    public interface IJustInterface
    {
        
    }
    
    public class JustClass : IJustInterface
    {

        public IAnotherInterface AnotherClass { get; }
        
        public JustClass(IAnotherInterface anotherClass)
        {
            AnotherClass = anotherClass;
        }
    }

    public interface IAnotherInterface
    {
        
    }

    public class AnotherClass : IAnotherInterface
    {
        public AnotherClass(IJustInterface justClass) {}
    }
}