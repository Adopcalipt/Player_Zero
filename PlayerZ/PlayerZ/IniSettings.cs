using PlayerZero.Classes;
using System.IO;

namespace PlayerZero
{
    public class IniSettings
    {
        private static readonly string sPath = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PZSet.ini";
        private static IniFile MyIni = null;

        public static void WriteSettingsIni()
        {
            LoggerLight.GetLogging("IniSettings.WriteSettingsIni");

            IniFile IniBuild = new IniFile();

            IniBuild.Write("Aggression", "" + DataStore.MySettings.iAggression + "", "Settings");
            IniBuild.Write("MaxPlayers", "" + DataStore.MySettings.iMaxPlayers + "", "Settings");
            IniBuild.Write("MinWait", "" + DataStore.MySettings.iMinWait + "", "Settings");
            IniBuild.Write("MaxWait", "" + DataStore.MySettings.iMaxWait + "", "Settings");
            IniBuild.Write("MinSession", "" + DataStore.MySettings.iMinSession + "", "Settings");
            IniBuild.Write("MaxSession", "" + DataStore.MySettings.iMaxSession + "", "Settings");
            IniBuild.Write("MinAccuracy", "" + DataStore.MySettings.iAccMin + "", "Settings");
            IniBuild.Write("MaxAccuracy", "" + DataStore.MySettings.iAccMax + "", "Settings");
            IniBuild.Write("SpaceWeaps", "" + DataStore.MySettings.bSpaceWeaps + "", "Settings");
            IniBuild.Write("Debug", "" + DataStore.MySettings.bDebugger + "", "Settings");
            IniBuild.Write("Players", "" + DataStore.MySettings.iGetlayList + "", "Controls");
            IniBuild.Write("ClearPlayers", "" + DataStore.MySettings.iClearPlayList + "", "Controls");
            IniBuild.Write("DisableMod", "" + DataStore.MySettings.iDisableMod + "", "Controls");
        }
        public static PZSettings LoadIniSetts()
        {
            LoggerLight.GetLogging("IniSettings.LoadIniSetts");

            PZSettings BuildSets = new PZSettings();
            IniFile IniBuild = new IniFile();

            if (File.Exists(sPath))
            {
                BuildSets.iAggression = ReadMyInt(IniBuild.Read("Aggression", "Settings"));
                BuildSets.iMaxPlayers = ReadMyInt(IniBuild.Read("MaxPlayers", "Settings"));
                BuildSets.iMinWait = ReadMyInt(IniBuild.Read("MinWait", "Settings"));
                BuildSets.iMaxWait = ReadMyInt(IniBuild.Read("MaxWait", "Settings"));
                BuildSets.iMinSession = ReadMyInt(IniBuild.Read("MinSession", "Settings"));
                BuildSets.iMaxSession = ReadMyInt(IniBuild.Read("MaxSession", "Settings"));
                BuildSets.iAccMin = ReadMyInt(IniBuild.Read("MinAccuracy", "Settings"));
                BuildSets.iAccMax = ReadMyInt(IniBuild.Read("MaxAccuracy", "Settings"));
                BuildSets.bSpaceWeaps = ReadMyBool(IniBuild.Read("SpaceWeaps", "Settings"));
                BuildSets.bDebugger = ReadMyBool(IniBuild.Read("Debug", "Settings"));
                BuildSets.iGetlayList = ReadMyInt(IniBuild.Read("Players", "Controls"));
                BuildSets.iClearPlayList = ReadMyInt(IniBuild.Read("ClearPlayers", "Controls"));
                BuildSets.iDisableMod = ReadMyInt(IniBuild.Read("DisableMod", "Controls"));
            }
            else
            {
                WriteSettingsIni();
            }

            return BuildSets;
        }
        public static int ReadMyInt(string sTing)
        {
            LoggerLight.GetLogging("IniSettings.ReadMyInt == " + sTing);
            int iNum = 0;
            int iTimes = 0;
            for (int i = sTing.Length - 1; i > -1; i--)
            {
                int iAdd = 0;
                string sComp = sTing.Substring(i, 1);

                if (sComp == "1")
                    iAdd = 1;
                else if (sComp == "2")
                    iAdd = 2;
                else if (sComp == "3")
                    iAdd = 3;
                else if (sComp == "4")
                    iAdd = 4;
                else if (sComp == "5")
                    iAdd = 5;
                else if (sComp == "6")
                    iAdd = 6;
                else if (sComp == "7")
                    iAdd = 7;
                else if (sComp == "8")
                    iAdd = 8;
                else if (sComp == "9")
                    iAdd = 9;

                if (iTimes == 0)
                {
                    iNum = iAdd;
                    iTimes = 1;
                }
                else
                    iNum += iAdd * iTimes;
                iTimes *= 10;
            }
            return iNum;
        }
        public static bool ReadMyBool(string sTing)
        {
            LoggerLight.GetLogging("IniSettings.ReadMyBool == " + sTing);

            bool b = false;
            if (sTing.Contains("True") || sTing.Contains("true") || sTing.Contains("TRUE"))
                b = true;
            return b;
        }
    }
}
