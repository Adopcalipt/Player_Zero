
namespace PlayerZero.Classes
{
    public class FindVeh
    {
        public float MinRadi { get; set; }
        public float MaxRadi { get; set; }
        public bool AddPlayer { get; set; }
        public bool CanFill { get; set; }
        public PlayerBrain Brains { get; set; }

        public FindVeh(float minRadi, float maxRadi, bool addPlayer, bool canFill, PlayerBrain newBrain)
        {
            MinRadi = minRadi; MaxRadi = maxRadi; AddPlayer = addPlayer; CanFill = canFill; Brains = newBrain;
        }
    }
}
