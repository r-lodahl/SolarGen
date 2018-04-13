using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyGenerator
{
    public class Planet : IMassObject
    {
        public float Mass { get; set; }
        public float DistanceFromCenter { get; set; }
        public Orbit OrbitType { get; set; }
    }
}
