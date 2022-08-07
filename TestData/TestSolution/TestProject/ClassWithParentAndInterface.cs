using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class ClassWithParentAndInterface : SimpleClass, Interface
    {
        public ClassWithParentAndInterface(string name) : base(name)
        {
        }

        public void Method1(int i)
        {
            
        }

        public string Method2(object o)
        {
            return "A";
        }
    }
}
