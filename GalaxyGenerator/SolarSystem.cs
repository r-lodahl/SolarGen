using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security;

namespace GalaxyGenerator
{
    public class SolarSystem
    {
        public SolarSystemType SystemType { get; set; }
        public StarClass MainStarClass { get; set; }
        public List<StarClass> SecondaryStarClasses { get; set; }
        public SolarSystemAge SystemAge { get; set; }
        public IMassObject RootMassObject { get; private set; }


        public void CreateOrderedBarycenters()
        {
            if (SecondaryStarClasses.Count == 0) return;

            var starList = SecondaryStarClasses.Select(o => new Star(o)).OrderByDescending(o => o.Mass).ToList();
            starList.Insert(0,new Star(MainStarClass));

            var barycenters = MakeBarycenters(starList);

            RootMassObject = MergeBarycenters(barycenters);
        }

        private static List<IMassObject> MakeBarycenters(IEnumerable<IMassObject> objectList)
        {
            IMassObject firstObject = null;
            var newObjects = new List<IMassObject>();
            foreach (var star in objectList)
            {
                if (firstObject == null) firstObject = star;
                else
                {
                    newObjects.Add(new Barycenter(firstObject, star, 0));
                    firstObject = null;
                }
            }
            if (firstObject != null) newObjects.Add(firstObject);
            return newObjects;
        }

        private static IMassObject MergeBarycenters(List<IMassObject> barycenterList)
        {
            while (barycenterList.Count > 1)
            {
                var leftBarycenter = barycenterList[0] as Barycenter;
                var leftDepth = leftBarycenter?.Depth ?? 0;

                var rightBarycenter = barycenterList[1] as Barycenter;
                var rightDepth = rightBarycenter?.Depth ?? 0;

                var barycenter = new Barycenter(barycenterList[0], barycenterList[1], Math.Max(leftDepth, rightDepth));
                barycenterList.RemoveAt(0);
                barycenterList.RemoveAt(1);
                barycenterList.Insert(0, barycenter);
            }

            return barycenterList[0];
        }
    }

}
