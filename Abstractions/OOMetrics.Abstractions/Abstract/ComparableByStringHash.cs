using OOMetrics.Abstractions.Interfaces;

namespace OOMetrics.Abstractions.Abstract
{
    public abstract class ComparableByStringHash : IComparableByStringHash
    {
        public bool CompareByStringHash(IComparableByStringHash? obj)
        {
            return Equals(obj);
        }
        public abstract override string ToString();
        public override bool Equals(object? obj)
        {
            return obj is null ? base.Equals(obj) : GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            var stringRepresentation = ToString();
            if (stringRepresentation == null)
            {
                return 0;
            }
            return stringRepresentation.GetHashCode();
        }


    }
}