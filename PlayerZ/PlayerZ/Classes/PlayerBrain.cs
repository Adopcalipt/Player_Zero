using GTA;

namespace PlayerZero.Classes
{
    public class PlayerBrain
    {
        public Ped ThisPed { get; set; }
        public Ped ThisEnemy { get; set; }
        public Vehicle ThisVeh { get; set; }
        public Vehicle ThisOppress { get; set; }
        public Blip ThisBlip { get; set; }
        public Blip DirBlip { get; set; }

        public int DeathSequence { get; set; }
        public int DeathTime { get; set; }
        public int Kills { get; set; }
        public int Killed { get; set; }
        public int TimeOn { get; set; }
        public int FindPlayer { get; set; }
        public int OffRadar { get; set; }
        public int PlaneLand { get; set; }
        public int FlightPath { get; set; }

        public bool OffRadarBool { get; set; }
        public bool Bounty { get; set; }
        public bool InCombat { get; set; }
        public bool SessionJumper { get; set; }
        public bool Horny { get; set; }
        public bool Driver { get; set; }
        public bool EnterVehQue { get; set; }
        public bool Passenger { get; set; }
        public bool Befallen { get; set; }
        public bool WanBeFriends { get; set; }
        public bool IsPlane { get; set; }
        public bool IsHeli { get; set; }

        public int BlipColour { get; set; }
        public int Level { get; set; }
        public int PrefredVehicle { get; set; }
        public bool Hacker { get; set; }
        public bool Friendly { get; set; }
        public bool Follower { get; set; }
        public bool ApprochPlayer { get; set; }
        public string MyName { get; set; }
        public string MyIdentity { get; set; }
        public PedFixtures PFMySetting { get; set; }

        public PlayerBrain()
        {
            ThisPed = null;
            ThisVeh = null;
            ThisOppress = null;
            ThisBlip = null;
            DirBlip = null;

            DeathSequence = 0;
            DeathTime = 0;
            Kills = 0;
            Killed = 0;
            TimeOn = 0;
            FindPlayer = 0;
            PlaneLand = -1;
            OffRadar = 0;
            OffRadarBool = false;
            Bounty = false;
            InCombat = false;
            SessionJumper = false;
            Horny = false;
            Driver = false;
            EnterVehQue = false;
            Passenger = false;
            Befallen = false;
            WanBeFriends = false;
            ApprochPlayer = true;
            IsPlane = false;
            IsHeli = false;

            BlipColour = 0;
            Level = 0;
            PrefredVehicle = 0;
            Hacker = false;
            Friendly = true;
            Follower = false;
            MyName = "";
            MyIdentity = "";
            PedFixtures PFMySetting = new PedFixtures();
        }
    }
}
