using GTA;

namespace PlayerZero.Classes
{
    public class FindPed
    {
        public float MinRadi { get; set; }
        public float MaxRadi { get; set; }
        public PlayerBrain Brains { get; set; }

        public FindPed(float minRadi, float maxRadi, PlayerBrain newBrain)
        {
            MinRadi = minRadi; MaxRadi = maxRadi; Brains = newBrain; 
        }
    }
}
