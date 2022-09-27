using PlayerZero.Classes;
using System.IO;

namespace PlayerZero
{
    public class IniSettings
    {
        private static readonly string sPath = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/Settings.ini";

        public static void WriteSettingsIni()
        {
            LoggerLight.GetLogging("IniSettings.WriteSettingsIni");

            IniFile IniBuild = new IniFile();

            IniBuild.Write("Aggression", "" + DataStore.MySettings.Aggression + "", "Settings");
            IniBuild.Write("MaxPlayers", "" + DataStore.MySettings.MaxPlayers + "", "Settings");
            IniBuild.Write("MinWait", "" + DataStore.MySettings.MinWait + "", "Settings");
            IniBuild.Write("MaxWait", "" + DataStore.MySettings.MaxWait + "", "Settings");
            IniBuild.Write("MinSession", "" + DataStore.MySettings.MinSession + "", "Settings");
            IniBuild.Write("MaxSession", "" + DataStore.MySettings.MaxSession + "", "Settings");
            IniBuild.Write("MinAccuracy", "" + DataStore.MySettings.AccMin + "", "Settings");
            IniBuild.Write("MaxAccuracy", "" + DataStore.MySettings.AccMax + "", "Settings");
            IniBuild.Write("SpaceWeaps", "" + DataStore.MySettings.SpaceWeaps + "", "Settings");
            IniBuild.Write("Debug", "" + DataStore.MySettings.Debugger + "", "Settings");
            IniBuild.Write("NoRadar", "" + DataStore.MySettings.NoRadar + "", "Settings");
            IniBuild.Write("NoNotify", "" + DataStore.MySettings.NoNotify + "", "Settings");
            IniBuild.Write("Players", "" + DataStore.MySettings.GetlayList + "", "Controls");
            IniBuild.Write("MenuOpen", "" + DataStore.MySettings.DisableMod + "", "Controls");
        }
        public static PZSettings LoadIniSetts()
        {
            LoggerLight.GetLogging("IniSettings.LoadIniSetts");

            PZSettings buildSets = new PZSettings();

            IniFile IniBuild = new IniFile();
            if (File.Exists(sPath))
            {
                buildSets.Aggression = ReadMyInt(IniBuild.Read("Aggression", "Settings"));
                buildSets.MaxPlayers = ReadMyInt(IniBuild.Read("MaxPlayers", "Settings"));
                buildSets.MinWait = ReadMyInt(IniBuild.Read("MinWait", "Settings"));
                buildSets.MaxWait = ReadMyInt(IniBuild.Read("MaxWait", "Settings"));
                buildSets.MinSession = ReadMyInt(IniBuild.Read("MinSession", "Settings"));
                buildSets.MaxSession = ReadMyInt(IniBuild.Read("MaxSession", "Settings"));
                buildSets.AccMin = ReadMyInt(IniBuild.Read("MinAccuracy", "Settings"));
                buildSets.AccMax = ReadMyInt(IniBuild.Read("MaxAccuracy", "Settings"));
                buildSets.SpaceWeaps = ReadMyBool(IniBuild.Read("SpaceWeaps", "Settings"));
                buildSets.Debugger = ReadMyBool(IniBuild.Read("Debug", "Settings"));
                buildSets.NoRadar = ReadMyBool(IniBuild.Read("NoRadar", "Settings"));
                buildSets.NoNotify = ReadMyBool(IniBuild.Read("NoNotify", "Settings"));
                buildSets.GetlayList = ReadMyInt(IniBuild.Read("Players", "Controls"));
                buildSets.DisableMod = ReadMyInt(IniBuild.Read("MenuOpen", "Controls"));
            }
            else
            {
                WriteSettingsIni();
            }

            return buildSets;
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
