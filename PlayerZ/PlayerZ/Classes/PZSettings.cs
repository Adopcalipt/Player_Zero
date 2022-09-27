namespace PlayerZero.Classes
{
    public class PZSettings
    {
        public int Aggression { get; set; }
        public int MaxPlayers { get; set; }
        public int MinWait { get; set; }
        public int MaxWait { get; set; }
        public int MinSession { get; set; }
        public int MaxSession { get; set; }
        public int AccMin { get; set; }
        public int AccMax { get; set; }
        public int GetlayList { get; set; }
        public int DisableMod { get; set; }

        public bool SpaceWeaps { get; set; }
        public bool Debugger { get; set; }
        public bool NoNotify { get; set; }
        public bool NoRadar { get; set; }

        public PZSettings()
        {
            Aggression = 5;
            MaxPlayers = 29;
            MinWait = 15000;
            MaxWait = 45000;
            MinSession = 60000;
            MaxSession = 300000;
            AccMin = 25;
            AccMax = 75;
            GetlayList = 19;
            DisableMod = 22;

            SpaceWeaps = true;
            Debugger = false;
            NoNotify = false;
            NoRadar = false;
        }
    }
}
