using System;
using System.Runtime.CompilerServices;

namespace GalaxyGenerator
{
    public enum StarClass
    {
        RedGiantGClassYellowGiant = 14,
        RedGiantKClassRedGiant = 15,
        RedGiantMClassRedGiant = 16,
        BlueGiantOClassBlueMainSequence = 250,
        BlueGiantBClassBlueWhiteMainSequence = 50,
        SupergiantOBClassBlueSupergiant = 250,
        SupergiantAGClassYellowSupergiant = 150,
        SupergiantKMClassRedSupergiant = 100,
        MajorAClassWhiteMainSequence = 10,
        MajorFClassYellowWhiteMainSequence = 8,
        MajorGClassYellowMainSequence = 5,
        MajorKClassOrangeMainSequence = 3,
        MajorDClassWhiteDwarf = 7,
        MinorMClassRedDwarf = 1,
        MinorLClassBrownDwarf = 1


    }


    public static class StarClassMethods
    {
        public static int GetMass(this StarClass sc)
        {
            switch (sc)
            {
                case StarClass.RedGiantGClassYellowGiant:
                    return 14;
                case StarClass.RedGiantKClassRedGiant:
                    return 15;
                case StarClass.RedGiantMClassRedGiant:
                    return 16;
                case StarClass.BlueGiantOClassBlueMainSequence:
                    return 250;
                case StarClass.BlueGiantBClassBlueWhiteMainSequence:
                    return 50;
                case StarClass.SupergiantAGClassYellowSupergiant:
                    return 250;
                case StarClass.SupergiantKMClassRedSupergiant:
                    return 150;
                case StarClass.MajorAClassWhiteMainSequence:
                    return 100;
                case StarClass.MajorFClassYellowWhiteMainSequence:
                    return 10;
                case StarClass.MajorGClassYellowMainSequence:
                    return 8;
                case StarClass.MajorKClassOrangeMainSequence:
                    return 5;
                case StarClass.MajorDClassWhiteDwarf:
                    return 3;
                case StarClass.MinorMClassRedDwarf:
                    return 1;
                default:
                    return 1;
            }

        }

    }
    
}
