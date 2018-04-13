namespace GalaxyGenerator
{
    public class Star : IMassObject
    {
        public StarClass Class { get; set; }
        public float Mass { get; set; }
        public float DistanceFromCenter { get; set; }

        public Star(StarClass starClass)
        {
            Class = starClass;
            Mass = starClass.GetMass();
        }
    }
}
