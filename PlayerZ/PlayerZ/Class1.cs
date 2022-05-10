using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using GTA;
using GTA.Native;
using GTA.Math;

namespace OnlinePlayerz
{
    public class Main : Script
    {
        private ScriptSettings GetKeys;

        private bool bTrainM = true;//set the test then set to false
        private bool bDebugger = false;
        private bool bLoadUp = true;
        private bool bPlayerList = false;
        private bool bHeistPop = true;
        private bool bHackerIn = false;
        private bool bPiggyBack = false;
        private bool bHackEvent = false;
        private bool bSpaceWeaps = false;       
        private bool bDisabled = false;

        private readonly string sVersion = "1.7";
        private readonly string sBeeLogs = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZLog.txt";
        private readonly string sMemory = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZsMemory.xml";
        private readonly string sOutfitts = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/Outfits.xml";
        private readonly string sRandFile = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/RanNum.Xml";
        private readonly string sPath = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PZSet.ini";

        private int iMaxPlayers = 29;
        private int iNpcList = 0;
        private int iBlpList = 0;
        private int iRotateFind = 0;
        private int iFollow = 0;
        private int iFolPos = 0;
        private int iCurrentPlayerz = 0;
        private int iOrbBurnOut = 0;
        private int iFindingTime = 0;
        private int iScale = 0;
        private int iMinWait = 15000;
        private int iMaxWait = 45000;
        private int iAccMin = 25;
        private int iAccMax = 75;
        private int iMinSession = 60000;
        private int iMaxSession = 300000;
        private int iAggression = 5;
        private int iGetlayList = 19;
        private int iClearPlayList = 131;
        private int iDisableMod = 73;

        private int iFollowMe = -1;
        private int GP_Player = Game.Player.Character.RelationshipGroup;
        private readonly int Gp_Friend = World.AddRelationshipGroup("FrendlyNPCs");
        private readonly int GP_Attack = World.AddRelationshipGroup("AttackNPCs");
        private readonly int Gp_Follow = World.AddRelationshipGroup("FollowerNPCs");
        private readonly int GP_Mental = World.AddRelationshipGroup("MentalNPCs");

        private Vector3 LetsGoHere = Vector3.Zero;

        private BackUpBrain BlowenMyBrains = new BackUpBrain();

        private List<bool> BeOnOff = new List<bool>();
        private List<int> iTimers = new List<int>();

        private List<Prop> Plops = new List<Prop>();
        private List<Vehicle> Vicks = new List<Vehicle>();
        private List<Vector3> AFKPlayers = new List<Vector3>();
        
        private List<PlayerBrain> PedList = new List<PlayerBrain>();
        private List<AfkPlayer> AFKList = new List<AfkPlayer>();

        private List<ClothBank> MaleCloth = new List<ClothBank>();
        private List<ClothBank> FemaleCloth = new List<ClothBank>();

        private PositionDirect FindMe = null;
        private List<FindVeh> MakeCarz = new List<FindVeh>();
        private List<FindPed> MakeFrenz = new List<FindPed>();
        private List<GetInAveh> GetInQUe = new List<GetInAveh>();

        private IniFile MyIni = new IniFile();

