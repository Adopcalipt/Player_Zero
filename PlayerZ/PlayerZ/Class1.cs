using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Serialization;
using GTA;
using GTA.Native;
using GTA.Math;

namespace OnlinePlayerz
{
    public class Main : Script
    {
        private ScriptSettings GetKeys;

        bool bTrainM = true;//set the test then set to false
        bool bLoadUp = true;
        bool bNoSpawnKills = false;
        bool bPlayerList = false;
        bool bHeistPop = true;
        bool bSpaceWeaps = false;

        string Version = "1.0";
        string sBeeLogs = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZLog.txt";
        string sMemory = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZsMemory.xml";
        string sTheIni = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZSettings.ini";
        string sOutfitts = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/Outfits.xml";

        int iLogCount = 0;
        int iMaxPlayers = 29;
        int iNpcList = 0;
        int iBlpList = 0;
        int iCurrentPlayerz = 0;
        int iScale = 0;
        int iMinWait = 15000;
        int iMaxWait = 45000;
        int iMinSession = 30000;
        int iMaxSession = 475000;
        int iAggression = 5;
        int iGetlayList = 19;
        int iClearPlayList = 23;

        int PlayerGroups = Game.Player.Character.RelationshipGroup;
        int AttackingNPCs = World.AddRelationshipGroup("AttackNPCs");
        int FollowingNPCs = World.AddRelationshipGroup("FollowerNPCs");
        int WarringNPCs = World.AddRelationshipGroup("WarNPCs");

        BackUpBrain BlowenMyBrains = new BackUpBrain();

        List<bool> BeOnOff = new List<bool>();
        List<int> iTimers = new List<int>();
        List<string> sDebuggler = new List<string>();

        List<Vector3> AFKPlayers = new List<Vector3>();

        List<ImNotRandom> iRandList = new List<ImNotRandom>();
        List<PlayerBrain> PedList = new List<PlayerBrain>();
        List<AfkPlayer> AFKList = new List<AfkPlayer>();

        List<ClothBank> MaleCloth = new List<ClothBank>();
        List<ClothBank> FemaleCloth = new List<ClothBank>();

        public Main()
        {
            Tick += OnTick;
            Interval = 1;
        }
        private void LoadSettings()
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("LoadSettings", tEx);

            if (File.Exists(sTheIni))
            {
                GetKeys = ScriptSettings.Load(sTheIni);

                iAggression = GetKeys.GetValue<int>("Settings", "Aggression", 7);
                iMaxPlayers = GetKeys.GetValue<int>("Settings", "MaxPlayers", 29);
                iMinWait = GetKeys.GetValue<int>("Settings", "iMinWait", 15000);
                iMaxWait = GetKeys.GetValue<int>("Settings", "iMaxWait", 45000);
                iMinSession = GetKeys.GetValue<int>("Settings", "iMinSession", 30000);
                iMaxSession = GetKeys.GetValue<int>("Settings", "iMaxSession", 475000);
                bSpaceWeaps = GetKeys.GetValue<bool>("Settings", "SpaceWeaps", false);
                iGetlayList = GetKeys.GetValue<int>("Settings", "iGetlayList", 19);
                iClearPlayList = GetKeys.GetValue<int>("Settings", "iClearPlayList", 23);
            }

            if (iAggression > 10)
                iAggression = 10;
            else if (iAggression < 0)
                iAggression = 0;

            if (iMaxPlayers > 29)
                iMaxPlayers = 29;
            else if (iMaxPlayers < 5)
                iMaxPlayers = 5;

            if (iMinWait > iMaxWait)
                iMaxWait = iMinWait + 1;

            if (iMinSession > iMaxSession)
                iMaxSession = iMinSession + 1;

