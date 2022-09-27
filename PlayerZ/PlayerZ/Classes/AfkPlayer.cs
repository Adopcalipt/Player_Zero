using GTA;

namespace PlayerZero.Classes
{
    public class AfkPlayer
    {
        public Blip ThisBlip { get; set; }
        public int TimeOn { get; set; }
        public int App { get; set; }
        public string MyName { get; set; }
        public string MyIdentity { get; set; }
        public int Level { get; set; }

        public AfkPlayer(Blip thisBlip, int timeOn, int app, string myName, string myId, int iLevel)
        {
            ThisBlip = thisBlip;
            TimeOn = timeOn;
            App = app;
            MyName = myName;
            MyIdentity = myId;
            Level = iLevel;
        }
    }
}
