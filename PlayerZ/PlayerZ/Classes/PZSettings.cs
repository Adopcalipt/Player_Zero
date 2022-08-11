namespace PlayerZero.Classes
{
    public class PZSettings
    {
        public int iAggression { get; set; }
        public int iMaxPlayers { get; set; }
        public int iMinWait { get; set; }
        public int iMaxWait { get; set; }
        public int iMinSession { get; set; }
        public int iMaxSession { get; set; }
        public int iAccMin { get; set; }
        public int iAccMax { get; set; }
        public int iGetlayList { get; set; }
        public int iClearPlayList { get; set; }
        public int iDisableMod { get; set; }

        public bool bSpaceWeaps { get; set; }
        public bool bDebugger { get; set; }

        public PZSettings()
        {
            iAggression = 5;
            iMaxPlayers = 29;
            iMinWait = 15000;
            iMaxWait = 45000;
            iMinSession = 60000;
            iMaxSession = 300000;
            iAccMin = 25;
            iAccMax = 75;
            iGetlayList = 19;
            iClearPlayList = 131;
            iDisableMod = 73;

            bSpaceWeaps = false;
            bDebugger = false;
        }
    }
}
