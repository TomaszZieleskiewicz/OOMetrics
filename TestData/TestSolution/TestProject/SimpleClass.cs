namespace TestProject
{
    public class SimpleClass
    {
        public string Name { get; set; }
        public SimpleClass(string name)
        {
            Name = name;
        }

        public int SimpleMethod()
        {
            var x = 2;
            return x;
        }
        public decimal OtherMethod(decimal y)
        {
            return y / SimpleMethod() + PrivateMethod();
        }
        private int PrivateMethod()
        {
            return 6;
        }
    }
}