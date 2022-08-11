using GTA;

namespace PlayerZero.Classes
{
    public class PlayerBrain
    {
        public Ped ThisPed { get; set; }
        public Vehicle ThisVeh { get; set; }
        public Blip ThisBlip { get; set; }
        public Blip DirBlip { get; set; }
        public int DeathSequence { get; set; }
        public int DeathTime { get; set; }
        public int Colours { get; set; }
        public int TimeOn { get; set; }
        public int Level { get; set; }
        public int Kills { get; set; }
        public int Killed { get; set; }
        public int OffRadar { get; set; }
        public int AirAttack { get; set; }
        public float AirDirect { get; set; }
        public bool OffRadarBool { get; set; }
        public bool Bounty { get; set; }
        public bool Hacker { get; set; }
        public bool InCombat { get; set; }
        public bool Friendly { get; set; }
        public bool Follower { get; set; }
        public bool ApprochPlayer { get; set; }
        public bool SessionJumper { get; set; }
        public bool Horny { get; set; }
        public bool Horny2 { get; set; }
        public bool Driver { get; set; }
        public bool Pilot { get; set; }
        public bool EnterVehQue { get; set; }
        public bool Passenger { get; set; }
        public bool Befallen { get; set; }
        public int FindPlayer { get; set; }
        public string MyName { get; set; }
        public PedFixtures PFMySetting { get; set; }

        public Vehicle ThisOppress { get; set; }
    }
}