            if (iAggression < 6)
                bNoSpawnKills = true;
            else
                bNoSpawnKills = false;
        }
        private void BeeLog(string sLogs, TextWriter tEx)
        {
            iLogCount += 1;
            if (iLogCount > 25000)
            {
                iLogCount = 0;
                File.Delete(sBeeLogs);
                for (int i = 0; i < sDebuggler.Count; i++)
                    tEx.WriteLine(sDebuggler[i]);
                tEx.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} {"--" + sLogs}");
                sDebuggler.Clear();
            }
            else if (iLogCount > 24950)
            {
                tEx.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} {"--" + sLogs}");
                sDebuggler.Add($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} {"--" + sLogs}");
            }
            else
                tEx.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} {"--" + sLogs}");
        }
        private void LoadUp()
        {
            if (File.Exists(sBeeLogs))
                File.Delete(sBeeLogs);

            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("LoadUp", tEx);

            World.SetRelationshipBetweenGroups(Relationship.Hate, PlayerGroups, AttackingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, AttackingNPCs, PlayerGroups);
            World.SetRelationshipBetweenGroups(Relationship.Companion, PlayerGroups, FollowingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Companion, FollowingNPCs, PlayerGroups);
            World.SetRelationshipBetweenGroups(Relationship.Hate, WarringNPCs, FollowingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, FollowingNPCs, WarringNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, WarringNPCs, AttackingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, AttackingNPCs, WarringNPCs);


            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Game.Player.Character.Handle, FollowingNPCs);
            Function.Call(Hash.SET_GROUP_FORMATION, FollowingNPCs, 3);
            iTimers.Clear();
            SetBTimer(30000, -1);
            SetBTimer(RandInt(iMinWait, iMaxWait), -1);
            BuildList();
            FindMisfits();
        }
        private void SetBTimer(int AddTime, int iSetPos)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("SetBTimer, AddTime == " + AddTime + ", iSetPos == " + iSetPos, tEx);


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
        public bool BOnBOff(int iBee)
        {
            if (BeOnOff.Count == 0)
            {
                for (int i = 0; i < 25; i++)
                    BeOnOff.Add(false);
            }

            bool bAlt = false;

            bAlt = BeOnOff[iBee];
            BeOnOff[iBee] = !BeOnOff[iBee];

            bAlt = !bAlt;
            return bAlt;
        }
        public string SillyNameList()
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("SillyNameList ", tEx);

            string MySilly = "";

            List<string> sSillyNames = new List<string>();

            sSillyNames.Add("0");              //0
            sSillyNames.Add("1");              //1
            sSillyNames.Add("2");              //2
            sSillyNames.Add("3");              //3
            sSillyNames.Add("4");              //4
            sSillyNames.Add("5");              //5
            sSillyNames.Add("6");              //6
            sSillyNames.Add("7");              //7
            sSillyNames.Add("8");              //8
            sSillyNames.Add("9");              //9
            sSillyNames.Add("ay");             //10
            sSillyNames.Add("ee");             //11
            sSillyNames.Add("igh");            //12
            sSillyNames.Add("ow");             //13
            sSillyNames.Add("oo");             //14
            sSillyNames.Add("or");             //15
            sSillyNames.Add("air");            //16
            sSillyNames.Add("ir");             //17
            sSillyNames.Add("ou");             //18
            sSillyNames.Add("oy");             //19
            sSillyNames.Add("ai");             //20
            sSillyNames.Add("ea");             //21
            sSillyNames.Add("ie");             //22
            sSillyNames.Add("ew");             //23
            sSillyNames.Add("ur");             //24
            sSillyNames.Add("ow");             //25
            sSillyNames.Add("oi");             //26
            sSillyNames.Add("ire");            //27
            sSillyNames.Add("ear");            //28
            sSillyNames.Add("ure");            //29
            sSillyNames.Add("tion");           //30
            sSillyNames.Add("ey");             //31
            sSillyNames.Add("ore");            //32
            sSillyNames.Add("ere");            //33
            sSillyNames.Add("oor");            //34
            sSillyNames.Add("X");              //35
            sSillyNames.Add("-");              //36
            sSillyNames.Add("^");              //37
            sSillyNames.Add("*");              //38
            sSillyNames.Add("#");              //39
            sSillyNames.Add("$");              //40
            sSillyNames.Add("TyHrd");          //41
            sSillyNames.Add("Luzz");           //42
            sSillyNames.Add("Killz");          //43
            sSillyNames.Add("| | |");          //44
            sSillyNames.Add("{[]}");           //45
            sSillyNames.Add("A");              //46
            sSillyNames.Add("B");              //47
            sSillyNames.Add("C");              ///48
            sSillyNames.Add("D");              ///49
            sSillyNames.Add("E");              ///50
            sSillyNames.Add("F");              ///51
            sSillyNames.Add("G");              ///52
            sSillyNames.Add("H");              ///53
            sSillyNames.Add("I");              ///54
            sSillyNames.Add("J");              ///55
            sSillyNames.Add("K");              ///56
            sSillyNames.Add("L");              ///57
            sSillyNames.Add("M");              ///58
            sSillyNames.Add("N");              ///59
            sSillyNames.Add("O");              ///60
            sSillyNames.Add("P");              ///61

            int iName = LessRandomz(2, 3, 1);

            for (int i = 0; i < iName; i++)
                MySilly = MySilly + sSillyNames[LessRandomz(10, 34, 2)];

            MySilly.Remove(0, 1);
            MySilly = sSillyNames[LessRandomz(46, 61, 3)] + MySilly;

            if (MySilly.Length < 8)
            {
                if (LessRandomz(0, 20, 4) < 15)
                {
                    iName = LessRandomz(1, 4, 5);
                    for (int i = 0; i < iName; i++)
                        MySilly = MySilly + sSillyNames[LessRandomz(0, 9, 6)];
                }
                else
                {
                    string sPrefix1 = sSillyNames[LessRandomz(35, 40, 7)];
                    string sPrefix2 = sSillyNames[LessRandomz(35, 40, 8)];

                    MySilly = sPrefix1 + sPrefix2 + MySilly + sPrefix2 + sPrefix1;
                }
            }
            else if (MySilly.Length < 4)
                MySilly = MySilly + sSillyNames[LessRandomz(41, 45, 9)];

            return MySilly;
        }
        private void InABuilding()
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("InABuilding", tEx);

            List<int> iKeepItReal = new List<int>();

            for (int i = 0; i < AFKList.Count(); i++)
                iKeepItReal.Add(AFKList[i].iApp);

            int iMit = RandInt(0, AFKPlayers.Count - 1);

            while (iKeepItReal.Contains(iMit))
                iMit = RandInt(0, AFKPlayers.Count - 1);

            string sName = SillyNameList();
            Blip FakeB = LocalBlip(AFKPlayers[iMit], 417, sName);

            AfkPlayer MyAfk = new AfkPlayer();
            MyAfk.ThisBlip = FakeB;
            MyAfk.iApp = iMit;
            MyAfk.iLevel = LessRandomz(1, 400, 10);
            MyAfk.iTimeOn = Game.GameTime + RandInt(iMinSession, iMaxSession);
            MyAfk.sMyName = sName;
            AFKList.Add(MyAfk);

            BackItUpBrain();

            iCurrentPlayerz += 1;
        }
        private void BuildList()
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("BuildList", tEx);

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
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("PedCleaning, iPed == " + iPed, tEx);

            UI.Notify("~h~" + PedList[iPed].sMyName + "~s~ " + sOff);
            DeListingBrains(true, iPed, bGone);
            iCurrentPlayerz -= 1;
        }
        private void DeListingBrains(bool bPed, int iPos, bool bGone)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("DeListingBrains, bPed == " + bPed + ", iPos == " + iPos, tEx);

            if (bPed)
            {
                PedList[iPos].ThisBlip.Remove();
                if (PedList[iPos].DirBlip != null)
                    PedList[iPos].DirBlip.Remove();

                if (PedList[iPos].ThisVeh != null)
                    PedList[iPos].ThisVeh.MarkAsNoLongerNeeded();

                if (bGone)
                    PedList[iPos].ThisPed.Delete();
                else
                    PedList[iPos].ThisPed.MarkAsNoLongerNeeded();
                PedList.RemoveAt(iPos);
            }
            else
            {
                AFKList[iPos].ThisBlip.Remove();
                UI.Notify("~h~" + AFKList[iPos].sMyName + "~s~ left");
                AFKList.RemoveAt(iPos);
            }

            BackItUpBrain();
        }
        public int LessRandomz(int iMin, int iMax, int iList)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("LessRandomz, iList == " + iList, tEx);

            while (iRandList.Count() < iList + 1)
            {
                ImNotRandom iBe = new ImNotRandom();
                iBe.iRando.Add(-1);
                iRandList.Add(iBe);
            }

            if (iRandList[iList].iRando.Count() == 0)
            {
                iRandList[iList].iRando.Clear();
                for (int i = iMin; i < iMax + 1; i++)
                    iRandList[iList].iRando.Add(i);
            }
            else if (iRandList[iList].iRando[0] == -1)
            {
                iRandList[iList].iRando.Clear();
                for (int i = iMin; i < iMax + 1; i++)
                    iRandList[iList].iRando.Add(i);
            }

            int iRando = 0;
            int iRemove = RandInt(0, iRandList[iList].iRando.Count() - 1);
            iRando = iRandList[iList].iRando[iRemove];
            iRandList[iList].iRando.RemoveAt(iRemove);

            return iRando;
        }
        public class ImNotRandom
        {
            public List<int> iRando { get; set; }

            public ImNotRandom()
            {
                iRando = new List<int>();
            }
        }
        private void NewPlayer()
        {
            SetBTimer(RandInt(iMinWait, iMaxWait), 1);
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("NewPlayer, iCurrentPlayerz == " + iCurrentPlayerz, tEx);

            LoadSettings();

            if (iCurrentPlayerz < iMaxPlayers)
            {
                if (BOnBOff(0))
                {
                    if (iAggression < 3)
                    {
                        int iPedMode = LessRandomz(1, 7, 0);

                        if (iPedMode == 3 || iPedMode == 6)
                            iPedMode = 4;

                        if (iPedMode < 5)
                            FindAPed(Game.Player.Character.Position, 95.00f, iPedMode, true, true);
                        else
                        {
                            int iTypeO = LessRandomz(1, 9, 12);
                            FindNearestVeh(Game.Player.Character.Position, RandVeh(iTypeO), 95.00f, iPedMode, true, iTypeO);
                        }
                    }
                    else if (iAggression < 6)
                    {
                        int iPedMode = LessRandomz(1, 7, 0);
                        if (iPedMode < 5)
                            FindAPed(Game.Player.Character.Position, 95.00f, iPedMode, true, true);
                        else
                        {
                            int iTypeO = 1;
                            if (iPedMode == 6)
                                iTypeO = LessRandomz(1, 9, 12);
                            else
                                iTypeO = LessRandomz(10, 11, 13);
                            FindNearestVeh(Game.Player.Character.Position, RandVeh(iTypeO), 95.00f, iPedMode, true, iTypeO);
                        }
                    }
                    else
                    {
                        int iPedMode = LessRandomz(1, 7, 0);

                        if (iPedMode == 4 || iPedMode == 2)
                            iPedMode = 6;

                        if (iPedMode < 5)
                            FindAPed(Game.Player.Character.Position, 95.00f, iPedMode, true, true);
                        else
                        {
                            int iTypeO = LessRandomz(10, 11, 13);
                            FindNearestVeh(Game.Player.Character.Position, RandVeh(iTypeO), 95.00f, iPedMode, true, iTypeO);
                        }
                    }
                }
                else
                    InABuilding();

                if (bHeistPop)
                {
                    if (iCurrentPlayerz + 3 < iMaxPlayers)
                        NearHiest(true);
                }
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
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("BeInAngle, fRange == " + fRange + ", fValue_01 == " + fValue_01 + ", fValue_02 == " + fValue_02, tEx);

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
        public Vehicle LookNear(Vector3 Vec3, string sModel, float fRadi, int iBrain, bool bBlip)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("LookNear, sModel == " + sModel + ", fRadi" + fRadi + ", iBrain == " + iBrain + ", bBlip == " + bBlip, tEx);

            Vehicle Vickary = null;

            if (World.GetNextPositionOnStreet(Game.Player.Character.Position).DistanceTo(Game.Player.Character.Position) < 95.00f)
            {
                Vehicle[] CarSpot = World.GetNearbyVehicles(Vec3, fRadi);
                for (int i = 0; i < CarSpot.Count(); i++)
                {
                    if (VehExists(CarSpot, i) && Vickary == null)
                    {
                        Vehicle Veh = CarSpot[i];
                        if (Veh.IsPersistent == false && Veh.Position.DistanceTo(Game.Player.Character.Position) > 10.00f && Veh.ClassType != VehicleClass.Boats && Veh.ClassType != VehicleClass.Cycles && Veh.ClassType != VehicleClass.Helicopters && Veh.ClassType != VehicleClass.Planes && Veh.ClassType != VehicleClass.Trains && Veh != Game.Player.Character.CurrentVehicle && !Veh.IsOnScreen)
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
        private void FindNearestVeh(Vector3 Vec3, string sModel, float fRadi, int iBrain, bool bBlip, int iType)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("FindNearestVeh, sModel == " + sModel + ", fRadi" + fRadi + ", iBrain == " + iBrain + ", bBlip == " + bBlip, tEx);

            Vehicle VickPos = LookNear(Vec3, sModel, fRadi, iBrain, bBlip);

            while (VickPos == null)
            {
                PlayerZerosAI(false);

                Script.Wait(1000);
                VickPos = LookNear(Game.Player.Character.Position, sModel, 150.00f, iBrain, bBlip);
            }
            Vector3 PedPos = VickPos.Position;
            float PedRot = VickPos.Heading;
            VickPos.Delete();

            VehicleSpawn(sModel, PedPos, PedRot, bBlip, iBrain, iType);
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
            bool bIsReal = false;

            int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehName);
            if (Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
                bIsReal = true;

            return bIsReal;
        }
        public string RandVeh(int iVechList)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("RandVeh, iVechList == " + iVechList, tEx);

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
                }
            }       //Super
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
            }       //Attack Planes
            else if (iVechList == 13)
            {
                sVehicles.Add("BUZZARD"); //<!-- Buzzard Attack Chopper -->
                sVehicles.Add("HUNTER"); //<!-- FH-1 Hunter -->
                sVehicles.Add("SAVAGE"); //
            }       //Attack Helicopters
            else
            {
                sVehicles.Add("BJXL"); //
            }

            if (sVehicles.Count == 0)
                sVehicles.Add("BJXL");

            return sVehicles[RandInt(0, sVehicles.Count - 1)];
        }
        public Vehicle VehicleSpawn(string sVehModel, Vector3 VecLocal, float VecHead, bool bBlip, int iBrain,int iVehType)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("VehicleSpawn, sVehModel == " + sVehModel + ", bBlip == " + bBlip + ", iBrain == " + iBrain, tEx);

            Vehicle BuildVehicle = null;

            if (sVehModel == "" || !IsItARealVehicle(sVehModel))
                sVehModel = RandVeh(iVehType);

            int iVehHash = Function.Call<int>(Hash.GET_HASH_KEY, sVehModel);

            if (Function.Call<bool>(Hash.IS_MODEL_IN_CDIMAGE, iVehHash) && Function.Call<bool>(Hash.IS_MODEL_A_VEHICLE, iVehHash))
            {
                Function.Call(Hash.REQUEST_MODEL, iVehHash);
                while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, iVehHash))
                    Wait(1);

                BuildVehicle = Function.Call<Vehicle>(Hash.CREATE_VEHICLE, iVehHash, VecLocal.X, VecLocal.Y, VecLocal.Z, VecHead, true, true);

                BuildVehicle.IsPersistent = true;

                if (iBrain > 0)
                    GenPlayerPed(BuildVehicle.Position, BuildVehicle.Heading, iBrain, BuildVehicle, -1);

                Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, iVehHash);
            }
            else
                BuildVehicle = null;

            return BuildVehicle;
        }
        public Vector3 FindAPed(Vector3 vZone, float fRadius, int iBrain, bool bBlip, bool bReplace)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("FindAPed", tEx);

            Ped VPedPos = DoStopTillDrop(vZone, fRadius, iBrain, bBlip, bReplace);

            while (VPedPos == null)
            {
                PlayerZerosAI(false);

                Script.Wait(1000);
                VPedPos = DoStopTillDrop(Game.Player.Character.Position, 150.00f, iBrain, bBlip, bReplace);
            }
            Vector3 PedPos = VPedPos.Position;
            float PedRot = VPedPos.Heading;
            VPedPos.Delete();
            if (bReplace)
                GenPlayerPed(PedPos, PedRot, iBrain, null, 0);

            return PedPos;
        }
        public Ped DoStopTillDrop(Vector3 vZone, float fRadius, int iBrain, bool bBlip, bool bReplace)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("DoStopTillDrop, iBrain == " + iBrain, tEx);

            Ped RandPed = null;
            Ped[] MadPeds = World.GetNearbyPeds(vZone, fRadius);

            for (int i = 0; i < MadPeds.Count(); i++)
            {
                if (PedExists(MadPeds, i) && RandPed == null)
                {
                    if (!MadPeds[i].IsOnScreen && !MadPeds[i].IsInVehicle() && Function.Call<int>(Hash.GET_PED_TYPE, MadPeds[i]) != 28 && MadPeds[i] != Game.Player.Character && !MadPeds[i].IsPersistent)
                    {
                        RandPed = MadPeds[i];
                        break;
                    }
                }
            }
            return RandPed;
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
        public Ped GenPlayerPed(Vector3 vLocal, float fAce, int iBrain, Vehicle vMyCar, int iSeat)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("GenPlayerPed, iBrain == " + iBrain, tEx);

            Ped MyPed = null;

            bool bMale = false;
            string sPeddy = "mp_f_freemode_01";

            if (BOnBOff(1))
            {
                bMale = true;
                sPeddy = "mp_m_freemode_01";
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
                    MyPed.Accuracy = RandInt(25, 75);
                    MyPed.MaxHealth = RandInt(200, 400);
                    MyPed.Health = MyPed.MaxHealth;

                    if (vMyCar != null)
                        WarptoAnyVeh(vMyCar, MyPed, iSeat);

                    GunningIt(MyPed);

                    NpcBrains(MyPed, iBrain);
                    OnlineFaceTypes(MyPed, bMale);
                    OnlineWardrobe(MyPed, bMale);
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
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("WarptoAnyVeh, iSeat == " + iSeat, tEx);

            while (!Peddy.IsInVehicle(Vhic))
            {
                Function.Call(Hash.SET_PED_INTO_VEHICLE, Peddy, Vhic, iSeat);
                Script.Wait(100);
            }
        }
        private void EnterAnyVeh(Vehicle Vhic, Ped Peddy, float Run)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("EnterAnyVeh, Run == " + Run, tEx);

            bool bFound = false;
            if (Vhic.Exists())
            {
                int iSeats = 0;
                while (!bFound && iSeats < Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Vhic.Handle))
                {
                    if (Function.Call<bool>(Hash.IS_VEHICLE_SEAT_FREE, Vhic.Handle, iSeats))
                        bFound = true;
                    else
                        iSeats += 1;
                }
                if (bFound)
                {
                    Function.Call(Hash.TASK_ENTER_VEHICLE, Peddy, Vhic, -1, iSeats, Run, 1, 0);
                    Peddy.BlockPermanentEvents = true;
                    Peddy.AlwaysKeepTask = true;
                }
            }
        }
        private void GetOutVehicle(Ped Peddy)
        {
            if (Peddy.IsInVehicle())
            {
                Vehicle PedVeh = Peddy.CurrentVehicle;
                Function.Call(Hash.TASK_LEAVE_VEHICLE, Peddy, PedVeh, 4160);
            }
        }
        private void FolllowTheLeader(Ped Peddy)
        {
            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, FollowingNPCs);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, Peddy.Handle, true);
            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, Peddy.Handle, Game.Player.Character.Handle, 0.0f, 3.0f, 0.0f, 1.0f, -1, 0.5f, true);
            Function.Call(Hash.SET_PED_CAN_TELEPORT_TO_GROUP_LEADER, Peddy.Handle, FollowingNPCs, true);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.BlockPermanentEvents = true;
            Peddy.CanBeTargetted = true;
        }
        private void ExMember(Ped Peddy)
        {
            if (Function.Call<bool>(Hash.IS_PED_GROUP_MEMBER, Peddy.Handle, FollowingNPCs))
                Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
        }
        private void DriveBye(Ped Peddy)
        {
            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    if (Game.Player.Character.IsInVehicle())
                        Peddy.Task.VehicleChase(Game.Player.Character);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, Game.Player.Character.Position, 10.00f, 45.00f, 0);

                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
                else
                    Peddy.Task.VehicleShootAtPed(Game.Player.Character);
            }
        }
        private void DriveAround(Ped Peddy)
        {
            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    Peddy.Task.CruiseWithVehicle(Peddy.CurrentVehicle, 85.00f, 0);
                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
            }
        }
        private void DriveTooo(Ped Peddy, bool bRunOver)
        {
            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {

                    if (bRunOver)
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, Game.Player.Character.Position, 1.00f, 45.00f, 0);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, Game.Player.Character.Position, 10.00f, 25.00f, 0);

                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
            }
        }      
        private void FightPlayer(Ped Peddy, bool bInVeh)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("FightPlayer ", tEx);

            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy.Handle);
            Peddy.IsEnemy = true;
            Peddy.CanBeTargette﻿d﻿ = true;
            Peddy.RelationshipGroup = AttackingNPCs;
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            if (!bInVeh)
                Peddy.Task.FightAgainst(Game.Player.Character);
            else
                DriveBye(Peddy);
        }
        private void AirAttack(int iPed)
        {
            Ped Peddy = PedList[iPed].ThisPed;
            if (RandInt(0, 50) < 25)
                HeliFighter(Peddy, iPed);
            else
                LaserFighter(Peddy, iPed);

        }
        private void HeliFighter(Ped Peddy,int iPed)
        {
            Vehicle vHeli = VehicleSpawn(RandVeh(13), new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 250.00f), 0.00f, false, 0, 13);
            WarptoAnyVeh(vHeli, Peddy, -1);
            PedList[iPed].ThisVeh = vHeli;
            FlyAway(Peddy, Game.Player.Character.Position, 250.00f, 0.00f);
            Function.Call(Hash.SET_PED_FIRING_PATTERN, Peddy.Handle, Function.Call<int>(Hash.GET_HASH_KEY, "FIRING_PATTERN_BURST_FIRE_HELI"));
        }
        private void LaserFighter(Ped Peddy, int iPed)
        {
            Vehicle vPlane = VehicleSpawn(RandVeh(12), new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 1550.00f), 0.00f, false, 0, 12);
            WarptoAnyVeh(vPlane, Peddy, -1);
            PedList[iPed].ThisVeh = vPlane;
            Function.Call(Hash.TASK_PLANE_MISSION, Peddy, vPlane, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, 180.0f, 1000.0f, -5000.0f);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy, 0, true);
            Peddy.AlwaysKeepTask = true;
            Peddy.BlockPermanentEvents = true;
        }
        private void FlyAway(Ped Pedd, Vector3 vHeliDest, float fSpeed, float flanding)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("FlyAway", tEx);

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
            //Pedd.AlwaysKeepTask = true;
            //Pedd.BlockPermanentEvents = true;
        }
        private void CombatMoves(Ped Peddy)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("CombatMoves ", tEx);

            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, WarringNPCs);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, Peddy.Handle, true);
        }//---Addsomething here
        private void HotPersute(Ped Peddy)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("HotPersute ", tEx);

            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy);

        }
        public int NearHiest(bool bLaunch)
        {
            int iNear = -1;
            List<Vector3> VectorList = new List<Vector3>();
            VectorList.Add(new Vector3(-1105.577f, -1692.11f, 4.345489f));      //0
            VectorList.Add(new Vector3(60.53763f, 8.939384f, 69.14648f));       //4
            VectorList.Add(new Vector3(718.9022f, -980.336f, 24.12285f));       //8
            VectorList.Add(new Vector3(1681.823f, 4817.896f, 42.01214f));       //12
            VectorList.Add(new Vector3(-1038.972f, -2736.403f, 20.16928f));     //16

            for (int i = 0; i < VectorList.Count; i++)
            {
                if (VectorList[i].DistanceTo(Game.Player.Character.Position) < 55.00f)
                {
                    iNear = i;
                    break;
                }
            }

            if (bLaunch && iNear != -1)
                HeistDrips(iNear);

            return iNear;
        }
        private void HeistDrips(int iMyArea)
        {
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
                GenPlayerPed(VectorList[i], 90.00f, 1, null, 0);

            Script.Wait(1200);
            World.AddExplosion(VectorList[0], ExplosionType.Grenade, 7.00f, 15.00f, true, false);
            bHeistPop = false;
        }
        public Blip DirectionalBlimp(Ped pEdd)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("PedBlimp ", tEx);

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, 399);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);
            Function.Call(Hash.SET_BLIP_PRIORITY, MyBlip.Handle, 1);
            Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, 58);
            Function.Call(Hash.SET_BLIP_DISPLAY, MyBlip.Handle, 8);

            return MyBlip;
        }
        public Blip PedBlimp(Ped pEdd, int iBlippy, string sName, bool bFriend)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("PedBlimp ", tEx);

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

            if (!bFriend)
                Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, 1);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME,"STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING,"Player " + sName);
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, "Player " + sName);
            Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);

            return MyBlip; 


            //street;
            //163_radar_passive
            //164_radar_usingmenu
            //303_radar_bounty_hit    
        }
        public Blip LocalBlip(Vector3 Vlocal, int iBlippy, string sName)
        {
            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, Vlocal.X, Vlocal.Y, Vlocal.Z);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, "Player " + sName);
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, "Player " + sName);
            Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);

            return MyBlip;
        }
        private void BlipDirect(Blip Blippy, float fDir)
        {
            int iHead = (int)fDir;
            Function.Call(Hash.SET_BLIP_ROTATION, Blippy.Handle, iHead);
        }
        private void GunningIt(Ped Peddy)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("GunningIt", tEx);

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
        private void NpcBrains(Ped Peddy, int iBrain)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("NpcBrains, iBrain == " + iBrain, tEx);

            PlayerBrain MyBrains = new PlayerBrain();

            MyBrains.ThisPed = Peddy;
            MyBrains.iBrain = iBrain;
            MyBrains.iDeathSequence = 0;
            MyBrains.iDeathTime = 0;
            MyBrains.iTimeOn = Game.GameTime + RandInt(iMinSession, iMaxSession);
            MyBrains.sMyName = SillyNameList();
            MyBrains.iLevel = LessRandomz(1, 400, 11);
            MyBrains.iKilled = 0;
            MyBrains.iKills = 0;
            MyBrains.iFindPlayer = 0;
            MyBrains.bInCombat = false;
            MyBrains.bBounty = false;
            MyBrains.bHorny = false;
            MyBrains.bHorny2 = true;
            MyBrains.bFollower = false;
            MyBrains.bOverFriendly = false;
            MyBrains.DirBlip = null;
            MyBrains.ThisVeh = null;

            Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, Peddy.Handle, true);
            Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, Peddy.Handle, 2);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 0, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 1, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 2, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 3, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 52, true);

            if (iBrain == 1)
            {
                if (iAggression < 7)
                {
                    Peddy.Task.WanderAround();
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.sMyName, true);
                    MyBrains.bFriendly = true;
                    MyBrains.bSessionJumper = false;
                }
                else
                {
                    FightPlayer(Peddy, false);
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.sMyName, true);
                    MyBrains.bFriendly = false;
                    MyBrains.bSessionJumper = false;
                    MyBrains.iLevel = 420;
                }
            }       //Friendly or Not
            else if (iBrain == 2)
            {
                Peddy.Task.WanderAround();
                MyBrains.DirBlip = DirectionalBlimp(Peddy);
                MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.sMyName, true);
                MyBrains.bFriendly = true;
                MyBrains.bSessionJumper = false;
            }       //Friendly
            else if (iBrain == 3)
            {
                FightPlayer(Peddy, false);
                MyBrains.ThisBlip = PedBlimp(Peddy, 303, MyBrains.sMyName, false);
                MyBrains.bBounty = true;
                MyBrains.bFriendly = false;
                MyBrains.bSessionJumper = false;
                MyBrains.iLevel = 9999;
            }       //Chaos
            else if (iBrain == 4)
            {
                Peddy.Task.WanderAround();
                MyBrains.DirBlip = DirectionalBlimp(Peddy);
                MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.sMyName, true);
                MyBrains.bFriendly = true;
                MyBrains.bSessionJumper = true;
            }       //Gets close disconects
            else if (iBrain == 5)
            {
                DriveTooo(Peddy, false);
                MyBrains.ThisBlip = PedBlimp(Peddy, 225, MyBrains.sMyName, true);
                MyBrains.bFriendly = true;
                MyBrains.bHorny = true;
                MyBrains.bSessionJumper = false;
                MyBrains.bOverFriendly = true;
                if (MyBrains.ThisPed.IsInVehicle())
                    MyBrains.ThisVeh = MyBrains.ThisPed.CurrentVehicle;
                MyBrains.ThisPed.CanBeDraggedOutOfVehicle = false;
                //build car show locals
            }       //CarShow
            else if (iBrain == 6)
            {
                CombatMoves(Peddy);
                FightPlayer(Peddy, true);
                Peddy.Task.FightAgainst(Game.Player.Character);
                Peddy.AlwaysKeepTask = true;
                MyBrains.ThisBlip = PedBlimp(Peddy, 303, MyBrains.sMyName, false);
                MyBrains.bBounty = true;
                MyBrains.bFriendly = false;
                MyBrains.bSessionJumper = false;
                if (MyBrains.ThisPed.IsInVehicle())
                {
                    MyBrains.ThisVeh = MyBrains.ThisPed.CurrentVehicle;
                    Function.Call(Hash.SET_VEHICLE_IS_WANTED, MyBrains.ThisPed.CurrentVehicle, true);
                    DriveTooo(Peddy, true);
                }
            }       //CarAttack
            else
            {
                DriveAround(Peddy);
                MyBrains.ThisBlip = PedBlimp(Peddy, 225, MyBrains.sMyName, true);
                MyBrains.bFriendly = true;
                MyBrains.bSessionJumper = true;
                if (MyBrains.ThisPed.IsInVehicle())
                    MyBrains.ThisVeh = MyBrains.ThisPed.CurrentVehicle;
            }       //DriveAwayFrom


            PedList.Add(MyBrains);

            BackItUpBrain();

            iCurrentPlayerz += 1;
        }
        public class PlayerBrain
        {
            public Ped ThisPed { get; set; }
            public Vehicle ThisVeh { get; set; }
            public Blip ThisBlip { get; set; }
            public Blip DirBlip { get; set; }
            public int iDeathSequence { get; set; }
            public int iDeathTime { get; set; }
            public int iBrain { get; set; }
            public int iTimeOn { get; set; }
            public int iLevel { get; set; }
            public int iKills { get; set; }
            public int iKilled { get; set; }
            public bool bBounty { get; set; }
            public bool bInCombat { get; set; }
            public bool bFriendly { get; set; }
            public bool bFollower { get; set; }
            public bool bOverFriendly { get; set; }
            public bool bSessionJumper { get; set; }
            public bool bHorny { get; set; }
            public bool bHorny2 { get; set; }
            public int iFindPlayer { get; set; }
            public string sMyName { get; set; }
        }
        public class AfkPlayer
        {
            public Blip ThisBlip { get; set; }
            public int iTimeOn { get; set; }
            public int iApp { get; set; }
            public string sMyName { get; set; }
            public int iLevel { get; set; }
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
            public List<int> BigBlip { get; set; }
            public List<int> BigDirBlip { get; set; }
            public List<int> BigAFKBlip { get; set; }

            public BackUpBrain()
            {
                BigBrain = new List<int>();
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
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("BackItUpBrain", tEx);

            BlowenMyBrains.BigBrain.Clear();
            BlowenMyBrains.BigBlip.Clear();
            BlowenMyBrains.BigDirBlip.Clear();
            BlowenMyBrains.BigAFKBlip.Clear();

            for (int i = 0; i < PedList.Count; i++)
            {
                BlowenMyBrains.BigBrain.Add(PedList[i].ThisPed.Handle);
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
            if (File.Exists(sMemory))
            {
                BlowenMyBrains = LoadPlayerBrain(sMemory);

                for (int i = 0; i < BlowenMyBrains.BigBrain.Count; i++)
                    FlushBrains(BlowenMyBrains.BigBrain[i] , true);

                for (int i = 0; i < BlowenMyBrains.BigBlip.Count; i++)
                    FlushBrains(BlowenMyBrains.BigBlip[i], false);

                for (int i = 0; i < BlowenMyBrains.BigDirBlip.Count; i++)
                    FlushBrains(BlowenMyBrains.BigDirBlip[i], false);

                for (int i = 0; i < BlowenMyBrains.BigAFKBlip.Count; i++)
                    FlushBrains(BlowenMyBrains.BigAFKBlip[i], false);

                using (StreamWriter tEx = File.AppendText(sBeeLogs))
                    BeeLog("FoundOldXML", tEx);
            }
        }
        private void FlushBrains(int iHandles, bool bBrain)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("FlushBrains", tEx);

            unsafe
            {
                if (bBrain)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, iHandles))
                    {
                        Ped Peed = new Ped(iHandles);
                        Peed.CurrentBlip.Remove();
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
            for (int i = 0; i < PedList.Count; i++)
            {
                GetOutVehicle(PedList[i].ThisPed);
                PedCleaning(i, "left", false);
            }

            for (int i = 0; i < AFKList.Count; i++)
            {
                DeListingBrains(false, i, true);
                iCurrentPlayerz -= 1;
            }
        }
        public int WhoShotMe(Ped MeDie)
        {
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
        private void OnlineFaceTypes(Ped Pedx, bool bMale)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("OnlineFaceTypes", tEx);

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

            if (bMale)
            {
                shapeFirstID = RandInt(0, 20);
                shapeSecondID = RandInt(0, 20);
                shapeThirdID = shapeFirstID;
                skinFirstID = shapeFirstID;
                skinSecondID = shapeSecondID;
                skinThirdID = shapeThirdID;
            }
            else
            {
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

            Function.Call(Hash.SET_PED_HEAD_BLEND_DATA, Pedx.Handle, shapeFirstID, shapeSecondID, shapeThirdID, skinFirstID, skinSecondID, skinThirdID, shapeMix, skinMix, thirdMix, isParent);

            int iFeature = 12;
            while (iFeature > 0)
            {
                iFeature = iFeature - 1;
                int iColour = 0;
                int iChange = RandInt(0, Function.Call<int>(Hash._GET_NUM_HEAD_OVERLAY_VALUES, iFeature));
                float fVar = RandFlow(0.45f, 0.99f);
                if (iFeature == 0)//Blemishes
                {
                    iChange = RandInt(0, iChange);
                }
                else if (iFeature == 1)//Facial Hair
                {
                    if (bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }
                else if (iFeature == 2)//Eyebrows
                {
                    iChange = RandInt(0, iChange);
                    iColour = 1;
                }
                else if (iFeature == 3)//Ageing
                {
                    iChange = 255;
                }
                else if (iFeature == 4)//Makeup
                {
                    if (RandInt(0, 50) < 40)
                    {
                        iChange = RandInt(0, iChange);
                    }
                    else
                        iChange = 255;
                }
                else if (iFeature == 5)//Blush
                {
                    if (!bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 2;
                }
                else if (iFeature == 6)//Complexion
                {
                    iChange = RandInt(0, iChange);
                }
                else if (iFeature == 7)//Sun Damage
                {
                    iChange = 255;
                }
                else if (iFeature == 8)//Lipstick
                {
                    if (!bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 2;
                }
                else if (iFeature == 9)//Moles/Freckles
                {
                    iChange = RandInt(0, iChange);
                }
                else if (iFeature == 10)//Chest Hair
                {
                    if (bMale)
                        iChange = RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }
                else if (iFeature == 11)//Body Blemishes
                {
                    iChange = RandInt(0, iChange);
                }
                Function.Call(Hash.SET_PED_HEAD_OVERLAY, Pedx.Handle, iFeature, iChange, fVar);

                if (iColour > 0)
                    Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, Pedx.Handle, iFeature, iColour, RandInt(0, 64), 0);

                int iCount = Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 2);
                int iAm = RandInt(1, iCount);
                while (iAm == 24)
                    iAm = RandInt(1, iCount);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, iAm, 0, 2);//hair
            }
            int iHair = RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));
            int iHair2 = RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));
            Function.Call(Hash._SET_PED_HAIR_COLOR, Pedx.Handle, iHair, iHair2);
        }
        private void OnlineWardrobe(Ped Pedx, bool bMale)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("OnlineWardrobe", tEx);

            if (bMale)
            {
                if (MaleCloth.Count > 0)
                {
                    Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx);
                    ClothBank MyWard = MaleCloth[RandInt(0, MaleCloth.Count - 1)];
                    OnlineSavedWard(Pedx, MyWard);
                }
                else
                {
                    int RanChar = RandInt(1, 6);
                    if (RanChar == 1)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 0, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 12, 4, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 1, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 1, 5, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 65, 3, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, 22, 4, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 11, 0, 2);//Top2
                    }
                    else if (RanChar == 2)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 0, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 14, 3, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 0, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 17, 0, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 23, 4, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 40, 1, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, 26, 3, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 35, 0, 2);//Top2
                    }
                    else if (RanChar == 3)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 147, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 0, 0, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 167, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 33, 0, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 36, 1, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 289, 0, 2);//Top2
                    }
                    else if (RanChar == 4)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 0, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 11, 4, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 19, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 88, 7, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 14, 2, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 273, 15, 2);//Top2
                    }
                    else if (RanChar == 5)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 125, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 0, 0, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, -1, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 114, 6, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 78, 6, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 287, 6, 2);//Top2
                    }
                    else
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 134, 8, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 0, 0, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 3, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 106, 8, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 83, 8, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 274, 8, 2);//Top2
                    }
                }
            }
            else
            {
                if (FemaleCloth.Count > 0)
                {
                    ClothBank MyWard = FemaleCloth[RandInt(0, FemaleCloth.Count - 1)];
                    OnlineSavedWard(Pedx, MyWard);
                }
                else
                {
                    int RanChar = RandInt(1, 5);
                    if (RanChar == 1)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 146, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 0, 0, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, -1, 1, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 113, 1, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 23, 8, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, 0, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 287, 1, 2);//Top2
                    }
                    else if (RanChar == 2)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 0, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 11, 3, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 169, 12, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 93, 4, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 3, 0, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, -1, 0, 2);//Top2
                    }
                    else if (RanChar == 3)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 0, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 13, 3, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, -1, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 98, 4, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 71, 4, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 1, 5, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 254, 4, 2);//Top2
                    }
                    else if (RanChar == 4)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 0, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 10, 1, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 15, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 9, 6, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 54, 3, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 100, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, 36, 1, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 13, 5, 2);//Top2
                    }
                    else if (RanChar == 5)
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 0, 0, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 2, 3, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 11, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 75, 1, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 20, 5, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, 0, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 208, 4, 2);//Top2
                    }
                    else
                    {
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 1, 134, 8, 2);//mask
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, 0, 0, 2);//hair
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 3, 13, 0, 2);//Torso
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 4, 113, 8, 2);//Legs
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 5, 0, 0, 2);//Hands
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 6, 87, 8, 2);//shoes
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 7, -1, 0, 2);//Scarf
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 8, -1, 0, 2);//AccTop
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 9, 0, 0, 2);//Armor
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 10, 0, 0, 2);//Emb--not used
                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 11, 287, 8, 2);//Top2
                    }
                }
            }
        }
        private void OnlineSavedWard(Ped Pedx, ClothBank MyCloths)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("OnlineSavedWard", tEx);

            Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx);

            for (int i = 0; i < MyCloths.ClothA.Count; i++)
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, i, MyCloths.ClothA[i], MyCloths.ClothB[i], 2);

            for (int i = 0; i < MyCloths.ExtraA.Count; i++)
                Function.Call(Hash.SET_PED_PROP_INDEX, Pedx, i, MyCloths.ExtraA[i], MyCloths.ExtraB[i], false);
        }
        private void Scaleform_PLAYER_LIST()
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("Scaleform_PLAYER_LIST", tEx);

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
                string sPlayer = PedList[i].sMyName;
                while (sPlayer.Count() < 14 && iFailed < 10)
                {
                    sPlayer = sPlayer + " ";
                    Script.Wait(1);
                    iFailed += 1;
                }

                sPlayer = sPlayer + PedList[i].iLevel;
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "SET_DATA_SLOT");
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, sPlayer);
                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                iAddOns += 1;
            }
            for (int i = 0; i < AFKList.Count; i++)
            {
                int iFailed = 0;
                string sPlayer = AFKList[i].sMyName;
                while (sPlayer.Count() < 14 && iFailed < 10)
                {
                    sPlayer = sPlayer + " ";
                    Script.Wait(1);
                    iFailed += 1;
                }

                sPlayer = sPlayer + AFKList[i].iLevel;
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "SET_DATA_SLOT");
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, sPlayer);
                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                iAddOns += 1;
            }

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "SET_DATA_SLOT");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, "Players in Session ;  ");
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, iScale, "DRAW_INSTRUCTIONAL_BUTTONS");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 1);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            bPlayerList = true;
        }
        private void CloseBaseHelpBar()
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("CloseBaseHelpBar", tEx);

            unsafe
            {
                int SF = iScale;
                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &SF);
            }

            bPlayerList = false;
        }
        private void PlayScales()
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("PlayScales", tEx);

            Scaleform_PLAYER_LIST();
            TopLeftUI("Press" + ControlSybols(iClearPlayList) + "to clear the session");
            int iStick = Game.GameTime + 8000;
            while (iStick > Game.GameTime && !ButtonDown(iClearPlayList, false))
            {
                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, iScale, 255, 255, 255, 255);
                Script.Wait(1);
            }
            CloseBaseHelpBar();

            if (ButtonDown(iClearPlayList, false))
                LaggOut();
        }
        private void FireOrb(int MyBrian)
        {
            PlayerBrain Brian = PedList[MyBrian];
            List<Vector3> FacList = new List<Vector3>();
            FacList.Add(new Vector3(1871.856f, 280.2685f, 164.3017f));
            FacList.Add(new Vector3(2074.258f, 1749.33f, 104.5142f));
            FacList.Add(new Vector3(2768.607f, 3919.833f, 45.81805f));
            FacList.Add(new Vector3(3407.416f, 5504.874f, 26.27827f));
            FacList.Add(new Vector3(1.844208f, 6832.069f, 15.81715f));
            FacList.Add(new Vector3(-2231.331f, 2417.907f, 12.18127f));
            FacList.Add(new Vector3(-6.777428f, 3326.627f, 41.63125f));
            FacList.Add(new Vector3(18.59906f, 2610.94f, 85.99267f));
            FacList.Add(new Vector3(1286.877f, 2846.37f, 49.39426f));

            Brian.ThisBlip = LocalBlip(FacList[RandInt(0, FacList.Count -1)], 590, Brian.sMyName);
            Script.Wait(5000);
            Vector3 PlayePos = Game.Player.Character.Position;
            if (World.GetGroundHeight(PlayePos) < PlayePos.Z)
            {
                Vector3 PlayerF = Game.Player.Character.Position + (Game.Player.Character.ForwardVector * 5);
                Vector3 PlayerB = Game.Player.Character.Position - (Game.Player.Character.ForwardVector * 5);
                Vector3 PlayerR = Game.Player.Character.Position + (Game.Player.Character.RightVector * 5);
                Vector3 PlayerL = Game.Player.Character.Position - (Game.Player.Character.RightVector * 5);
                OrbExp(PlayePos, PlayerF, PlayerB, PlayerR, PlayerL);
                OrbLoad(Brian.sMyName);
                Script.Wait(4000);
                PedCleaning(iNpcList, "left", false);
            }
        }
        private void OrbExp(Vector3 Pos1, Vector3 Pos2, Vector3 Pos3, Vector3 Pos4, Vector3 Pos5)
        {
            Function.Call(Hash.ADD_EXPLOSION, Pos2.X, Pos2.Y, Pos2.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_EXPLOSION, Pos3.X, Pos3.Y, Pos3.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_EXPLOSION, Pos4.X, Pos4.Y, Pos4.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_EXPLOSION, Pos5.X, Pos5.Y, Pos5.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_EXPLOSION, Pos1.X, Pos1.Y, Pos1.Z, 54, 1.00f, true, false, 1.00f);

            Function.Call(Hash.PLAY_SOUND_FROM_COORD, -1, "DLC_XM_Explosions_Orbital_Cannon", Pos1.X, Pos1.Y, Pos1.Z, 0, 0, 1, 0);
            Function.Call((Hash)0x6C38AF3693A69A91, "scr_xm_orbital");
        }
        private void OrbLoad(string sWhoDidit)
        {
            using (StreamWriter tEx = File.AppendText(sBeeLogs))
                BeeLog("OrbLoa", tEx);

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
            List<string> ControlList = new List<string>();
            ControlList.Add(" ~INPUT_NEXT_CAMERA~ ");//V~ ");//BACK
            ControlList.Add(" ~INPUT_LOOK_LR~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_UD~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_UP_ONLY~ ");//(NONE);~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_DOWN_ONLY~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_LEFT_ONLY~ ");//(NONE);~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_RIGHT_ONLY~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_CINEMATIC_SLOWMO~ ");//(NONE);~ ");//R3
            ControlList.Add(" ~INPUT_SCRIPTED_FLY_UD~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SCRIPTED_FLY_LR~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SCRIPTED_FLY_ZUP~ ");//PAGEUP~ ");//LT
            ControlList.Add(" ~INPUT_SCRIPTED_FLY_ZDOWN~ ");//PAGEDOWN~ ");//RT
            ControlList.Add(" ~INPUT_WEAPON_WHEEL_UD~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_WEAPON_WHEEL_LR~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_WEAPON_WHEEL_NEXT~ ");//SCROLLWHEEL DOWN~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_WEAPON_WHEEL_PREV~ ");//SCROLLWHEEL UP~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_SELECT_NEXT_WEAPON~ ");//SCROLLWHEEL DOWN~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_PREV_WEAPON~ ");//SCROLLWHEEL UP~ ");//(NONE);
            ControlList.Add(" ~INPUT_SKIP_CUTSCENE~ ");//ENTER / LEFT MOUSE BUTTON / SPACEBAR~ ");//A
            ControlList.Add(" ~INPUT_CHARACTER_WHEEL~ ");//LEFT ALT~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_MULTIPLAYER_INFO~ ");//Z~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_SPRINT~ ");//LEFT SHIFT~ ");//A
            ControlList.Add(" ~INPUT_JUMP~ ");//SPACEBAR~ ");//X
            ControlList.Add(" ~INPUT_ENTER~ ");//F~ ");//Y
            ControlList.Add(" ~INPUT_ATTACK~ ");//LEFT MOUSE BUTTON~ ");//RT
            ControlList.Add(" ~INPUT_AIM~ ");//RIGHT MOUSE BUTTON~ ");//LT
            ControlList.Add(" ~INPUT_LOOK_BEHIND~ ");//C~ ");//R3
            ControlList.Add(" ~INPUT_PHONE~ ");//ARROW UP / SCROLLWHEEL BUTTON (PRESS);~ ");//DPAD UP
            ControlList.Add(" ~INPUT_SPECIAL_ABILITY~ ");//(NONE);~ ");//L3
            ControlList.Add(" ~INPUT_SPECIAL_ABILITY_SECONDARY~ ");//B~ ");//R3
            ControlList.Add(" ~INPUT_MOVE_LR~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_UD~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_UP_ONLY~ ");//W~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_DOWN_ONLY~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_LEFT_ONLY~ ");//A~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_RIGHT_ONLY~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_DUCK~ ");//LEFT CTRL~ ");//L3
            ControlList.Add(" ~INPUT_SELECT_WEAPON~ ");//TAB~ ");//LB
            ControlList.Add(" ~INPUT_PICKUP~ ");//E~ ");//LB
            ControlList.Add(" ~INPUT_SNIPER_ZOOM~ ");//[~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_IN_ONLY~ ");//]~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_OUT_ONLY~ ");//[~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_IN_SECONDARY~ ");//]~ ");//DPAD UP
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_OUT_SECONDARY~ ");//[~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_COVER~ ");//Q~ ");//RB
            ControlList.Add(" ~INPUT_RELOAD~ ");//R~ ");//B
            ControlList.Add(" ~INPUT_TALK~ ");//E~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_DETONATE~ ");//G~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_HUD_SPECIAL~ ");//Z~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_ARREST~ ");//F~ ");//Y
            ControlList.Add(" ~INPUT_ACCURATE_AIM~ ");//SCROLLWHEEL DOWN~ ");//R3
            ControlList.Add(" ~INPUT_CONTEXT~ ");//E~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_CONTEXT_SECONDARY~ ");//Q~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_WEAPON_SPECIAL~ ");//(NONE);~ ");//Y
            ControlList.Add(" ~INPUT_WEAPON_SPECIAL_TWO~ ");//E~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_DIVE~ ");//SPACEBAR~ ");//RB
            ControlList.Add(" ~INPUT_DROP_WEAPON~ ");//F9~ ");//Y
            ControlList.Add(" ~INPUT_DROP_AMMO~ ");//F10~ ");//B
            ControlList.Add(" ~INPUT_THROW_GRENADE~ ");//G~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_VEH_MOVE_LR~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_UD~ ");//LEFT CTRL~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_UP_ONLY~ ");//LEFT SHIFT~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_DOWN_ONLY~ ");//LEFT CTRL~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_LEFT_ONLY~ ");//A~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_RIGHT_ONLY~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SPECIAL~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_GUN_LR~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_GUN_UD~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_AIM~ ");//RIGHT MOUSE BUTTON~ ");//LB
            ControlList.Add(" ~INPUT_VEH_ATTACK~ ");//LEFT MOUSE BUTTON~ ");//RB
            ControlList.Add(" ~INPUT_VEH_ATTACK2~ ");//RIGHT MOUSE BUTTON~ ");//A
            ControlList.Add(" ~INPUT_VEH_ACCELERATE~ ");//W~ ");//RT
            ControlList.Add(" ~INPUT_VEH_BRAKE~ ");//S~ ");//LT
            ControlList.Add(" ~INPUT_VEH_DUCK~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_VEH_HEADLIGHT~ ");//H~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_EXIT~ ");//F~ ");//Y
            ControlList.Add(" ~INPUT_VEH_HANDBRAKE~ ");//SPACEBAR~ ");//RB
            ControlList.Add(" ~INPUT_VEH_HOTWIRE_LEFT~ ");//W~ ");//LT
            ControlList.Add(" ~INPUT_VEH_HOTWIRE_RIGHT~ ");//S~ ");//RT
            ControlList.Add(" ~INPUT_VEH_LOOK_BEHIND~ ");//C~ ");//R3
            ControlList.Add(" ~INPUT_VEH_CIN_CAM~ ");//R~ ");//B
            ControlList.Add(" ~INPUT_VEH_NEXT_RADIO~ ");//.~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_PREV_RADIO~ ");//,~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_NEXT_RADIO_TRACK~ ");//=~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_PREV_RADIO_TRACK~ ");//-~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_RADIO_WHEEL~ ");//Q~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_VEH_HORN~ ");//E~ ");//L3
            ControlList.Add(" ~INPUT_VEH_FLY_THROTTLE_UP~ ");//W~ ");//RT
            ControlList.Add(" ~INPUT_VEH_FLY_THROTTLE_DOWN~ ");//S~ ");//LT
            ControlList.Add(" ~INPUT_VEH_FLY_YAW_LEFT~ ");//A~ ");//LB
            ControlList.Add(" ~INPUT_VEH_FLY_YAW_RIGHT~ ");//D~ ");//RB
            ControlList.Add(" ~INPUT_VEH_PASSENGER_AIM~ ");//RIGHT MOUSE BUTTON~ ");//LT
            ControlList.Add(" ~INPUT_VEH_PASSENGER_ATTACK~ ");//LEFT MOUSE BUTTON~ ");//RT
            ControlList.Add(" ~INPUT_VEH_SPECIAL_ABILITY_FRANKLIN~ ");//(NONE);~ ");//R3
            ControlList.Add(" ~INPUT_VEH_STUNT_UD~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_CINEMATIC_UD~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_CINEMATIC_UP_ONLY~ ");//NUMPAD- / SCROLLWHEEL UP~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_CINEMATIC_DOWN_ONLY~ ");//NUMPAD+ / SCROLLWHEEL DOWN~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_CINEMATIC_LR~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_SELECT_NEXT_WEAPON~ ");//SCROLLWHEEL UP~ ");//X
            ControlList.Add(" ~INPUT_VEH_SELECT_PREV_WEAPON~ ");//[~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_ROOF~ ");//H~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_JUMP~ ");//SPACEBAR~ ");//RB
            ControlList.Add(" ~INPUT_VEH_GRAPPLING_HOOK~ ");//E~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_SHUFFLE~ ");//H~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_DROP_PROJECTILE~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_VEH_MOUSE_CONTROL_OVERRIDE~ ");//LEFT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_FLY_ROLL_LR~ ");//NUMPAD 6~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_FLY_ROLL_LEFT_ONLY~ ");//NUMPAD 4~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_FLY_ROLL_RIGHT_ONLY~ ");//NUMPAD 6~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_FLY_PITCH_UD~ ");//NUMPAD 5~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_FLY_PITCH_UP_ONLY~ ");//NUMPAD 8~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_FLY_PITCH_DOWN_ONLY~ ");//NUMPAD 5~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_FLY_UNDERCARRIAGE~ ");//G~ ");//L3
            ControlList.Add(" ~INPUT_VEH_FLY_ATTACK~ ");//RIGHT MOUSE BUTTON~ ");//A
            ControlList.Add(" ~INPUT_VEH_FLY_SELECT_NEXT_WEAPON~ ");//SCROLLWHEEL UP~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_VEH_FLY_SELECT_PREV_WEAPON~ ");//[~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_FLY_SELECT_TARGET_LEFT~ ");//NUMPAD 7~ ");//LB
            ControlList.Add(" ~INPUT_VEH_FLY_SELECT_TARGET_RIGHT~ ");//NUMPAD 9~ ");//RB
            ControlList.Add(" ~INPUT_VEH_FLY_VERTICAL_FLIGHT_MODE~ ");//E~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_FLY_DUCK~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_VEH_FLY_ATTACK_CAMERA~ ");//INSERT~ ");//R3
            ControlList.Add(" ~INPUT_VEH_FLY_MOUSE_CONTROL_OVERRIDE~ ");//LEFT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_SUB_TURN_LR~ ");//NUMPAD 6~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SUB_TURN_LEFT_ONLY~ ");//NUMPAD 4~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SUB_TURN_RIGHT_ONLY~ ");//NUMPAD 6~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SUB_PITCH_UD~ ");//NUMPAD 5~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SUB_PITCH_UP_ONLY~ ");//NUMPAD 8~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SUB_PITCH_DOWN_ONLY~ ");//NUMPAD 5~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SUB_THROTTLE_UP~ ");//W~ ");//RT
            ControlList.Add(" ~INPUT_VEH_SUB_THROTTLE_DOWN~ ");//S~ ");//LT
            ControlList.Add(" ~INPUT_VEH_SUB_ASCEND~ ");//LEFT SHIFT~ ");//X
            ControlList.Add(" ~INPUT_VEH_SUB_DESCEND~ ");//LEFT CTRL~ ");//A
            ControlList.Add(" ~INPUT_VEH_SUB_TURN_HARD_LEFT~ ");//A~ ");//LB
            ControlList.Add(" ~INPUT_VEH_SUB_TURN_HARD_RIGHT~ ");//D~ ");//RB
            ControlList.Add(" ~INPUT_VEH_SUB_MOUSE_CONTROL_OVERRIDE~ ");//LEFT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_PUSHBIKE_PEDAL~ ");//W~ ");//A
            ControlList.Add(" ~INPUT_VEH_PUSHBIKE_SPRINT~ ");//CAPSLOCK~ ");//A
            ControlList.Add(" ~INPUT_VEH_PUSHBIKE_FRONT_BRAKE~ ");//Q~ ");//LT
            ControlList.Add(" ~INPUT_VEH_PUSHBIKE_REAR_BRAKE~ ");//S~ ");//RT
            ControlList.Add(" ~INPUT_MELEE_ATTACK_LIGHT~ ");//R~ ");//B
            ControlList.Add(" ~INPUT_MELEE_ATTACK_HEAVY~ ");//Q~ ");//A
            ControlList.Add(" ~INPUT_MELEE_ATTACK_ALTERNATE~ ");//LEFT MOUSE BUTTON~ ");//RT
            ControlList.Add(" ~INPUT_MELEE_BLOCK~ ");//SPACEBAR~ ");//X
            ControlList.Add(" ~INPUT_PARACHUTE_DEPLOY~ ");//F / LEFT MOUSE BUTTON~ ");//Y
            ControlList.Add(" ~INPUT_PARACHUTE_DETACH~ ");//F~ ");//Y
            ControlList.Add(" ~INPUT_PARACHUTE_TURN_LR~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_PARACHUTE_TURN_LEFT_ONLY~ ");//A~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_PARACHUTE_TURN_RIGHT_ONLY~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_PARACHUTE_PITCH_UD~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_PARACHUTE_PITCH_UP_ONLY~ ");//W~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_PARACHUTE_PITCH_DOWN_ONLY~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_PARACHUTE_BRAKE_LEFT~ ");//Q~ ");//LB
            ControlList.Add(" ~INPUT_PARACHUTE_BRAKE_RIGHT~ ");//E~ ");//RB
            ControlList.Add(" ~INPUT_PARACHUTE_SMOKE~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_PARACHUTE_PRECISION_LANDING~ ");//LEFT SHIFT~ ");//(NONE);
            ControlList.Add(" ~INPUT_MAP~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_UNARMED~ ");//1~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_MELEE~ ");//2~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_HANDGUN~ ");//6~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_SHOTGUN~ ");//3~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_SMG~ ");//7~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_AUTO_RIFLE~ ");//8~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_SNIPER~ ");//9~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_HEAVY~ ");//4~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_WEAPON_SPECIAL~ ");//5~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_CHARACTER_MICHAEL~ ");//F5~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_CHARACTER_FRANKLIN~ ");//F6~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_CHARACTER_TREVOR~ ");//F7~ ");//(NONE);
            ControlList.Add(" ~INPUT_SELECT_CHARACTER_MULTIPLAYER~ ");//F8 (CONSOLE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_SAVE_REPLAY_CLIP~ ");//F3~ ");//B
            ControlList.Add(" ~INPUT_SPECIAL_ABILITY_PC~ ");//CAPSLOCK~ ");//(NONE);
            ControlList.Add(" ~INPUT_CELLPHONE_UP~ ");//ARROW UP~ ");//DPAD UP
            ControlList.Add(" ~INPUT_CELLPHONE_DOWN~ ");//ARROW DOWN~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_CELLPHONE_LEFT~ ");//ARROW LEFT~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_CELLPHONE_RIGHT~ ");//ARROW RIGHT~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_CELLPHONE_SELECT~ ");//ENTER / LEFT MOUSE BUTTON~ ");//A
            ControlList.Add(" ~INPUT_CELLPHONE_CANCEL~ ");//BACKSPACE / ESC / RIGHT MOUSE BUTTON~ ");//B
            ControlList.Add(" ~INPUT_CELLPHONE_OPTION~ ");//DELETE~ ");//Y
            ControlList.Add(" ~INPUT_CELLPHONE_EXTRA_OPTION~ ");//SPACEBAR~ ");//X
            ControlList.Add(" ~INPUT_CELLPHONE_SCROLL_FORWARD~ ");//SCROLLWHEEL DOWN~ ");//(NONE);
            ControlList.Add(" ~INPUT_CELLPHONE_SCROLL_BACKWARD~ ");//SCROLLWHEEL UP~ ");//(NONE);
            ControlList.Add(" ~INPUT_CELLPHONE_CAMERA_FOCUS_LOCK~ ");//L~ ");//RT
            ControlList.Add(" ~INPUT_CELLPHONE_CAMERA_GRID~ ");//G~ ");//RB
            ControlList.Add(" ~INPUT_CELLPHONE_CAMERA_SELFIE~ ");//E~ ");//R3
            ControlList.Add(" ~INPUT_CELLPHONE_CAMERA_DOF~ ");//F~ ");//LB
            ControlList.Add(" ~INPUT_CELLPHONE_CAMERA_EXPRESSION~ ");//X~ ");//L3
            ControlList.Add(" ~INPUT_FRONTEND_DOWN~ ");//ARROW DOWN~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_FRONTEND_UP~ ");//ARROW UP~ ");//DPAD UP
            ControlList.Add(" ~INPUT_FRONTEND_LEFT~ ");//ARROW LEFT~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_FRONTEND_RIGHT~ ");//ARROW RIGHT~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_FRONTEND_RDOWN~ ");//ENTER~ ");//A
            ControlList.Add(" ~INPUT_FRONTEND_RUP~ ");//TAB~ ");//Y
            ControlList.Add(" ~INPUT_FRONTEND_RLEFT~ ");//(NONE);~ ");//X
            ControlList.Add(" ~INPUT_FRONTEND_RRIGHT~ ");//BACKSPACE~ ");//B
            ControlList.Add(" ~INPUT_FRONTEND_AXIS_X~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_FRONTEND_AXIS_Y~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_FRONTEND_RIGHT_AXIS_X~ ");//]~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_FRONTEND_RIGHT_AXIS_Y~ ");//SCROLLWHEEL DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_FRONTEND_PAUSE~ ");//P~ ");//START
            ControlList.Add(" ~INPUT_FRONTEND_PAUSE_ALTERNATE~ ");//ESC~ ");//(NONE);
            ControlList.Add(" ~INPUT_FRONTEND_ACCEPT~ ");//ENTER / NUMPAD ENTER~ ");//A
            ControlList.Add(" ~INPUT_FRONTEND_CANCEL~ ");//BACKSPACE / ESC~ ");//B
            ControlList.Add(" ~INPUT_FRONTEND_X~ ");//SPACEBAR~ ");//X
            ControlList.Add(" ~INPUT_FRONTEND_Y~ ");//TAB~ ");//Y
            ControlList.Add(" ~INPUT_FRONTEND_LB~ ");//Q~ ");//LB
            ControlList.Add(" ~INPUT_FRONTEND_RB~ ");//E~ ");//RB
            ControlList.Add(" ~INPUT_FRONTEND_LT~ ");//PAGE DOWN~ ");//LT
            ControlList.Add(" ~INPUT_FRONTEND_RT~ ");//PAGE UP~ ");//RT
            ControlList.Add(" ~INPUT_FRONTEND_LS~ ");//LEFT SHIFT~ ");//L3
            ControlList.Add(" ~INPUT_FRONTEND_RS~ ");//LEFT CONTROL~ ");//R3
            ControlList.Add(" ~INPUT_FRONTEND_LEADERBOARD~ ");//TAB~ ");//RB
            ControlList.Add(" ~INPUT_FRONTEND_SOCIAL_CLUB~ ");//HOME~ ");//BACK
            ControlList.Add(" ~INPUT_FRONTEND_SOCIAL_CLUB_SECONDARY~ ");//HOME~ ");//RB
            ControlList.Add(" ~INPUT_FRONTEND_DELETE~ ");//DELETE~ ");//X
            ControlList.Add(" ~INPUT_FRONTEND_ENDSCREEN_ACCEPT~ ");//ENTER~ ");//A
            ControlList.Add(" ~INPUT_FRONTEND_ENDSCREEN_EXPAND~ ");//SPACEBAR~ ");//X
            ControlList.Add(" ~INPUT_FRONTEND_SELECT~ ");//CAPSLOCK~ ");//BACK
            ControlList.Add(" ~INPUT_SCRIPT_LEFT_AXIS_X~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SCRIPT_LEFT_AXIS_Y~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SCRIPT_RIGHT_AXIS_X~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SCRIPT_RIGHT_AXIS_Y~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SCRIPT_RUP~ ");//RIGHT MOUSE BUTTON~ ");//Y
            ControlList.Add(" ~INPUT_SCRIPT_RDOWN~ ");//LEFT MOUSE BUTTON~ ");//A
            ControlList.Add(" ~INPUT_SCRIPT_RLEFT~ ");//LEFT CTRL~ ");//X
            ControlList.Add(" ~INPUT_SCRIPT_RRIGHT~ ");//RIGHT MOUSE BUTTON~ ");//B
            ControlList.Add(" ~INPUT_SCRIPT_LB~ ");//(NONE);~ ");//LB
            ControlList.Add(" ~INPUT_SCRIPT_RB~ ");//(NONE);~ ");//RB
            ControlList.Add(" ~INPUT_SCRIPT_LT~ ");//(NONE);~ ");//LT
            ControlList.Add(" ~INPUT_SCRIPT_RT~ ");//LEFT MOUSE BUTTON~ ");//RT
            ControlList.Add(" ~INPUT_SCRIPT_LS~ ");//(NONE);~ ");//L3
            ControlList.Add(" ~INPUT_SCRIPT_RS~ ");//(NONE);~ ");//R3
            ControlList.Add(" ~INPUT_SCRIPT_PAD_UP~ ");//W~ ");//DPAD UP
            ControlList.Add(" ~INPUT_SCRIPT_PAD_DOWN~ ");//S~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_SCRIPT_PAD_LEFT~ ");//A~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_SCRIPT_PAD_RIGHT~ ");//D~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_SCRIPT_SELECT~ ");//V~ ");//BACK
            ControlList.Add(" ~INPUT_CURSOR_ACCEPT~ ");//LEFT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_CURSOR_CANCEL~ ");//RIGHT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_CURSOR_X~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_CURSOR_Y~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_CURSOR_SCROLL_UP~ ");//SCROLLWHEEL UP~ ");//(NONE);
            ControlList.Add(" ~INPUT_CURSOR_SCROLL_DOWN~ ");//SCROLLWHEEL DOWN~ ");//(NONE);
            ControlList.Add(" ~INPUT_ENTER_CHEAT_CODE~ ");//~ / `~ ");//(NONE);
            ControlList.Add(" ~INPUT_INTERACTION_MENU~ ");//M~ ");//BACK
            ControlList.Add(" ~INPUT_MP_TEXT_CHAT_ALL~ ");//T~ ");//(NONE);
            ControlList.Add(" ~INPUT_MP_TEXT_CHAT_TEAM~ ");//Y~ ");//(NONE);
            ControlList.Add(" ~INPUT_MP_TEXT_CHAT_FRIENDS~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_MP_TEXT_CHAT_CREW~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_PUSH_TO_TALK~ ");//N~ ");//(NONE);
            ControlList.Add(" ~INPUT_CREATOR_LS~ ");//R~ ");//L3
            ControlList.Add(" ~INPUT_CREATOR_RS~ ");//F~ ");//R3
            ControlList.Add(" ~INPUT_CREATOR_LT~ ");//X~ ");//LT
            ControlList.Add(" ~INPUT_CREATOR_RT~ ");//C~ ");//RT
            ControlList.Add(" ~INPUT_CREATOR_MENU_TOGGLE~ ");//LEFT SHIFT~ ");//(NONE);
            ControlList.Add(" ~INPUT_CREATOR_ACCEPT~ ");//SPACEBAR~ ");//A
            ControlList.Add(" ~INPUT_CREATOR_DELETE~ ");//DELETE~ ");//X
            ControlList.Add(" ~INPUT_ATTACK2~ ");//LEFT MOUSE BUTTON~ ");//RT
            ControlList.Add(" ~INPUT_RAPPEL_JUMP~ ");//(NONE);~ ");//A
            ControlList.Add(" ~INPUT_RAPPEL_LONG_JUMP~ ");//(NONE);~ ");//X
            ControlList.Add(" ~INPUT_RAPPEL_SMASH_WINDOW~ ");//(NONE);~ ");//RT
            ControlList.Add(" ~INPUT_PREV_WEAPON~ ");//SCROLLWHEEL UP~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_NEXT_WEAPON~ ");//SCROLLWHEEL DOWN~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_MELEE_ATTACK1~ ");//R~ ");//B
            ControlList.Add(" ~INPUT_MELEE_ATTACK2~ ");//Q~ ");//A
            ControlList.Add(" ~INPUT_WHISTLE~ ");//(NONE);~ ");//(NONE);
            ControlList.Add(" ~INPUT_MOVE_LEFT~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_RIGHT~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_UP~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_MOVE_DOWN~ ");//S~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_LOOK_LEFT~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_RIGHT~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_UP~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_LOOK_DOWN~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_IN~ ");//[~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_OUT~ ");//[~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_IN_ALTERNATE~ ");//[~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SNIPER_ZOOM_OUT_ALTERNATE~ ");//[~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_LEFT~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_RIGHT~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_UP~ ");//LEFT CTRL~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_MOVE_DOWN~ ");//LEFT CTRL~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_GUN_LEFT~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_GUN_RIGHT~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_GUN_UP~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_GUN_DOWN~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_LOOK_LEFT~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_LOOK_RIGHT~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_REPLAY_START_STOP_RECORDING~ ");//F1~ ");//A
            ControlList.Add(" ~INPUT_REPLAY_START_STOP_RECORDING_SECONDARY~ ");//F2~ ");//X
            ControlList.Add(" ~INPUT_SCALED_LOOK_LR~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SCALED_LOOK_UD~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SCALED_LOOK_UP_ONLY~ ");//(NONE);~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SCALED_LOOK_DOWN_ONLY~ ");//(NONE);~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SCALED_LOOK_LEFT_ONLY~ ");//(NONE);~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_SCALED_LOOK_RIGHT_ONLY~ ");//(NONE);~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_REPLAY_MARKER_DELETE~ ");//DELETE~ ");//X
            ControlList.Add(" ~INPUT_REPLAY_CLIP_DELETE~ ");//DELETE~ ");//Y
            ControlList.Add(" ~INPUT_REPLAY_PAUSE~ ");//SPACEBAR~ ");//A
            ControlList.Add(" ~INPUT_REPLAY_REWIND~ ");//ARROW DOWN~ ");//LB
            ControlList.Add(" ~INPUT_REPLAY_FFWD~ ");//ARROW UP~ ");//RB
            ControlList.Add(" ~INPUT_REPLAY_NEWMARKER~ ");//M~ ");//A
            ControlList.Add(" ~INPUT_REPLAY_RECORD~ ");//S~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_SCREENSHOT~ ");//U~ ");//DPAD UP
            ControlList.Add(" ~INPUT_REPLAY_HIDEHUD~ ");//H~ ");//R3
            ControlList.Add(" ~INPUT_REPLAY_STARTPOINT~ ");//B~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_ENDPOINT~ ");//N~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_ADVANCE~ ");//ARROW RIGHT~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_REPLAY_BACK~ ");//ARROW LEFT~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_REPLAY_TOOLS~ ");//T~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_REPLAY_RESTART~ ");//R~ ");//BACK
            ControlList.Add(" ~INPUT_REPLAY_SHOWHOTKEY~ ");//K~ ");//DPAD DOWN
            ControlList.Add(" ~INPUT_REPLAY_CYCLEMARKERLEFT~ ");//[~ ");//DPAD LEFT
            ControlList.Add(" ~INPUT_REPLAY_CYCLEMARKERRIGHT~ ");//]~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_REPLAY_FOVINCREASE~ ");//NUMPAD +~ ");//RB
            ControlList.Add(" ~INPUT_REPLAY_FOVDECREASE~ ");//NUMPAD -~ ");//LB
            ControlList.Add(" ~INPUT_REPLAY_CAMERAUP~ ");//PAGE UP~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_CAMERADOWN~ ");//PAGE DOWN~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_SAVE~ ");//F5~ ");//START
            ControlList.Add(" ~INPUT_REPLAY_TOGGLETIME~ ");//C~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_TOGGLETIPS~ ");//V~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_PREVIEW~ ");//SPACEBAR~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_TOGGLE_TIMELINE~ ");//ESC~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_TIMELINE_PICKUP_CLIP~ ");//X~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_TIMELINE_DUPLICATE_CLIP~ ");//C~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_TIMELINE_PLACE_CLIP~ ");//V~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_CTRL~ ");//LEFT CTRL~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_TIMELINE_SAVE~ ");//F5~ ");//(NONE);
            ControlList.Add(" ~INPUT_REPLAY_PREVIEW_AUDIO~ ");//SPACEBAR~ ");//RT
            ControlList.Add(" ~INPUT_VEH_DRIVE_LOOK~ ");//LEFT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_DRIVE_LOOK2~ ");//RIGHT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_VEH_FLY_ATTACK2~ ");//RIGHT MOUSE BUTTON~ ");//(NONE);
            ControlList.Add(" ~INPUT_RADIO_WHEEL_UD~ ");//MOUSE DOWN~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_RADIO_WHEEL_LR~ ");//MOUSE RIGHT~ ");//RIGHT STICK
            ControlList.Add(" ~INPUT_VEH_SLOWMO_UD~ ");//SCROLLWHEEL DOWN~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SLOWMO_UP_ONLY~ ");//SCROLLWHEEL UP~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_SLOWMO_DOWN_ONLY~ ");//SCROLLWHEEL DOWN~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_HYDRAULICS_CONTROL_TOGGLE~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_VEH_HYDRAULICS_CONTROL_LEFT~ ");//A~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_HYDRAULICS_CONTROL_RIGHT~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_HYDRAULICS_CONTROL_UP~ ");//LEFT SHIFT~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_HYDRAULICS_CONTROL_DOWN~ ");//LEFT CTRL~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_HYDRAULICS_CONTROL_UD~ ");//D~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_VEH_HYDRAULICS_CONTROL_LR~ ");//LEFT CTRL~ ");//LEFT STICK
            ControlList.Add(" ~INPUT_SWITCH_VISOR~ ");//F11~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_MELEE_HOLD~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_VEH_MELEE_LEFT~ ");//LEFT MOUSE BUTTON~ ");//LB
            ControlList.Add(" ~INPUT_VEH_MELEE_RIGHT~ ");//RIGHT MOUSE BUTTON~ ");//RB
            ControlList.Add(" ~INPUT_MAP_POI~ ");//SCROLLWHEEL BUTTON (PRESS);~ ");//Y
            ControlList.Add(" ~INPUT_REPLAY_SNAPMATIC_PHOTO~ ");//TAB~ ");//X
            ControlList.Add(" ~INPUT_VEH_CAR_JUMP~ ");//E~ ");//L3
            ControlList.Add(" ~INPUT_VEH_ROCKET_BOOST~ ");//E~ ");//L3
            ControlList.Add(" ~INPUT_VEH_FLY_BOOST~ ");//LEFT SHIFT~ ");//L3
            ControlList.Add(" ~INPUT_VEH_PARACHUTE~ ");//SPACEBAR~ ");//A
            ControlList.Add(" ~INPUT_VEH_BIKE_WINGS~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_VEH_FLY_BOMB_BAY~ ");//E~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_FLY_COUNTER~ ");//E~ ");//DPAD RIGHT
            ControlList.Add(" ~INPUT_VEH_TRANSFORM~ ");//X~ ");//A
            ControlList.Add(" ~INPUT_QUAD_LOCO_REVERSE~ ");//~ ");//RB
            ControlList.Add(" ~INPUT_RESPAWN_FASTER~ ");//~ ");//
            ControlList.Add(" ~INPUT_HUDMARKER_SELECT~ ");

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
            while (Game.Player.Character.GetKiller() == Peddy)
                Script.Wait(1);
            Script.Wait(1000);
            UI.Notify("You  " + iKills + " - " + iKilled + " " + Kellar);
        }
        private void PlayerZerosAI(bool bInALoop)
        {
            if (ButtonDown(iGetlayList, false) && !bPlayerList)
                PlayScales();

            if (ThisBrian(iNpcList) != null)
            {
                PlayerBrain Brian = PedList[iNpcList];

                if (Brian.DirBlip != null)
                    BlipDirect(Brian.DirBlip, Brian.ThisPed.Heading);

                if (Brian.ThisVeh != null)
                {
                    if (Brian.ThisVeh.IsDead)
                    {
                        Brian.ThisVeh.MarkAsNoLongerNeeded();
                        Brian.ThisVeh = null;
                    }
                }

                if (Brian.iTimeOn < Game.GameTime)
                {
                    GetOutVehicle(Brian.ThisPed);
                    PedCleaning(iNpcList, "left", false);
                }
                else if (Game.Player.Character.GetKiller() == Brian.ThisPed)
                {
                    Brian.iKills += 1;
                    WhileYouDead(Brian.sMyName, Brian.iKilled, Brian.iKills, Brian.ThisPed);
                    if (bNoSpawnKills)
                        PedCleaning(iNpcList, "left", false);
                }
                else if (Brian.ThisPed.IsDead)
                {
                    if (Brian.iDeathSequence == 0)
                    {
                        if (Brian.iKilled > RandInt(13, 22))
                            PedCleaning(iNpcList, "left", false);
                        else
                        {
                            int iDie = WhoShotMe(Brian.ThisPed);
                            Brian.ThisBlip.Remove();
                            Brian.bOverFriendly = false;
                            if (Brian.DirBlip != null)
                            {
                                Brian.DirBlip.Remove();
                                Brian.DirBlip = null;
                            }

                            if (Brian.ThisPed.GetKiller() == Game.Player.Character)
                            {
                                if (Brian.bBounty)
                                    Game.Player.Money += 7000;
                                Brian.bFriendly = false;
                                Brian.iKilled += 1;
                                UI.Notify("You  " + Brian.iKilled + " - " + Brian.iKills + " " + Brian.sMyName);
                            }
                            else if (iDie != -1)
                            {
                                UI.Notify(PedList[iDie].sMyName + " Killed " + Brian.sMyName);
                                Brian.ThisPed.Task.FightAgainst(PedList[iDie].ThisPed);
                                Brian.ThisPed.AlwaysKeepTask = true;
                            }
                            else
                                UI.Notify(Brian.sMyName + "died");

                            Brian.bBounty = false;
                            Brian.iDeathSequence += 1;
                            Brian.iDeathTime = Game.GameTime + 10000;
                            Brian.iTimeOn += 60000;
                        }
                    }
                    else if (Brian.iDeathSequence == 1 || Brian.iDeathSequence == 3 || Brian.iDeathSequence == 5 || Brian.iDeathSequence == 7)
                    {
                        if (Brian.iDeathTime < Game.GameTime)
                        {
                            Brian.ThisPed.Alpha = 80;
                            Brian.iDeathSequence += 1;
                            Brian.iDeathTime = Game.GameTime + 500;
                        }
                    }
                    else if (Brian.iDeathSequence == 2 || Brian.iDeathSequence == 4 || Brian.iDeathSequence == 6)
                    {
                        if (Brian.iDeathTime < Game.GameTime)
                        {
                            Brian.ThisPed.Alpha = 255;
                            Brian.iDeathSequence += 1;
                            Brian.iDeathTime = Game.GameTime + 500;
                        }
                    }
                    else if (Brian.iDeathSequence == 8)
                    {
                        if (bInALoop)
                        {
                            if (Brian.iDeathTime < Game.GameTime)
                            {
                                if (Brian.iKilled > 15 && Brian.iKills == 0 && iAggression > 7)
                                    FireOrb(iNpcList);
                                else
                                {
                                    Vector3 Posy = FindAPed(Game.Player.Character.Position, 95.00f, 0, false, false);
                                    Brian.ThisPed.Position = Posy;
                                    Brian.ThisPed.Alpha = 255;
                                    Brian.iDeathSequence = 0;
                                    Brian.DirBlip = DirectionalBlimp(Brian.ThisPed);
                                    Brian.ThisBlip = PedBlimp(Brian.ThisPed, 1, Brian.sMyName, Brian.bFriendly);
                                    Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Brian.ThisPed.Handle);
                                    Function.Call(Hash.RESURRECT_PED, Brian.ThisPed.Handle);
                                    Function.Call(Hash.REVIVE_INJURED_PED, Brian.ThisPed.Handle);
                                    Brian.ThisPed.IsInvincible = false;
                                    if (!Brian.bFriendly)
                                        FightPlayer(Brian.ThisPed, false);
                                    GunningIt(Brian.ThisPed);
                                }
                            }
                        }
                    }
                }
                else if (Brian.bOverFriendly)
                {
                    if (Brian.ThisPed.IsInVehicle())
                    {
                        if (Game.Player.Character.Position.DistanceTo(Brian.ThisPed.Position) < 5.00f)
                        {
                            if (Brian.bHorny)
                            {
                                Brian.bHorny = false;
                                Brian.ThisVeh.SoundHorn(3000);
                                TopLeftUI("Press" + ControlSybols(23) + "to enter vehicle");
                            }
                            else if (!Game.Player.Character.IsInVehicle())
                            {
                                if (ButtonDown(23, true))
                                    EnterAnyVeh(Brian.ThisVeh, Game.Player.Character, 2.00f);
                            }
                            else
                            {
                                Brian.bOverFriendly = false;
                                FolllowTheLeader(Brian.ThisPed);
                                DriveAround(Brian.ThisPed);
                            }
                        }
                        else if (Game.Player.Character.Position.DistanceTo(Brian.ThisPed.Position) > 45.00f)
                        {
                            if (Brian.iFindPlayer < Game.GameTime)
                            {
                                Brian.iFindPlayer = Game.GameTime + 5000;
                                DriveTooo(Brian.ThisPed, false);
                            }
                        }
                    }
                }
                else if (Brian.bSessionJumper)
                {
                    if (Brian.ThisPed.Position.DistanceTo(Game.Player.Character.Position) < 10.00f)
                        PedCleaning(iNpcList, "has disappeared", true);
                }
                else if (Brian.bFollower)
                {
                    if (Game.Player.Character.IsInVehicle())
                    {
                        Vehicle DisVeh = Game.Player.Character.CurrentVehicle;
                        if (!Brian.ThisPed.IsInVehicle(DisVeh))
                        {
                            EnterAnyVeh(DisVeh, Brian.ThisPed, 2.00f);
                            Script.Wait(5000);
                        }
                    }
                    else
                    {
                        if (Brian.ThisPed.IsInVehicle())
                        {
                            Brian.ThisPed.Task.LeaveVehicle();
                            Script.Wait(5000);
                            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, Brian.ThisPed, Game.Player.Character.Handle, 0.0f, 3.0f, 0.0f, 1.0f, -1, 0.5f, true);
                        }
                    }
                }
                else if (Brian.bFriendly)
                {
                    if (Brian.ThisPed.HasBeenDamagedBy(Game.Player.Character))
                    {
                        Brian.ThisBlip.Remove();
                        ExMember(Brian.ThisPed);
                        FightPlayer(Brian.ThisPed, false);
                        Brian.bFriendly = false;
                        Brian.bFollower = false;
                        if (Brian.DirBlip == null)
                            Brian.DirBlip = DirectionalBlimp(Brian.ThisPed);
                        Brian.ThisBlip = PedBlimp(Brian.ThisPed, 1, Brian.sMyName, Brian.bFriendly);
                    }
                    else
                    {
                        if (Game.Player.Character.Position.DistanceTo(Brian.ThisPed.Position) < 7.00f && Brian.ThisVeh == null)
                        {
                            if (Game.Player.Character.IsInVehicle())
                            {
                                if (Game.Player.Character.SeatIndex == VehicleSeat.Driver)
                                {
                                    if (Brian.bHorny2)
                                    {
                                        TopLeftUI("Press" + ControlSybols(86) + "to attract the players attention");
                                        Brian.bHorny2 = false;
                                    }
                                    else if (ButtonDown(86, false))
                                    {
                                        if (iAggression < 9)
                                        {
                                            FolllowTheLeader(Brian.ThisPed);
                                            Brian.iTimeOn += 60000;
                                            Brian.bFollower = true;
                                        }
                                        else
                                        {
                                            Brian.ThisBlip.Remove();
                                            ExMember(Brian.ThisPed);
                                            FightPlayer(Brian.ThisPed, false);
                                            Brian.bFriendly = false;
                                            Brian.bFollower = false;
                                            if (Brian.DirBlip == null)
                                                Brian.DirBlip = DirectionalBlimp(Brian.ThisPed);
                                            Brian.ThisBlip = PedBlimp(Brian.ThisPed, 1, Brian.sMyName, Brian.bFriendly);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Brian.ThisPed.Position.DistanceTo(Game.Player.Character.Position) > 350.00f && iAggression > 4)
                    {
                        if (Brian.ThisVeh == null)
                            AirAttack(iNpcList);
                    }
                }
                iNpcList += 1;
            }
            else
                iNpcList = 0;

            if (ThisAFKer(iBlpList) != null)
            {
                AfkPlayer HouseBlip = ThisAFKer(iBlpList);

                if (HouseBlip.iTimeOn < Game.GameTime)
                {
                    DeListingBrains(false, iBlpList, true);
                    iCurrentPlayerz -= 1;
                }
                iBlpList += 1;
            }
            else
                iBlpList = 0;

            if (bInALoop)
            {
                if (BTimer(1))
                    NewPlayer();
            }
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
                PlayerZerosAI(true);
        }
    }
}