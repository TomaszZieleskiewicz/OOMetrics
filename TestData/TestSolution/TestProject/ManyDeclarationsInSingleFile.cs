using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public record AnotherRecord
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
    public interface AnotherInterface
    {
        void Method1(int i);
        string Method2(object o);
    }
    public class AnotherSimpleClass: AnotherInterface
    {
        public string Name { get; set; }
        public AnotherSimpleClass(string name)
        {
            Name = name;
        }

        public void Method1(int i)
        {
            throw new NotImplementedException();
        }

        public string Method2(object o)
        {
            throw new NotImplementedException();
        }
    }
    public struct AnotherStruct
    {
        public AnotherStruct(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public override string ToString() => $"({X}, {Y})";
    }

}
