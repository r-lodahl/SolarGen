namespace GalaxyGenerator
{
    public class Barycenter : IMassObject
    {
        public float Mass { get; set; }
        public float DistanceFromCenter { get; set; }
        public IMassObject LeftChild { get; }
        public IMassObject RightChild { get; }
        public byte Depth { get; set; }

        public Barycenter(IMassObject leftChild, IMassObject rightChild, byte depth)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Depth = depth;
        }

        public void CalculateRelativeDistance(float distance)
        {
            var totalMass = LeftChild.Mass + RightChild.Mass;
            LeftChild.DistanceFromCenter = distance * LeftChild.Mass / totalMass;
            RightChild.DistanceFromCenter = -distance * RightChild.Mass / totalMass;
        }
    }
}
