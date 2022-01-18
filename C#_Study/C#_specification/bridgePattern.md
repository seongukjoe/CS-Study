
'''cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Abstraction ab = new RefinedAbstraction();

            ab.Implementor = new ConcreteImplementorA();
            ab.Operation();

            ab.Implementor = new ConcreteImplementorB();
            ab.Operation();

            Console.ReadKey();
        }
    }

    public class Abstraction
    {
        protected Implementor implementor;
        public Implementor Implementor
        {
            set { implementor = value; }
        }
        public virtual void Operation()
        {
            implementor.Operation();
        }
    }

    public abstract class Implementor
    {
        public abstract void Operation();
    }

    public class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            implementor.Operation();
        }
    }

    public class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("Concrete A Operation");
        }
    }
    public class ConcreteImplementorB : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("Concrete B Operation");
        }
    }

}
'''