        class IniFile   // revision 11
        {
            string Path;
            string EXE = "PZSet";

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile()
            {
                Path = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PZSet.ini";
            }
            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }
            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }
            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }
            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }
            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }
        public int ReadMyInt(string sTing)
        {
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
        public bool ReadMyBool(string sTing)
        {
            bool b = false;
            if (sTing.Contains("True") || sTing.Contains("true") || sTing.Contains("TRUE"))
                b = true;
            return b;
        }
        public Main()
        {
            Tick += OnTick;
            Interval = 1;
        }
        private void LoadIniSetts()
        {
            GetLogging("LoadSettings");
            if (File.Exists(sPath))
            {
                iAggression = ReadMyInt(MyIni.Read("Aggression", "Settings"));
                iMaxPlayers = ReadMyInt(MyIni.Read("MaxPlayers", "Settings"));
                iMinWait = ReadMyInt(MyIni.Read("MinWait", "Settings"));
                iMaxWait = ReadMyInt(MyIni.Read("MaxWait", "Settings"));
                iMinSession = ReadMyInt(MyIni.Read("MinSession", "Settings"));
                iMaxSession = ReadMyInt(MyIni.Read("MaxSession", "Settings"));
                iAccMin = ReadMyInt(MyIni.Read("MinAccuracy", "Settings"));
                iAccMax = ReadMyInt(MyIni.Read("MaxAccuracy", "Settings"));
                bSpaceWeaps = ReadMyBool(MyIni.Read("SpaceWeaps", "Settings"));
                bDebugger = ReadMyBool(MyIni.Read("Debug", "Settings"));
                iGetlayList = ReadMyInt(MyIni.Read("Players", "Controls"));
                iClearPlayList = ReadMyInt(MyIni.Read("ClearPlayers", "Controls"));
                iDisableMod = ReadMyInt(MyIni.Read("DisableMod", "Controls"));
            }
            else
            {
                MyIni.Write("Aggression", "" + iAggression + "", "Settings");
                MyIni.Write("MaxPlayers", "" + iMaxPlayers + "", "Settings");
                MyIni.Write("MinWait", "" + iMinWait + "", "Settings");
                MyIni.Write("MaxWait", "" + iMaxWait + "", "Settings");
                MyIni.Write("MinSession", "" + iMinSession + "", "Settings");
                MyIni.Write("MaxSession", "" + iMaxSession + "", "Settings");
                MyIni.Write("MinAccuracy", "" + iAccMin + "", "Settings");
                MyIni.Write("MaxAccuracy", "" + iAccMax + "", "Settings");
                MyIni.Write("SpaceWeaps", "" + bSpaceWeaps + "", "Settings");
                MyIni.Write("Debug", "" + bDebugger + "", "Settings");
                MyIni.Write("Players", "" + iGetlayList + "", "Controls");
                MyIni.Write("ClearPlayers", "" + iClearPlayList + "", "Controls");
                MyIni.Write("DisableMod", "" + iDisableMod + "", "Controls");
            }
        }
        private void LoadSettings()
        {
            GetLogging("LoadSettings");

            string sTheIni = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZSettings.ini";
            if (File.Exists(sTheIni))
            {
                GetKeys = ScriptSettings.Load(sTheIni);

                iAggression = GetKeys.GetValue<int>("Settings", "Aggression", 7);
                iMaxPlayers = GetKeys.GetValue<int>("Settings", "MaxPlayers", 29);
                iMinWait = GetKeys.GetValue<int>("Settings", "iMinWait", 15000);
                iMaxWait = GetKeys.GetValue<int>("Settings", "iMaxWait", 45000);
                iMinSession = GetKeys.GetValue<int>("Settings", "iMinSession", 30000);
                iMaxSession = GetKeys.GetValue<int>("Settings", "iMaxSession", 475000);
                iAccMin = GetKeys.GetValue<int>("Settings", "iMinAccuracy", 25);
                iAccMax = GetKeys.GetValue<int>("Settings", "iMaxAccuracy", 75);
                bSpaceWeaps = GetKeys.GetValue<bool>("Settings", "SpaceWeaps", false);
                iGetlayList = GetKeys.GetValue<int>("Settings", "iGetlayList", 19);
                iClearPlayList = GetKeys.GetValue<int>("Settings", "iClearPlayList", 131);
                iDisableMod = GetKeys.GetValue<int>("Settings", "iDisableMod", 28);
                bDebugger = GetKeys.GetValue<bool>("Settings", "bDebugger", false);
            }

            if (iAggression > 11)
                iAggression = 11;
            else if (iAggression < 0)
                iAggression = 0;

            if (iMaxPlayers > 29)
                iMaxPlayers = 29;
            else if (iMaxPlayers < 5)
                iMaxPlayers = 5;

            if (iMinWait > iMaxWait)
                iMaxWait = iMinWait + 1;

            if (iAccMin > iAccMax)
                iAccMax = iAccMin + 1;

            if (iAccMax > 99)
                iAccMax = 100;

            if (iAccMin < 1)
                iAccMax = 0;

            if (iMinSession > iMaxSession)
                iMaxSession = iMinSession + 1;
        }
        private void BeeLog(string sLogs, TextWriter tEx)
        {
            unsafe
            {
                tEx.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} {"--" + sLogs}");
            }
        }
        private void GetLogging(string sMyLog)
        {
            if (bDebugger)
            {
                using (StreamWriter tEx = File.AppendText(sBeeLogs))
                    BeeLog(sMyLog, tEx);
            }
        }
        private void LoadUp()
        {
            if (File.Exists(sBeeLogs))
                File.Delete(sBeeLogs);

            GetLogging("LoadUp");

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, GP_Player, Gp_Follow);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, Gp_Follow, GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, Gp_Follow, Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, Gp_Friend, Gp_Follow);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Attack, Gp_Follow);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, Gp_Follow, GP_Attack);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Mental, Gp_Follow);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, Gp_Follow, GP_Mental);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, GP_Player, Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 2, Gp_Friend, GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Attack, Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, Gp_Friend, GP_Attack);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Player, GP_Attack);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Attack, GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Mental, Gp_Friend);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, Gp_Friend, GP_Mental);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Attack, GP_Mental);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Mental, GP_Attack);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Player, GP_Mental);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Mental, GP_Player);

            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, GP_Mental, GP_Mental);

            iFollowMe = Function.Call<int>(Hash.CREATE_GROUP);
            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Game.Player.Character.Handle, iFollowMe);
            Function.Call(Hash.SET_GROUP_FORMATION, iFollowMe, 3);

            iTimers.Clear();
            SetBTimer(30000, -1);
            SetBTimer(RandInt(iMinWait, iMaxWait), -1);
            LoadIniSetts();
            BuildList();
            FindMisfits();
        }
        private void SetBTimer(int AddTime, int iSetPos)
        {
            if (iSetPos != -1)
            {
                if (iSetPos < iTimers.Count)
                    iTimers[iSetPos] = Game.GameTime + AddTime;
                else
                    iTimers.Add(Game.GameTime + AddTime);
            }
            else
                iTimers.Add(Game.GameTime + AddTime);
        }
        public bool BTimer(int iMyTime)
        {
            bool bIntime = false;

            if (iMyTime < iTimers.Count)
            {
                if (iTimers[iMyTime] < Game.GameTime)
                    bIntime = true;
            }
            return bIntime;
        }
        public class RandomPlusList
        {
            public List<RandomPlus> BigRanList { get; set; }

            public RandomPlusList()
            {
                BigRanList = new List<RandomPlus>();
            }
        }
        public class RandomPlus
        {
            public List<int> RandNums { get; set; }

            public RandomPlus()
            {
                RandNums = new List<int>();
            }
        }
        public RandomPlusList LoadRando(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(RandomPlusList));
            using (StreamReader sr = new StreamReader(fileName))
            {
                return (RandomPlusList)xml.Deserialize(sr);
            }
        }
        public void SaveRando(RandomPlusList config, string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(RandomPlusList));
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                xml.Serialize(sw, config);
            }
        }
        public int FindRandom(int iList, int iMin, int iMax)
        {
            GetLogging("FindRandom, iList == " + iList);

            int iBe = 0;
            RandomPlusList XSets = new RandomPlusList();

            if (File.Exists(sRandFile))
            {
                XSets = LoadRando(sRandFile);

                if (XSets.BigRanList.Count() < iList + 1)
                {
                    for (int i = XSets.BigRanList.Count() - 1; i < iList + 1; i++)
                    {
                        RandomPlus iBlank = new RandomPlus();
                        XSets.BigRanList.Add(iBlank);
                    }
                }

                for (int i = 0; i < XSets.BigRanList[iList].RandNums.Count; i++)
                {
                    if (XSets.BigRanList[iList].RandNums[i] > iMax || XSets.BigRanList[iList].RandNums[i] < iMin)
                        XSets.BigRanList[iList].RandNums.RemoveAt(i);
                }

                if (XSets.BigRanList[iList].RandNums.Count == 0)
                {
                    for (int i = iMin; i < iMax + 1; i++)
                        XSets.BigRanList[iList].RandNums.Add(i);
                }

                int iRanNum = RandInt(0, XSets.BigRanList[iList].RandNums.Count - 1);
                iBe = XSets.BigRanList[iList].RandNums[iRanNum];
                XSets.BigRanList[iList].RandNums.RemoveAt(iRanNum);
            }
            else
            {
                for (int i = 0; i < iList + 1; i++)
                {
                    RandomPlus iBlank = new RandomPlus();
                    XSets.BigRanList.Add(iBlank);
                }

                for (int i = iMin; i < iMax + 1; i++)
                    XSets.BigRanList[iList].RandNums.Add(i);

                int iRanNum = RandInt(0, XSets.BigRanList[iList].RandNums.Count - 1);
                iBe = XSets.BigRanList[iList].RandNums[iRanNum];
                XSets.BigRanList[iList].RandNums.RemoveAt(iRanNum);
            }
            SaveRando(XSets, sRandFile);

            return iBe;
        }
        public string SillyNameList()
        {
            GetLogging("SillyNameList");

            string MySilly = "";

            List<string> sSillyNames = new List<string>
            {
                "0",              //0
                "1",              //1
                "2",              //2
                "3",              //3
                "4",              //4
                "5",              //5
                "6",              //6
                "7",              //7
                "8",              //8
                "9",              //9
                "ay",             //10
                "ee",             //11
                "igh",            //12
                "ow",             //13
                "oo",             //14
                "or",             //15
                "air",            //16
                "ir",             //17
                "ou",             //18
                "oy",             //19
                "ai",             //20
                "ea",             //21
                "ie",             //22
                "ew",             //23
                "ur",             //24
                "ow",             //25
                "oi",             //26
                "ire",            //27
                "ear",            //28
                "ure",            //29
                "tion",           //30
                "ey",             //31
                "ore",            //32
                "ere",            //33
                "oor",            //34
                "X",              //35
                "-",              //36
                "^",              //37
                "*",              //38
                "#",              //39
                "$",              //40
                "TyHrd",          //41
                "Luzz",           //42
                "Killz",          //43
                "| | |",          //44
                "{[]}",           //45
                "A",              //46
                "B",              //47
                "C",              ///48
                "D",              ///49
                "E",              ///50
                "F",              ///51
                "G",              ///52
                "H",              ///53
                "I",              ///54
                "J",              ///55
                "K",              ///56
                "L",              ///57
                "M",              ///58
                "N",              ///59
                "O",              ///60
                "P"               ///61
            };

            int iName = FindRandom(3, 2, 3);

            for (int i = 0; i < iName; i++)
                MySilly = MySilly + sSillyNames[FindRandom(4, 10, 34)];

            MySilly.Remove(0, 1);
            MySilly = sSillyNames[FindRandom(5, 46, 61)] + MySilly;

            if (MySilly.Length < 8)
            {
                if (FindRandom(7, 0, 20) < 15)
                {
                    iName = FindRandom(8, 1, 4);
                    for (int i = 0; i < iName; i++)
                        MySilly = MySilly + sSillyNames[FindRandom(9, 0, 9)];
                }
                else
                {
                    string sPrefix1 = sSillyNames[FindRandom(10, 35, 40)];
                    string sPrefix2 = sSillyNames[FindRandom(11, 35, 40)];

                    MySilly = sPrefix1 + sPrefix2 + MySilly + sPrefix2 + sPrefix1;
                }
            }
            else if (MySilly.Length < 4)
                MySilly = MySilly + sSillyNames[FindRandom(6, 41, 45)];

            return MySilly;
        }
        private void InABuilding()
        {
            GetLogging("InABuilding");

            List<int> iKeepItReal = new List<int>();

            for (int i = 0; i < AFKList.Count(); i++)
                iKeepItReal.Add(AFKList[i].App);

            int iMit = RandInt(0, AFKPlayers.Count - 1);

            while (iKeepItReal.Contains(iMit))
                iMit = RandInt(0, AFKPlayers.Count - 1);

            string sName = SillyNameList();
            Blip FakeB = LocalBlip(AFKPlayers[iMit], 417, sName);

            AfkPlayer MyAfk = new AfkPlayer
            {
                ThisBlip = FakeB,
                App = iMit,
                Level = FindRandom(12, 1, 400),
                TimeOn = Game.GameTime + RandInt(iMinSession, iMaxSession),
                MyName = sName
            };
            AFKList.Add(MyAfk);

            BackItUpBrain();
        }
        private void BuildList()
        {
            GetLogging("BuildList");

            //.Name = "3 Alta St"
            AFKPlayers.Add(new Vector3(-259.8061F, -969.4397F, 30.21999F));
            //.Name = "4 Integrity Way"
            AFKPlayers.Add(new Vector3(-48.77471F, -589.6611F, 36.95303F));
            //.Name = "Del Perro Hts"
            AFKPlayers.Add(new Vector3(-1441.338F, -544.1608F, 33.74182F));
            //.Name = "Eclipse Towers"
            AFKPlayers.Add(new Vector3(-778.8126F, 312.6634F, 84.69843F));
            //.Name = "Richards Majestic"
            AFKPlayers.Add(new Vector3(-935.0035F, -380.3281F, 37.96134F));
            //.Name = "Tinsel Towers"
            AFKPlayers.Add(new Vector3(-614.3498F, 36.95148F, 42.57011F));
            //.Name = "Weazel Plaza"
            AFKPlayers.Add(new Vector3(-913.6771F, -456.4787F, 38.59988F));
            //.Name = "2862 Hillcrest Avenue"
            AFKPlayers.Add(new Vector3(-687.3952F, 595.6556F, 142.642F));
            //.Name = "2866 Hillcrest Avenue"
            AFKPlayers.Add(new Vector3(-733.7261F, 594.4695F, 141.181F));
            //.Name = "2868 Hillcrest Avenue"
            AFKPlayers.Add(new Vector3(-753.1172F, 622.2552F, 141.5237F));
            //.Name = "2874 Hillcrest Avenue"
            AFKPlayers.Add(new Vector3(-854.3719F, 695.4094F, 147.7927F));
            //.Name = "2113 Mad Wayne Thunder Drive"
            AFKPlayers.Add(new Vector3(-1295.323F, 454.9241F, 96.50246F));
            //.Name = "2117 Milton Road"
            AFKPlayers.Add(new Vector3(-558.5587F, 664.2407F, 144.4564F));
            //.Name = "2044 North Conker Avenue"
            AFKPlayers.Add(new Vector3(346.2178F, 441.8962F, 146.702F));
            //.Name = "2045 North Conker Avenue"
            AFKPlayers.Add(new Vector3(372.3179F, 427.8956F, 144.6842F));
            //.Name = "3677 Whispymound Drive"
            AFKPlayers.Add(new Vector3(118.2447F, 564.3248F, 182.9595F));
            //.Name = "3655 Wild Oats Drive"
            AFKPlayers.Add(new Vector3(-176.128F, 501.8195F, 136.42F));
            //.Name = "0115 Bay City Ave"
            AFKPlayers.Add(new Vector3(-968.9597F, -1433.243F, 6.679171F));
            //.Name = "Dream Tower"
            AFKPlayers.Add(new Vector3(-763.0345F, -750.8807F, 26.87314F));
            //.Name = "4 Hangman Ave"
            AFKPlayers.Add(new Vector3(-1406.162F, 528.3837F, 122.8313F));
            //.Name = "0604 Las Lagunas Blvd"
            AFKPlayers.Add(new Vector3(11.93229F, 81.0723F, 77.43513F));
            //.Name = "0184 Milton Rd"
            AFKPlayers.Add(new Vector3(-510.2473F, 108.7654F, 62.80054F));
            //.Name = "1162 Power St"
            AFKPlayers.Add(new Vector3(284.9267F, -161.9545F, 63.61711F));
            //.Name = "4401 Procopio Dr"
            AFKPlayers.Add(new Vector3(-301.8188F, 6328.303F, 31.88649F));
            //.Name = "4584 Procopio Dr"
            AFKPlayers.Add(new Vector3(-107.2951F, 6529.278F, 28.85814F));
            //.Name = "0504 S Mo Milton Dr"
            AFKPlayers.Add(new Vector3(-628.4631F, 168.0906F, 60.14972F));
            //.Name = "0325 South Rockford Dr"
            AFKPlayers.Add(new Vector3(-833.2334F, -862.5095F, 19.68971F));
            //.Name = "0605 Spanish Ave"
            AFKPlayers.Add(new Vector3(5.622878F, 36.0617F, 70.53041F));
            //.Name = "12 Sustancia Rd"
            AFKPlayers.Add(new Vector3(1336.244F, -1579.887F, 53.05425F));
            //.Name = "The Royale"
            AFKPlayers.Add(new Vector3(-198.8772F, 86.29745F, 68.75475F));
            //.Name = "1115 Blvd Del Perro"
            AFKPlayers.Add(new Vector3(-1607.753F, -433.6076F, 39.42718F));
            //.Name = "1561 San Vitas St"
            AFKPlayers.Add(new Vector3(-201.2312F, 185.0877F, 79.32613F));
            //.Name = "1237 Prosperity St"
            AFKPlayers.Add(new Vector3(-1562.376F, -404.384F, 41.38401F));
            //.Name = "0069 Cougar Ave"
            AFKPlayers.Add(new Vector3(-1532.888F, -325.702F, 46.9112F));
            //.Name = "2143 Las Lagunas Blvd"
            AFKPlayers.Add(new Vector3(-41.5342F, -59.85516F, 62.65963F));
            //.Name = "1893 Grapeseed Ave"
            AFKPlayers.Add(new Vector3(1662.762F, 4775.079F, 41.00756F));
            //.Name = "0232 Paleto Blvd"
            AFKPlayers.Add(new Vector3(-14.08983F, 6556.662F, 32.24046F));
            //.Name = "0112 S Rockford Dr"
            AFKPlayers.Add(new Vector3(-813.6389F, -979.8933F, 13.1934F));
            //.Name = "2057 Vespucci Blvd"
            AFKPlayers.Add(new Vector3(-663.3229F, -853.7469F, 23.44383F));
            //.Name = "140 Zancudo Ave"
            AFKPlayers.Add(new Vector3(1900.146F, 3781.11F, 31.81827F));
        }
        private void FindMisfits()
        {
            GetLogging("FindMisfits");

            if (Directory.Exists("" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/"))
            {
                if (File.Exists(sOutfitts))
                {
                    ClothBankXML TheOutXML = LoadOutfitXML(sOutfitts);

                    for (int i = 0; i < TheOutXML.Outfits.Count; i++)
                    {
                        if (TheOutXML.Outfits[i].Char == 4)
                            FemaleCloth.Add(TheOutXML.Outfits[i]);
                        else if (TheOutXML.Outfits[i].Char == 5)
                            MaleCloth.Add(TheOutXML.Outfits[i]);
                    }

                }
            }
        }
        public class ClothBank
        {
            public int Char { get; set; }

            public List<int> ClothA { get; set; }
            public List<int> ClothB { get; set; }

            public List<int> ExtraA { get; set; }
            public List<int> ExtraB { get; set; }

            public ClothBank()
            {
                ClothA = new List<int>();
                ClothB = new List<int>();
                ExtraA = new List<int>();
                ExtraB = new List<int>();
            }
        }
        public class ClothBankXML
        {
            public List<ClothBank> Outfits { get; set; }

            public ClothBankXML()
            {
                Outfits = new List<ClothBank>();
            }

        }
        public ClothBankXML LoadOutfitXML(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(ClothBankXML));
            using (StreamReader sr = new StreamReader(fileName))
            {
                return (ClothBankXML)xml.Deserialize(sr);
            }
        }
        private void PedCleaning(int iPed, string sOff, bool bGone)
        {
            GetLogging("PedCleaning, iPed == " + iPed);

            int iBe = ReteaveBrain(iPed);
            UI.Notify("~h~" + PedList[iBe].MyName + "~s~ " + sOff);

            DeListingBrains(true, iPed, bGone);
            iCurrentPlayerz -= 1;
        }
        private void DeListingBrains(bool bPed, int iPos, bool bGone)
        {
            GetLogging("DeListingBrains, bPed == " + bPed + ", iPos == " + iPos);

            if (bPed)
            {
                iPos = ReteaveBrain(iPos);

                if (PedList[iPos].ThisPed == null)
                {
                    if (PedList[iPos].ThisBlip != null)
                        PedList[iPos].ThisBlip.Remove();

                    if (PedList[iPos].DirBlip != null)
                        PedList[iPos].DirBlip.Remove();

                    if (PedList[iPos].ThisVeh != null)
                        PedList[iPos].ThisVeh.MarkAsNoLongerNeeded();

                    PedList.RemoveAt(iPos);
                }
                else
                { 
                    if (PedList[iPos].Hacker)
                    {
                        for (int i = 0; i < Plops.Count; i++)
                            Plops[i].MarkAsNoLongerNeeded();

                        for (int i = 0; i < Vicks.Count; i++)
                            Vicks[i].MarkAsNoLongerNeeded();
                        Plops.Clear();
                        Vicks.Clear();
                        PedList[iPos].ThisPed.Detach();
                        bHackerIn = false;
                        bPiggyBack = false;
                        FireOrb(-1, PedList[iPos].ThisPed);
                    }

                    if (PedList[iPos].Follower)
                        iFollow -= 1;

                    if (PedList[iPos].ThisBlip != null)
                        PedList[iPos].ThisBlip.Remove();

                    if (PedList[iPos].DirBlip != null)
                        PedList[iPos].DirBlip.Remove();

                    if (PedList[iPos].ThisOppress != null)
                    {
                        EmptyVeh(PedList[iPos].ThisOppress);
                        PedList[iPos].ThisOppress.Explode();
                        PedList[iPos].ThisOppress.MarkAsNoLongerNeeded();
                        PedList[iPos].ThisOppress = null;

                        if (PedList[iPos].ThisVeh != null)
                        {
                            EmptyVeh(PedList[iPos].ThisVeh);
                            PedList[iPos].ThisVeh.Delete();
                            PedList[iPos].ThisVeh = null;
                        }
                    }
                    else if (PedList[iPos].ThisVeh != null)
                    {
                        EmptyVeh(PedList[iPos].ThisVeh);
                        PedList[iPos].ThisVeh.MarkAsNoLongerNeeded();
                        PedList[iPos].ThisVeh = null;
                    }

                    if (bGone)
                        PedList[iPos].ThisPed.Delete();
                    else
                        PedList[iPos].ThisPed.MarkAsNoLongerNeeded();
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, PedList[iPos].ThisPed.Handle);
                    PedList.RemoveAt(iPos);
                }
            }
            else
            {
                AFKList[iPos].ThisBlip.Remove();
                UI.Notify("~h~" + AFKList[iPos].MyName + "~s~ left");
                AFKList.RemoveAt(iPos);
            }

            BackItUpBrain();
        }
        private void NewPlayer()
        {
            GetLogging("NewPlayer, iCurrentPlayerz == " + iCurrentPlayerz);
            SetBTimer(RandInt(iMinWait, iMaxWait), 1);
            LoadIniSetts();
            int iHeister = NearHiest();

            if (iCurrentPlayerz + 5 < iMaxPlayers && iHeister != -1 && bHeistPop)
                HeistDrips(iHeister);
            else if (iCurrentPlayerz < iMaxPlayers)
            {
                if (FindRandom(0, 1, 10) < 6)
                {
                    if (FindRandom(1, 1, 10) < 6)
                        SearchPed(35.00f, 220.00f, null, 0, -1);
                    else
                    {
                        int iTypeO = FindRandom(2, 1, 9);
                        SearchVeh(15.00f, 145.00f, RandVeh(iTypeO), true, -1);
                    }
                }
                else
                    InABuilding();
                iCurrentPlayerz += 1;
            }
        }
        public int RandInt(int iMin, int iMax)
        {
            return Function.Call<int>(Hash.GET_RANDOM_INT_IN_RANGE, iMin, iMax);
        }
        public float RandFlow(float fMin, float fMax)
        {
            return Function.Call<float>(Hash.GET_RANDOM_INT_IN_RANGE, fMin, fMax);
        }
        public bool BeInAngle(float fRange, float fValue_01, float fValue_02)
        {
            GetLogging("BeInAngle, fRange == " + fRange + ", fValue_01 == " + fValue_01 + ", fValue_02 == " + fValue_02);

            bool bInRange = false;

            if (fValue_01 < fRange)
            {
                if (fValue_02 > 360.00 - fRange)
                    bInRange = true;
            }
            else if (fValue_02 < fRange)
            {
                if (fValue_01 > 360.00 - fRange)
                    bInRange = true;
            }
            else if (fValue_01 < fValue_02 + fRange)
            {
                if (fValue_01 > fValue_02 - fRange)
                    bInRange = true;
            }

            return bInRange;
        }
        public class PosDir
        {
            public Vector3 Pos { get; set; }
            public float Dir { get; set; }
        }
        public Vehicle LookNear(Vector3 Vec3)
        {
            Vehicle Vickary = null;

            if (World.GetNextPositionOnStreet(YoPoza()).DistanceTo(YoPoza()) < 95.00f)
            {
                Vehicle[] CarSpot = World.GetNearbyVehicles(Vec3, 200.00f);
                for (int i = 0; i < CarSpot.Count(); i++)
                {
                    if (VehExists(CarSpot, i) && Vickary == null)
                    {
                        Vehicle Veh = CarSpot[i];
                        if (!Veh.IsPersistent && Veh.Position.DistanceTo(YoPoza()) > 15.00f && Veh.ClassType != VehicleClass.Boats && Veh.ClassType != VehicleClass.Cycles && Veh.ClassType != VehicleClass.Helicopters && Veh.ClassType != VehicleClass.Planes && Veh.ClassType != VehicleClass.Trains && !Veh.IsOnScreen)
                        {
                            if (!Veh.IsSeatFree(VehicleSeat.Driver) || Veh.EngineRunning)
                                Vickary = Veh;
                            break;
                        }
                    }
                }
            }
            return Vickary;
        }
        public bool VehExists(Vehicle[] Vehlist, int iPos)
        {
            bool bExist = false;

            if (iPos < Vehlist.Count())
            {
                unsafe
                {
                    if (Vehlist[iPos].Exists())
                        bExist = true;
                }
            }
            return bExist;
        }
        public bool IsItARealVehicle(string sVehName)
        {
            GetLogging("IsItARealVehicle, sVehName == " + sVehName);

            bool bIsReal = false;

            int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehName);
            if (Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
                bIsReal = true;

            return bIsReal;
        }
        public string RandVeh(int iVechList)
        {
            GetLogging("RandVeh, iVechList == " + iVechList);

            List<string> sVehicles = new List<string>();

            if (iVechList == 1)
            {
                sVehicles.Add("PFISTER811");
                sVehicles.Add("ADDER");
                sVehicles.Add("AUTARCH");
                sVehicles.Add("BANSHEE2");
                sVehicles.Add("BULLET");
                sVehicles.Add("CHEETAH");
                sVehicles.Add("CYCLONE");
                sVehicles.Add("ENTITYXF");
                sVehicles.Add("ENTITY2");
                sVehicles.Add("SHEAVA");
                sVehicles.Add("FMJ");
                sVehicles.Add("GP1");
                sVehicles.Add("INFERNUS");
                sVehicles.Add("ITALIGTB");
                sVehicles.Add("ITALIGTB2");
                sVehicles.Add("OSIRIS");
                sVehicles.Add("NERO");
                sVehicles.Add("NERO2");
                sVehicles.Add("PENETRATOR");
                sVehicles.Add("LE7B");
                sVehicles.Add("REAPER");
                sVehicles.Add("VOLTIC2");
                sVehicles.Add("SC1");
                sVehicles.Add("SULTANRS");
                sVehicles.Add("T20");
                sVehicles.Add("TAIPAN");
                sVehicles.Add("TEMPESTA");
                sVehicles.Add("TEZERACT");
                sVehicles.Add("TURISMOR");
                sVehicles.Add("TYRANT");
                sVehicles.Add("TYRUS");
                sVehicles.Add("VACCA");
                sVehicles.Add("VAGNER");
                sVehicles.Add("VISIONE");
                sVehicles.Add("VOLTIC");
                sVehicles.Add("PROTOTIPO");
                sVehicles.Add("XA21");
                sVehicles.Add("ZENTORNO");
                if (bTrainM)
                {
                    sVehicles.Add("DEVESTE");
                    sVehicles.Add("EMERUS");
                    sVehicles.Add("FURIA");
                    sVehicles.Add("KRIEGER");
                    sVehicles.Add("THRAX");
                    sVehicles.Add("ZORRUSSO");
                    sVehicles.Add("TIGON");
                    sVehicles.Add("champion"); //Dewbauchee Champion
                    sVehicles.Add("ignus"); //Pegassi Ignus
                    sVehicles.Add("zeno"); //Overflod Zeno
                }
            }            //Super
            else if (iVechList == 2)
            {
                sVehicles.Add("COGCABRIO"); //
                sVehicles.Add("EXEMPLAR"); //
                sVehicles.Add("F620"); //
                sVehicles.Add("FELON"); //
                sVehicles.Add("FELON2"); //<!-- Felon GT -->
                sVehicles.Add("JACKAL"); //
                sVehicles.Add("ORACLE"); //
                sVehicles.Add("ORACLE2"); //<!-- Oracle XS -->
                if (bTrainM)
                    sVehicles.Add("PREVION"); //<!-- PREVION -->
            }       //Coupe
            else if (iVechList == 3)
            {
                sVehicles.Add("NINEF"); //
                sVehicles.Add("NINEF2"); //<!-- 9F Cabrio -->
                sVehicles.Add("ALPHA"); //
                sVehicles.Add("BANSHEE"); //
                sVehicles.Add("BESTIAGTS"); //
                sVehicles.Add("BLISTA2"); //<!-- Blista Compact -->
                sVehicles.Add("BUFFALO"); //
                sVehicles.Add("BUFFALO2"); //<!-- Buffalo S -->
                sVehicles.Add("CARBONIZZARE"); //
                sVehicles.Add("COMET2"); //<!-- Comet -->
                sVehicles.Add("COMET3"); //<!-- Comet Retro Custom -->
                sVehicles.Add("COMET4"); //<!-- Comet Safari -->
                sVehicles.Add("COMET5"); //<!-- Comet SR -->
                sVehicles.Add("COQUETTE"); //
                sVehicles.Add("TAMPA2"); //<!-- Drift Tampa -->
                sVehicles.Add("ELEGY"); //<!-- Elegy Retro Custom -->
                sVehicles.Add("ELEGY2"); //<!-- Elegy RH8 -->
                sVehicles.Add("FELTZER2"); //<!-- Feltzer -->
                sVehicles.Add("FLASHGT"); //
                sVehicles.Add("FUROREGT"); //
                sVehicles.Add("FUSILADE"); //
                sVehicles.Add("FUTO"); //
                sVehicles.Add("GB200"); //
                sVehicles.Add("BLISTA3"); //<!-- Go Go Monkey Blista -->
                sVehicles.Add("HOTRING"); //
                sVehicles.Add("JESTER"); //
                sVehicles.Add("JESTER2"); //<!-- Jester (Racecar) -->
                sVehicles.Add("JESTER3"); //<!-- Jester Classic -->
                sVehicles.Add("KHAMELION"); //
                sVehicles.Add("KURUMA"); //
                sVehicles.Add("LYNX"); //
                sVehicles.Add("MASSACRO"); //
                sVehicles.Add("MASSACRO2"); //<!-- Massacro (Racecar) -->
                sVehicles.Add("NEON"); //
                sVehicles.Add("OMNIS"); //
                sVehicles.Add("PARIAH"); //
                sVehicles.Add("PENUMBRA"); //
                sVehicles.Add("RAIDEN"); //
                sVehicles.Add("RAPIDGT"); //
                sVehicles.Add("RAPIDGT2"); //<!-- Rapid GT Cabrio -->
                sVehicles.Add("RAPTOR"); //
                sVehicles.Add("REVOLTER"); //
                sVehicles.Add("RUSTON"); //
                sVehicles.Add("SCHAFTER4"); //<!-- Schafter LWB -->
                sVehicles.Add("SCHAFTER3"); //<!-- Schafter V12 -->
                sVehicles.Add("SCHWARZER"); //
                sVehicles.Add("SENTINEL3"); //<!-- Sentinel Classic -->
                sVehicles.Add("SEVEN70"); //
                sVehicles.Add("SPECTER"); //
                sVehicles.Add("SPECTER2"); //<!-- Specter Custom -->
                sVehicles.Add("BUFFALO3"); //<!-- Sprunk Buffalo -->
                sVehicles.Add("STREITER"); //
                sVehicles.Add("SULTAN"); //
                sVehicles.Add("SURANO"); //
                sVehicles.Add("TROPOS"); //
                sVehicles.Add("VERLIERER2"); //
                if (bTrainM)
                {
                    sVehicles.Add("DRAFTER"); //<!-- 8F Drafter -->
                    sVehicles.Add("IMORGON"); //
                    sVehicles.Add("ISSI7"); //<!-- Issi Sport -->
                    sVehicles.Add("ITALIGTO"); //
                    sVehicles.Add("JUGULAR"); //
                    sVehicles.Add("KOMODA"); //
                    sVehicles.Add("LOCUST"); //
                    sVehicles.Add("NEO"); //
                    sVehicles.Add("PARAGON"); //
                    sVehicles.Add("PARAGON2"); //<!-- Paragon R (Armored) -->
                    sVehicles.Add("SCHLAGEN"); //
                    sVehicles.Add("SUGOI"); //
                    sVehicles.Add("SULTAN2"); //<!-- Sultan Classic -->
                    sVehicles.Add("VSTR"); //<!-- V-STR -->
                    sVehicles.Add("COQUETTE4"); //<!-- Coquette D10  -->
                    sVehicles.Add("PENUMBRA2"); //<!-- Penumbra FF   -->
                    sVehicles.Add("ITALIRSX"); //><!-- Grotti Itali RSX -->Spports
                    sVehicles.Add("CALICO"); //
                    sVehicles.Add("COMET6"); //
                    sVehicles.Add("CYPHER"); //
                    sVehicles.Add("EUROS"); //
                    sVehicles.Add("FUTO2"); //
                    sVehicles.Add("GROWLER"); //
                    sVehicles.Add("JESTER4"); //
                    sVehicles.Add("REMUS"); //
                    sVehicles.Add("RT3000"); //
                    sVehicles.Add("SULTAN3"); //
                    sVehicles.Add("VECTRE"); //
                    sVehicles.Add("ZR350"); //
                    sVehicles.Add("comet7"); //Pfister Comet S2 Cabrio
                }
            }       //Sport
            else if (iVechList == 4)
            {
                sVehicles.Add("BLADE"); //
                sVehicles.Add("BUCCANEER2"); //<!-- Buccaneer Custom -->
                sVehicles.Add("STALION2"); //<!-- Burger Shot Stallion -->
                sVehicles.Add("CHINO2"); //<!-- Chino Custom -->
                sVehicles.Add("COQUETTE3"); //<!-- Coquette BlackFin -->
                sVehicles.Add("DOMINATOR3"); //<!-- Dominator GTX -->
                sVehicles.Add("YOSEMITE2"); //<!-- Drift Yosemite -->
                sVehicles.Add("ELLIE"); //
                sVehicles.Add("FACTION2"); //<!-- Faction Custom -->
                sVehicles.Add("FACTION3"); //<!-- Faction Custom Donk -->
                sVehicles.Add("GAUNTLET"); //
                sVehicles.Add("HERMES"); //
                sVehicles.Add("HOTKNIFE"); //
                sVehicles.Add("HUSTLER"); //
                sVehicles.Add("SLAMVAN2"); //<!-- Lost Slamvan -->
                sVehicles.Add("LURCHER"); //
                sVehicles.Add("MOONBEAM2"); //<!-- Moonbeam Custom -->
                sVehicles.Add("NIGHTSHADE"); //
                sVehicles.Add("PICADOR"); //
                sVehicles.Add("DOMINATOR2"); //<!-- Pisswasser Dominator -->
                sVehicles.Add("GAUNTLET2"); //<!-- Redwood Gauntlet -->
                sVehicles.Add("SABREGT2"); //<!-- Sabre Turbo Custom -->
                sVehicles.Add("SLAMVAN3"); //<!-- Slamvan Custom -->
                sVehicles.Add("TAMPA"); //
                sVehicles.Add("VIGERO"); //
                sVehicles.Add("VIRGO"); //
                sVehicles.Add("VIRGO3"); //<!-- Virgo Classic -->
                sVehicles.Add("VIRGO2"); //<!-- Virgo Classic Custom -->
                sVehicles.Add("VOODOO2"); //<!-- Voodoo Custom -->
                sVehicles.Add("YOSEMITE"); //
                if (bTrainM)
                {
                    sVehicles.Add("CLIQUE"); //
                    sVehicles.Add("DEVIANT"); //
                    sVehicles.Add("GAUNTLET3"); //<!-- Gauntlet Classic -->
                    sVehicles.Add("GAUNTLET4"); //<!-- Gauntlet Hellfire -->
                    sVehicles.Add("PEYOTE2"); //<!-- Peyote Gasser -->
                    sVehicles.Add("IMPALER"); //
                    sVehicles.Add("TULIP"); //
                    sVehicles.Add("VAMOS"); //
                    sVehicles.Add("DUKES3"); //
                    sVehicles.Add("GAUNTLET5"); //
                    sVehicles.Add("MANANA2"); //
                    sVehicles.Add("PEYOTE3"); //
                    sVehicles.Add("GLENDALE2"); //
                    sVehicles.Add("YOSEMITE3"); //
                    sVehicles.Add("DOMINATOR7"); //
                    sVehicles.Add("DOMINATOR8"); //
                    sVehicles.Add("buffalo4"); //Bravado Buffalo STX
                }
            }       //Mussle
            else if (iVechList == 5)
            {
                sVehicles.Add("Z190"); //<!-- 190z -->
                sVehicles.Add("ARDENT"); //
                sVehicles.Add("CASCO"); //
                sVehicles.Add("CHEBUREK"); //
                sVehicles.Add("CHEETAH2"); //<!-- Cheetah Classic -->
                sVehicles.Add("COQUETTE2"); //<!-- Coquette Classic -->
                sVehicles.Add("FAGALOA"); //
                sVehicles.Add("BTYPE2"); //<!-- FrÃ¤nken Stange -->
                sVehicles.Add("GT500"); //
                sVehicles.Add("INFERNUS2"); //<!-- Infernus Classic -->
                sVehicles.Add("JB700"); //
                sVehicles.Add("MAMBA"); //
                sVehicles.Add("MANANA"); //
                sVehicles.Add("MICHELLI"); //
                sVehicles.Add("MONROE"); //
                sVehicles.Add("PEYOTE"); //
                sVehicles.Add("PIGALLE"); //
                sVehicles.Add("RAPIDGT3"); //<!-- Rapid GT Classic -->
                sVehicles.Add("RETINUE"); //
                sVehicles.Add("BTYPE"); //<!-- Roosevelt -->
                sVehicles.Add("BTYPE3"); //<!-- Roosevelt Valor -->
                sVehicles.Add("SAVESTRA"); //
                sVehicles.Add("STINGER"); //
                sVehicles.Add("STINGERGT"); //
                sVehicles.Add("FELTZER3"); //<!-- Stirling GT -->
                sVehicles.Add("SWINGER"); //
                sVehicles.Add("TORERO"); //
                sVehicles.Add("TORNADO"); //
                sVehicles.Add("TORNADO2"); //<!-- Tornado Cabrio -->
                sVehicles.Add("TORNADO3"); //<!-- Rusty Tornado -->
                sVehicles.Add("TORNADO4"); //<!-- Mariachi Tornado -->
                sVehicles.Add("TORNADO5"); //<!-- Tornado Custom -->
                sVehicles.Add("TORNADO6"); //<!-- Tornado Rat Rod -->
                sVehicles.Add("TURISMO2"); //<!-- Turismo Classic -->
                sVehicles.Add("VISERIS"); //
                sVehicles.Add("ZTYPE"); //
                if (bTrainM)
                {
                    sVehicles.Add("DYNASTY"); //
                    sVehicles.Add("JB7002"); //<!-- JB 700W -->
                    sVehicles.Add("NEBULA"); //
                    sVehicles.Add("RETINUE2"); //<!-- Retinue MkII -->
                    sVehicles.Add("ZION3"); //<!-- Zion Classic -->
                    sVehicles.Add("COQUETTE4"); //<!-- Coquette D10  -->
                    sVehicles.Add("TOREADOR"); //><!-- Pegassi Toreador -->sportsClassic
                }
            }       //SportsClassic
            else if (iVechList == 6)
            {
                sVehicles.Add("BRIOSO"); //
                sVehicles.Add("ISSI3"); //<!-- Issi Classic -->
                if (bTrainM)
                {
                    sVehicles.Add("ASBO"); //
                    sVehicles.Add("KANJO"); //<!-- Blista Kanjo -->
                    sVehicles.Add("CLUB");
                    sVehicles.Add("BRIOSO2"); //>Grotti Brioso 300
                    sVehicles.Add("WEEVIL"); //><!-- BF Weevil -->
                }
            }       //Compact
            else if (iVechList == 7)
            {
                sVehicles.Add("COGNOSCENTI"); //
                sVehicles.Add("COGNOSCENTI2"); //<!-- Cognoscenti (Armored) -->
                sVehicles.Add("COG55"); //<!-- Cognoscenti 55 -->
                sVehicles.Add("COG552"); //<!-- Cognoscenti 55 (Armored) -->
                sVehicles.Add("PRIMO2"); //<!-- Primo Custom -->
                sVehicles.Add("SCHAFTER6"); //<!-- Schafter LWB (Armored) -->
                sVehicles.Add("SCHAFTER5"); //<!-- Schafter V12 (Armored) -->
                if (bTrainM)
                {
                    sVehicles.Add("TAILGATER2"); //
                    sVehicles.Add("WARRENER2"); //
                    sVehicles.Add("cinquemila"); //Lampadati Cinquemila
                    sVehicles.Add("deity"); //Enus Deity
                }
            }       //Sedan
            else if (iVechList == 8)
            {
                sVehicles.Add("BRAWLER"); //
                sVehicles.Add("TROPHYTRUCK2"); //<!-- Desert Raid -->
                sVehicles.Add("DUBSTA3"); //<!-- Dubsta 6x6 -->
                sVehicles.Add("FREECRAWLER"); //
                sVehicles.Add("HELLION");
                sVehicles.Add("KALAHARI"); //
                sVehicles.Add("KAMACHO"); //
                sVehicles.Add("RIATA"); //
                sVehicles.Add("REBEL"); //<!-- Rusty Rebel -->
                sVehicles.Add("TROPHYTRUCK"); //
                sVehicles.Add("RALLYTRUCK"); //<!-- Dune -->
                if (bTrainM)
                {
                    sVehicles.Add("CARACARA2"); //<!-- Caracara 4x4 -->
                    sVehicles.Add("EVERON"); //
                    sVehicles.Add("OUTLAW"); //
                    sVehicles.Add("VAGRANT"); //
                    sVehicles.Add("ZHABA"); //
                    sVehicles.Add("WINKY"); //><!-- Vapid Winky -->	
                    sVehicles.Add("SQUADDIE");
                    sVehicles.Add("patriot3"); //Mammoth Patriot Mil-Spec
                }
            }       //Offroad
            else if (iVechList == 9)
            {
                sVehicles.Add("AKUMA"); //
                sVehicles.Add("AVARUS"); //
                sVehicles.Add("CHIMERA"); //
                sVehicles.Add("DEFILER"); //
                sVehicles.Add("DIABLOUS"); //
                sVehicles.Add("DIABLOUS2"); //<!-- Diabolus Custom -->
                sVehicles.Add("DOUBLE"); //
                sVehicles.Add("ENDURO"); //
                sVehicles.Add("ESSKEY"); //
                sVehicles.Add("FAGGIO2"); //
                sVehicles.Add("FAGGIO3"); //<!-- Faggio Mod -->
                sVehicles.Add("FAGGIO"); //<!-- Faggio Sport -->
                sVehicles.Add("GARGOYLE"); //
                sVehicles.Add("HAKUCHOU"); //
                sVehicles.Add("HAKUCHOU2"); //<!-- Hakuchou Drag -->
                sVehicles.Add("HEXER"); //
                sVehicles.Add("INNOVATION"); //
                sVehicles.Add("LECTRO"); //
                sVehicles.Add("MANCHEZ"); //
                sVehicles.Add("NEMESIS"); //
                sVehicles.Add("NIGHTBLADE"); //
                sVehicles.Add("PCJ"); //
                sVehicles.Add("RATBIKE"); //
                sVehicles.Add("RUFFIAN"); //
                sVehicles.Add("SANCHEZ"); //<!-- Sanchez livery variant -->
                sVehicles.Add("SANCHEZ2"); //
                sVehicles.Add("THRUST"); //
                sVehicles.Add("VADER"); //
                sVehicles.Add("VINDICATOR"); //
                sVehicles.Add("WOLFSBANE"); //
                sVehicles.Add("ZOMBIEB"); //<!-- Zombie Chopper -->
                if (bTrainM)
                {
                    sVehicles.Add("MANCHEZ2"); //><!-- Maibatsu Manchez Scout -->
                    sVehicles.Add("reever"); //Western Reever
                    sVehicles.Add("shinobi"); //Nagasaki Shinobi
                }
            }       //Motorcycle
            else if (iVechList == 10)
            {
                sVehicles.Add("DUKES2"); //<!-- Duke O'Death -->
                sVehicles.Add("CARACARA"); //
                sVehicles.Add("DUNE3"); //<!-- Dune FAV -->
                sVehicles.Add("DUNE4"); //<!-- Ramp Buggy mission variant -->
                sVehicles.Add("DUNE5"); //<!-- Ramp Buggy -->
                sVehicles.Add("MARSHALL"); //
                sVehicles.Add("MONSTER"); //<!-- Liberator -->
                sVehicles.Add("MENACER"); //
                sVehicles.Add("NIGHTSHARK"); //
                sVehicles.Add("TECHNICAL"); //
                sVehicles.Add("TECHNICAL2"); //<!-- Technical Aqua -->
                sVehicles.Add("TECHNICAL3"); //<!-- Technical Custom -->
                sVehicles.Add("MULE4"); //<!-- Mule Custom -->
                sVehicles.Add("POUNDER2"); //<!-- Pounder Custom -->
                sVehicles.Add("SPEEDO4"); //<!-- Speedo Custom -->
                sVehicles.Add("PHANTOM2"); //<!-- Phantom Wedge -->
                sVehicles.Add("STOCKADE"); //<!-- Securicar -->
                sVehicles.Add("BOXVILLE5"); //<!-- Armored Boxville -->
                sVehicles.Add("TERBYTE"); //
                sVehicles.Add("BRICKADE"); //
                sVehicles.Add("HALFTRACK"); //
                sVehicles.Add("VIGILANTE"); //
                sVehicles.Add("TAMPA3"); //<!-- Weaponized Tampa -->
                sVehicles.Add("RUINER2"); //<!-- Ruiner 2000 -->
                sVehicles.Add("STROMBERG"); //
                sVehicles.Add("DELUXO"); //
                sVehicles.Add("SCRAMJET"); //
                if (bTrainM)
                {
                    sVehicles.Add("ZR380"); //<!-- Apocalypse ZR380 -->
                    sVehicles.Add("ZR3802"); //<!-- Future Shock ZR380 -->
                    sVehicles.Add("ZR3803"); //<!-- Nightmare ZR380 -->
                    sVehicles.Add("DOMINATOR4"); //<!-- Apocalypse Dominator -->
                    sVehicles.Add("IMPALER2"); //<!-- Apocalypse Impaler -->
                    sVehicles.Add("IMPERATOR"); //<!-- Apocalypse Imperator -->
                    sVehicles.Add("SLAMVAN4"); //<!-- Apocalypse Slamvan -->
                    sVehicles.Add("DOMINATOR5"); //<!-- Future Shock Dominator -->
                    sVehicles.Add("IMPALER3"); //<!-- Future Shock Impaler -->
                    sVehicles.Add("IMPERATOR2"); //<!-- Future Shock Imperator -->
                    sVehicles.Add("SLAMVAN5"); //<!-- Future Shock Slamvan -->
                    sVehicles.Add("DOMINATOR6"); //<!-- Nightmare Dominator -->
                    sVehicles.Add("IMPALER4"); //<!-- Nightmare Impaler -->
                    sVehicles.Add("IMPERATOR3"); //<!-- Nightmare Imperator -->
                    sVehicles.Add("SLAMVAN6"); //<!-- Nightmare Slamvan -->
                    sVehicles.Add("CERBERUS"); //<!-- Apocalypse Cerberus -->
                    sVehicles.Add("CERBERUS2"); //<!-- Future Shock Cerberus -->
                    sVehicles.Add("CERBERUS3"); //<!-- Nightmare Cerberus -->
                    sVehicles.Add("ISSI4"); //<!-- Apocalypse Issi -->
                    sVehicles.Add("ISSI5"); //<!-- Future Shock Issi -->
                    sVehicles.Add("ISSI6"); //<!-- Nightmare Issi -->
                    sVehicles.Add("BRUISER"); //<!-- Apocalypse Bruiser -->
                    sVehicles.Add("BRUTUS"); //<!-- Apocalypse Brutus -->
                    sVehicles.Add("MONSTER3"); //<!-- Apocalypse Sasquatch -->
                    sVehicles.Add("BRUISER2"); //<!-- Future Shock Bruiser -->
                    sVehicles.Add("BRUTUS2"); //<!-- Future Shock Brutus -->
                    sVehicles.Add("MONSTER4"); //<!-- Future Shock Sasquatch -->
                    sVehicles.Add("BRUISER3"); //<!-- Nightmare Bruiser -->
                    sVehicles.Add("BRUTUS3"); //<!-- Nightmare Brutus -->
                    sVehicles.Add("MONSTER5"); //<!-- Nightmare Sasquatch -->
                    sVehicles.Add("SCARAB"); //<!-- Apocalypse Scarab -->
                    sVehicles.Add("SCARAB2"); //<!-- Future Shock Scarab -->
                    sVehicles.Add("SCARAB3"); //<!-- Nightmare Scarab -->
                }
            }       //Millatary / SaintsRow
            else if (iVechList == 11)
            {
                sVehicles.Add("KURUMA2"); //<!-- Kuruma (Armored) -->
                sVehicles.Add("RHINO"); //
                sVehicles.Add("KHANJALI"); //<!-- TM-02 Khanjali -->
                sVehicles.Add("INSURGENT"); //
                sVehicles.Add("INSURGENT2"); //<!-- Insurgent Pick-Up -->
                sVehicles.Add("INSURGENT3"); //<!-- Insurgent Pick-Up Custom -->
                sVehicles.Add("CHERNOBOG"); //
                sVehicles.Add("APC"); //
                sVehicles.Add("RIOT2"); //<!-- RCV -->
            }       //Tanks and Rockets
            else if (iVechList == 12)
            {
                sVehicles.Add("STRIKEFORCE"); //<!-- B-11 Strikeforce -->
                sVehicles.Add("HYDRA"); //
                sVehicles.Add("STARLING"); //<!-- LF-22 Starling -->
                sVehicles.Add("LAZER"); //<!-- P-996 LAZER -->
                sVehicles.Add("PYRO"); //
                sVehicles.Add("ROGUE"); //
                sVehicles.Add("MOLOTOK"); //<!-- V-65 Molotok -->
                sVehicles.Add("volatol"); //
                sVehicles.Add("bombushka");
                sVehicles.Add("seabreeze");
                sVehicles.Add("nokota");
                sVehicles.Add("mogul");
                sVehicles.Add("microlight");
                sVehicles.Add("howard");
                sVehicles.Add("tula");
                sVehicles.Add("alphaz1");
            }       //Attack Planes
            else if (iVechList == 13)
            {
                sVehicles.Add("BUZZARD"); //<!-- Buzzard Attack Chopper -->
                sVehicles.Add("HUNTER"); //<!-- FH-1 Hunter -->
                sVehicles.Add("SAVAGE"); //
                if (bTrainM)
                {
                    sVehicles.Add("seasparrow2"); //
                    sVehicles.Add("seasparrow");
                    sVehicles.Add("akula");
                    sVehicles.Add("havok");
                }
            }       //Attack Helicopters
            else
            {
                sVehicles.Add("BJXL"); //
            }

            if (sVehicles.Count == 0)
                sVehicles.Add("BJXL");

            return sVehicles[RandInt(0, sVehicles.Count - 1)];
        }
        public Vehicle VehicleSpawn(string sVehModel, Vector3 VecLocal, float VecHead, bool bAddPlayer, int iAddtoBrain, bool bAirVeh)
        {
            GetLogging("VehicleSpawn, sVehModel == " + sVehModel + ", bAddPlayer == " + bAddPlayer);

            Vehicle BuildVehicle = null;

            if (!IsItARealVehicle(sVehModel))
                sVehModel = "MAMBA";

            int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehModel);

            if (Function.Call<bool>(Hash.IS_MODEL_IN_CDIMAGE, iVehHash) && Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
            {
                Function.Call(Hash.REQUEST_MODEL, iVehHash);
                while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, iVehHash))
                    Wait(1);

                BuildVehicle = Function.Call<Vehicle>(Hash.CREATE_VEHICLE, iVehHash, VecLocal.X, VecLocal.Y, VecLocal.Z, VecHead, true, true);

                BuildVehicle.IsPersistent = true;

                if (bAddPlayer)
                {
                    GenPlayerPed(BuildVehicle.Position, BuildVehicle.Heading, BuildVehicle, -1, -1);

                    if (iCurrentPlayerz + 7 < iMaxPlayers && RandInt(0, 20) < 5)
                    {
                        for (int i = 0; i < BuildVehicle.PassengerSeats - 1; i++)
                            GenPlayerPed(BuildVehicle.Position, BuildVehicle.Heading, BuildVehicle, i, -1);
                    }
                }
                else
                {
                    int ThisBrain = ReteaveBrain(iAddtoBrain);
                    PedList[ThisBrain].ThisVeh = BuildVehicle;
                    WarptoAnyVeh(BuildVehicle, PedList[ThisBrain].ThisPed, -1);
                    PedList[ThisBrain].EnterVehQue = false;
                    if (bAirVeh)
                        PedList[ThisBrain].Pilot = true;
                    else
                        PedList[ThisBrain].Driver = true;
                    if (PedList[ThisBrain].ThisBlip != null)
                        ClearPedBlips(iAddtoBrain);

                    PedList[ThisBrain].ThisBlip = PedBlimp(PedList[ThisBrain].ThisPed, OhMyBlip(BuildVehicle), PedList[ThisBrain].MyName, PedList[ThisBrain].Colours);
                    DriveTooo(PedList[ThisBrain].ThisPed, false);
                }
                if (bAirVeh)
                    MaxOutAllModsNoWheels(BuildVehicle);
                else
                    MaxModsRandExtras(BuildVehicle);

                Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, iVehHash);
            }
            else
                BuildVehicle = null;

            return BuildVehicle;
        }
        private void MaxModsRandExtras(Vehicle Vehic)
        {
            GetLogging("MaxModsRandExtras");

            bool bMotoBike = Vehic.ClassType == VehicleClass.Motorcycles;

            for (int i = 0; i < 50; i++)
            {
                int iSpoilher = Function.Call<int>(Hash.GET_NUM_VEHICLE_MODS, Vehic.Handle, i);

                if (i == 11 || i == 12 || i == 13 || i == 15 || i == 16)
                {
                    iSpoilher -= 1;
                    Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, iSpoilher, true);
                }
                else if (i == 18 || i == 22)
                    iSpoilher = -1;

                else if (i == 24)
                {
                    iSpoilher = Function.Call<int>(Hash.GET_VEHICLE_MOD, Vehic.Handle, 23);
                    if (bMotoBike)
                        Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, iSpoilher, true);

                }
                else
                {
                    if (iSpoilher != 0)
                        iSpoilher = RandInt(0, iSpoilher - 1);

                    Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, RandInt(0, iSpoilher - 1), true);
                }
            }
            if (Vehic.ClassType != VehicleClass.Cycles || Vehic.ClassType != VehicleClass.Helicopters || Vehic.ClassType != VehicleClass.Boats || Vehic.ClassType != VehicleClass.Planes)
            {
                Vehic.ToggleMod(VehicleToggleMod.XenonHeadlights, true);
                Vehic.ToggleMod(VehicleToggleMod.Turbo, true);
            }
        }
        private void MaxOutAllModsNoWheels(Vehicle Vehic)
        {
            GetLogging("MaxOutAllModsNoWheels");

            Function.Call(Hash.SET_VEHICLE_MOD_KIT, Vehic.Handle, 0);
            for (int i = 0; i < 50; i++)
            {
                int iSpoilher = Function.Call<int>(Hash.GET_NUM_VEHICLE_MODS, Vehic.Handle, i);

                if (i == 18 || i == 22 || i == 23 || i == 24)
                {

                }
                else
                {
                    iSpoilher -= 1;
                    Function.Call(Hash.SET_VEHICLE_MOD, Vehic.Handle, i, iSpoilher, true);
                }
            }
            if (Vehic.ClassType != VehicleClass.Cycles || Vehic.ClassType != VehicleClass.Helicopters || Vehic.ClassType != VehicleClass.Boats || Vehic.ClassType != VehicleClass.Planes)
            {
                Vehic.ToggleMod(VehicleToggleMod.XenonHeadlights, true);
                Vehic.ToggleMod(VehicleToggleMod.Turbo, true);
            }
            Function.Call(Hash._SET_VEHICLE_LANDING_GEAR, Vehic.Handle, 3);
        }
        public bool PedExists(Ped[] Peddylist, int iPos)
        {
            bool bExist = false;

            if (iPos < Peddylist.Count())
            {
                unsafe
                {
                    if (Peddylist[iPos].Exists())
                        bExist = true;
                }
            }

            return bExist;
        }
        public Ped GenPlayerPed(Vector3 vLocal, float fAce, Vehicle vMyCar, int iSeat, int iReload)
        {
            GetLogging("GenPlayerPed, iSeat == " + iSeat + ", iReload == " + iReload);

            Ped MyPed = null;
            bool bMale = false;
            int iOldPed = ReteaveBrain(iReload);
            string sPeddy = "mp_f_freemode_01";

            if (iOldPed == -1)
            {
                if (FindRandom(13, 0, 20) < 10)
                {
                    bMale = true;
                    sPeddy = "mp_m_freemode_01";
                }
            }
            else
            {
                if (PedList[iOldPed].PFMySetting.PFMale)
                {
                    bMale = true;
                    sPeddy = "mp_m_freemode_01";
                }
            }

            var model = new Model(sPeddy);
            model.Request();    // Check if the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                while (!model.IsLoaded)
                    Wait(1);

                MyPed = Function.Call<Ped>(Hash.CREATE_PED, 4, model, vLocal.X, vLocal.Y, vLocal.Z, fAce, true, false);
                Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, model.Hash);

                if (MyPed.Exists())
                {
                    int iAccuracy = RandInt(iAccMin, iAccMax);
                    Function.Call(Hash.SET_PED_ACCURACY, MyPed.Handle, iAccuracy);
                    MyPed.MaxHealth = RandInt(200, 400);
                    MyPed.Health = MyPed.MaxHealth;

                    if (iOldPed == -1)
                        OnlineFaceTypes(MyPed, bMale, vMyCar, iSeat, null, -1);
                    else
                        OnlineFaceTypes(MyPed, bMale, vMyCar, iSeat, PedList[iOldPed].PFMySetting, iReload);
                }
                else
                    MyPed = null;
            }
            else
                MyPed = null;

            return MyPed;
        }
        private void WarptoAnyVeh(Vehicle Vhic, Ped Peddy, int iSeat)
        {
            GetLogging("WarptoAnyVeh, iSeat == " + iSeat);

            Function.Call(Hash.SET_PED_INTO_VEHICLE, Peddy.Handle, Vhic.Handle, iSeat);
        }
        private void GetOutVehicle(Ped Peddy, int iPed)
        {
            GetLogging("GetOutVehicle");

            if (Peddy.IsInVehicle())
            {
                Vehicle PedVeh = Peddy.CurrentVehicle;
                Function.Call(Hash.TASK_LEAVE_VEHICLE, Peddy.Handle, PedVeh.Handle, 4160);
            }
            if (iPed != -1)
            {
                iPed = ReteaveBrain(iPed);
                ClearPedBlips(PedList[iPed].Level);
                PedList[iPed].DirBlip = DirectionalBlimp(PedList[iPed].ThisPed);
                PedList[iPed].ThisBlip = PedBlimp(PedList[iPed].ThisPed, 1, PedList[iPed].MyName, PedList[iPed].Colours);
            }
        }
        private void PlayerEnterVeh(Vehicle Vhick)
        {
            GetLogging("PlayerEnterVeh");

            iFindingTime = Game.GameTime + 1000;
            int iSeats = 0;

            while (iSeats < Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Vhick.Handle))
            {
                if (Function.Call<bool>(Hash.IS_VEHICLE_SEAT_FREE, Vhick.Handle, iSeats))
                    break;
                else
                    iSeats += 1;
            }
            int ThreePass = 3;
            while (!Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, Game.Player.Character.Handle) && ThreePass > 0)
            {
                Script.Wait(500);
                Function.Call(Hash.TASK_ENTER_VEHICLE, Game.Player.Character.Handle, Vhick.Handle, -1, iSeats, 1.50f, 1, 0);
                ThreePass -= 1;
            }
    
            if (ThreePass < 1)
                    WarptoAnyVeh(Vhick, Game.Player.Character, iSeats);

        }
        private void EmptyVeh(Vehicle Vhic)
        {
            GetLogging("EmptyVeh");

            if (Vhic.Exists())
            {
                int iSeats = 0;
                while (iSeats < Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Vhic.Handle))
                {
                    if (!Function.Call<bool>(Hash.IS_VEHICLE_SEAT_FREE, Vhic.Handle, iSeats))
                        Function.Call(Hash.TASK_LEAVE_VEHICLE, Function.Call<Ped>(Hash.GET_PED_IN_VEHICLE_SEAT, Vhic.Handle, iSeats).Handle, Vhic.Handle, 4160);
                    iSeats += 1;
                }
            }
        }
        private void FolllowTheLeader(Ped Peddy)
        {
            GetLogging("FolllowTheLeader");

            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, iFollowMe);
            Peddy.RelationshipGroup = Gp_Follow;
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.BlockPermanentEvents = false;
            Peddy.AlwaysKeepTask = true;

            if (iAggression > 5)
                Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_PLAYER, Peddy.Handle, Game.Player.Character.Handle, true);
            else
                Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_PLAYER, Peddy.Handle, Game.Player.Character.Handle, false);
        }
        private void OhDoKeepUp(Ped Peddy)
        {
            Peddy.Task.ClearAll();

            float fXpos = -2.50f;
            float fYpos = 1.50f;

            iFolPos += 1;
            if (iFolPos == 1)
            {
                fXpos = -2.50f;
                fYpos = 0.00f;
            }
            else if (iFolPos == 2)
            {
                fXpos = -2.50f;
                fYpos = -2.50f;
            }
            else if (iFolPos == 3)
            {
                fXpos = 2.50f;
                fYpos = 0.00f;
            }
            else if (iFolPos == 4)
            {
                fXpos = 1.50f;
                fYpos = 0.00f;
            }
            else if (iFolPos == 5)
            {
                fXpos = -1.50f;
                fYpos = 0.00f;
            }
            else if (iFolPos == 6)
            {
                fXpos = 2.50f;
                fYpos = -2.50f;
            }
            else if (iFolPos == 7)
            {
                fXpos = -1.50f;
                fYpos = -2.50f;
                iFolPos = 0;
            }

            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, Peddy.Handle, Game.Player.Character.Handle, fXpos, fYpos, 0.0f, 1.0f, -1, 2.5f, true);
        }
        private void DriveBye(Ped Peddy)
        {
            GetLogging("DriveBye");

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    if (Game.Player.Character.IsInVehicle())
                        Peddy.Task.VehicleChase(Game.Player.Character);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, YoPoza(), 10.00f, 45.00f, 0);

                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
                else
                    Peddy.Task.VehicleShootAtPed(Game.Player.Character);
            }
        }
        private void DriveAround(Ped Peddy)
        {
            GetLogging("DriveAround");

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    float fAggi = iAggression / 100;
                    Peddy.Task.CruiseWithVehicle(Peddy.CurrentVehicle, 85.00f, 2883621);
                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_DRIVER_AGGRESSIVENESS, Peddy.Handle, fAggi);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
            }
        }
        private void DriveTooo(Ped Peddy, bool bRunOver)
        {
            GetLogging("DriveTooo, bRunOver == " + bRunOver);

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {

                    if (bRunOver)
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, YoPoza(), 1.00f, 45.00f, 0);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, YoPoza(), 10.00f, 25.00f, 0);

                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
            }
        }
        private void DriveToooDest(Ped Peddy, Vector3 Vme)
        {
            GetLogging("DriveToooDest, Vme == " + Vme);

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    Peddy.Task.DriveTo(Peddy.CurrentVehicle, Vme, 1.00f, 45.00f, 2883621);
                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
            }
        }
        private void FightPlayer(Ped Peddy, bool bInVeh)
        {
            GetLogging("FightPlayer");

            Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
            Peddy.RelationshipGroup = GP_Attack;
            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy.Handle);
            Peddy.IsEnemy = true;
            Peddy.CanBeTargette﻿d﻿ = true;
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 46, true);
            if (!bInVeh)
                Peddy.Task.FightAgainst(Game.Player.Character);
            else
                DriveBye(Peddy);
        }
        private void AirAttack(int iPed)
        {
            GetLogging("AirAttack, iPed == " + iPed);

            int iFinder = FindRandom(21, 1, 100);

            if (iFinder < 33)
                HeliFighter(iPed);
            else if (iFinder < 66)
                LaserFighter(iPed);
            else
                OppresorFighter(iPed);
        }
        private void HeliFighter(int iPed)
        {
            GetLogging("HeliFighter, iPed == " + iPed);

            int MyPed = ReteaveBrain(iPed);
            Ped Peddy = PedList[MyPed].ThisPed;
            PedList[MyPed].AirAttack = 1;
            string sVeh = RandVeh(13);
            Vehicle vHeli = VehicleSpawn(sVeh, new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 250.00f), 0.00f, false, iPed, true);
            FlyAway(PedList[MyPed].ThisPed, YoPoza(), 250.00f, 0.00f);
            Function.Call(Hash.SET_PED_FIRING_PATTERN, Peddy.Handle, Function.Call<int>(Hash.GET_HASH_KEY, "FIRING_PATTERN_BURST_FIRE_HELI"));
            Peddy.RelationshipGroup = GP_Mental;
            Peddy.BlockPermanentEvents = false;
        }
        private void LaserFighter(int iPed)
        {
            GetLogging("LaserFighter, iPed == " + iPed);

            int MyPed = ReteaveBrain(iPed);
            Ped Peddy = PedList[MyPed].ThisPed;
            PedList[MyPed].AirAttack = 2;
            PedList[MyPed].AirDirect = (float)RandInt(0, 360);
            string sVeh = RandVeh(12);
            Vehicle vPlane = VehicleSpawn(sVeh, new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 1550.00f), 0.00f, false, iPed, true);
            Function.Call(Hash.TASK_PLANE_MISSION, PedList[MyPed].ThisPed.Handle, PedList[MyPed].ThisVeh.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, PedList[MyPed].AirDirect, 1000.0f, -5000.0f);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.RelationshipGroup = GP_Mental;
            Peddy.BlockPermanentEvents = false;
        }
        private void OppresorFighter(int iPed)
        {
            GetLogging("OppresorFighter, iPed == " + iPed);


            int MyPed = ReteaveBrain(iPed);
            Ped Peddy = PedList[MyPed].ThisPed;
            PedList[MyPed].AirAttack = 3;
            PedList[MyPed].AirDirect = (float)RandInt(0, 360);
            Vehicle Planes = VehicleSpawn("hydra", new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 1550.00f), 0.00f, false, iPed, true);
            ClearPedBlips(iPed);
            PedList[MyPed].ThisBlip = PedBlimp(Peddy, 639, PedList[MyPed].MyName, PedList[MyPed].Colours);            
            Function.Call(Hash.TASK_PLANE_MISSION, Peddy.Handle, Planes.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, PedList[MyPed].AirDirect, 300.0f, -5000.0f);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.RelationshipGroup = GP_Mental;
            Peddy.BlockPermanentEvents = false;
            
            Vehicle Bike = World.CreateVehicle(VehicleHash.Oppressor2, Planes.Position);
            Bike.IsPersistent = true;
            PedList[MyPed].ThisOppress = Bike;
            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, Bike.Handle, Planes.Handle, Function.Call<int>(Hash.GET_PED_BONE_INDEX, Planes.Handle, 0), 0.00f, 3.32999945f, -0.10f, 0.00f, 0.00f, 0.00f, false, false, false, false, 2, true);

            Planes.IsVisible = false;
            Bike.IsVisible = true;
            Peddy.IsVisible = true;
            //VehAnim(Peddy, "veh@bike@sport@front@base", "sit");
        }
        private void FlyAway(Ped Pedd, Vector3 vHeliDest, float fSpeed, float flanding)
        {
            GetLogging("FlyAway");

            Vehicle vHeli = Pedd.CurrentVehicle;
            vHeli.FreezePosition = false;

            float HeliDesX = vHeliDest.X;
            float HeliDesY = vHeliDest.Y;
            float HeliDesZ = vHeliDest.Z;
            float HeliSpeed = fSpeed;
            float HeliLandArea = flanding;

            float dx = Pedd.Position.X - HeliDesX;
            float dy = Pedd.Position.Y - HeliDesY;
            float HeliDirect = Function.Call<float>(Hash.GET_HEADING_FROM_VECTOR_2D, dx, dy) - 180.00f;
            Function.Call(Hash.TASK_HELI_MISSION, Pedd.Handle, vHeli.Handle, 0, 0, HeliDesX, HeliDesY, HeliDesZ, 9, HeliSpeed, HeliLandArea, HeliDirect, -1, -1, -1, 0);
            Pedd.AlwaysKeepTask = true;
            Pedd.BlockPermanentEvents = true;
        }
        private void HackerTime(Ped Peddy)
        {
            GetLogging("HackerTime");

            if (YoPoza().DistanceTo(new Vector3(-778.81F, 312.66F, 84.70F)) < 80.00f)
            {
                Prop Plop = World.CreateProp("prop_windmill_01", new Vector3(-832.50f, 290.95f, 82.00f), new Vector3(-90.00f, 94.72f, 0.00f), true, false);
                Plops.Add(new Prop(Plop.Handle));
            }// Add windmill  
            else if (Peddy.IsInVehicle())
            {
                RoBoCar(Peddy.CurrentVehicle);
            }
            else if (Game.Player.Character.IsInVehicle())
            {
                Game.Player.Character.IsInvincible = true;
                EmptyVeh(Game.Player.Character.CurrentVehicle);
                Game.Player.Character.CurrentVehicle.Explode();
                Script.Wait(4000);
                Game.Player.Character.IsInvincible = false;

                ForceAnim(Peddy, "amb@code_human_in_bus_passenger_idles@female@sit@idle_a", "idle_a", Peddy.Position, Peddy.Rotation);
                Peddy.AttachTo(Game.Player.Character, 31086, new Vector3(0.10f, 0.15f, 0.61f), new Vector3(0.00f, 0.00f, 180.00f));
                bPiggyBack = true;
            }// MegaMonster
            else
            {
                ForceAnim(Peddy, "amb@code_human_in_bus_passenger_idles@female@sit@idle_a", "idle_a", Peddy.Position, Peddy.Rotation);
                Peddy.AttachTo(Game.Player.Character, 31086, new Vector3(0.10f, 0.15f, 0.61f), new Vector3(0.00f, 0.00f, 180.00f));
                bPiggyBack = true;
            }// Clones

            //Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy.Handle);
        }
        private void RoBoCar(Vehicle Atchoo)
        {
            GetLogging("RoBoCar");

            Atchoo.IsVisible = false;

            List<Vector3> Pos = new List<Vector3>();
            List<Vector3> Rot = new List<Vector3>();
            List<int> iBones = new List<int>();

            Pos.Add(new Vector3(6.80006075f, 0.00f, 0.00f));
            Rot.Add(new Vector3(0.00f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-6.80006075f, 0.00f, 0.00f));
            Rot.Add(new Vector3(0.00f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(6.80006075f, -2.63999796f, 4.40f));
            Rot.Add(new Vector3(-80.99f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-6.77995205f, -2.63999796f, 4.40f));
            Rot.Add(new Vector3(-80.99f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-6.77995205f, -3.33999729f, 12.00f));
            Rot.Add(new Vector3(-80.99f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-6.67995214f, -3.9399972f, 16.50f));
            Rot.Add(new Vector3(23.480093f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(6.82003975f, -3.33999729f, 12.00f));
            Rot.Add(new Vector3(-80.9999924f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-3.17995548f, -3.9399972f, 16.50f));
            Rot.Add(new Vector3(23.480093f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(0.320043772f, -3.9399972f, 16.50f));
            Rot.Add(new Vector3(23.480093f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(3.82004237f, -3.9399972f, 16.50f));
            Rot.Add(new Vector3(23.480093f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(6.92003965f, -3.9399972f, 16.50f));
            Rot.Add(new Vector3(23.480093f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-7.17995167f, -0.939998865f, 21.6000195f));
            Rot.Add(new Vector3(81.8809357f, 179.999985f, -1.1920929f));
            iBones.Add(0);
            Pos.Add(new Vector3(-7.17995167f, -2.43999863f, 28.300045f));
            Rot.Add(new Vector3(-54.1197128f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-3.67995501f, -2.43999863f, 28.300045f));
            Rot.Add(new Vector3(-54.1197128f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-0.179956198f, -2.43999863f, 28.300045f));
            Rot.Add(new Vector3(-54.1197128f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(3.32004309f, -2.43999863f, 28.300045f));
            Rot.Add(new Vector3(-54.1197128f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(6.82003975f, -2.43999863f, 28.300045f));
            Rot.Add(new Vector3(-54.1197128f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(8.52004051f, -4.53999662f, 30.6000519f));
            Rot.Add(new Vector3(-0.71962744f, 88.9999924f, 90.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-9.0799551f, -4.53999662f, 30.6000519f));
            Rot.Add(new Vector3(180.980362f, 88.9999924f, 90.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-9.0799551f, -4.23999691f, 31.900053f));
            Rot.Add(new Vector3(203.980362f, 179.999985f, 90.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(7.92004108f, -4.43999672f, 31.900053f));
            Rot.Add(new Vector3(204.080368f, 180.90004f, 270.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-14.97995f, -4.53999662f, 28.6000519f));
            Rot.Add(new Vector3(140.980362f, 89.9999847f, 90.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(14.0200891f, -4.53999662f, 28.6000519f));
            Rot.Add(new Vector3(40.0809174f, 89.9999847f, 90.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(16.7200718f, -3.33999777f, 24.6000309f));
            Rot.Add(new Vector3(-35.0200005f, -4.99999762f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-17.2799854f, -3.33999777f, 24.6000309f));
            Rot.Add(new Vector3(-35.0200005f, -4.99999762f, 0.00f));
            iBones.Add(0);//
            Pos.Add(new Vector3(-17.3799858f, 2.36000133f, 22.000021f));
            Rot.Add(new Vector3(-9.21995926f, -4.99999762f, 0.00f));
            iBones.Add(16);
            Pos.Add(new Vector3(16.7200718f, 2.36000133f, 22.000021f));
            Rot.Add(new Vector3(-9.21995926f, -4.99999762f, 0.00f));
            iBones.Add(16);
            Pos.Add(new Vector3(15.6200676f, 7.35999727f, 20.8000164f));
            Rot.Add(new Vector3(-9.21995926f, -4.99999762f, 28.9999962f));//Hydra 01
            iBones.Add(16);//27
            Pos.Add(new Vector3(-16.0799809f, 7.35999727f, 20.8000164f));
            Rot.Add(new Vector3(-9.21995926f, -4.99999762f, -29.0001526f));//Hydra 02
            iBones.Add(16);//28
            Pos.Add(new Vector3(-0.179956198f, -6.239995f, 33.2000389f));//skylift
            Rot.Add(new Vector3(-48.6197968f, 0.00f, 0.00f));
            iBones.Add(0);//29
            Pos.Add(new Vector3(6.92003918f, -6.83999634f, 19.5000114f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(3.42004251f, -6.83999634f, 19.5000114f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-0.0799564719f, -6.83999634f, 19.5000114f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-3.57995534f, -6.83999634f, 19.5000114f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-6.77995253f, -6.83999634f, 19.5000114f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-6.77995253f, -5.43999767f, 26.4000378f));
            Rot.Add(new Vector3(72.3803635f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(-3.27995586f, -5.43999767f, 26.4000378f));
            Rot.Add(new Vector3(72.3803635f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(0.220043272f, -5.43999767f, 26.4000378f));//laser 1
            Rot.Add(new Vector3(72.3803635f, 0.00f, 0.00f));
            iBones.Add(0);//37
            Pos.Add(new Vector3(3.72004223f, -5.43999767f, 26.4000378f));
            Rot.Add(new Vector3(72.3803635f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(7.22003937f, -5.43999767f, 26.4000378f));
            Rot.Add(new Vector3(72.3803635f, 0.00f, 0.00f));
            iBones.Add(0);
            Pos.Add(new Vector3(7.8200388f, -4.83999634f, 20.50f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 89.9999924f));
            iBones.Add(0);
            Pos.Add(new Vector3(7.8200388f, -2.43999863f, 21.6000042f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 89.9999924f));
            iBones.Add(0);
            Pos.Add(new Vector3(7.72003889f, -3.73999929f, 25.6000042f));
            Rot.Add(new Vector3(-90.5199051f, 180.90004f, 89.9999924f));
            iBones.Add(0);
            Pos.Add(new Vector3(-7.67996264f, -3.73999929f, 25.6000042f));
            Rot.Add(new Vector3(-90.5199051f, 360.599884f, 89.9999924f));
            iBones.Add(0);
            Pos.Add(new Vector3(-7.97995806f, -2.43999863f, 21.6000042f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 269.999969f));
            iBones.Add(0);
            Pos.Add(new Vector3(-7.57995844f, -5.53999567f, 19.7999973f));
            Rot.Add(new Vector3(90.4800873f, 0.00f, 269.999969f));
            iBones.Add(0);
            Pos.Add(new Vector3(-3.67995501f, -0.939998865f, 21.6000195f));
            Rot.Add(new Vector3(81.8809357f, 179.999985f, -1.1920929f));
            iBones.Add(0);
            Pos.Add(new Vector3(-0.179956198f, -0.939998865f, 21.6000195f));
            Rot.Add(new Vector3(81.8809357f, 179.999985f, -1.1920929f));
            iBones.Add(0);
            Pos.Add(new Vector3(3.32004285f, -0.939998865f, 21.6000195f));
            Rot.Add(new Vector3(81.8809357f, 179.999985f, -1.1920929f));
            iBones.Add(0);
            Pos.Add(new Vector3(6.82003975f, -0.939998865f, 21.6000195f));
            Rot.Add(new Vector3(81.8809357f, 179.999985f, -1.1920929f));
            iBones.Add(0);
            Pos.Add(new Vector3(-0.149956211f, -6.03999519f, 30.900053f));
            Rot.Add(new Vector3(-66.6195221f, 0.00f, 0.00f));
            iBones.Add(0);

            Vector3 Hyda_01 = new Vector3(0.299999982f, 2.99999928f, 2.39999986f);
            Vector3 Hydb_01 = new Vector3(0.00f, 0.00f, 0.00f);
            int iHyd_01 = 27;

            Vector3 Hyda_02 = new Vector3(0.299999982f, 3.3999989f, 2.29999995f);
            Vector3 Hydb_02 = new Vector3(0.00f, 0.00f, 0.00f);
            int iHyd_02 = 28;

            Vector3 Skylifta = new Vector3(0.00f, -1.61999893f, 0.399999917f);
            Vector3 Skyliftb = new Vector3(39.1999741f, 0.00f, 0.00f);
            int iSky = 29;

            Vector3 Lasa_01 = new Vector3(-2.79999948f, -3.59999895f, 2.29999995f);
            Vector3 Lasb_01 = new Vector3(1.10f, 0.00f, 0.00f);
            int Las_01 = 37;

            Vector3 Lasa_02 = new Vector3(2.39999986f, -3.59999895f, 2.29999995f);
            Vector3 Lasb_02 = new Vector3(1.10f, 0.00f, 0.00f);
            int Las_02 = 37;

            for (int i = 0; i < iBones.Count; i++)
            {
                Vehicle Tanks = World.CreateVehicle(VehicleHash.Rhino, new Vector3(0.00f, 0.00f, 150.00f));
                Tanks.IsPersistent = true;
                Tanks.AttachTo(Atchoo, iBones[i], Pos[i], Rot[i]);
                Vicks.Add(new Vehicle(Tanks.Handle));
            }
            Vehicle Planes = World.CreateVehicle(VehicleHash.Hydra, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(Vicks[iHyd_01], 0, Hyda_01, Hydb_01);
            Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Hydra, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(Vicks[iHyd_02], 0, Hyda_02, Hydb_02);
            Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Skylift, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(Vicks[iSky], 0, Skylifta, Skyliftb);
            Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Lazer, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(Vicks[Las_01], 0, Lasa_01, Lasa_01);
            Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Lazer, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(Vicks[Las_02], 0, Lasa_02, Lasa_02);
            Vicks.Add(new Vehicle(Planes.Handle));
        }
        private void ForceAnim(Ped peddy, string sAnimDict, string sAnimName, Vector3 AnPos, Vector3 AnRot)
        {
            GetLogging("ForceAnim, sAnimName == " + sAnimName);

            peddy.Task.ClearAll();
            Function.Call(Hash.REQUEST_ANIM_DICT, sAnimDict);
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, sAnimDict))
                Script.Wait(1);
            Function.Call(Hash.TASK_PLAY_ANIM_ADVANCED, peddy.Handle, sAnimDict, sAnimName, AnPos.X, AnPos.Y, AnPos.Z, AnRot.X, AnRot.Y, AnRot.Z, 8.0f, 0.00f, -1, 1, 0.01f, 0, 0);
            Function.Call(Hash.REMOVE_ANIM_DICT, sAnimDict);
        }
        private void VehAnim(Ped peddy, string sAnimDict, string sAnimName)
        {
            Function.Call(Hash.REQUEST_ANIM_DICT, sAnimDict);
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, sAnimDict))
                Script.Wait(1);
            Function.Call(Hash.TASK_PLAY_ANIM, peddy.Handle, sAnimDict, sAnimName, 8.0f, 1.0f, 1000, 33, .1f, false, false, false);
        }
        private void ClearPedBlips(int iPed)
        {
            GetLogging("ClearPedBlips, iPed == " + iPed);

            iPed = ReteaveBrain(iPed);

            PlayerBrain PeBrain = PedList[iPed];
            if (PeBrain.ThisBlip != null)
            {
                PeBrain.ThisBlip.Remove();
                PeBrain.ThisBlip = null;
            }
            if (PeBrain.DirBlip != null)
            {
                PeBrain.DirBlip.Remove();
                PeBrain.DirBlip = null;
            }
        }
        public int NearHiest()
        {
            GetLogging("NearHiest");

            int iNear = -1;
            List<Vector3> VectorList = new List<Vector3>
            {
                new Vector3(-1105.577f, -1692.11f, 4.345489f),      //0
                new Vector3(60.53763f, 8.939384f, 69.14648f),       //4
                new Vector3(718.9022f, -980.336f, 24.12285f),       //8
                new Vector3(1681.823f, 4817.896f, 42.01214f),       //12
                new Vector3(-1038.972f, -2736.403f, 20.16928f)     //16
            };

            for (int i = 0; i < VectorList.Count; i++)
            {
                if (VectorList[i].DistanceTo(YoPoza()) < 55.00f)
                {
                    iNear = i;
                    break;
                }
            }
            return iNear;
        }
        private void HeistDrips(int iMyArea)
        {
            GetLogging("HeistDrips, iMyArea == " + iMyArea);

            List<Vector3> VectorList = new List<Vector3>();

            if (iMyArea == 0)
            {
                VectorList.Add(new Vector3(-1105.577f, -1692.11f, 4.345489f));      //0
                VectorList.Add(new Vector3(-1107.141f, -1690.495f, 4.353377f));     //1
                VectorList.Add(new Vector3(-1105.481f, -1690.545f, 4.325913f));     //2--Vesp Beach
                VectorList.Add(new Vector3(716.5444f, -979.9716f, 24.11811f));      //3+Darnel
            }
            else if (iMyArea == 1)
            {
                VectorList.Add(new Vector3(60.53763f, 8.939384f, 69.14648f));       //4
                VectorList.Add(new Vector3(58.98491f, 8.12832f, 69.18693f));        //5
                VectorList.Add(new Vector3(60.66815f, 6.700741f, 69.12641f));       //6
                VectorList.Add(new Vector3(61.93483f, 7.88855f, 69.09691f));        //7--Vinwood
            }
            else if (iMyArea == 2)
            {
                VectorList.Add(new Vector3(718.9022f, -980.336f, 24.12285f));       //8
                VectorList.Add(new Vector3(717.793f, -982.8883f, 24.13336f));       //9
                VectorList.Add(new Vector3(715.0596f, -982.1675f, 24.1188f));       //10
                VectorList.Add(new Vector3(716.5444f, -979.9716f, 24.11811f));      //11--Darnels LaMessa
            }
            else if (iMyArea == 3)
            {
                VectorList.Add(new Vector3(1681.823f, 4817.896f, 42.01214f));       //12
                VectorList.Add(new Vector3(1681.932f, 4819.233f, 42.03329f));       //13
                VectorList.Add(new Vector3(1681.289f, 4820.775f, 42.05544f));       //14
                VectorList.Add(new Vector3(1681.09f, 4822.904f, 42.05639f));        //15--Grapesead
            }
            else 
            {
                VectorList.Add(new Vector3(-1038.972f, -2736.403f, 20.16928f));     //16
                VectorList.Add(new Vector3(-1037.887f, -2738.665f, 20.16928f));     //17
                VectorList.Add(new Vector3(-1035.784f, -2738.059f, 20.16928f));     //18
                VectorList.Add(new Vector3(-1036.624f, -2735.942f, 20.16928f));     //19--Airport
            }

            for (int i = 0; i < VectorList.Count; i++)
                GenPlayerPed(VectorList[i], 90.00f, null, 0, -1);
            iCurrentPlayerz += 4;
            Script.Wait(1200);
            World.AddExplosion(VectorList[0], ExplosionType.Grenade, 7.00f, 15.00f, true, false);
            bHeistPop = false;
        }
        private void CeoBikersGreaf()
        {
            GetLogging("CeoBikersGreaf");

        }
        public Blip DirectionalBlimp(Ped pEdd)
        {
            GetLogging("DirectionalBlimp");

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, 399);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);
            Function.Call(Hash.SET_BLIP_PRIORITY, MyBlip.Handle, 1);
            Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, 85);
            Function.Call(Hash.SET_BLIP_DISPLAY, MyBlip.Handle, 8);

            return MyBlip;
        }
        public Blip PedBlimp(Ped pEdd, int iBlippy, string sName, int iColour)
        {
            GetLogging("PedBlimp, iBlippy == " + iBlippy + ", sName == " + sName + ", iColour" + iColour);

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

            Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, iColour);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME,"STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING," Player: " + sName);
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
            Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);

            return MyBlip;
        }
        public Blip LocalBlip(Vector3 Vlocal, int iBlippy, string sName)
        {
            GetLogging("LocalBlip, iBlippy == " + iBlippy + ", sName == " + sName);

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, Vlocal.X, Vlocal.Y, Vlocal.Z);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
            Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);

            return MyBlip;
        }
        private void BlipDirect(Blip Blippy, float fDir)
        {
            int iHead = (int)fDir;
            if (Blippy.Exists())
                Function.Call(Hash.SET_BLIP_ROTATION, Blippy.Handle, iHead);
        }
        private void GunningIt(Ped Peddy)
        {
            GetLogging("GunningIt");

            List<string> sWeapList = new List<string>();

            int iGun = 0;

            if (bSpaceWeaps)
                iGun = RandInt(0, 8);
            else
                iGun = RandInt(0, 7);

            if (iGun == 1)
            {
                sWeapList.Add("WEAPON_dagger");  //0x92A27487",
                sWeapList.Add("WEAPON_pipebomb");  //0xBA45E8B8",
                sWeapList.Add("WEAPON_navyrevolver");  //0x917F6C8C"
                sWeapList.Add("WEAPON_combatpdw");  //0xA3D4D34",
                sWeapList.Add("WEAPON_sawnoffshotgun");  //0x7846A318",
                sWeapList.Add("WEAPON_sniperrifle");  //0x5FC3C11",
            }
            else if (iGun == 2)
            {
                sWeapList.Add("WEAPON_hammer");  //0x4E875F73",
                sWeapList.Add("WEAPON_revolver");  //0xC1B3C3D1",
                sWeapList.Add("WEAPON_smg");  //0x2BE6766B",
                sWeapList.Add("WEAPON_pumpshotgun");  //0x1D073A89",
                sWeapList.Add("WEAPON_advancedrifle");  //0xAF113F99",
            }
            else if (iGun == 3)
            {
                sWeapList.Add("WEAPON_battleaxe");  //0xCD274149",
                sWeapList.Add("WEAPON_molotov");  //0x24B17070",
                sWeapList.Add("WEAPON_stungun");  //0x3656C8C1",
                sWeapList.Add("WEAPON_microsmg");  //0x13532244",
                sWeapList.Add("WEAPON_musket");  //0xA89CB99E",
                sWeapList.Add("WEAPON_gusenberg");  //0x61012683"--69
            }
            else if (iGun == 4)
            {
                sWeapList.Add("WEAPON_golfclub");  //0x440E4788",
                sWeapList.Add("WEAPON_grenade");  //0x93E220BD",
                sWeapList.Add("WEAPON_appistol");  //0x22D8FE39",
                sWeapList.Add("WEAPON_assaultshotgun");  //0xE284C527",
                sWeapList.Add("WEAPON_mg");  //0x9D07F764",
            }
            else if (iGun == 5)
            {
                sWeapList.Add("WEAPON_machete");  //0xDD5DF8D9",
                sWeapList.Add("WEAPON_heavypistol");  //0xD205520E",
                sWeapList.Add("WEAPON_microsmg");  //0x13532244",
                sWeapList.Add("WEAPON_specialcarbine");  //0xC0A3098D",

            }
            else if (iGun == 6)
            {
                sWeapList.Add("WEAPON_flashlight");  //0x8BB05FD7",
                sWeapList.Add("WEAPON_GADGETPISTOL");  //0xAF3696A1",--new to cayo ppero
                sWeapList.Add("WEAPON_MILITARYRIFLE");  //0x624FE830"--65
                sWeapList.Add("WEAPON_COMBATSHOTGUN");  //0x5A96BA4--54
            }
            else if (iGun == 7)
            {
                sWeapList.Add("WEAPON_marksmanrifle");  //0xC734385A",
            }
            else if (iGun == 8)
            {
                sWeapList.Add("WEAPON_raypistol");  //0xAF3696A1",--36
                sWeapList.Add("WEAPON_raycarbine");  //0x476BF155"--44
                sWeapList.Add("weapon_rayminigun");
            }
            else
            {
                sWeapList.Add("WEAPON_pistol_mk2");  //0xBFE256D4",---------19
                sWeapList.Add("WEAPON_snspistol_mk2");  //0x88374054",---24
                sWeapList.Add("WEAPON_revolver_mk2");  //0xCB96392F",----29
                sWeapList.Add("WEAPON_pumpshotgun_mk2");  //0x555AF99A",-----------46
                sWeapList.Add("WEAPON_assaultrifle_mk2");  //0x394F415C",-------56
                sWeapList.Add("WEAPON_carbinerifle_mk2");  //0xFAD1F1C9",------58
                sWeapList.Add("WEAPON_specialcarbine_mk2");  //0x969C3D67",------61
                sWeapList.Add("WEAPON_bullpuprifle_mk2");  //0x84D6FAFD",----63
                sWeapList.Add("WEAPON_combatmg_mk2");  //0xDBBD7280",------68
                sWeapList.Add("WEAPON_heavysniper_mk2");  //0xA914799",---72
                sWeapList.Add("WEAPON_marksmanrifle_mk2");  //0x6A6C02E0"--74
            }

            for (int i = 0; i < sWeapList.Count; i++)
                Function.Call(Hash.GIVE_WEAPON_TO_PED, Peddy, Function.Call<int>(Hash.GET_HASH_KEY, sWeapList[i]), 9999, false, true);
        }
        public int UniqueLevels()
        {
            GetLogging("UniqueLevels");

            int iNumber = FindRandom(14, 1, 1000);

            while (BrainNumberCheck(iNumber))
            {
                iNumber = FindRandom(15, 1, 400);
            }
            return iNumber;
        }
        public bool BrainNumberCheck(int iNumber)
        {
            GetLogging("BrainNumberCheck, iNumber == " + iNumber);

            bool bRunAgain = false;
            for (int i = 0; i < PedList.Count; i++)
            {
                if (PedList[i].Level == iNumber)
                {
                    bRunAgain = true;
                    break;
                }
            }
            return bRunAgain;
        }
        public int ReteaveBrain(int iNumber)
        {
            GetLogging("ReteaveBrain, iNumber == " + iNumber);

            int iAm = -1;
            for (int i = 0; i < PedList.Count; i++)
            {
                if (PedList[i].Level == iNumber)
                {
                    iAm = i;
                    break;
                }
            }
            return iAm;
        }
        public class PedFixtures
        {
            public bool PFMale { get; set; }

            public int PFshapeFirstID { get; set; }
            public int PFshapeSecondID { get; set; }
            public int PFshapeThirdID { get; set; }
            public int PFskinFirstID { get; set; }
            public int PFskinSecondID { get; set; }
            public int PFskinThirdID { get; set; }
            public float PFshapeMix { get; set; }
            public float PFskinMix { get; set; }
            public float PFthirdMix { get; set; }

            public List<int> PFFeature { get; set; }
            public List<int> PFChange { get; set; }
            public List<int> PFColour { get; set; }
            public List<float> PFAmount { get; set; }

            public int iHairCut { get; set; }
            public int PFHair01 { get; set; }
            public int PFHair02 { get; set; }
            public ClothBank PedClothB { get; set; }

            public PedFixtures()
            {
                PFFeature = new List<int>();
                PFChange = new List<int>();
                PFColour = new List<int>();
                PFAmount = new List<float>();
            }
        }
        private void OnlineFaceTypes(Ped Pedx, bool bMale, Vehicle vMyCar, int iSeat, PedFixtures Fixtures, int iReload)
        {
            GetLogging("OnlineFaceTypes, iSeat == " + iSeat + ", iReload == " + iReload);

            PedFixtures MyNewFixings = new PedFixtures();

            int shapeFirstID = 0;
            int shapeSecondID = 0;
            int shapeThirdID = 0;
            int skinFirstID = 1;
            int skinSecondID = 1;
            int skinThirdID = 1;
            float shapeMix = 0.0f;
            float skinMix = 0.0f;
            float thirdMix = 0.0f;
            bool isParent = false;

            if (Fixtures == null)
            {
                if (bMale)
                {
                    MyNewFixings.PFMale = true;
                    shapeFirstID = RandInt(0, 20);
                    shapeSecondID = RandInt(0, 20);
                    shapeThirdID = shapeFirstID;
                    skinFirstID = shapeFirstID;
                    skinSecondID = shapeSecondID;
                    skinThirdID = shapeThirdID;
                }
                else
                {
                    MyNewFixings.PFMale = false;
                    shapeFirstID = RandInt(21, 41);
                    shapeSecondID = RandInt(21, 41);
                    shapeThirdID = shapeFirstID;
                    skinFirstID = shapeFirstID;
                    skinSecondID = shapeSecondID;
                    skinThirdID = shapeThirdID;
                }
                shapeMix = RandFlow(-0.9f, 0.9f);
                skinMix = RandFlow(0.9f, 0.99f);
                thirdMix = RandFlow(-0.9f, 0.9f);

                MyNewFixings.PFshapeFirstID = shapeFirstID;
                MyNewFixings.PFshapeSecondID = shapeSecondID;
                MyNewFixings.PFshapeThirdID = shapeThirdID;
                MyNewFixings.PFskinFirstID = skinFirstID;
                MyNewFixings.PFskinSecondID = skinSecondID;
                MyNewFixings.PFskinThirdID = skinThirdID;
                MyNewFixings.PFshapeMix = shapeMix;
                MyNewFixings.PFskinMix = skinMix;
                MyNewFixings.PFthirdMix = thirdMix;
            }
            else
            {
                bMale = Fixtures.PFMale;
                shapeFirstID = Fixtures.PFshapeFirstID;
                shapeSecondID = Fixtures.PFshapeSecondID;
                shapeThirdID = Fixtures.PFshapeThirdID;
                skinFirstID = Fixtures.PFskinFirstID;
                skinSecondID = Fixtures.PFskinSecondID;
                skinThirdID = Fixtures.PFskinThirdID;
                shapeMix = Fixtures.PFshapeMix;
                skinMix = Fixtures.PFskinMix;
                thirdMix = Fixtures.PFthirdMix;
            }

            Function.Call(Hash.SET_PED_HEAD_BLEND_DATA, Pedx.Handle, shapeFirstID, shapeSecondID, shapeThirdID, skinFirstID, skinSecondID, skinThirdID, shapeMix, skinMix, thirdMix, isParent);

            int iFeature = 0;

            while (iFeature < 12)
            {
                int iColour = 0;
                int iChange = RandInt(0, Function.Call<int>(Hash._GET_NUM_HEAD_OVERLAY_VALUES, iFeature));
                float fVar = RandFlow(0.45f, 0.99f);

                if (iFeature == 0)
                {
                    iChange = RandInt(0, iChange);
                }//Blemishes
                else if (iFeature == 1)
                {
                    if (bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }//Facial Hair
                else if (iFeature == 2)
                {
                    iChange = RandInt(0, iChange);
                    iColour = 1;
                }//Eyebrows
                else if (iFeature == 3)
                {
                    iChange = 255;
                }//Ageing
                else if (iFeature == 4)
                {
                    if (RandInt(0, 50) < 40)
                    {
                        iChange = RandInt(0, iChange);
                    }
                    else
                        iChange = 255;
                }//Makeup
                else if (iFeature == 5)
                {
                    if (!bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 2;
                }//Blush
                else if (iFeature == 6)
                {
                    iChange = RandInt(0, iChange);
                }//Complexion
                else if (iFeature == 7)
                {
                    iChange = 255;
                }//Sun Damage
                else if (iFeature == 8)
                {
                    if (!bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 2;
                }//Lipstick
                else if (iFeature == 9)
                {
                    iChange = RandInt(0, iChange);
                }//Moles/Freckles
                else if (iFeature == 10)
                {
                    if (bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }//Chest Hair
                else if (iFeature == 11)
                {
                    iChange = RandInt(0, iChange);
                }//Body Blemishes

                int AddColour = RandInt(0, 64);

                if (Fixtures == null)
                {
                    MyNewFixings.PFFeature.Add(iChange);
                    MyNewFixings.PFColour.Add(AddColour);
                    MyNewFixings.PFAmount.Add(fVar);
                }
                else
                {
                    iChange = Fixtures.PFFeature[iFeature];
                    AddColour = Fixtures.PFColour[iFeature];
                    fVar = Fixtures.PFAmount[iFeature];
                }

                Function.Call(Hash.SET_PED_HEAD_OVERLAY, Pedx.Handle, iFeature, iChange, fVar);

                if (iColour > 0)
                    Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, Pedx.Handle, iFeature, iColour, AddColour, 0);

                iFeature += 1;
            }

            if (Fixtures == null)
            {
                ClothBank MyNewWard = new ClothBank();
                Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx.Handle);

                if (bMale)
                {
                    if (MaleCloth.Count > 0)
                        MyNewWard = MaleCloth[RandInt(0, MaleCloth.Count - 1)];
                    else
                    {
                        int RanChar = RandInt(1, 6);
                        if (RanChar == 1)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(12);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ClothA.Add(1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(1);
                            MyNewWard.ClothB.Add(5);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(65);
                            MyNewWard.ClothB.Add(3);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(22);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(11);
                            MyNewWard.ClothB.Add(11);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 2)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(14);
                            MyNewWard.ClothB.Add(3);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(17);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(23);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ClothA.Add(40);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(26);
                            MyNewWard.ClothB.Add(3);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(35);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 3)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(147);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(167);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(33);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(36);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(286);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 4)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(11);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ClothA.Add(19);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(88);
                            MyNewWard.ClothB.Add(7);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(14);
                            MyNewWard.ClothB.Add(2);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(273);
                            MyNewWard.ClothB.Add(15);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 5)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(125);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(114);
                            MyNewWard.ClothB.Add(6);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(78);
                            MyNewWard.ClothB.Add(6);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(287);
                            MyNewWard.ClothB.Add(6);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(134);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(3);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(106);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(83);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(274);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                    }
                }
                else
                {
                    if (FemaleCloth.Count > 0)
                        MyNewWard = FemaleCloth[RandInt(0, FemaleCloth.Count - 1)];
                    else
                    {
                        int RanChar = RandInt(1, 5);
                        if (RanChar == 1)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(146);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ClothA.Add(113);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(23);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(287);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 2)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(11);
                            MyNewWard.ClothB.Add(3);

                            MyNewWard.ClothA.Add(169);
                            MyNewWard.ClothB.Add(12);

                            MyNewWard.ClothA.Add(93);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(3);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 3)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(13);
                            MyNewWard.ClothB.Add(3);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(98);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(71);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ClothA.Add(1);
                            MyNewWard.ClothB.Add(5);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(254);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 4)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(10);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ClothA.Add(15);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(9);
                            MyNewWard.ClothB.Add(6);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(54);
                            MyNewWard.ClothB.Add(3);

                            MyNewWard.ClothA.Add(100);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(36);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(13);
                            MyNewWard.ClothB.Add(5);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 5)
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(2);
                            MyNewWard.ClothB.Add(3);

                            MyNewWard.ClothA.Add(11);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(75);
                            MyNewWard.ClothB.Add(1);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(20);
                            MyNewWard.ClothB.Add(5);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(208);
                            MyNewWard.ClothB.Add(4);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                        else
                        {
                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(134);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(13);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(113);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(87);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(-1);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(0);
                            MyNewWard.ClothB.Add(0);

                            MyNewWard.ClothA.Add(287);
                            MyNewWard.ClothB.Add(8);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);

                            MyNewWard.ExtraA.Add(0);
                            MyNewWard.ExtraB.Add(0);
                        }
                    }
                }

                MyNewFixings.PedClothB = MyNewWard;

                OnlineSavedWard(Pedx, MyNewWard, bMale);
            }
            else
                OnlineSavedWard(Pedx, Fixtures.PedClothB, bMale);


            int iHairStyle = 0;
            if (Fixtures == null)
            {
                if (bMale)
                    iHairStyle = RandInt(25, 76);
                else
                    iHairStyle = RandInt(25, 80);

                MyNewFixings.iHairCut = iHairStyle;
            }
            else
                iHairStyle = Fixtures.iHairCut;

            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx.Handle, 2, iHairStyle, 0, 2);//hair

            int iHair = RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));
            int iHair2 = RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));

            if (Fixtures == null)
            {
                MyNewFixings.PFHair01 = iHair;
                MyNewFixings.PFHair02 = iHair2;
            }
            else
            {
                iHair = Fixtures.PFHair01;
                iHair2 = Fixtures.PFHair02;
            }

            Function.Call(Hash._SET_PED_HAIR_COLOR, Pedx.Handle, iHair, iHair2);

            if (Fixtures == null)
                NpcBrains(Pedx, vMyCar, iSeat, MyNewFixings, -1);
            else
                NpcBrains(Pedx, vMyCar, iSeat, null, iReload);
        }
        private void OnlineSavedWard(Ped Pedx, ClothBank MyCloths, bool bMale)
        {
            GetLogging("OnlineSavedWard");

            Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx.Handle);

            for (int i = 0; i < MyCloths.ClothA.Count; i++)
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx.Handle, i, MyCloths.ClothA[i], MyCloths.ClothB[i], 2);

            for (int i = 0; i < MyCloths.ExtraA.Count; i++)
                Function.Call(Hash.SET_PED_PROP_INDEX, Pedx.Handle, i, MyCloths.ExtraA[i], MyCloths.ExtraB[i], false);
        }
        private void NpcBrains(Ped Peddy, Vehicle VeHic, int iSeat, PedFixtures Fixtures, int iReload)
        {
            GetLogging("NpcBrains, iSeat == " + iSeat + ", iReload == " + iReload);

            if (iReload != -1)
            {
                int iPosNum = ReteaveBrain(iReload);
                if (iPosNum == -1)
                {
                    Peddy.Delete();
                }
                else
                {
                    PedList[iPosNum].ThisPed = Peddy;
                    PedList[iPosNum].DirBlip = DirectionalBlimp(Peddy);
                    PedList[iPosNum].ThisBlip = PedBlimp(Peddy, 1, PedList[iPosNum].MyName, PedList[iPosNum].Colours);
                    PedList[iPosNum].DeathSequence = 0;
                    PedList[iPosNum].EnterVehQue = false;
                    PedList[iPosNum].Befallen = false;

                    Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, Peddy.Handle, 2);
                    Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_PATH_PREFER_TO_AVOID_WATER, Peddy.Handle, false);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 0, true);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 1, true);
                    if (iAggression > 2)
                        Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 2, true);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 3, true);
                    Peddy.CanBeTargetted = true;

                    if (!PedList[iPosNum].Friendly)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                        Peddy.RelationshipGroup = GP_Attack;
                        if (PedList[iPosNum].OffRadar == 0 && RandInt(0, 40) < 10)
                            PedList[iPosNum].OffRadar = -1;
                        FightPlayer(Peddy, false);
                    }
                    else
                    {
                        if (PedList[iPosNum].Follower)
                        {
                            FolllowTheLeader(Peddy);
                            OhDoKeepUp(Peddy);
                        }
                        else
                        {
                            Peddy.Task.WanderAround();
                            Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                            Peddy.RelationshipGroup = Gp_Friend;
                        }
                    }
                    GunningIt(Peddy);
                }
            }
            else
            {
                PlayerBrain MyBrains = new PlayerBrain
                {
                    ThisPed = Peddy,
                    PFMySetting = Fixtures,
                    DeathSequence = 0,
                    DeathTime = 0,
                    TimeOn = Game.GameTime + RandInt(iMinSession, iMaxSession),
                    MyName = SillyNameList(),
                    Level = UniqueLevels(),
                    Killed = 0,
                    Kills = 0,
                    FindPlayer = 0,
                    Colours = 0,
                    OffRadar = 0,
                    AirAttack = 0,
                    AirDirect = 0.00f,
                    OffRadarBool = false,
                    Friendly = true,
                    Hacker = false,
                    InCombat = false,
                    Bounty = false,
                    Horny = false,
                    Horny2 = false,
                    Follower = false,
                    SessionJumper = false,
                    ApprochPlayer = false,
                    EnterVehQue = false,
                    Driver = false,
                    Pilot = false,
                    Befallen = false,
                    Passenger = false,
                    DirBlip = null,
                    ThisOppress = null
                };

                if (iSeat == -1)
                    MyBrains.ThisVeh = VeHic;
                else
                    MyBrains.ThisVeh = null;

                Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, Peddy.Handle, 2);
                Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_PATH_PREFER_TO_AVOID_WATER, Peddy.Handle, false);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 0, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 1, true);
                if (iAggression > 3)
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 2, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 3, true);
                Peddy.CanBeTargetted = true;

                int iBrain = 1;

                if (iAggression < 4 && VeHic == null)
                {
                    if (FindRandom(16, 0, 40) < 10)
                        iBrain = 2;
                }
                else if (iAggression < 6)
                {
                    if (FindRandom(17, 0, 60) < 5)
                        iBrain = 3;
                    else if (VeHic == null)
                    {
                        if (FindRandom(18, 0, 40) < 10)
                            iBrain = 2;
                    }
                }
                else if (iAggression < 8)
                {
                    if (FindRandom(19, 0, 60) < 40)
                        iBrain = 3;
                    else if (VeHic == null)
                    {
                        if (FindRandom(20, 0, 40) < 10)
                            iBrain = 2;
                    }
                }
                else if (iAggression < 11)
                {
                    iBrain = 3;
                }
                else
                {
                    if (!bHackerIn)
                        iBrain = 4;
                    else
                        iBrain = 3;

                }

                if (iBrain == 1)
                {
                    Peddy.Task.WanderAround();
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.MyName, 0);
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                    Peddy.RelationshipGroup = Gp_Friend;
                }            //Friend
                else if (iBrain == 2)
                {
                    Peddy.Task.WanderAround();
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.MyName, 0);
                    MyBrains.SessionJumper = true;
                }       //Disconect
                else if (iBrain == 3)
                {
                    FightPlayer(Peddy, false);
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.MyName, 1);
                    MyBrains.Colours = 1;
                    MyBrains.Friendly = false;
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                    Peddy.RelationshipGroup = GP_Mental;
                }       //Enemy
                else
                {
                    bHackerIn = true;
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 163, MyBrains.MyName, 1);
                    MyBrains.TimeOn = Game.GameTime + 60000;
                    MyBrains.Colours = 1;
                    bHackEvent = false;
                    MyBrains.Friendly = false;
                    MyBrains.Hacker = true;
                    Peddy.IsInvincible = true;
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                    Peddy.RelationshipGroup = GP_Mental;
                }                        //Hacker

                if (VeHic != null)
                {
                    WarptoAnyVeh(VeHic, Peddy, iSeat);
                    if (iSeat == -1)
                    {
                        if (MyBrains.ThisBlip != null)
                        {
                            MyBrains.ThisBlip.Remove();
                            MyBrains.ThisBlip = null;
                        }
                        if (MyBrains.DirBlip != null)
                        {
                            MyBrains.DirBlip.Remove();
                            MyBrains.DirBlip = null;
                        }
                        DriveTooo(Peddy, !MyBrains.Friendly);

                        MyBrains.ThisBlip = PedBlimp(Peddy, OhMyBlip(VeHic), MyBrains.MyName, MyBrains.Colours);

                        if (MyBrains.Friendly)
                            MyBrains.ThisPed.CanBeDraggedOutOfVehicle = false;
                        else
                            Function.Call(Hash.SET_VEHICLE_IS_WANTED, VeHic.Handle, true);

                        MyBrains.ApprochPlayer = true;
                        MyBrains.Driver = true;
                    }
                    else
                    {
                        if (MyBrains.ThisBlip != null)
                        {
                            MyBrains.ThisBlip.Remove();
                            MyBrains.ThisBlip = null;
                        }
                        if (MyBrains.DirBlip != null)
                        {
                            MyBrains.DirBlip.Remove();
                            MyBrains.DirBlip = null;
                        }
                        MyBrains.Passenger = true;
                    }
                }
                else if (RandInt(0, 40) < 10 && iAggression > 5)
                {
                    if (MyBrains.ThisBlip != null)
                    {
                        MyBrains.ThisBlip.Remove();
                        MyBrains.ThisBlip = null;
                    }
                    if (MyBrains.DirBlip != null)
                    {
                        MyBrains.DirBlip.Remove();
                        MyBrains.DirBlip = null;
                    }
                    MyBrains.ThisBlip = PedBlimp(Peddy, 303, MyBrains.MyName, 1);
                    MyBrains.Bounty = true;
                }
                
                PedList.Add(MyBrains);

                BackItUpBrain();

                if (iAggression > 1)
                    GunningIt(Peddy);
            }
        }
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
        public class AfkPlayer
        {
            public Blip ThisBlip { get; set; }
            public int TimeOn { get; set; }
            public int App { get; set; }
            public string MyName { get; set; }
            public int Level { get; set; }
        }
        public PlayerBrain ThisBrian(int iCurrent)
        {
            PlayerBrain Brains = null;

            if (iCurrent < PedList.Count)
                Brains = PedList[iCurrent];

            return Brains;
        }
        public AfkPlayer ThisAFKer(int iCurrent)
        {
            AfkPlayer Afker = null;

            if (iCurrent < AFKList.Count)
                Afker = AFKList[iCurrent];

            return Afker;
        }
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
        public void SavePlayerBrain(BackUpBrain config, string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(BackUpBrain));
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                xml.Serialize(sw, config);
            }
        }
        public BackUpBrain LoadPlayerBrain(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(BackUpBrain));
            using (StreamReader sr = new StreamReader(fileName))
            {
                return (BackUpBrain)xml.Deserialize(sr);
            }
        }
        private void BackItUpBrain()
        {
            Script.Wait(100);
            GetLogging("BackItUpBrain");

            BlowenMyBrains.BigBrain.Clear();
            BlowenMyBrains.BigVeh.Clear();
            BlowenMyBrains.BigBlip.Clear();
            BlowenMyBrains.BigDirBlip.Clear();
            BlowenMyBrains.BigAFKBlip.Clear();

            for (int i = 0; i < PedList.Count; i++)
            {
                if (PedList[i].ThisPed != null)
                    BlowenMyBrains.BigBrain.Add(PedList[i].ThisPed.Handle);

                if (PedList[i].ThisVeh != null)
                    BlowenMyBrains.BigVeh.Add(PedList[i].ThisVeh.Handle);

                if (PedList[i].ThisBlip != null)
                    BlowenMyBrains.BigBlip.Add(PedList[i].ThisBlip.Handle);

                if (PedList[i].DirBlip != null)
                    BlowenMyBrains.BigDirBlip.Add(PedList[i].DirBlip.Handle);
            }


            for (int i = 0; i < AFKList.Count; i++)
                BlowenMyBrains.BigAFKBlip.Add(AFKList[i].ThisBlip.Handle);

            SavePlayerBrain(BlowenMyBrains, sMemory);
        }
        private void FindBrains()
        {
            GetLogging("FindBrains");

            if (File.Exists(sMemory))
            {
                BlowenMyBrains = LoadPlayerBrain(sMemory);

                for (int i = 0; i < BlowenMyBrains.BigBrain.Count; i++)
                    FlushBrains(BlowenMyBrains.BigBrain[i] , 1);

                for (int i = 0; i < BlowenMyBrains.BigVeh.Count; i++)
                    FlushBrains(BlowenMyBrains.BigVeh[i], 2);

                for (int i = 0; i < BlowenMyBrains.BigBlip.Count; i++)
                    FlushBrains(BlowenMyBrains.BigBlip[i], 0);

                for (int i = 0; i < BlowenMyBrains.BigDirBlip.Count; i++)
                    FlushBrains(BlowenMyBrains.BigDirBlip[i], 0);

                for (int i = 0; i < BlowenMyBrains.BigAFKBlip.Count; i++)
                    FlushBrains(BlowenMyBrains.BigAFKBlip[i], 0);

                using (StreamWriter tEx = File.AppendText(sBeeLogs))
                    BeeLog("FoundOldXML", tEx);
            }
        }
        private void FlushBrains(int iHandles, int iType)
        {
            GetLogging("FlushBrains, iHandles == " + iHandles + ", iType == " + iType);

            unsafe
            {
                if (iType == 1)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, iHandles))
                    {
                        Ped Peed = new Ped(iHandles);
                        Peed.Delete();
                    }
                }
                else if (iType == 2)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, iHandles))
                    {
                        Vehicle Peed = new Vehicle(iHandles);
                        Peed.Delete();
                    }
                }
                else
                {
                    if (Function.Call<bool>(Hash.DOES_BLIP_EXIST, iHandles))
                    {
                        Blip BeeLip = new Blip(iHandles);
                        BeeLip.Remove();
                    }
                }
            }
        }
        private void LaggOut()
        {
            GetLogging("LaggOut");

            PlayerDump();
            bool bSearching = true;
            while (bSearching)
            {
                Script.Wait(100);
                PlayerDump();
                if (PedList.Count == 0)
                    bSearching = false;
            }
            iFollow = 0;
        }
        private void PlayerDump()
        { 
            GetLogging("PlayerDump");

            for (int i = 0; i < PedList.Count; i++)
            {
                if (PedList[i].ThisPed != null)
                {
                    if (PedList[i].ThisPed.Exists())
                    {
                        GetOutVehicle(PedList[i].ThisPed, PedList[i].Level);
                        PedCleaning(PedList[i].Level, "left", false);
                    }
                    else
                        PedList.RemoveAt(i);
                }
                else
                    PedList.RemoveAt(i);
            }

            for (int i = 0; i < AFKList.Count; i++)
            {
                DeListingBrains(false, i, true);
                iCurrentPlayerz -= 1;
            }

            MakeFrenz.Clear();
            MakeCarz.Clear();
            GetInQUe.Clear();
        }
        public int OhMyBlip(Vehicle MyVehic)
        {
            GetLogging("OhMyBlip");

            int iVehHash = MyVehic.Model.GetHashCode();
            int iBeLip = 1;

            if (MyVehic.ClassType == VehicleClass.Boats)
                iBeLip = 427;
            else if (MyVehic.ClassType == VehicleClass.Helicopters)
                iBeLip = 422;
            else if (MyVehic.ClassType == VehicleClass.Motorcycles)
                iBeLip = 226;
            else if (MyVehic.ClassType == VehicleClass.Planes)
                iBeLip = 424;
            else
                iBeLip = 225;

            if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CRUSADER"))
                iBeLip = 800;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN3"))
                iBeLip = 799;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "VALKYRIE2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "VALKYRIE"))
                iBeLip = 759;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SQUADDIE"))
                iBeLip = 757;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "seasparrow2"))
                iBeLip = 753;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "WINKY"))
                iBeLip = 745;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZHABA"))
                iBeLip = 737;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "OUTLAW"))
                iBeLip = 735;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "VAGRANT"))
                iBeLip = 736;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZR380") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZR3802") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ZR3803"))
                iBeLip = 669;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN5") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SLAMVAN6"))
                iBeLip = 668;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCARAB") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCARAB2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCARAB3"))
                iBeLip = 667;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MONSTER3") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MONSTER4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MONSTER5"))
                iBeLip = 666;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ISSI4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ISSI5") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ISSI6"))
                iBeLip = 665;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPERATOR") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPERATOR2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPERATOR3"))
                iBeLip = 664;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPALER2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPALER3") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "IMPALER4"))
                iBeLip = 663;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DOMINATOR4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DOMINATOR5") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DOMINATOR6"))
                iBeLip = 662;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CERBERUS") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CERBERUS2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "CERBERUS3"))
                iBeLip = 660;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUTUS") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUTUS2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUTUS3"))
                iBeLip = 659;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUISER") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUISER2") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "BRUISER3"))
                iBeLip = 658;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "STRIKEFORCE"))
                iBeLip = 640;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "OPPRESSOR2"))
                iBeLip = 639;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "SCRAMJET"))
                iBeLip = 634;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "seasparrow"))
                iBeLip = 612;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "APC"))
                iBeLip = 603;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "akula"))
                iBeLip = 602;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "volatol"))
                iBeLip = 600;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "KHANJALI"))
                iBeLip = 598;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DELUXO"))
                iBeLip = 596;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "bombushka"))
                iBeLip = 585;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "seabreeze"))
                iBeLip = 584;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "STARLING"))
                iBeLip = 583;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "ROGUE"))
                iBeLip = 582;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "PYRO"))
                iBeLip = 581;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "nokota"))
                iBeLip = 580;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "MOLOTOK"))
                iBeLip = 579;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "mogul"))
                iBeLip = 578;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "microlight"))
                iBeLip = 577;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "HUNTER"))
                iBeLip = 576;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "howard"))
                iBeLip = 575;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "havok"))
                iBeLip = 574;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "tula"))
                iBeLip = 573;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "alphaz1"))
                iBeLip = 572;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DUNE3"))
                iBeLip = 561;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "HALFTRACK"))
                iBeLip = 560;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "OPPRESSOR"))
                iBeLip = 559;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "TECHNICAL2"))
                iBeLip = 534;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "voltic2"))
                iBeLip = 533;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "wastelander"))
                iBeLip = 532;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DUNE4") || iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "DUNE5"))
                iBeLip = 531;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "RUINER2"))
                iBeLip = 530;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "PHANTOM2"))
                iBeLip = 528;
            else if (iVehHash == Function.Call<int>(Hash.GET_HASH_KEY, "RHINO"))
                iBeLip = 421;

            return iBeLip;
        }
        public int WhoShotMe(Ped MeDie)
        {
            GetLogging("WhoShotMe");

            int iShoot = -1;

            for (int i = 0; i < PedList.Count; i++)
            {
                if (MeDie.GetKiller() == PedList[i].ThisPed)
                {
                    iShoot = i;
                    break;
                }
            }
            return iShoot;
        }
        private void Scaleform_PLAYER_LIST()
        {
            GetLogging("Scaleform_PLAYER_LIST");

            iScale = Function.Call<int>((Hash)0x11FE353CF9733E6F, "INSTRUCTIONAL_BUTTONS");

            while (!Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, iScale))
                Script.Wait(1);

            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_VOID, iScale, "CLEAR_ALL");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "TOGGLE_MOUSE_BUTTONS");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_BOOL, 0);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_VOID, iScale, "CREATE_CONTAINER");
            int iAddOns = 0;

            for (int i = 0; i < PedList.Count; i++)
            {
                int iFailed = 0;
                string sPlayer = PedList[i].MyName;
                while (sPlayer.Count() < 14 && iFailed < 10)
                {
                    sPlayer = sPlayer + " ";
                    Script.Wait(1);
                    iFailed += 1;
                }

                sPlayer = sPlayer + PedList[i].Level;
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "SET_DATA_SLOT");
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, sPlayer);
                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                iAddOns += 1;
            }
            for (int i = 0; i < AFKList.Count; i++)
            {
                int iFailed = 0;
                string sPlayer = AFKList[i].MyName;
                while (sPlayer.Count() < 14 && iFailed < 10)
                {
                    sPlayer = sPlayer + " ";
                    Script.Wait(1);
                    iFailed += 1;
                }

                sPlayer = sPlayer + AFKList[i].Level;
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "SET_DATA_SLOT");
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, sPlayer);
                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                iAddOns += 1;
            }

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "SET_DATA_SLOT");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
            if (bDisabled)
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, "Mod Disabled");
            else
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, "Players in Session ;  ");


            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "DRAW_INSTRUCTIONAL_BUTTONS");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 1);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            bPlayerList = true;
        }
        private void CloseBaseHelpBar()
        {
            GetLogging("CloseBaseHelpBar");

            unsafe
            {
                int SF = iScale;
                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &SF);
            }

            bPlayerList = false;
        }
        private void PlayScales()
        {
            GetLogging("PlayScales");

            Scaleform_PLAYER_LIST();

            if (bDisabled)
                TopLeftUI("Press" + ControlSybols(iDisableMod) + " to enable mod");
            else
                TopLeftUI("Press" + ControlSybols(iClearPlayList) + "to clear the session and " + ControlSybols(iDisableMod) + " to disable mod");

            int iStick = Game.GameTime + 8000;
            while (iStick > Game.GameTime && !ButtonDown(iClearPlayList, false) && !ButtonDown(iDisableMod, false))
            {
                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, iScale, 255, 255, 255, 255);
                Script.Wait(1);
            }
            CloseBaseHelpBar();

            while (ButtonDown(iGetlayList, true))
            {
                Script.Wait(500); 
                if (WhileButtonDown(iClearPlayList, true) && !bDisabled)
                    LaggOut();
                else if (WhileButtonDown(iDisableMod, true))
                    bDisabled = !bDisabled;
            }
        }
        private void FireOrb(int MyBrian, Ped Target)
        {
            GetLogging("FireOrb, MyBrian == " + MyBrian);

            Ped pFired = Game.Player.Character;

            if (MyBrian != -1)
            {
                MyBrian = ReteaveBrain(MyBrian);

                List<Vector3> FacList = new List<Vector3>
                {
                    new Vector3(1871.856f, 280.2685f, 164.3017f),
                    new Vector3(2074.258f, 1749.33f, 104.5142f),
                    new Vector3(2768.607f, 3919.833f, 45.81805f),
                    new Vector3(3407.416f, 5504.874f, 26.27827f),
                    new Vector3(1.844208f, 6832.069f, 15.81715f),
                    new Vector3(-2231.331f, 2417.907f, 12.18127f),
                    new Vector3(-6.777428f, 3326.627f, 41.63125f),
                    new Vector3(18.59906f, 2610.94f, 85.99267f),
                    new Vector3(1286.877f, 2846.37f, 49.39426f)
                };

                ClearPedBlips(PedList[MyBrian].Level);
                PedList[MyBrian].ThisBlip = LocalBlip(FacList[RandInt(0, FacList.Count - 1)], 590, PedList[MyBrian].MyName);

                pFired = PedList[MyBrian].ThisPed;
                Script.Wait(7500);
            }

            Vector3 PlayePos = Target.Position;
            if (World.GetGroundHeight(PlayePos) < PlayePos.Z)
            {
                Vector3 PlayerF = Target.Position + (Target.ForwardVector * 5);
                Vector3 PlayerB = Target.Position - (Target.ForwardVector * 5);
                Vector3 PlayerR = Target.Position + (Target.RightVector * 5);
                Vector3 PlayerL = Target.Position - (Target.RightVector * 5);
                OrbExp(pFired, PlayePos, PlayerF, PlayerB, PlayerR, PlayerL);
                if (MyBrian != -1)
                {
                    OrbLoad(PedList[MyBrian].MyName);
                    Script.Wait(4000);
                    PedCleaning(PedList[MyBrian].Level, "left", false);
                }
            }
        }
        private void OrbExp(Ped PFired, Vector3 Pos1, Vector3 Pos2, Vector3 Pos3, Vector3 Pos4, Vector3 Pos5)
        {
            GetLogging("OrbExp, Pos1 == " + Pos1);

            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos2.X, Pos2.Y, Pos2.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos3.X, Pos3.Y, Pos3.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos4.X, Pos4.Y, Pos4.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos5.X, Pos5.Y, Pos5.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos1.X, Pos1.Y, Pos1.Z, 54, 1.00f, true, false, 1.00f);

            Function.Call(Hash.PLAY_SOUND_FROM_COORD, -1, "DLC_XM_Explosions_Orbital_Cannon", Pos1.X, Pos1.Y, Pos1.Z, 0, 0, 1, 0);
            Function.Call((Hash)0x6C38AF3693A69A91, "scr_xm_orbital");
        }
        private void OrbLoad(string sWhoDidit)
        {
            GetLogging("OrbLoad, sWhoDidit == " + sWhoDidit);

            UI.Notify(sWhoDidit +" obliterated you with the Orbital Cannon.");

            iScale = Function.Call<int>((Hash)0x11FE353CF9733E6F, "MIDSIZED_MESSAGE");
            Script.Wait(1500);
            while (!Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, iScale))
                Script.Wait(1);

            Function.Call(Hash._START_SCREEN_EFFECT, "SuccessNeutral", 8500, false);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "SHOW_SHARD_MIDSIZED_MESSAGE");
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
            Function.Call((Hash)0x6C188BE134E074AA, "obliterated");
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
            Function.Call((Hash)0x6C188BE134E074AA, "");
            Function.Call(Hash._END_TEXT_COMPONENT);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 3);// color 0,1=white 2=black 3=grey 6,7,8=red 9,10,11=blue 12,13,14=yellow 15,16,17=orange 18,19,20=green 21,22,23=purple 
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            int iWait4Sec = Game.GameTime + 8000;

            while (iWait4Sec > Game.GameTime)
            {
                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, iScale, 255, 255, 255, 255);
                Script.Wait(1);
            }

            unsafe
            {
                int SF = iScale;
                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &SF);
            }
        }
        public string ControlSybols(int iButton)
        {
            List<string> ControlList = new List<string>
            {
                " ~INPUT_NEXT_CAMERA~ ",//V~ ");//BACK
                " ~INPUT_LOOK_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_LOOK_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_LOOK_UP_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_LOOK_DOWN_ONLY~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_LOOK_LEFT_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_LOOK_RIGHT_ONLY~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_CINEMATIC_SLOWMO~ ",//(NONE);~ ");//R3
                " ~INPUT_SCRIPTED_FLY_UD~ ",//S~ ");//LEFT STICK
                " ~INPUT_SCRIPTED_FLY_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_SCRIPTED_FLY_ZUP~ ",//PAGEUP~ ");//LT
                " ~INPUT_SCRIPTED_FLY_ZDOWN~ ",//PAGEDOWN~ ");//RT
                " ~INPUT_WEAPON_WHEEL_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_WEAPON_WHEEL_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_WEAPON_WHEEL_NEXT~ ",//SCROLLWHEEL DOWN~ ");//DPAD RIGHT
                " ~INPUT_WEAPON_WHEEL_PREV~ ",//SCROLLWHEEL UP~ ");//DPAD LEFT
                " ~INPUT_SELECT_NEXT_WEAPON~ ",//SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_SELECT_PREV_WEAPON~ ",//SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_SKIP_CUTSCENE~ ",//ENTER / LEFT MOUSE BUTTON / SPACEBAR~ ");//A
                " ~INPUT_CHARACTER_WHEEL~ ",//LEFT ALT~ ");//DPAD DOWN
                " ~INPUT_MULTIPLAYER_INFO~ ",//Z~ ");//DPAD DOWN
                " ~INPUT_SPRINT~ ",//LEFT SHIFT~ ");//A
                " ~INPUT_JUMP~ ",//SPACEBAR~ ");//X
                " ~INPUT_ENTER~ ",//F~ ");//Y
                " ~INPUT_ATTACK~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_AIM~ ",//RIGHT MOUSE BUTTON~ ");//LT
                " ~INPUT_LOOK_BEHIND~ ",//C~ ");//R3
                " ~INPUT_PHONE~ ",//ARROW UP / SCROLLWHEEL BUTTON (PRESS);~ ");//DPAD UP
                " ~INPUT_SPECIAL_ABILITY~ ",//(NONE);~ ");//L3
                " ~INPUT_SPECIAL_ABILITY_SECONDARY~ ",//B~ ");//R3
                " ~INPUT_MOVE_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_MOVE_UD~ ",//S~ ");//LEFT STICK
                " ~INPUT_MOVE_UP_ONLY~ ",//W~ ");//LEFT STICK
                " ~INPUT_MOVE_DOWN_ONLY~ ",//S~ ");//LEFT STICK
                " ~INPUT_MOVE_LEFT_ONLY~ ",//A~ ");//LEFT STICK
                " ~INPUT_MOVE_RIGHT_ONLY~ ",//D~ ");//LEFT STICK
                " ~INPUT_DUCK~ ",//LEFT CTRL~ ");//L3
                " ~INPUT_SELECT_WEAPON~ ",//TAB~ ");//LB
                " ~INPUT_PICKUP~ ",//E~ ");//LB
                " ~INPUT_SNIPER_ZOOM~ ",//[~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_IN_ONLY~ ",//]~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_OUT_ONLY~ ",//[~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_IN_SECONDARY~ ",//]~ ");//DPAD UP
                " ~INPUT_SNIPER_ZOOM_OUT_SECONDARY~ ",//[~ ");//DPAD DOWN
                " ~INPUT_COVER~ ",//Q~ ");//RB
                " ~INPUT_RELOAD~ ",//R~ ");//B
                " ~INPUT_TALK~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_DETONATE~ ",//G~ ");//DPAD LEFT
                " ~INPUT_HUD_SPECIAL~ ",//Z~ ");//DPAD DOWN
                " ~INPUT_ARREST~ ",//F~ ");//Y
                " ~INPUT_ACCURATE_AIM~ ",//SCROLLWHEEL DOWN~ ");//R3
                " ~INPUT_CONTEXT~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_CONTEXT_SECONDARY~ ",//Q~ ");//DPAD LEFT
                " ~INPUT_WEAPON_SPECIAL~ ",//(NONE);~ ");//Y
                " ~INPUT_WEAPON_SPECIAL_TWO~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_DIVE~ ",//SPACEBAR~ ");//RB
                " ~INPUT_DROP_WEAPON~ ",//F9~ ");//Y
                " ~INPUT_DROP_AMMO~ ",//F10~ ");//B
                " ~INPUT_THROW_GRENADE~ ",//G~ ");//DPAD LEFT
                " ~INPUT_VEH_MOVE_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_UD~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_UP_ONLY~ ",//LEFT SHIFT~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_DOWN_ONLY~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_LEFT_ONLY~ ",//A~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_RIGHT_ONLY~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_SPECIAL~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_VEH_GUN_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_VEH_AIM~ ",//RIGHT MOUSE BUTTON~ ");//LB
                " ~INPUT_VEH_ATTACK~ ",//LEFT MOUSE BUTTON~ ");//RB
                " ~INPUT_VEH_ATTACK2~ ",//RIGHT MOUSE BUTTON~ ");//A
                " ~INPUT_VEH_ACCELERATE~ ",//W~ ");//RT
                " ~INPUT_VEH_BRAKE~ ",//S~ ");//LT
                " ~INPUT_VEH_DUCK~ ",//X~ ");//A
                " ~INPUT_VEH_HEADLIGHT~ ",//H~ ");//DPAD RIGHT
                " ~INPUT_VEH_EXIT~ ",//F~ ");//Y
                " ~INPUT_VEH_HANDBRAKE~ ",//SPACEBAR~ ");//RB
                " ~INPUT_VEH_HOTWIRE_LEFT~ ",//W~ ");//LT
                " ~INPUT_VEH_HOTWIRE_RIGHT~ ",//S~ ");//RT
                " ~INPUT_VEH_LOOK_BEHIND~ ",//C~ ");//R3
                " ~INPUT_VEH_CIN_CAM~ ",//R~ ");//B
                " ~INPUT_VEH_NEXT_RADIO~ ",//.~ ");//(NONE);
                " ~INPUT_VEH_PREV_RADIO~ ",//,~ ");//(NONE);
                " ~INPUT_VEH_NEXT_RADIO_TRACK~ ",//=~ ");//(NONE);
                " ~INPUT_VEH_PREV_RADIO_TRACK~ ",//-~ ");//(NONE);
                " ~INPUT_VEH_RADIO_WHEEL~ ",//Q~ ");//DPAD LEFT
                " ~INPUT_VEH_HORN~ ",//E~ ");//L3
                " ~INPUT_VEH_FLY_THROTTLE_UP~ ",//W~ ");//RT
                " ~INPUT_VEH_FLY_THROTTLE_DOWN~ ",//S~ ");//LT
                " ~INPUT_VEH_FLY_YAW_LEFT~ ",//A~ ");//LB
                " ~INPUT_VEH_FLY_YAW_RIGHT~ ",//D~ ");//RB
                " ~INPUT_VEH_PASSENGER_AIM~ ",//RIGHT MOUSE BUTTON~ ");//LT
                " ~INPUT_VEH_PASSENGER_ATTACK~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_VEH_SPECIAL_ABILITY_FRANKLIN~ ",//(NONE);~ ");//R3
                " ~INPUT_VEH_STUNT_UD~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_VEH_CINEMATIC_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_VEH_CINEMATIC_UP_ONLY~ ",//NUMPAD- / SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_VEH_CINEMATIC_DOWN_ONLY~ ",//NUMPAD+ / SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_VEH_CINEMATIC_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_SELECT_NEXT_WEAPON~ ",//SCROLLWHEEL UP~ ");//X
                " ~INPUT_VEH_SELECT_PREV_WEAPON~ ",//[~ ");//(NONE);
                " ~INPUT_VEH_ROOF~ ",//H~ ");//DPAD RIGHT
                " ~INPUT_VEH_JUMP~ ",//SPACEBAR~ ");//RB
                " ~INPUT_VEH_GRAPPLING_HOOK~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_SHUFFLE~ ",//H~ ");//DPAD RIGHT
                " ~INPUT_VEH_DROP_PROJECTILE~ ",//X~ ");//A
                " ~INPUT_VEH_MOUSE_CONTROL_OVERRIDE~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_FLY_ROLL_LR~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_ROLL_LEFT_ONLY~ ",//NUMPAD 4~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_ROLL_RIGHT_ONLY~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_PITCH_UD~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_PITCH_UP_ONLY~ ",//NUMPAD 8~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_PITCH_DOWN_ONLY~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_FLY_UNDERCARRIAGE~ ",//G~ ");//L3
                " ~INPUT_VEH_FLY_ATTACK~ ",//RIGHT MOUSE BUTTON~ ");//A
                " ~INPUT_VEH_FLY_SELECT_NEXT_WEAPON~ ",//SCROLLWHEEL UP~ ");//DPAD LEFT
                " ~INPUT_VEH_FLY_SELECT_PREV_WEAPON~ ",//[~ ");//(NONE);
                " ~INPUT_VEH_FLY_SELECT_TARGET_LEFT~ ",//NUMPAD 7~ ");//LB
                " ~INPUT_VEH_FLY_SELECT_TARGET_RIGHT~ ",//NUMPAD 9~ ");//RB
                " ~INPUT_VEH_FLY_VERTICAL_FLIGHT_MODE~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_FLY_DUCK~ ",//X~ ");//A
                " ~INPUT_VEH_FLY_ATTACK_CAMERA~ ",//INSERT~ ");//R3
                " ~INPUT_VEH_FLY_MOUSE_CONTROL_OVERRIDE~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_SUB_TURN_LR~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_TURN_LEFT_ONLY~ ",//NUMPAD 4~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_TURN_RIGHT_ONLY~ ",//NUMPAD 6~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_PITCH_UD~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_PITCH_UP_ONLY~ ",//NUMPAD 8~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_PITCH_DOWN_ONLY~ ",//NUMPAD 5~ ");//LEFT STICK
                " ~INPUT_VEH_SUB_THROTTLE_UP~ ",//W~ ");//RT
                " ~INPUT_VEH_SUB_THROTTLE_DOWN~ ",//S~ ");//LT
                " ~INPUT_VEH_SUB_ASCEND~ ",//LEFT SHIFT~ ");//X
                " ~INPUT_VEH_SUB_DESCEND~ ",//LEFT CTRL~ ");//A
                " ~INPUT_VEH_SUB_TURN_HARD_LEFT~ ",//A~ ");//LB
                " ~INPUT_VEH_SUB_TURN_HARD_RIGHT~ ",//D~ ");//RB
                " ~INPUT_VEH_SUB_MOUSE_CONTROL_OVERRIDE~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_PUSHBIKE_PEDAL~ ",//W~ ");//A
                " ~INPUT_VEH_PUSHBIKE_SPRINT~ ",//CAPSLOCK~ ");//A
                " ~INPUT_VEH_PUSHBIKE_FRONT_BRAKE~ ",//Q~ ");//LT
                " ~INPUT_VEH_PUSHBIKE_REAR_BRAKE~ ",//S~ ");//RT
                " ~INPUT_MELEE_ATTACK_LIGHT~ ",//R~ ");//B
                " ~INPUT_MELEE_ATTACK_HEAVY~ ",//Q~ ");//A
                " ~INPUT_MELEE_ATTACK_ALTERNATE~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_MELEE_BLOCK~ ",//SPACEBAR~ ");//X
                " ~INPUT_PARACHUTE_DEPLOY~ ",//F / LEFT MOUSE BUTTON~ ");//Y
                " ~INPUT_PARACHUTE_DETACH~ ",//F~ ");//Y
                " ~INPUT_PARACHUTE_TURN_LR~ ",//D~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_TURN_LEFT_ONLY~ ",//A~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_TURN_RIGHT_ONLY~ ",//D~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_PITCH_UD~ ",//S~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_PITCH_UP_ONLY~ ",//W~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_PITCH_DOWN_ONLY~ ",//S~ ");//LEFT STICK
                " ~INPUT_PARACHUTE_BRAKE_LEFT~ ",//Q~ ");//LB
                " ~INPUT_PARACHUTE_BRAKE_RIGHT~ ",//E~ ");//RB
                " ~INPUT_PARACHUTE_SMOKE~ ",//X~ ");//A
                " ~INPUT_PARACHUTE_PRECISION_LANDING~ ",//LEFT SHIFT~ ");//(NONE);
                " ~INPUT_MAP~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_UNARMED~ ",//1~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_MELEE~ ",//2~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_HANDGUN~ ",//6~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SHOTGUN~ ",//3~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SMG~ ",//7~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_AUTO_RIFLE~ ",//8~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SNIPER~ ",//9~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_HEAVY~ ",//4~ ");//(NONE);
                " ~INPUT_SELECT_WEAPON_SPECIAL~ ",//5~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_MICHAEL~ ",//F5~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_FRANKLIN~ ",//F6~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_TREVOR~ ",//F7~ ");//(NONE);
                " ~INPUT_SELECT_CHARACTER_MULTIPLAYER~ ",//F8 (CONSOLE);~ ");//(NONE);
                " ~INPUT_SAVE_REPLAY_CLIP~ ",//F3~ ");//B
                " ~INPUT_SPECIAL_ABILITY_PC~ ",//CAPSLOCK~ ");//(NONE);
                " ~INPUT_CELLPHONE_UP~ ",//ARROW UP~ ");//DPAD UP
                " ~INPUT_CELLPHONE_DOWN~ ",//ARROW DOWN~ ");//DPAD DOWN
                " ~INPUT_CELLPHONE_LEFT~ ",//ARROW LEFT~ ");//DPAD LEFT
                " ~INPUT_CELLPHONE_RIGHT~ ",//ARROW RIGHT~ ");//DPAD RIGHT
                " ~INPUT_CELLPHONE_SELECT~ ",//ENTER / LEFT MOUSE BUTTON~ ");//A
                " ~INPUT_CELLPHONE_CANCEL~ ",//BACKSPACE / ESC / RIGHT MOUSE BUTTON~ ");//B
                " ~INPUT_CELLPHONE_OPTION~ ",//DELETE~ ");//Y
                " ~INPUT_CELLPHONE_EXTRA_OPTION~ ",//SPACEBAR~ ");//X
                " ~INPUT_CELLPHONE_SCROLL_FORWARD~ ",//SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_CELLPHONE_SCROLL_BACKWARD~ ",//SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_CELLPHONE_CAMERA_FOCUS_LOCK~ ",//L~ ");//RT
                " ~INPUT_CELLPHONE_CAMERA_GRID~ ",//G~ ");//RB
                " ~INPUT_CELLPHONE_CAMERA_SELFIE~ ",//E~ ");//R3
                " ~INPUT_CELLPHONE_CAMERA_DOF~ ",//F~ ");//LB
                " ~INPUT_CELLPHONE_CAMERA_EXPRESSION~ ",//X~ ");//L3
                " ~INPUT_FRONTEND_DOWN~ ",//ARROW DOWN~ ");//DPAD DOWN
                " ~INPUT_FRONTEND_UP~ ",//ARROW UP~ ");//DPAD UP
                " ~INPUT_FRONTEND_LEFT~ ",//ARROW LEFT~ ");//DPAD LEFT
                " ~INPUT_FRONTEND_RIGHT~ ",//ARROW RIGHT~ ");//DPAD RIGHT
                " ~INPUT_FRONTEND_RDOWN~ ",//ENTER~ ");//A
                " ~INPUT_FRONTEND_RUP~ ",//TAB~ ");//Y
                " ~INPUT_FRONTEND_RLEFT~ ",//(NONE);~ ");//X
                " ~INPUT_FRONTEND_RRIGHT~ ",//BACKSPACE~ ");//B
                " ~INPUT_FRONTEND_AXIS_X~ ",//D~ ");//LEFT STICK
                " ~INPUT_FRONTEND_AXIS_Y~ ",//S~ ");//LEFT STICK
                " ~INPUT_FRONTEND_RIGHT_AXIS_X~ ",//]~ ");//RIGHT STICK
                " ~INPUT_FRONTEND_RIGHT_AXIS_Y~ ",//SCROLLWHEEL DOWN~ ");//RIGHT STICK
                " ~INPUT_FRONTEND_PAUSE~ ",//P~ ");//START
                " ~INPUT_FRONTEND_PAUSE_ALTERNATE~ ",//ESC~ ");//(NONE);
                " ~INPUT_FRONTEND_ACCEPT~ ",//ENTER / NUMPAD ENTER~ ");//A
                " ~INPUT_FRONTEND_CANCEL~ ",//BACKSPACE / ESC~ ");//B
                " ~INPUT_FRONTEND_X~ ",//SPACEBAR~ ");//X
                " ~INPUT_FRONTEND_Y~ ",//TAB~ ");//Y
                " ~INPUT_FRONTEND_LB~ ",//Q~ ");//LB
                " ~INPUT_FRONTEND_RB~ ",//E~ ");//RB
                " ~INPUT_FRONTEND_LT~ ",//PAGE DOWN~ ");//LT
                " ~INPUT_FRONTEND_RT~ ",//PAGE UP~ ");//RT
                " ~INPUT_FRONTEND_LS~ ",//LEFT SHIFT~ ");//L3
                " ~INPUT_FRONTEND_RS~ ",//LEFT CONTROL~ ");//R3
                " ~INPUT_FRONTEND_LEADERBOARD~ ",//TAB~ ");//RB
                " ~INPUT_FRONTEND_SOCIAL_CLUB~ ",//HOME~ ");//BACK
                " ~INPUT_FRONTEND_SOCIAL_CLUB_SECONDARY~ ",//HOME~ ");//RB
                " ~INPUT_FRONTEND_DELETE~ ",//DELETE~ ");//X
                " ~INPUT_FRONTEND_ENDSCREEN_ACCEPT~ ",//ENTER~ ");//A
                " ~INPUT_FRONTEND_ENDSCREEN_EXPAND~ ",//SPACEBAR~ ");//X
                " ~INPUT_FRONTEND_SELECT~ ",//CAPSLOCK~ ");//BACK
                " ~INPUT_SCRIPT_LEFT_AXIS_X~ ",//D~ ");//LEFT STICK
                " ~INPUT_SCRIPT_LEFT_AXIS_Y~ ",//S~ ");//LEFT STICK
                " ~INPUT_SCRIPT_RIGHT_AXIS_X~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_SCRIPT_RIGHT_AXIS_Y~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_SCRIPT_RUP~ ",//RIGHT MOUSE BUTTON~ ");//Y
                " ~INPUT_SCRIPT_RDOWN~ ",//LEFT MOUSE BUTTON~ ");//A
                " ~INPUT_SCRIPT_RLEFT~ ",//LEFT CTRL~ ");//X
                " ~INPUT_SCRIPT_RRIGHT~ ",//RIGHT MOUSE BUTTON~ ");//B
                " ~INPUT_SCRIPT_LB~ ",//(NONE);~ ");//LB
                " ~INPUT_SCRIPT_RB~ ",//(NONE);~ ");//RB
                " ~INPUT_SCRIPT_LT~ ",//(NONE);~ ");//LT
                " ~INPUT_SCRIPT_RT~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_SCRIPT_LS~ ",//(NONE);~ ");//L3
                " ~INPUT_SCRIPT_RS~ ",//(NONE);~ ");//R3
                " ~INPUT_SCRIPT_PAD_UP~ ",//W~ ");//DPAD UP
                " ~INPUT_SCRIPT_PAD_DOWN~ ",//S~ ");//DPAD DOWN
                " ~INPUT_SCRIPT_PAD_LEFT~ ",//A~ ");//DPAD LEFT
                " ~INPUT_SCRIPT_PAD_RIGHT~ ",//D~ ");//DPAD RIGHT
                " ~INPUT_SCRIPT_SELECT~ ",//V~ ");//BACK
                " ~INPUT_CURSOR_ACCEPT~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_CURSOR_CANCEL~ ",//RIGHT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_CURSOR_X~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_CURSOR_Y~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_CURSOR_SCROLL_UP~ ",//SCROLLWHEEL UP~ ");//(NONE);
                " ~INPUT_CURSOR_SCROLL_DOWN~ ",//SCROLLWHEEL DOWN~ ");//(NONE);
                " ~INPUT_ENTER_CHEAT_CODE~ ",//~ / `~ ");//(NONE);
                " ~INPUT_INTERACTION_MENU~ ",//M~ ");//BACK
                " ~INPUT_MP_TEXT_CHAT_ALL~ ",//T~ ");//(NONE);
                " ~INPUT_MP_TEXT_CHAT_TEAM~ ",//Y~ ");//(NONE);
                " ~INPUT_MP_TEXT_CHAT_FRIENDS~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_MP_TEXT_CHAT_CREW~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_PUSH_TO_TALK~ ",//N~ ");//(NONE);
                " ~INPUT_CREATOR_LS~ ",//R~ ");//L3
                " ~INPUT_CREATOR_RS~ ",//F~ ");//R3
                " ~INPUT_CREATOR_LT~ ",//X~ ");//LT
                " ~INPUT_CREATOR_RT~ ",//C~ ");//RT
                " ~INPUT_CREATOR_MENU_TOGGLE~ ",//LEFT SHIFT~ ");//(NONE);
                " ~INPUT_CREATOR_ACCEPT~ ",//SPACEBAR~ ");//A
                " ~INPUT_CREATOR_DELETE~ ",//DELETE~ ");//X
                " ~INPUT_ATTACK2~ ",//LEFT MOUSE BUTTON~ ");//RT
                " ~INPUT_RAPPEL_JUMP~ ",//(NONE);~ ");//A
                " ~INPUT_RAPPEL_LONG_JUMP~ ",//(NONE);~ ");//X
                " ~INPUT_RAPPEL_SMASH_WINDOW~ ",//(NONE);~ ");//RT
                " ~INPUT_PREV_WEAPON~ ",//SCROLLWHEEL UP~ ");//DPAD LEFT
                " ~INPUT_NEXT_WEAPON~ ",//SCROLLWHEEL DOWN~ ");//DPAD RIGHT
                " ~INPUT_MELEE_ATTACK1~ ",//R~ ");//B
                " ~INPUT_MELEE_ATTACK2~ ",//Q~ ");//A
                " ~INPUT_WHISTLE~ ",//(NONE);~ ");//(NONE);
                " ~INPUT_MOVE_LEFT~ ",//D~ ");//LEFT STICK
                " ~INPUT_MOVE_RIGHT~ ",//D~ ");//LEFT STICK
                " ~INPUT_MOVE_UP~ ",//S~ ");//LEFT STICK
                " ~INPUT_MOVE_DOWN~ ",//S~ ");//LEFT STICK
                " ~INPUT_LOOK_LEFT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_LOOK_RIGHT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_LOOK_UP~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_LOOK_DOWN~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_SNIPER_ZOOM_IN~ ",//[~ ");//RIGHT STICK
                " ~INPUT_SNIPER_ZOOM_OUT~ ",//[~ ");//RIGHT STICK
                " ~INPUT_SNIPER_ZOOM_IN_ALTERNATE~ ",//[~ ");//LEFT STICK
                " ~INPUT_SNIPER_ZOOM_OUT_ALTERNATE~ ",//[~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_LEFT~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_RIGHT~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_UP~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_MOVE_DOWN~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_GUN_LEFT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_RIGHT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_UP~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_GUN_DOWN~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_LOOK_LEFT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_LOOK_RIGHT~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_REPLAY_START_STOP_RECORDING~ ",//F1~ ");//A
                " ~INPUT_REPLAY_START_STOP_RECORDING_SECONDARY~ ",//F2~ ");//X
                " ~INPUT_SCALED_LOOK_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_UP_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_DOWN_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_LEFT_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_SCALED_LOOK_RIGHT_ONLY~ ",//(NONE);~ ");//RIGHT STICK
                " ~INPUT_REPLAY_MARKER_DELETE~ ",//DELETE~ ");//X
                " ~INPUT_REPLAY_CLIP_DELETE~ ",//DELETE~ ");//Y
                " ~INPUT_REPLAY_PAUSE~ ",//SPACEBAR~ ");//A
                " ~INPUT_REPLAY_REWIND~ ",//ARROW DOWN~ ");//LB
                " ~INPUT_REPLAY_FFWD~ ",//ARROW UP~ ");//RB
                " ~INPUT_REPLAY_NEWMARKER~ ",//M~ ");//A
                " ~INPUT_REPLAY_RECORD~ ",//S~ ");//(NONE);
                " ~INPUT_REPLAY_SCREENSHOT~ ",//U~ ");//DPAD UP
                " ~INPUT_REPLAY_HIDEHUD~ ",//H~ ");//R3
                " ~INPUT_REPLAY_STARTPOINT~ ",//B~ ");//(NONE);
                " ~INPUT_REPLAY_ENDPOINT~ ",//N~ ");//(NONE);
                " ~INPUT_REPLAY_ADVANCE~ ",//ARROW RIGHT~ ");//DPAD RIGHT
                " ~INPUT_REPLAY_BACK~ ",//ARROW LEFT~ ");//DPAD LEFT
                " ~INPUT_REPLAY_TOOLS~ ",//T~ ");//DPAD DOWN
                " ~INPUT_REPLAY_RESTART~ ",//R~ ");//BACK
                " ~INPUT_REPLAY_SHOWHOTKEY~ ",//K~ ");//DPAD DOWN
                " ~INPUT_REPLAY_CYCLEMARKERLEFT~ ",//[~ ");//DPAD LEFT
                " ~INPUT_REPLAY_CYCLEMARKERRIGHT~ ",//]~ ");//DPAD RIGHT
                " ~INPUT_REPLAY_FOVINCREASE~ ",//NUMPAD +~ ");//RB
                " ~INPUT_REPLAY_FOVDECREASE~ ",//NUMPAD -~ ");//LB
                " ~INPUT_REPLAY_CAMERAUP~ ",//PAGE UP~ ");//(NONE);
                " ~INPUT_REPLAY_CAMERADOWN~ ",//PAGE DOWN~ ");//(NONE);
                " ~INPUT_REPLAY_SAVE~ ",//F5~ ");//START
                " ~INPUT_REPLAY_TOGGLETIME~ ",//C~ ");//(NONE);
                " ~INPUT_REPLAY_TOGGLETIPS~ ",//V~ ");//(NONE);
                " ~INPUT_REPLAY_PREVIEW~ ",//SPACEBAR~ ");//(NONE);
                " ~INPUT_REPLAY_TOGGLE_TIMELINE~ ",//ESC~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_PICKUP_CLIP~ ",//X~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_DUPLICATE_CLIP~ ",//C~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_PLACE_CLIP~ ",//V~ ");//(NONE);
                " ~INPUT_REPLAY_CTRL~ ",//LEFT CTRL~ ");//(NONE);
                " ~INPUT_REPLAY_TIMELINE_SAVE~ ",//F5~ ");//(NONE);
                " ~INPUT_REPLAY_PREVIEW_AUDIO~ ",//SPACEBAR~ ");//RT
                " ~INPUT_VEH_DRIVE_LOOK~ ",//LEFT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_DRIVE_LOOK2~ ",//RIGHT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_VEH_FLY_ATTACK2~ ",//RIGHT MOUSE BUTTON~ ");//(NONE);
                " ~INPUT_RADIO_WHEEL_UD~ ",//MOUSE DOWN~ ");//RIGHT STICK
                " ~INPUT_RADIO_WHEEL_LR~ ",//MOUSE RIGHT~ ");//RIGHT STICK
                " ~INPUT_VEH_SLOWMO_UD~ ",//SCROLLWHEEL DOWN~ ");//LEFT STICK
                " ~INPUT_VEH_SLOWMO_UP_ONLY~ ",//SCROLLWHEEL UP~ ");//LEFT STICK
                " ~INPUT_VEH_SLOWMO_DOWN_ONLY~ ",//SCROLLWHEEL DOWN~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_TOGGLE~ ",//X~ ");//A
                " ~INPUT_VEH_HYDRAULICS_CONTROL_LEFT~ ",//A~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_RIGHT~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_UP~ ",//LEFT SHIFT~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_DOWN~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_UD~ ",//D~ ");//LEFT STICK
                " ~INPUT_VEH_HYDRAULICS_CONTROL_LR~ ",//LEFT CTRL~ ");//LEFT STICK
                " ~INPUT_SWITCH_VISOR~ ",//F11~ ");//DPAD RIGHT
                " ~INPUT_VEH_MELEE_HOLD~ ",//X~ ");//A
                " ~INPUT_VEH_MELEE_LEFT~ ",//LEFT MOUSE BUTTON~ ");//LB
                " ~INPUT_VEH_MELEE_RIGHT~ ",//RIGHT MOUSE BUTTON~ ");//RB
                " ~INPUT_MAP_POI~ ",//SCROLLWHEEL BUTTON (PRESS);~ ");//Y
                " ~INPUT_REPLAY_SNAPMATIC_PHOTO~ ",//TAB~ ");//X
                " ~INPUT_VEH_CAR_JUMP~ ",//E~ ");//L3
                " ~INPUT_VEH_ROCKET_BOOST~ ",//E~ ");//L3
                " ~INPUT_VEH_FLY_BOOST~ ",//LEFT SHIFT~ ");//L3
                " ~INPUT_VEH_PARACHUTE~ ",//SPACEBAR~ ");//A
                " ~INPUT_VEH_BIKE_WINGS~ ",//X~ ");//A
                " ~INPUT_VEH_FLY_BOMB_BAY~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_FLY_COUNTER~ ",//E~ ");//DPAD RIGHT
                " ~INPUT_VEH_TRANSFORM~ ",//X~ ");//A
                " ~INPUT_QUAD_LOCO_REVERSE~ ",//~ ");//RB
                " ~INPUT_RESPAWN_FASTER~ ",//~ ");//
                " ~INPUT_HUDMARKER_SELECT~ "
            };

            return ControlList[iButton];
        }
        public bool WhileButtonDown(int CButt, bool bDisable)
        {
            if (bDisable)
                ButtonDisabler(CButt); ;

            bool bButt = Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, CButt);

            if (bButt)
            {
                while (!Function.Call<bool>(Hash.IS_DISABLED_CONTROL_JUST_RELEASED, 0, CButt))
                    Script.Wait(1);
            }

            return bButt;
        }
        public bool ButtonDown(int CButt, bool bDisable)
        {
            if (bDisable)
                ButtonDisabler(CButt);
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, CButt);
        }
        private void ButtonDisabler(int LButt)
        {
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, LButt, true);
        }
        private void TopLeftUI(string sText)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, sText);
            Function.Call(Hash._0x238FFE5C7B0498A6, false, false, false, 5000);
        }
        private void WhileYouDead(string Kellar, int iKills, int iKilled, Ped Peddy)
        {
            GetLogging("WhileYouDead, string == " + Kellar + ", iKills == " + iKills + ", iKilled == " + iKilled);

            while (Game.Player.Character.GetKiller() == Peddy)
                Script.Wait(1);
            Script.Wait(1000);
            UI.Notify("You  " + iKills + " - " + iKilled + " " + Kellar);
        }
        public class FindVeh
        {
            public float MinRadi { get; set; }
            public float MaxRadi { get; set; }
            public string VehModel { get; set; }
            public bool AddPlayer { get; set; }
            public int AddtoBrain { get; set; }
        }
        public class PositionDirect
        {
            public Vector3 Pos { get; set; }
            public float Dir { get; set; }
        }
        public PositionDirect GetVehPos(float fMinRadi, float fMaxRadi)
        {
            GetLogging("GetVehPos");
            iFindingTime = Game.GameTime + 1000;
            Vector3 vArea = Game.Player.Character.Position - (Game.Player.Character.ForwardVector * 15);
            PositionDirect MyPosDir = null;
            Vehicle[] CarSpot = World.GetNearbyVehicles(vArea, fMaxRadi);
            for (int i = 0; i < CarSpot.Count(); i++)
            {
                if (VehExists(CarSpot, i))
                {
                    Vehicle Veh = CarSpot[i];
                    if (Veh.IsPersistent == false && Veh.Position.DistanceTo(Game.Player.Character.Position) > fMinRadi && Veh.ClassType != VehicleClass.Boats && Veh.ClassType != VehicleClass.Cycles && Veh.ClassType != VehicleClass.Helicopters && Veh.ClassType != VehicleClass.Planes && Veh.ClassType != VehicleClass.Trains && Veh != Game.Player.Character.CurrentVehicle && !Veh.IsOnScreen && Veh.EngineRunning)
                    {
                        MyPosDir = new PositionDirect
                        {
                            Pos = Veh.Position,
                            Dir = Veh.Heading
                        };
                        Veh.Delete();
                        break;
                    }

                }
            }

            return MyPosDir;
        }
        private void VehRelpace(PositionDirect MyPos, FindVeh MyVeh)
        {
            VehicleSpawn(MyVeh.VehModel, MyPos.Pos, MyPos.Dir, MyVeh.AddPlayer, MyVeh.AddtoBrain, false);
        }
        private void SearchVeh(float fMin, float fMax, string sVehModel, bool bAddPlayer, int iBrainLevel)
        {
            GetLogging("SearchVeh, sVehModel == " + sVehModel + ",iBrainLevel == " + iBrainLevel);
            FindVeh MyFinda = new FindVeh
            {
                MinRadi = fMin,
                MaxRadi = fMax,
                VehModel = sVehModel,
                AddPlayer = bAddPlayer,
                AddtoBrain = iBrainLevel
            };
            MakeCarz.Add(MyFinda);
        }
        public class FindPed
        {
            public float MinRadi { get; set; }
            public float MaxRadi { get; set; }
            public Vehicle MyCar { get; set; }
            public int Seat { get; set; }
            public int Reload { get; set; }
        }
        public PositionDirect GetPedPos(float fMinRadi, float fMaxRadi)
        {
            GetLogging("GetPedPos");
            Vector3 vArea = Game.Player.Character.Position - (Game.Player.Character.ForwardVector * 15);
            iFindingTime = Game.GameTime + 500;
            PositionDirect MyPosDir = null;
            Ped[] MadPeds = World.GetNearbyPeds(vArea, fMaxRadi);
            for (int i = 0; i < MadPeds.Count(); i++)
            {
                if (PedExists(MadPeds, i))
                {
                    Ped MadP = MadPeds[i];

                    if (!MadP.IsOnScreen && !MadP.IsInVehicle() && !MadP.IsDead && Function.Call<int>(Hash.GET_PED_TYPE, MadP.Handle) != 28 && MadP != Game.Player.Character && !MadP.IsPersistent && MadP.Position.DistanceTo(Game.Player.Character.Position) > fMinRadi)
                    {
                        MyPosDir = new PositionDirect
                        {
                            Pos = MadP.Position,
                            Dir = MadP.Heading
                        };
                        MadP.Delete();
                        break;
                    }
                }
            }

            return MyPosDir;
        }
        private void PedRelpace(PositionDirect MyPos, FindPed MyPeds)
        {
            GenPlayerPed(MyPos.Pos, MyPos.Dir, MyPeds.MyCar, MyPeds.Seat, MyPeds.Reload);
        }
        private void SearchPed(float fMin, float fMax, Vehicle vMyCar, int iSeat, int iReload)
        {
            GetLogging("SearchPed, iSeat == " + iSeat + ",iReload == " + iReload);
            FindPed MyFinda = new FindPed
            {
                MinRadi = fMin,
                MaxRadi = fMax,
                MyCar = vMyCar,
                Seat  = iSeat,
                Reload = iReload
            };
            MakeFrenz.Add(MyFinda);
        }
        public class GetInAveh
        {
            public Ped Peddy { get; set; }
            public Vehicle Vhic { get; set; }
            public int Seats { get; set; }
            public int PedLevel { get; set; }
        }
        private void SearchSeats(Ped pPeddy, Vehicle vVhic, int iPedLevel)
        {
            GetLogging("SearchSeats, iPedLevel == " + iPedLevel);
            GetInAveh MyFinda = new GetInAveh
            {
                Peddy = pPeddy,
                Vhic = vVhic,
                PedLevel = iPedLevel,
                Seats = -1
            };
            GetInQUe.Add(MyFinda); ;
        }
        private void PedDoGetIn(GetInAveh GetingOn)
        {
            GetLogging("PedDoGetIn");

            iFindingTime = Game.GameTime + 1000;
            int iSeats = GetingOn.Seats;

            if (iSeats == -1)
            {
                while (iSeats < Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, GetingOn.Vhic.Handle))
                {
                    if (Function.Call<bool>(Hash.IS_VEHICLE_SEAT_FREE, GetingOn.Vhic.Handle, iSeats))
                        break;
                    else
                        iSeats += 1;
                }
                if (iSeats < Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, GetingOn.Vhic.Handle))
                    GetInQUe[0].Seats = iSeats;
                else
                    iSeats = -1;
            }

            if (iSeats != -1)
            {
                if (GetingOn.Peddy.Position.DistanceTo(GetingOn.Vhic.Position) < 65.00f)
                {
                    if (GetingOn.Peddy.Position.DistanceTo(GetingOn.Vhic.Position) > 65.00f)
                        WarptoAnyVeh(GetingOn.Vhic, GetingOn.Peddy, GetingOn.Seats);
                    else if (!Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, GetingOn.Peddy.Handle))
                        Function.Call(Hash.TASK_ENTER_VEHICLE, GetingOn.Peddy.Handle, GetingOn.Vhic.Handle, -1, GetingOn.Seats, 1.50f, 1, 0);
                }
                else
                    WarptoAnyVeh(GetingOn.Vhic, GetingOn.Peddy, iSeats);
            }
            else
            {
                if (GetingOn.PedLevel != -1)
                {
                    int iBPed = ReteaveBrain(GetingOn.PedLevel);
                    if (PedList[iBPed].ThisVeh != null)
                        PedList[iBPed].ThisVeh.MarkAsNoLongerNeeded();
                    GetInQUe.RemoveAt(0);
                    SearchVeh(1.00f, 95.00f, RandVeh(RandInt(1, 9)), false, GetingOn.PedLevel);
                }
            }
        }
        private void PlayerZerosAI()
        {
            if (ButtonDown(iGetlayList, false) && !bPlayerList)
                PlayScales();
            if (bDisabled)
            {
                if (ThisBrian(0) != null || ThisAFKer(0) != null)
                    LaggOut();
            }
            else
            {
                if (ThisBrian(iNpcList) != null)
                {
                    int iBe = iNpcList;
                    if (PedList[iBe].DirBlip != null)
                        BlipDirect(PedList[iBe].DirBlip, PedList[iBe].ThisPed.Heading);

                    if (PedList[iBe].ThisPed == null)
                    {

                    }
                    else if (PedList[iBe].EnterVehQue)
                    {
                        if (!Game.Player.Character.IsInVehicle())
                        {
                            PedList[iBe].EnterVehQue = false;
                            GetInQUe.Clear();
                        }
                    }
                    else if (PedList[iBe].TimeOn < Game.GameTime)
                    {
                        PedList[iBe].AirAttack = 0;
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, PedList[iBe].ThisPed.Handle);
                        GetOutVehicle(PedList[iBe].ThisPed, PedList[iBe].Level);
                        PedCleaning(PedList[iBe].Level, "left", false);
                    }
                    else if (Game.Player.Character.GetKiller() == PedList[iBe].ThisPed)
                    {
                        PedList[iBe].Kills += 1;
                        WhileYouDead(PedList[iBe].MyName, PedList[iBe].Killed, PedList[iBe].Kills, PedList[iBe].ThisPed);
                        if (iAggression < 6)
                            PedCleaning(PedList[iBe].Level, "left", false);
                    }
                    else if (PedList[iBe].ThisPed.IsDead)
                    {
                        if (PedList[iBe].DeathSequence == 0)
                        {
                            PedList[iBe].AirAttack = 0;

                            if (PedList[iBe].ThisOppress != null)
                            {
                                EmptyVeh(PedList[iBe].ThisOppress);
                                PedList[iBe].ThisOppress.Explode();
                                PedList[iBe].ThisOppress.MarkAsNoLongerNeeded();
                                PedList[iBe].ThisOppress = null;

                                if (PedList[iBe].ThisVeh != null)
                                {
                                    EmptyVeh(PedList[iBe].ThisVeh);
                                    PedList[iBe].ThisVeh.Delete();
                                    PedList[iBe].ThisVeh = null;
                                }
                            }
                            else if (PedList[iBe].ThisVeh != null)
                            {
                                EmptyVeh(PedList[iBe].ThisVeh);
                                PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                PedList[iBe].ThisVeh = null;
                            }

                            int iDie = WhoShotMe(PedList[iBe].ThisPed);

                            ClearPedBlips(PedList[iBe].Level);

                            if (PedList[iBe].ThisPed.GetKiller() == Game.Player.Character)
                            {
                                if (PedList[iBe].Bounty)
                                    Game.Player.Money += 7000;
                                PedList[iBe].Friendly = false;
                                PedList[iBe].Colours = 1;
                                PedList[iBe].ApprochPlayer = false;
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, PedList[iBe].ThisPed.Handle);
                                PedList[iBe].Follower = false;
                                PedList[iBe].Killed += 1;
                                UI.Notify("You  " + PedList[iBe].Killed + " - " + PedList[iBe].Kills + " " + PedList[iBe].MyName);
                            }
                            else if (iDie != -1)
                                UI.Notify(PedList[iDie].MyName + " Killed " + PedList[iBe].MyName);
                            else
                                UI.Notify(PedList[iBe].MyName + " died");

                            PedList[iBe].Bounty = false;
                            PedList[iBe].DeathSequence += 1;
                            PedList[iBe].DeathTime = Game.GameTime + 10000;
                            PedList[iBe].TimeOn += 60000;
                            Function.Call(Hash.REMOVE_PED_FROM_GROUP, PedList[iBe].ThisPed.Handle);
                        }
                        else if (PedList[iBe].DeathSequence == 1 || PedList[iBe].DeathSequence == 3 || PedList[iBe].DeathSequence == 5 || PedList[iBe].DeathSequence == 7)
                        {
                            if (PedList[iBe].DeathTime < Game.GameTime)
                            {
                                PedList[iBe].ThisPed.Alpha = 80;
                                PedList[iBe].DeathSequence += 1;
                                PedList[iBe].DeathTime = Game.GameTime + 500;
                            }
                        }
                        else if (PedList[iBe].DeathSequence == 2 || PedList[iBe].DeathSequence == 4 || PedList[iBe].DeathSequence == 6)
                        {
                            if (PedList[iBe].DeathTime < Game.GameTime)
                            {
                                PedList[iBe].ThisPed.Alpha = 255;
                                PedList[iBe].DeathSequence += 1;
                                PedList[iBe].DeathTime = Game.GameTime + 500;
                            }
                        }
                        else if (PedList[iBe].DeathSequence == 8)
                        {
                            if (PedList[iBe].DeathTime < Game.GameTime)
                            {
                                if (PedList[iBe].Killed > RandInt(13, 22) || iAggression < 2)
                                {
                                    PedCleaning(PedList[iBe].Level, "left", false);
                                }
                                else if (PedList[iBe].Killed > 15 && PedList[iBe].Kills == 0 && iAggression > 7)
                                    FireOrb(PedList[iBe].Level, Game.Player.Character);
                                else
                                {
                                    ClearPedBlips(PedList[iBe].Level);
                                    PedList[iBe].DeathSequence = 10;
                                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, PedList[iBe].ThisPed.Handle);
                                    PedList[iBe].ThisPed.Delete();
                                    PedList[iBe].ThisPed = null;
                                    SearchPed(35.00f, 220.00f, null, 0, PedList[iBe].Level);
                                }
                            }
                        }
                    }
                    else if (PedList[iBe].ThisPed.IsInAir && !PedList[iBe].ThisPed.IsInVehicle())
                    {
                        if (PedList[iBe].Befallen)
                        {
                            if (PedList[iBe].DeathTime < Game.GameTime)
                                PedList[iBe].ThisPed.Kill();
                        }
                        else
                        {
                            PedList[iBe].Befallen = true;
                            PedList[iBe].DeathTime = Game.GameTime + 5000;
                        }
                    }
                    else if (PedList[iBe].Befallen)
                        PedList[iBe].Befallen = false;
                    else if (PedList[iBe].Hacker && !bHackEvent)
                    {
                        if (PedList[iBe].ThisPed.Position.DistanceTo(YoPoza()) < 40.00f)
                        {
                            bHackEvent = true;
                            HackerTime(PedList[iBe].ThisPed);
                        }
                    }
                    else if (PedList[iBe].SessionJumper)
                    {
                        if (PedList[iBe].ThisPed.Position.DistanceTo(YoPoza()) < 10.00f)
                            PedCleaning(PedList[iBe].Level, "has disappeared", true);
                    }
                    else if (PedList[iBe].AirAttack != 0)
                    {
                        if (!PedList[iBe].ThisPed.IsInVehicle())
                            PedList[iBe].AirAttack = 0;
                        else if (PedList[iBe].FindPlayer < Game.GameTime)
                        {
                            PedList[iBe].FindPlayer = Game.GameTime + 5000;
                            if (Game.Player.Character.IsInVehicle())
                            {
                                if (PedList[iBe].AirAttack == 1)
                                    FlyAway(PedList[iBe].ThisPed, YoPoza(), 250.00f, 0.00f);
                                else if (PedList[iBe].AirAttack == 2)
                                {
                                    int iFlight = 6;
                                    if (iAggression > 8)
                                        iFlight = 2;

                                    Function.Call(Hash.TASK_PLANE_MISSION, PedList[iBe].ThisPed.Handle, PedList[iBe].ThisVeh.Handle, Game.Player.Character.CurrentVehicle.Handle, 0, 0, 0, 0, iFlight, 0.0f, 0.0f, PedList[iBe].AirDirect, 1000.0f, -5000.0f);
                                }
                                else if (PedList[iBe].AirAttack == 3)
                                    Function.Call(Hash.TASK_PLANE_MISSION, PedList[iBe].ThisPed.Handle, PedList[iBe].ThisVeh.Handle, Game.Player.Character.CurrentVehicle.Handle, 0, 0, 0, 0, 6, 0.0f, 0.0f, PedList[iBe].AirDirect, 300.0f, -5000.0f);
                            }
                            else
                            {
                                if (PedList[iBe].AirAttack == 1)
                                    FlyAway(PedList[iBe].ThisPed, YoPoza(), 250.00f, 0.00f);
                                else if (PedList[iBe].AirAttack == 2)
                                {
                                    int iFlight = 6;
                                    if (iAggression > 8)
                                        iFlight = 2;

                                    Function.Call(Hash.TASK_PLANE_MISSION, PedList[iBe].ThisPed.Handle, PedList[iBe].ThisVeh.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, iFlight, 0.0f, 0.0f, PedList[iBe].AirDirect, 1000.0f, -5000.0f);
                                }
                                else if (PedList[iBe].AirAttack == 3)
                                    Function.Call(Hash.TASK_PLANE_MISSION, PedList[iBe].ThisPed.Handle, PedList[iBe].ThisVeh.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, PedList[iBe].AirDirect, 300.0f, -5000.0f);
                            }
                        }
                    }
                    else if (PedList[iBe].Driver)
                    {
                        if (PedList[iBe].ThisVeh != null)
                        {
                            if (PedList[iBe].ThisPed.IsInVehicle())
                            {

                                if (PedList[iBe].DirBlip != null)
                                {
                                    ClearPedBlips(PedList[iBe].Level);
                                    PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, OhMyBlip(PedList[iBe].ThisVeh), PedList[iBe].MyName, PedList[iBe].Colours);
                                }
                                else if (PedList[iBe].Follower)
                                {
                                    if (Game.Player.Character.IsInVehicle(PedList[iBe].ThisPed.CurrentVehicle))
                                    {
                                        if (Game.IsWaypointActive)
                                        {
                                            if (World.GetWaypointPosition() != LetsGoHere)
                                            {
                                                LetsGoHere = World.GetWaypointPosition();
                                                DriveToooDest(PedList[iBe].ThisPed, LetsGoHere);
                                            }
                                        }
                                    }
                                    else if (Game.Player.Character.IsInVehicle())
                                    {
                                        if (PedList[iBe].ThisBlip == null)
                                            PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 225, PedList[iBe].MyName, PedList[iBe].Colours);

                                        if (YoPoza().DistanceTo(PedList[iBe].ThisPed.Position) > 25.00f)
                                        {
                                            if (PedList[iBe].FindPlayer < Game.GameTime)
                                            {
                                                PedList[iBe].FindPlayer = Game.GameTime + 5000;
                                                DriveTooo(PedList[iBe].ThisPed, false);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        GetOutVehicle(PedList[iBe].ThisPed, PedList[iBe].Level);
                                        if (PedList[iBe].ThisVeh != null)
                                        {
                                            PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                            PedList[iBe].ThisVeh = null;
                                        }
                                        PedList[iBe].Passenger = false;
                                        PedList[iBe].Driver = false;
                                        OhDoKeepUp(PedList[iBe].ThisPed);
                                    }
                                }
                                else if (PedList[iBe].ApprochPlayer)
                                {
                                    if (iAggression < 9 && PedList[iBe].Friendly && iFollow < 7)
                                    {
                                        if (YoPoza().DistanceTo(PedList[iBe].ThisPed.Position) < 5.00f)
                                        {
                                            if (!PedList[iBe].Horny)
                                            {
                                                PedList[iBe].Horny = true;
                                                PedList[iBe].ThisVeh.SoundHorn(3000);
                                                TopLeftUI("Press" + ControlSybols(23) + "to enter vehicle");
                                            }
                                            else if (!Game.Player.Character.IsInVehicle())
                                            {
                                                if (ButtonDown(23, true))
                                                {
                                                    PedList[iBe].TimeOn = Game.GameTime + 600000;
                                                    PedList[iBe].ApprochPlayer = false;
                                                    PedList[iBe].Colours = 38;
                                                    PedList[iBe].Follower = true;
                                                    FolllowTheLeader(PedList[iBe].ThisPed);
                                                    iFollow += 1;
                                                    PlayerEnterVeh(PedList[iBe].ThisVeh);
                                                    DriveAround(PedList[iBe].ThisPed);
                                                }
                                            }
                                            else
                                            {
                                                PedList[iBe].ApprochPlayer = false;
                                                DriveAround(PedList[iBe].ThisPed);
                                            }
                                        }
                                        else if (YoPoza().DistanceTo(PedList[iBe].ThisPed.Position) > 25.00f)
                                        {
                                            if (PedList[iBe].FindPlayer < Game.GameTime)
                                            {
                                                PedList[iBe].FindPlayer = Game.GameTime + 5000;
                                                DriveTooo(PedList[iBe].ThisPed, false);
                                            }
                                        }
                                    }
                                    else if (iAggression > 8)
                                    {
                                        if (PedList[iBe].FindPlayer < Game.GameTime)
                                        {
                                            PedList[iBe].FindPlayer = Game.GameTime + 5000;
                                            DriveBye(PedList[iBe].ThisPed);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (PedList[iBe].Follower)
                                {
                                    if (PedList[iBe].ThisPed.IsInCombat)
                                    {
                                        if (PedList[iBe].DirBlip == null)
                                        {
                                            ClearPedBlips(PedList[iBe].Level);
                                            PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                            PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                        }
                                    }
                                    else
                                    {
                                        if (Game.Player.Character.IsInVehicle())
                                        {
                                            if (PedList[iBe].ThisVeh != null)
                                            {
                                                PedList[iBe].EnterVehQue = true;
                                                SearchSeats(PedList[iBe].ThisPed, PedList[iBe].ThisVeh, PedList[iBe].Level);
                                            }
                                            else
                                                PedList[iBe].Driver = false;
                                        }
                                        else
                                        {
                                            if (PedList[iBe].ThisVeh != null)
                                            {
                                                PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                                PedList[iBe].ThisVeh = null;
                                            }
                                            PedList[iBe].Driver = false;

                                            if (PedList[iBe].DirBlip == null)
                                            {
                                                ClearPedBlips(PedList[iBe].Level);
                                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    EmptyVeh(PedList[iBe].ThisVeh);
                                    PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                    PedList[iBe].ThisVeh = null;

                                    if (PedList[iBe].DirBlip == null)
                                    {
                                        ClearPedBlips(PedList[iBe].Level);
                                        PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                        PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                    }
                                }
                            }
                        }
                        else
                            PedList[iBe].Driver = false;
                    }
                    else if (PedList[iBe].Passenger)
                    {
                        if (PedList[iBe].Follower)
                        {
                            if (!Game.Player.Character.IsInVehicle())
                            {
                                PedList[iBe].Passenger = false;
                                ClearPedBlips(PedList[iBe].Level);
                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                PedList[iBe].ThisPed.Task.LeaveVehicle();
                                OhDoKeepUp(PedList[iBe].ThisPed);
                            }
                        }
                        else
                        {
                            if (!PedList[iBe].ThisPed.IsInVehicle())
                            {
                                PedList[iBe].Passenger = false;
                                ClearPedBlips(PedList[iBe].Level);
                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                            }
                            else if (Game.Player.Character.IsInVehicle(PedList[iBe].ThisPed.CurrentVehicle) && PedList[iBe].Friendly && iFollow < 7)
                            {
                                FolllowTheLeader(PedList[iBe].ThisPed);
                                PedList[iBe].Colours = 38;
                                PedList[iBe].Follower = true;
                                iFollow += 1;
                            }
                        }
                    }
                    else if (PedList[iBe].Follower)
                    {
                        if (Game.Player.Character.IsInVehicle())
                        {
                            PedList[iBe].Passenger = true;
                            Vehicle DisVeh = Game.Player.Character.CurrentVehicle;
                            SearchSeats(PedList[iBe].ThisPed, DisVeh, PedList[iBe].Level);
                            PedList[iBe].EnterVehQue = true;
                            PedList[iBe].TimeOn += 60000;
                        }
                        else if (YoPoza().DistanceTo(PedList[iBe].ThisPed.Position) > 150.00f)
                        {
                            PedList[iBe].ThisPed.Position = YoPoza() + (Game.Player.Character.RightVector * 2);
                            OhDoKeepUp(PedList[iBe].ThisPed);
                        }
                    }
                    else if (PedList[iBe].Friendly)
                    {
                        if (PedList[iBe].ThisPed.HasBeenDamagedBy(Game.Player.Character) || PedList[iBe].ThisPed.IsInCombatAgainst(Game.Player.Character) && iAggression > 2)
                        {
                            ClearPedBlips(PedList[iBe].Level);
                            PedList[iBe].Colours = 1;
                            if (iAggression < 5)
                                PedList[iBe].TimeOn = Game.GameTime + 120000;
                            else
                                PedList[iBe].TimeOn += 120000;

                            FightPlayer(PedList[iBe].ThisPed, false);
                            PedList[iBe].Friendly = false;
                            if (PedList[iBe].Follower)
                            {
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, PedList[iBe].ThisPed.Handle);
                                PedList[iBe].Follower = false;
                                iFollow -= 1;
                            }
                            PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                            PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                        }
                        else
                        {
                            if (YoPoza().DistanceTo(PedList[iBe].ThisPed.Position) < 7.00f && !PedList[iBe].ThisPed.IsInVehicle() && iFollow < 7)
                            {
                                if (Game.Player.Character.IsInVehicle())
                                {
                                    if (Game.Player.Character.SeatIndex == VehicleSeat.Driver)
                                    {
                                        if (!PedList[iBe].Horny2)
                                        {
                                            TopLeftUI("Press" + ControlSybols(86) + "to attract the players attention");
                                            PedList[iBe].Horny2 = true;
                                        }
                                        else if (ButtonDown(86, false))
                                        {
                                            if (iAggression < 9)
                                            {
                                                ClearPedBlips(PedList[iBe].Level);

                                                PedList[iBe].Colours = 38;
                                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                                FolllowTheLeader(PedList[iBe].ThisPed);
                                                OhDoKeepUp(PedList[iBe].ThisPed);
                                                PedList[iBe].TimeOn = Game.GameTime + 600000;
                                                iFollow += 1;

                                                PedList[iBe].Follower = true;
                                            }
                                            else
                                            {
                                                ClearPedBlips(PedList[iBe].Level);
                                                FightPlayer(PedList[iBe].ThisPed, false);
                                                PedList[iBe].Colours = 3;
                                                PedList[iBe].Friendly = false;
                                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!PedList[iBe].Horny2)
                                    {
                                        TopLeftUI("Press" + ControlSybols(46) + "to invte this player");
                                        PedList[iBe].Horny2 = true;
                                    }
                                    else if (ButtonDown(46, false))
                                    {
                                        if (iAggression < 9)
                                        {
                                            ClearPedBlips(PedList[iBe].Level);

                                            PedList[iBe].Colours = 38;
                                            PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                            PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                            FolllowTheLeader(PedList[iBe].ThisPed);
                                            OhDoKeepUp(PedList[iBe].ThisPed);
                                            PedList[iBe].TimeOn = Game.GameTime + 600000;
                                            iFollow += 1;

                                            PedList[iBe].Follower = true;
                                        }
                                        else
                                        {
                                            ClearPedBlips(PedList[iBe].Level);
                                            FightPlayer(PedList[iBe].ThisPed, false);
                                            PedList[iBe].Colours = 3;
                                            PedList[iBe].Friendly = false;
                                            PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                            PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!PedList[iBe].OffRadarBool && PedList[iBe].OffRadar == -1)
                        {

                            PedList[iBe].OffRadar = Game.GameTime + 300000;
                            ClearPedBlips(PedList[iBe].Level);
                            UI.Notify("~h~" + PedList[iBe].MyName + "~s~ has gone off radar");
                        }
                        else if (PedList[iBe].OffRadarBool)
                        {
                            if (PedList[iBe].OffRadar < Game.GameTime)
                            {
                                PedList[iBe].OffRadarBool = false;
                                ClearPedBlips(PedList[iBe].Level);
                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].MyName, PedList[iBe].Colours);
                            }
                        }
                        else if (PedList[iBe].ThisPed.Position.DistanceTo(YoPoza()) > 350.00f && iAggression > 6 && PedList[iBe].DeathSequence == 0)
                        {
                            if (PedList[iBe].ThisVeh == null)
                                AirAttack(PedList[iBe].Level);
                        }
                        else if (bPiggyBack)
                        {
                            if (PedList[iBe].ThisPed.IsInCombatAgainst(Game.Player.Character))
                            {
                                if (iOrbBurnOut < Game.GameTime)
                                {
                                    iOrbBurnOut = Game.GameTime + 25000;

                                    FireOrb(-1, PedList[iBe].ThisPed);
                                }
                            }
                        }
                    }

                    iNpcList += 1;
                }
                else
                    iNpcList = 0;

                if (ThisAFKer(iBlpList) != null)
                {
                    AfkPlayer HouseBlip = ThisAFKer(iBlpList);

                    if (HouseBlip.TimeOn < Game.GameTime)
                    {
                        DeListingBrains(false, iBlpList, true);
                        iCurrentPlayerz -= 1;
                    }
                    iBlpList += 1;
                }
                else
                    iBlpList = 0;

                if (BTimer(1))
                    NewPlayer();
            }
        }
        public Vector3 YoPoza()
        {
            Ped XPed = Game.Player.Character;
            return XPed.Position;
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (bLoadUp)
            {
                if (!Game.IsLoading)
                {
                    FindBrains();
                    bLoadUp = false;
                    LoadUp();
                }
            }
            else
                PlayerZerosAI();
            if (!bDisabled)
            {
                iRotateFind += 1;
                if (iRotateFind == 1)
                {
                    if (GetInQUe.Count > 0)
                    {
                        if (!GetInQUe[0].Peddy.IsInVehicle(GetInQUe[0].Vhic) && !GetInQUe[0].Peddy.IsDead && !GetInQUe[0].Peddy.IsFalling)
                        {
                            if (iFindingTime < Game.GameTime)
                                PedDoGetIn(GetInQUe[0]);
                        }
                        else
                        {
                            int iBPed = ReteaveBrain(GetInQUe[0].PedLevel);
                            PedList[iBPed].EnterVehQue = false;
                            if (PedList[iBPed].ThisBlip != null)
                                ClearPedBlips(GetInQUe[0].PedLevel);
                            GetInQUe.RemoveAt(0);
                        }
                    }
                }
                else if (iRotateFind == 2)
                {
                    if (MakeFrenz.Count > 0)
                    {
                        if (FindMe == null)
                        {
                            if (iFindingTime < Game.GameTime)
                                FindMe = GetPedPos(MakeFrenz[0].MinRadi, MakeFrenz[0].MaxRadi);
                        }
                        else
                        {
                            PedRelpace(FindMe, MakeFrenz[0]);
                            MakeFrenz.RemoveAt(0);
                            FindMe = null;
                        }
                    }
                }
                else
                {
                    if (MakeCarz.Count > 0)
                    {
                        if (FindMe == null)
                        {
                            if (iFindingTime < Game.GameTime)
                                FindMe = GetVehPos(MakeCarz[0].MinRadi, MakeCarz[0].MaxRadi);
                        }
                        else
                        {
                            VehRelpace(FindMe, MakeCarz[0]);
                            MakeCarz.RemoveAt(0);
                            FindMe = null;
                        }
                    }
                    iRotateFind = 0;
                }  
            }
        }
    }
}
