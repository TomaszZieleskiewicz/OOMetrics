namespace OtherTestProject
{
    public class ClassWithParentThatUsesOtherTypes : ClassUsingTypesFromOtherProject
    {
        public ClassWithParentThatUsesOtherTypes(SimpleClass classA, TestProject.SimpleClass classB) : base(classA, classB)
        {
        }
    }
}
