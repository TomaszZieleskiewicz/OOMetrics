using TestProject;

namespace OtherTestProject
{
    public class ClassUsingTypesFromOtherProject
    {
        public SimpleClass ClassA;
        public TestProject.SimpleClass ClassB;
        private Struct AStruct;
        private static readonly TestProject.Enum x = TestProject.Enum.VALUE_3;
        public ClassUsingTypesFromOtherProject(SimpleClass classA, TestProject.SimpleClass classB)
        {
            ClassA = classA;
            ClassB = classB;
            AStruct = new Struct();
        }
    }
}