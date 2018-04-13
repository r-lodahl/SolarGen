using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GalaxyGenerator
{
    public class SolarGenerator
    {
        /*
	Unusual Objects (set chances as playability requires it)
	Nebulae: Emission/Reflection/Dark:
	- Emission: Open Clusters, milchig leuchtend
	- Reflection/Dark: Stern vor/hinter dem Nebel ~30Parsecs, für gewöhnlich nicht dense
	
	Planetary Nebulae: Überreste vom Tod einer RedGiants zu WhiteDwarf. Sphärisch um den Stern (Stundenglass
	bei mehreren Sternen). Leuchten, nicht dense, ca 1/3 Parsec groß, kurzlebig.
	
	Supernova Remnants: Selbiges wie Planetary Nebulae nur von ner Supernova, ca 1 Parsec groß
	
	Neutronen Sternen: Klein, pulsierend, selten
	
	Schwarze Löcher: Noch kleiner, noch schlechter erklärbar, selten
	
	-------------------------
	
	Supergiants and ClassOBlueGiants: Immer New-Born-Stage, nur Gas und Asteroiden
	
	ClassBBlueGiants and und AMainSequence: Maximal Young-Stage: instabile Orbits, feste Planeten, viele Asteroiden und Impacts
	
	
	
	
	
	*/

        private Random random;
        private SolarSystem system;

        // System Chances
        private float superSystemChance = 0.01f; // Subchance that a giant system will be supergiant
        private float giantSystemChance = 0.01f; // If not it will be a major system

        // Supergiant Star Chances
        private float supergiantOBClassBlueSupergiantThreshold = 0.375f;
        private float supergiantAGClassYellowSupergiantThreshold = 0.5f;

        // Giant Star Chances
        private float redGiantChance = 0.72f;

        private float yellowGiantThreshold = 0.2f;
        private float kClassRedGiantThreshold = 0.94f;

        private float blueGiantThreshold = 0.999f;

        // Major Star Chances
        private float majorAClassWhiteMainSequenceThreshold = 0.04f;
        private float majorFClassYellowWhiteMainSequenceThreshold = 0.16f;
        private float majorGClassYellowMainSequenceThreshold = 0.35f;
        private float majorKClassOrangeMainSequenceThreshold = 0.75f;

        // Minor Star Chances
        private float minorMClassRedDwarfThreshold = 0.9f;

        // Super Giant Star Numbers
        private float superSolitaryThreshold = 0.2f;
        private float superBinaryThreshold = 0.5f;
        private float superTrinaryThreshold = 0.75f;
        private float superQuaternaryThreshold = 0.95f;
        private float superQuinaryThreshold = 0.975f;
        private float superSextaryThreshold = 0.99f;

        // Giant and Major Star Numbers
        private float solitaryThreshold = 0.42f;
        private float binaryThreshold = 0.85f;
        private float trinaryThreshold = 0.97f;

        // Secondary Type Chances
        private float redGiantMajorSecondaryThreshold = 0.66f;
        private float blueGiantBlueGiantSecondaryThreshold = 0.7f;
        private float blueGiantMajorSecondaryThreshold = 0.9f;
        private float supergiantSupergiantSecondaryThreshold = 0.625f;
        private float majorMajorSecondaryThreshold = 0.66f;

        // Star Age Chances
        private float starAgeBMainSequenceNewbornThreshold = 0.1f;
        private float starAgeAMainSequenceNewbornThreshold = 0.01f;
        private float starAgeFMainSequenceYoungThrehold = 0.33f;
        private float starAgeGMainSequenceYoungThreshold = 0.1f;
        private float starAgeKMainSequenceYoungThreshold = 0.02f;

        //Star Separation Thresholds
        private float binarySeparationNoneThreshold = 0.16f;
        private float binarySeparationLowThreshold = 0.24f;
        private float binarySeparationSomeThreshold = 0.42f;
        private float binarySeparationMediumThreshold = 0.58f;
        private float binarySeparationHighThreshold = 0.8f;
        private float binarySeparationVeryHighThreshold = 0.91f;

        private float subPairSeparationNoneThreshold = 0.15f;
        private float subPairSeparationLowThreshold = 0.3f;
        private float subPairSeparationMediumThreshold = 0.65f;

        private float quaternySeparationLowThreshold = 0.5f;
        private float quaternySeparationMediumThreshold = 0.83f;

        //Star Distances
        private float noneDistance = 0f;
        private float binaryLow = 0.6f;
        private float binarySome = 10f;
        private float binaryMedium = 60f;
        private float binaryHigh = 600f;
        private float binaryVeryHigh = 6000f;
        private float binaryVeryVeryHigh = 60000f;

        private float subPairLow = 0.6f;
        private float subPairMedium = 10f;
        private float subPairHigh = 60f;

        private float quaternyLow = 600f;
        private float quaternyMedium = 6000f;
        private float quaternyHigh = 60000f;

        private float moreFirstTwoPairs = 600f;
        private float moreFirstFourPairs = 6000f;
        private float moreFirstSixPairs = 60000f;

        //Jovian Chances
        private float giantJovianChance = 0.08f;
        private float majorJovianChance = 0.25f;

        // Planet Numbers
        private int maxPlanetNumberNoJovian = 24;
        private int maxPlanetNumberJovian = 16;

        // Jovian Masses
        private float majorSmallJovianThreshold = 0.16f;
        private float majorNormalJovianThreshold = 0.48f;
        private float majorBigJovianThreshold = 0.82f;

        private float blueGiantNormalJovianThreshold = 0.33f;
        private float blueGiantBigJovianThreshold = 0.66f;

        private float massSmallJovian = 0.3f;
        private float massNormalJovian = 1f;
        private float massBigJovian = 3f;
        private float massTransJovian = 10f;

        // Jovian Orbit Chances
        private float giantInnerOrbitThreshold = 0.33f;
        private float giantMiddleOribitThreshold = 0.66f;

        private float majorEpistellarOrbitThreshold = 0.25f;
        private float majorInnerOrbitThreshold = 0.5f;
        private float majorMiddleOrbitThreshold = 0.875f;

        // Jovian Orbit Length 
        private float[] epistellarOrbit = {0.02f, 0.04f, 0.07f, 0.1f, 0.15f};
        private float[] epistellarThresholds = {0.1f, 0.4f, 0.6f, 0.8f};
        private float[] innerOrbit = {0.2f, 0.3f, 0.4f, 0.5f, 0.7f, 0.9f, 1.1f, 1.4f, 1.7f, 2f};
        private float[] innerThresholds = {0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f};
        private float[] middleOrbit = {2.5f, 3f, 3.5f, 4f, 4.5f, 5f, 5.5f, 6f};
        private float[] middleThresholds = {0.125f, 0.25f, 0.375f, 0.5f, 0.625f, 0.75f, 0.875f};
        private float[] outerOrbit = {7f, 8f, 9f, 10f, 11f, 12f};
        private float[] outerThresholds = {0.16f, 0.33f, 0.48f, 0.66f, 0.83f};

        // Before Jovian Planets:
        private int minPlanetsBeforeEpistellarAndInnerJovian = -1;
        private int maxPlanetsBeforeEpistellarAndInnerJovian = 2;
        private int minPlanetsBeforeOtherJovian = 1;
        private int maxPlanetsBeforeOtherJovian = 5;

        public void GenerateSystem()
        {
            system = new SolarSystem();

            system.SystemType = GetSystemType();

            switch (system.SystemType)
            {
                case SolarSystemType.Supergiant:
                    CreateSupergiantSystem();
                    break;
                case SolarSystemType.Giant:
                    CreateGiantSystem();
                    break;
                case SolarSystemType.Major:
                    CreateMajorSystem();
                    break;
                default:
                    // Debug.Warning("Illegal SystemType created, reverting to Major");
                    CreateMajorSystem();
                    break;
            }
        }

        public SolarSystemType GetSystemType()
        {
            var chance = random.NextDouble();

            if (chance >= giantSystemChance) return SolarSystemType.Major;
            chance = random.NextDouble();

            return chance < superSystemChance ? SolarSystemType.Supergiant : SolarSystemType.Giant;
        }

        private void CreateSupergiantSystem()
        {
        }

        private void CreateMajorSystem()
        {
        }

        public void CreateGiantSystem()
        {
            var chance = random.NextDouble();

            system.MainStarClass = chance < redGiantChance ? GetRedGiantStarClass() : GetBlueGiantStarClass();

            var numberOfSecondaries = GetMajorSecondaryStarNumbers();

            for (var i = 0; i < numberOfSecondaries; i++)
            {
                chance = random.NextDouble();

                if (system.MainStarClass == StarClass.BlueGiantBClassBlueWhiteMainSequence ||
                    system.MainStarClass == StarClass.BlueGiantOClassBlueMainSequence)
                {
                    if (chance < blueGiantBlueGiantSecondaryThreshold)
                    {
                        system.SecondaryStarClasses.Add(GetBlueGiantStarClass());
                    }
                    else if (chance < blueGiantMajorSecondaryThreshold)
                    {
                        system.SecondaryStarClasses.Add(GetMajorStarClass());
                    }
                    else
                    {
                        system.SecondaryStarClasses.Add(GetMinorStarClass());
                    }
                }
                else
                {
                    system.SecondaryStarClasses.Add(chance < redGiantMajorSecondaryThreshold
                        ? GetMajorStarClass()
                        : GetMinorStarClass()
                    );
                }
            }

            if (system.MainStarClass == StarClass.BlueGiantOClassBlueMainSequence)
            {
                system.SystemAge = SolarSystemAge.Newborn;
            }
            else if (system.MainStarClass == StarClass.BlueGiantBClassBlueWhiteMainSequence)
            {
                chance = random.NextDouble();

                if (chance < starAgeBMainSequenceNewbornThreshold) system.SystemAge = SolarSystemAge.Newborn;
                else system.SystemAge = SolarSystemAge.Young;

            }
            else // RedGiants are always (old) mature systems
            {
                system.SystemAge = SolarSystemAge.Mature;
            }

            CreateBarycenters();

            chance = random.NextDouble();

            if (chance < giantJovianChance)
            {
                GeneratePrimeJovian();

                var numOfInsidePlanets = random.Next(min)



            }
            else
            {

            }



        }

        private void GeneratePrimeJovian()
        {
            var chance = random.NextDouble();
            var jovian = new JovianPlanet();


            if (chance < majorSmallJovianThreshold) // We need to use other chances on Blue Giants!
            {
                jovian.Mass = massSmallJovian;
            }
            else if (chance < majorNormalJovianThreshold)
            {
                jovian.Mass = massNormalJovian;
            }
            else if (chance < majorBigJovianThreshold)
            {
                jovian.Mass = massBigJovian;
            }
            else
            {
                jovian.Mass = massTransJovian;
            }

            // Make Orbit
            JovianOrbitRules(jovian);
        }

        private void JovianOrbitRules(Planet planet, Orbit limit = Orbit.Outer)
        {
            var chance = random.NextDouble();

            if (limit == Orbit.Epistellar || chance < majorEpistellarOrbitThreshold) // Other for Blue GIANT
            {
                planet.OrbitType = Orbit.Epistellar;
                chance = random.NextDouble();
                var idx = epistellarThresholds.TakeWhile(x => x >= chance).Count();
                planet.DistanceFromCenter = epistellarOrbit[idx];
            }
            else if (limit == Orbit.Inner || chance < majorInnerOrbitThreshold)
            {
                planet.OrbitType = Orbit.Inner;
                chance = random.NextDouble();
                var idx = innerThresholds.TakeWhile(x => x >= chance).Count();
                planet.DistanceFromCenter = innerOrbit[idx];
            }
            else if (limit == Orbit.Middle || chance < majorMiddleOrbitThreshold)
            {
                planet.OrbitType = Orbit.Middle;
                chance = random.NextDouble();
                var idx = middleThresholds.TakeWhile(x => x >= chance).Count();
                planet.DistanceFromCenter = middleOrbit[idx];
            }
            else
            {
                planet.OrbitType = Orbit.Outer;
                chance = random.NextDouble();
                var idx = outerThresholds.TakeWhile(x => x >= chance).Count();
                planet.DistanceFromCenter = outerOrbit[idx];
            }

        }


        private void CreateBarycenters()
        {
            system.CreateOrderedBarycenters();

            var root = system.RootMassObject;
            if (!(root is Barycenter rootBarycenter)) return;
            
            CalculateBarycenterDistance(rootBarycenter, rootBarycenter.Depth);
        }

        private void CalculateBarycenterDistance(IMassObject massObject, int maxDepth)
        {
            if (!(massObject is Barycenter barycenter)) return;  // Return on single stars or singular systems, their distance is always 0 to themselves
            float distance;
            var currentDepth = barycenter.Depth;

            if (maxDepth > 1) // Big systems: quinary and more
            {
                if (currentDepth == 3)
                {
                    distance = moreFirstSixPairs;
                }
                else if (currentDepth == 2)
                {
                    distance = moreFirstFourPairs;
                }
                else if (currentDepth == 1)
                {
                    distance = moreFirstTwoPairs;
                }
                else
                {
                    var chance = random.NextDouble();

                    if (chance < subPairSeparationNoneThreshold) distance = noneDistance;
                    else if (chance < subPairSeparationLowThreshold) distance = subPairLow;
                    else if (chance < subPairSeparationMediumThreshold) distance = subPairMedium;
                    else distance = subPairHigh;
                }

            }
            else if (maxDepth == 1) //Trinary or quaterny systems
            {
                var chance = random.NextDouble();
                if (currentDepth == maxDepth) // main-pair
                {
                    if (chance < quaternySeparationLowThreshold) distance = quaternyLow;
                    else if (chance < quaternySeparationMediumThreshold) distance = quaternyMedium;
                    else distance = quaternyHigh; 
                }
                else // subpairs
                {
                    if (chance < subPairSeparationNoneThreshold) distance = noneDistance;
                    else if (chance < subPairSeparationLowThreshold) distance = subPairLow;
                    else if (chance < subPairSeparationMediumThreshold) distance = subPairMedium;
                    else distance = subPairHigh;
                }

            } // Binary systems
            else
            {
                var chance = random.NextDouble();

                if (chance < binarySeparationNoneThreshold) distance = noneDistance;
                else if (chance < binarySeparationLowThreshold) distance = binaryLow;
                else if (chance < binarySeparationSomeThreshold) distance = binarySome;
                else if (chance < binarySeparationMediumThreshold) distance = binaryMedium;
                else if (chance < binarySeparationHighThreshold) distance = binaryHigh;
                else if (chance < binarySeparationVeryHighThreshold) distance = binaryVeryHigh;
                else distance = binaryVeryVeryHigh;
            }

            barycenter.CalculateRelativeDistance(distance);
            
        }

        private StarClass GetSupergiantStarClass()
        {
            var chance = random.NextDouble();

            if (chance < supergiantOBClassBlueSupergiantThreshold) return StarClass.SupergiantOBClassBlueSupergiant;
            if (chance < supergiantAGClassYellowSupergiantThreshold) return StarClass.SupergiantAGClassYellowSupergiant;
            return StarClass.SupergiantKMClassRedSupergiant;
        }

        private StarClass GetBlueGiantStarClass()
        {
            var chance = random.NextDouble();

            return chance < blueGiantThreshold
                ? StarClass.BlueGiantOClassBlueMainSequence
                : StarClass.BlueGiantBClassBlueWhiteMainSequence;
        }

        private StarClass GetRedGiantStarClass()
        {
            var chance = random.NextDouble();

            if (chance < yellowGiantThreshold) return StarClass.RedGiantGClassYellowGiant;
            return chance < kClassRedGiantThreshold
                ? StarClass.RedGiantKClassRedGiant
                : StarClass.RedGiantMClassRedGiant;
        }

        private StarClass GetMajorStarClass()
        {
            var chance = random.NextDouble();

            if (chance < majorAClassWhiteMainSequenceThreshold) return StarClass.MajorAClassWhiteMainSequence;
            if (chance < majorFClassYellowWhiteMainSequenceThreshold)
                return StarClass.MajorFClassYellowWhiteMainSequence;
            if (chance < majorGClassYellowMainSequenceThreshold) return StarClass.MajorGClassYellowMainSequence;
            return chance < majorKClassOrangeMainSequenceThreshold
                ? StarClass.MajorKClassOrangeMainSequence
                : StarClass.MajorDClassWhiteDwarf;
        }

        private StarClass GetMinorStarClass()
        {
            var chance = random.NextDouble();

            return chance < minorMClassRedDwarfThreshold
                ? StarClass.MinorMClassRedDwarf
                : StarClass.MinorLClassBrownDwarf;
        }

        private int GetMajorSecondaryStarNumbers()
        {
            var chance = random.NextDouble();

            if (chance < solitaryThreshold) return 0;
            if (chance < binaryThreshold) return 1;
            return chance < trinaryThreshold ? 2 : 3;
        }
    }
}