namespace OOMetrics.Abstractions
{
    public abstract class ComparableByStringHash
    {
        public abstract override string ToString();
        public override bool Equals(object? obj)
        {
            return (obj is null) ? base.Equals(obj) : GetHashCode() == obj.GetHashCode();
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
