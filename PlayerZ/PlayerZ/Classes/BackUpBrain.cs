using System.Collections.Generic;

namespace PlayerZero.Classes
{
    public class BackUpBrain
    {
        public List<int> BigBrain { get; set; }
        public List<int> BigVeh { get; set; }
        public List<int> BigBlip { get; set; }
        public List<int> BigDirBlip { get; set; }
        public List<int> BigAFKBlip { get; set; }

        public BackUpBrain()
        {
            BigBrain = new List<int>();
            BigVeh = new List<int>();
            BigBlip = new List<int>();
            BigDirBlip = new List<int>();
            BigAFKBlip = new List<int>();
        }
    }
}
