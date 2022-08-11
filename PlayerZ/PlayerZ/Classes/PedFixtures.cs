using System.Collections.Generic;

namespace PlayerZero.Classes
{
    public class PedFixtures
    {
        public bool PFMale { get; set; }

        public int PFshapeFirstID { get; set; }
        public int PFshapeSecondID { get; set; }
        public int PFshapeThirdID { get; set; }
        public int PFskinFirstID { get; set; }
        public int PFskinSecondID { get; set; }
        public int PFskinThirdID { get; set; }
        public float PFshapeMix { get; set; }
        public float PFskinMix { get; set; }
        public float PFthirdMix { get; set; }

        public List<int> PFFeature { get; set; }
        public List<int> PFChange { get; set; }
        public List<int> PFColour { get; set; }
        public List<float> PFAmount { get; set; }
        public List<string> TatBase { get; set; }
        public List<string> TatName { get; set; }

        public int iHairCut { get; set; }
        public int PFHair01 { get; set; }
        public int PFHair02 { get; set; }
        public ClothBank PedClothB { get; set; }

        public PedFixtures()
        {
            PFFeature = new List<int>();
            PFChange = new List<int>();
            PFColour = new List<int>();
            PFAmount = new List<float>();
            TatBase = new List<string>();
            TatName = new List<string>();
        }
    }
}
