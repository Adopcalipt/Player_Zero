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
        bool bDebugger = false;
        bool bLoadUp = true;
        bool bPlayerList = false;
        bool bHeistPop = true;
        bool bHackerIn = false;
        bool bPiggyBack = false;
        bool bHackEvent = false;
        bool bSpaceWeaps = false;
        bool bDisabled = false;
        bool bSearching = true;
        bool bStuckOnYou = false;

        string Version = "1.4";
        string sBeeLogs = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZLog.txt";
        string sMemory = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZsMemory.xml";
        string sTheIni = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/PlayerZSettings.ini";
        string sOutfitts = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/Outfits.xml";

        int iMaxPlayers = 29;
        int iNpcList = 0;
        int iBlpList = 0;
        int iFollow = 0;
        int iCurrentPlayerz = 0;
        int iOrbBurnOut = 0;
        int iScale = 0;
        int iMinWait = 15000;
        int iMaxWait = 45000;
        int iAccMin = 25;
        int iAccMax = 75;
        int iMinSession = 30000;
        int iMaxSession = 475000;
        int iAggression = 5;
        int iGetlayList = 19;
        int iClearPlayList = 131;
        int iDisableMod = 28;

        int iPlayerGroups = Game.Player.Character.RelationshipGroup;
        int iAttackingNPCs = World.AddRelationshipGroup("AttackNPCs");
        int iFollowingNPCs = World.AddRelationshipGroup("FollowerNPCs");
        int iWarringNPCs = World.AddRelationshipGroup("WarNPCs");

        Vector3 LetsGoHere = Vector3.Zero;

        BackUpBrain BlowenMyBrains = new BackUpBrain();

        List<bool> BeOnOff = new List<bool>();
        List<int> iTimers = new List<int>();
        List<string> sDebuggler = new List<string>();

        List<Prop> Plops = new List<Prop>();
        List<Vehicle> Vicks = new List<Vehicle>();
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
            GetLogging("LoadSettings");

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

            World.SetRelationshipBetweenGroups(Relationship.Hate, iPlayerGroups, iAttackingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, iAttackingNPCs, iPlayerGroups);
            World.SetRelationshipBetweenGroups(Relationship.Companion, iPlayerGroups, iFollowingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Companion, iFollowingNPCs, iPlayerGroups);
            World.SetRelationshipBetweenGroups(Relationship.Hate, iWarringNPCs, iPlayerGroups);
            World.SetRelationshipBetweenGroups(Relationship.Hate, iPlayerGroups, iWarringNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, iWarringNPCs, iFollowingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, iFollowingNPCs, iWarringNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, iWarringNPCs, iAttackingNPCs);
            World.SetRelationshipBetweenGroups(Relationship.Hate, iAttackingNPCs, iWarringNPCs);


            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Game.Player.Character.Handle, iFollowingNPCs);
            Function.Call(Hash.SET_GROUP_FORMATION, iFollowingNPCs, 3);
            iTimers.Clear();
            SetBTimer(30000, -1);
            SetBTimer(RandInt(iMinWait, iMaxWait), -1);
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
            GetLogging("SillyNameList");

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
            GetLogging("InABuilding");

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
            UI.Notify("~h~" + PedList[iBe].sMyName + "~s~ " + sOff);

            DeListingBrains(true, iPed, bGone);
            bSearching = true;
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
                    bSearching = false;
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
                    if (PedList[iPos].bHacker)
                    {
                        for (int i = 0; i < Plops.Count; i++)
                            Plops[i].MarkAsNoLongerNeeded();

                        for (int i = 0; i < Vicks.Count; i++)
                            Vicks[i].MarkAsNoLongerNeeded();

                        PedList[iPos].ThisPed.Detach();
                        bHackerIn = false;
                        bPiggyBack = false;
                        FireOrb(-1, PedList[iPos].ThisPed);
                    }

                    if (PedList[iPos].bFollower)
                        iFollow -= 1;
                    if (PedList[iPos].ThisBlip != null)
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
            GetLogging("NewPlayer, iCurrentPlayerz == " + iCurrentPlayerz);
            SetBTimer(RandInt(iMinWait, iMaxWait), 1);
            LoadSettings();

            if (iCurrentPlayerz < iMaxPlayers)
            {
                if (BOnBOff(0))
                {
                    bStuckOnYou = true;
                    if (BOnBOff(3))
                        FindAPed(Game.Player.Character.Position, 95.00f, true, -1);
                    else
                    {
                        int iTypeO = LessRandomz(1, 9, 12);
                        FindNearestVeh(Game.Player.Character.Position, RandVeh(iTypeO), true);
                    }

                    if (iCurrentPlayerz + 5 < iMaxPlayers)
                    {
                        if (bHeistPop)
                            NearHiest(true);
                        else
                            CeoBikersGreaf();
                    }
                }
                else
                    InABuilding();
        
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

            if (World.GetNextPositionOnStreet(Game.Player.Character.Position).DistanceTo(Game.Player.Character.Position) < 95.00f)
            {
                Vehicle[] CarSpot = World.GetNearbyVehicles(Vec3, 200.00f);
                for (int i = 0; i < CarSpot.Count(); i++)
                {
                    if (VehExists(CarSpot, i) && Vickary == null)
                    {
                        Vehicle Veh = CarSpot[i];
                        if (!Veh.IsPersistent && Veh.Position.DistanceTo(Game.Player.Character.Position) > 15.00f && Veh.ClassType != VehicleClass.Boats && Veh.ClassType != VehicleClass.Cycles && Veh.ClassType != VehicleClass.Helicopters && Veh.ClassType != VehicleClass.Planes && Veh.ClassType != VehicleClass.Trains && !Veh.IsOnScreen)
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
        public Vehicle FindNearestVeh(Vector3 Vec3, string sModel, bool bAddPlayer)
        {
            GetLogging("FindNearestVeh, sModel == " + sModel);

            Vehicle VickPos = LookNear(Vec3);

            while (VickPos == null && bSearching)
            {
                PlayerZerosAI();

                Script.Wait(100);
                VickPos = LookNear(Game.Player.Character.Position);
            }
            bStuckOnYou = false;
            if (bSearching)
            {
                Vector3 PedPos = VickPos.Position;
                float PedRot = VickPos.Heading;
                VickPos.Delete();
                VickPos = VehicleSpawn(sModel, PedPos, PedRot, bAddPlayer, true);
            }
            else
                VickPos = null;

            return VickPos;
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
                    sVehicles.Add("SQUADDIE"); 
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
        public Vehicle VehicleSpawn(string sVehModel, Vector3 VecLocal, float VecHead, bool bAddPlayer, bool bRandomFeat)
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

                Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, iVehHash);
            }
            else
                BuildVehicle = null;

            return BuildVehicle;
        }
        public Vector3 FindAPed(Vector3 vZone, float fRadius, bool bReplace, int iReload)
        {
            GetLogging("FindAPed, iReload == " + iReload);

            Ped VPedPos = DoStopTillDrop(vZone, fRadius);
            Vector3 PedPos = Vector3.Zero;
            while (VPedPos == null && bSearching)
            {
                PlayerZerosAI();
                Script.Wait(100);
                VPedPos = DoStopTillDrop(Game.Player.Character.Position, 150.00f);
            }
            bStuckOnYou = false;
            if (bSearching)
            {
                PedPos = VPedPos.Position;
                float PedRot = VPedPos.Heading;
                VPedPos.Delete();
                if (bReplace)
                    GenPlayerPed(PedPos, PedRot, null, 0, iReload);
            }

            return PedPos;
        }
        public Ped DoStopTillDrop(Vector3 vZone, float fRadius)
        {
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
        public Ped GenPlayerPed(Vector3 vLocal, float fAce, Vehicle vMyCar, int iSeat, int iReload)
        {
            GetLogging("GenPlayerPed, iSeat == " + iSeat + ", iReload == " + iReload);

            Ped MyPed = null;
            bool bMale = false;
            string sPeddy = "mp_f_freemode_01";

            if (iReload == -1)
            {
                if (BOnBOff(1))
                {
                    bMale = true;
                    sPeddy = "mp_m_freemode_01";
                }
            }
            else
            {
                iReload = ReteaveBrain(iReload);
                if (PedList[iReload].PFMySetting.PFMale)
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
                    Function.Call(Hash.SET_PED_ACCURACY, MyPed, iAccuracy);
                    MyPed.MaxHealth = RandInt(200, 400);
                    MyPed.Health = MyPed.MaxHealth;

                    if (iReload == -1)
                    {
                        PedFixtures MyFixtures = new PedFixtures();
                        MyFixtures.PFMale = bMale;

                        OnlineFaceTypes(MyPed, bMale, vMyCar, iSeat, MyFixtures, iReload);
                    }
                    else
                        OnlineFaceTypes(MyPed, bMale, vMyCar, iSeat, null, PedList[iReload].iLevel);
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

            Function.Call(Hash.SET_PED_INTO_VEHICLE, Peddy, Vhic, iSeat);
        }
        private void EnterAnyVeh(Vehicle Vhic, Ped Peddy, float Run, int iBPed)
        {
            GetLogging("EnterAnyVeh, iBPed == " + iBPed);

            bool bFound = false;
            Peddy.BlockPermanentEvents = true;
            if (Vhic.Exists())
            {
                int iSeats = 0;
                while (!bFound && iSeats < Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, Vhic.Handle))
                {
                    if (Function.Call<bool>(Hash.IS_VEHICLE_SEAT_FREE, Vhic.Handle, iSeats))
                    {
                        bFound = true;
                        break;
                    }
                    else
                        iSeats += 1;
                }
                if (bFound)
                {
                    if (Peddy.Position.DistanceTo(Vhic.Position) < 65.00f)
                    {
                        while (!Peddy.IsInVehicle(Vhic) && !Peddy.IsDead && !Peddy.IsFalling && bSearching)
                        {
                            if (Peddy.Position.DistanceTo(Vhic.Position) > 65.00f || Game.Player.Character.Position.DistanceTo(Peddy.Position) > 65.00f)
                                WarptoAnyVeh(Vhic, Peddy, iSeats);
                            else if (!Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, Peddy))
                                Function.Call(Hash.TASK_ENTER_VEHICLE, Peddy, Vhic, -1, iSeats, Run, 1, 0);

                            Script.Wait(100);
                        }
                    }
                    else
                        WarptoAnyVeh(Vhic, Peddy, iSeats);
                }
                else
                {
                    if (iBPed != -1)
                    {
                        iBPed = ReteaveBrain(iBPed);
                        if (PedList[iBPed].ThisVeh != null)
                            PedList[iBPed].ThisVeh.MarkAsNoLongerNeeded();

                        if (!bStuckOnYou)
                        {
                            PedList[iBPed].ThisVeh = FindNearestVeh(Game.Player.Character.Position, RandVeh(RandInt(1, 9)), false);
                            while (!Peddy.IsInVehicle() && !Peddy.IsDead && !Peddy.IsFalling && PedList[iBPed].ThisVeh != null && Game.Player.Character.IsInVehicle() && bSearching)
                            {
                                if (Peddy.Position.DistanceTo(Vhic.Position) > 65.00f || Game.Player.Character.Position.DistanceTo(Peddy.Position) > 65.00f)
                                    WarptoAnyVeh(PedList[iBPed].ThisVeh, Peddy, -1);
                                else if (!Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, Peddy))
                                    Function.Call(Hash.TASK_ENTER_VEHICLE, Peddy, PedList[iBPed].ThisVeh, -1, -1, Run, 1, 0);

                                Script.Wait(100);
                            }
                            if (bSearching)
                                PedList[iBPed].bDriver = true;
                        }
                    }
                }
            }
            if (bSearching)
                Peddy.BlockPermanentEvents = false;
        }
        private void GetOutVehicle(Ped Peddy, int iPed)
        {
            GetLogging("GetOutVehicle");

            if (Peddy.IsInVehicle())
            {
                Vehicle PedVeh = Peddy.CurrentVehicle;
                Function.Call(Hash.TASK_LEAVE_VEHICLE, Peddy, PedVeh, 4160);
            }
            if (iPed != -1)
            {
                iPed = ReteaveBrain(iPed);
                ClearPedBlips(PedList[iPed].iLevel);
                PedList[iPed].DirBlip = DirectionalBlimp(PedList[iPed].ThisPed);
                PedList[iPed].ThisBlip = PedBlimp(PedList[iPed].ThisPed, 1, PedList[iPed].sMyName, PedList[iPed].iColours);
            }
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

            Peddy.RelationshipGroup = Game.Player.Character.RelationshipGroup;
            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, iPlayerGroups);
            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, Peddy.Handle, Game.Player.Character.Handle, 0.0f, 3.0f, 0.0f, 1.0f, -1, 0.5f, true);
            Function.Call(Hash.SET_PED_CAN_TELEPORT_TO_GROUP_LEADER, Peddy.Handle, iFollowingNPCs, true);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.BlockPermanentEvents = false;
            Peddy.AlwaysKeepTask = true;
            Peddy.CanBeTargetted = true;
        }
        private void ExMember(Ped Peddy)
        {
            GetLogging("ExMember");

            if (Function.Call<bool>(Hash.IS_PED_GROUP_MEMBER, Peddy.Handle, iPlayerGroups))
                Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
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
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, Game.Player.Character.Position, 1.00f, 45.00f, 0);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, Game.Player.Character.Position, 10.00f, 25.00f, 0);

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

            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy.Handle);
            Peddy.IsEnemy = true;
            Peddy.CanBeTargette﻿d﻿ = true;
            Peddy.RelationshipGroup = iAttackingNPCs;
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

            iPed = ReteaveBrain(iPed);

            Ped Peddy = PedList[iPed].ThisPed;
            if (RandInt(0, 50) < 25)
                HeliFighter(Peddy, iPed);
            else
                LaserFighter(Peddy, iPed);
        }
        private void HeliFighter(Ped Peddy,int iPed)
        {
            GetLogging("HeliFighter, iPed == " + iPed);

            ClearPedBlips(PedList[iPed].iLevel);
            string sVeh = RandVeh(13);
            Vehicle vHeli = VehicleSpawn(sVeh, new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 250.00f), 0.00f, false, false);
            WarptoAnyVeh(vHeli, Peddy, -1);
            PedList[iPed].ThisVeh = vHeli;
            PedList[iPed].ThisBlip = PedBlimp(Peddy, 422, PedList[iPed].sMyName, PedList[iPed].iColours);
            FlyAway(Peddy, Game.Player.Character.Position, 250.00f, 0.00f);
            Function.Call(Hash.SET_PED_FIRING_PATTERN, Peddy.Handle, Function.Call<int>(Hash.GET_HASH_KEY, "FIRING_PATTERN_BURST_FIRE_HELI"));
            Peddy.AlwaysKeepTask = true;
            Peddy.BlockPermanentEvents = true;
        }
        private void LaserFighter(Ped Peddy, int iPed)
        {
            GetLogging("LaserFighter, iPed == " + iPed);

            ClearPedBlips(PedList[iPed].iLevel);
            string sVeh = RandVeh(12);
            Vehicle vPlane = VehicleSpawn(sVeh, new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 1550.00f), 0.00f, false, false);
            WarptoAnyVeh(vPlane, Peddy, -1);
            PedList[iPed].ThisVeh = vPlane;
            PedList[iPed].ThisBlip = PedBlimp(Peddy, 424, PedList[iPed].sMyName, PedList[iPed].iColours);
            Function.Call(Hash.TASK_PLANE_MISSION, Peddy, vPlane, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, 180.0f, 1000.0f, -5000.0f);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy, 0, true);
            Peddy.AlwaysKeepTask = true;
            Peddy.BlockPermanentEvents = true;
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

            if (Game.Player.Character.Position.DistanceTo(new Vector3(-778.81F, 312.66F, 84.70F)) < 80.00f)
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

            //Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy);
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
        public int NearHiest(bool bLaunch)
        {
            GetLogging("NearHiest, bLaunch == " + bLaunch);

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
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING,"Player " + sName);
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, "Player " + sName);
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

            int iNumber = LessRandomz(1, 1000, 11);

            while (BrainNumberCheck(iNumber))
            {
                iNumber = LessRandomz(1, 400, 11);
            }
            return iNumber;
        }
        public bool BrainNumberCheck(int iNumber)
        {
            GetLogging("BrainNumberCheck, iNumber == " + iNumber);

            bool bRunAgain = false;
            for (int i = 0; i < PedList.Count; i++)
            {
                if (PedList[i].iLevel == iNumber)
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
                if (PedList[i].iLevel == iNumber)
                {
                    iAm = i;
                    break;
                }
            }

            return iAm;
        }
        private void NpcBrains(Ped Peddy, Vehicle VeHic, int iSeat, PedFixtures Fixtures, int iReload)
        {
            GetLogging("NpcBrains, iSeat == " + iSeat + ", iReload == " + iReload);

            if (iReload != -1)
            {
                iReload = ReteaveBrain(iReload);
                PedList[iReload].ThisPed = Peddy;
                PedList[iReload].DirBlip = DirectionalBlimp(Peddy);
                PedList[iReload].ThisBlip = PedBlimp(Peddy, 1, PedList[iReload].sMyName, PedList[iReload].iColours);
                PedList[iReload].iDeathSequence = 0;

                Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, Peddy.Handle, 2);
                Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 0, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 1, true);
                if (iAggression > 2)
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 2, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 3, true);
                Peddy.CanBeTargetted = true;

                if (!PedList[iReload].bFriendly)
                {
                    if (PedList[iReload].iOffRadar == 0 && RandInt(0, 40) < 10)
                        PedList[iReload].iOffRadar = -1;
                    FightPlayer(Peddy, false);
                }
                else
                {
                    if (PedList[iReload].bFollower)
                        FolllowTheLeader(Peddy);
                    else
                        Peddy.Task.WanderAround();
                }

                GunningIt(Peddy);
            }
            else
            {
                PlayerBrain MyBrains = new PlayerBrain();

                MyBrains.ThisPed = Peddy;
                MyBrains.iDeathSequence = 0;
                MyBrains.iDeathTime = 0;
                MyBrains.iTimeOn = Game.GameTime + RandInt(iMinSession, iMaxSession);
                MyBrains.sMyName = SillyNameList();
                MyBrains.iLevel = UniqueLevels();
                MyBrains.iKilled = 0;
                MyBrains.iKills = 0;
                MyBrains.iFindPlayer = 0;
                MyBrains.iColours = 0;
                MyBrains.iOffRadar = 0;
                MyBrains.bOffRadar = false;
                MyBrains.bFriendly = true;
                MyBrains.bHacker = false;
                MyBrains.bInCombat = false;
                MyBrains.bBounty = false;
                MyBrains.bHorny = false;
                MyBrains.bHorny2 = false;
                MyBrains.bFollower = false;
                MyBrains.bSessionJumper = false;
                MyBrains.bApprochPlayer = false;
                MyBrains.bDriver = false;
                MyBrains.bPassenger = false;
                MyBrains.DirBlip = null;

                if (iSeat == -1)
                    MyBrains.ThisVeh = VeHic;
                else
                    MyBrains.ThisVeh = null;

                Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, Peddy.Handle, 2);
                Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, Peddy.Handle, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 0, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 1, true);
                if (iAggression > 3)
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 2, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 3, true);
                Peddy.CanBeTargetted = true;
                //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 52, true);

                int iBrain = 0;
                if (iAggression == 11 && !bHackerIn && LessRandomz(0, 40, 0) < 10)
                    iBrain = 4;
                else if (iAggression < 3)
                {
                    iBrain = LessRandomz(0, 40, 0);
                    if (iBrain < 5)
                        iBrain = 2;
                    else
                        iBrain = 1;
                }
                else if (iAggression < 7)
                {
                    iBrain = LessRandomz(0, 40, 0);
                    if (iBrain < 5)
                        iBrain = 2;
                    else if (iBrain < 15)
                        iBrain = 3;
                    else
                        iBrain = 1;
                }
                else
                    iBrain = 3;

                if (iBrain == 1)
                {
                    Peddy.Task.WanderAround();
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.sMyName, 0);
                    Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, iFollowingNPCs);
                }            //Friend
                else if (iBrain == 2)
                {
                    Peddy.Task.WanderAround();
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.sMyName, 0);
                    MyBrains.bSessionJumper = true;
                }       //Disconect
                else if (iBrain == 3)
                {
                    FightPlayer(Peddy, false);
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 1, MyBrains.sMyName, 1);
                    MyBrains.iColours = 1;
                    MyBrains.bFriendly = false;
                    Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, iWarringNPCs);
                }       //Enemy
                else
                {
                    bHackerIn = true;
                    MyBrains.DirBlip = DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = PedBlimp(Peddy, 163, MyBrains.sMyName, 1);
                    MyBrains.iTimeOn = Game.GameTime + 60000;
                    MyBrains.iColours = 1;
                    bHackEvent = false;
                    MyBrains.bFriendly = false;
                    MyBrains.bHacker = true;
                    Peddy.IsInvincible = true;
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
                        DriveTooo(Peddy, !MyBrains.bFriendly);
                        int iClass = 0;
                        if (VeHic.ClassType == VehicleClass.Helicopters)
                            iClass = 422;
                        else if (VeHic.ClassType == VehicleClass.Motorcycles)
                            iClass = 348;
                        else if (VeHic.ClassType == VehicleClass.Planes)
                            iClass = 424;
                        else
                            iClass = 225;
                        MyBrains.ThisBlip = PedBlimp(Peddy, OhMyBlip(VeHic.Model.Hash, iClass), MyBrains.sMyName, MyBrains.iColours);

                        if (MyBrains.bFriendly)
                            MyBrains.ThisPed.CanBeDraggedOutOfVehicle = false;
                        else
                            Function.Call(Hash.SET_VEHICLE_IS_WANTED, VeHic, true);

                        MyBrains.bApprochPlayer = true;
                        MyBrains.bDriver = true;
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
                        MyBrains.bPassenger = true;
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
                    MyBrains.ThisBlip = PedBlimp(Peddy, 303, MyBrains.sMyName, 3);
                    MyBrains.bBounty = true;
                }

                MyBrains.PFMySetting = Fixtures;

                PedList.Add(MyBrains);

                BackItUpBrain();

                if (iAggression > 1)
                    GunningIt(Peddy);

                iCurrentPlayerz += 1;
            }
        }
        public class PlayerBrain
        {
            public Ped ThisPed { get; set; }
            public Vehicle ThisVeh { get; set; }
            public Blip ThisBlip { get; set; }
            public Blip DirBlip { get; set; }
            public int iDeathSequence { get; set; }
            public int iDeathTime { get; set; }
            public int iColours { get; set; }
            public int iTimeOn { get; set; }
            public int iLevel { get; set; }
            public int iKills { get; set; }
            public int iKilled { get; set; }
            public int iOffRadar { get; set; }
            public bool bOffRadar { get; set; }
            public bool bBounty { get; set; }
            public bool bHacker { get; set; }
            public bool bInCombat { get; set; }
            public bool bFriendly { get; set; }
            public bool bFollower { get; set; }
            public bool bApprochPlayer { get; set; }
            public bool bSessionJumper { get; set; }
            public bool bHorny { get; set; }
            public bool bHorny2 { get; set; }
            public bool bDriver { get; set; }
            public bool bPassenger { get; set; }
            public int iFindPlayer { get; set; }
            public string sMyName { get; set; }
            public PedFixtures PFMySetting { get; set; }
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

            while (!bSearching)
            {
                Script.Wait(RandInt(500, 2000));
                PlayerDump();
                if (PedList.Count == 0)
                    bSearching = true;
            }
        }
        private void PlayerDump()
        { 
            GetLogging("PlayerDump");

            for (int i = 0; i < PedList.Count; i++)
            {
                GetOutVehicle(PedList[i].ThisPed, PedList[i].iLevel);
                PedCleaning(PedList[i].iLevel, "left", false);
            }

            for (int i = 0; i < AFKList.Count; i++)
            {
                DeListingBrains(false, i, true);
                iCurrentPlayerz -= 1;
            }
        }
        public int OhMyBlip(int iVehHash, int iBeLip)
        {
            GetLogging("OhMyBlip, iVehHash == " + iVehHash + ", iBeLip == " + iBeLip);

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
            public List<int> PFHeadColour { get; set; }
            public List<float> PFAmount { get; set; }

            public int PFHair01 { get; set; }
            public int PFHair02 { get; set; }

            public List<int> PFClothA { get; set; }
            public List<int> PFClothB { get; set; }

            public List<int> PFExtraA { get; set; }
            public List<int> PFExtraB { get; set; }

            public PedFixtures()
            {
                PFFeature = new List<int>();
                PFChange = new List<int>();
                PFColour = new List<int>();
                PFHeadColour = new List<int>();
                PFAmount = new List<float>();
                PFClothA = new List<int>();
                PFClothB = new List<int>();
                PFExtraA = new List<int>();
                PFExtraB = new List<int>();
            }
        }
        private void OnlineFaceTypes(Ped Pedx, bool bMale, Vehicle vMyCar, int iSeat, PedFixtures Fixtures, int iReload)
        {
            GetLogging("OnlineFaceTypes, iSeat == " + iSeat + ", iReload == " + iReload);

            if (iReload != -1)
                iReload = ReteaveBrain(iReload);

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

            if (Fixtures != null)
            {
                Fixtures.PFshapeFirstID = shapeFirstID;
                Fixtures.PFshapeSecondID = shapeSecondID;
                Fixtures.PFshapeThirdID = shapeThirdID;
                Fixtures.PFskinFirstID = skinFirstID;
                Fixtures.PFskinSecondID = skinSecondID;
                Fixtures.PFskinThirdID = skinThirdID;
                Fixtures.PFshapeMix = shapeMix;
                Fixtures.PFskinMix = skinMix;
                Fixtures.PFthirdMix = thirdMix;
            }
            else
            {
                shapeFirstID = PedList[iReload].PFMySetting.PFshapeFirstID;
                shapeSecondID = PedList[iReload].PFMySetting.PFshapeSecondID;
                shapeThirdID = PedList[iReload].PFMySetting.PFshapeThirdID;
                skinFirstID = PedList[iReload].PFMySetting.PFskinFirstID;
                skinSecondID = PedList[iReload].PFMySetting.PFskinSecondID;
                skinThirdID = PedList[iReload].PFMySetting.PFskinThirdID;
                shapeMix = PedList[iReload].PFMySetting.PFshapeMix;
                skinMix = PedList[iReload].PFMySetting.PFskinMix;
                thirdMix = PedList[iReload].PFMySetting.PFthirdMix;
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

                if (Fixtures != null)
                {
                    Fixtures.PFFeature.Add(iChange);
                    Fixtures.PFColour.Add(AddColour);
                    Fixtures.PFAmount.Add(fVar);
                }
                else
                {
                    iChange = PedList[iReload].PFMySetting.PFFeature[iFeature];
                    AddColour = PedList[iReload].PFMySetting.PFColour[iFeature];
                    fVar = PedList[iReload].PFMySetting.PFAmount[iFeature];
                }

                Function.Call(Hash.SET_PED_HEAD_OVERLAY, Pedx.Handle, iFeature, iChange, fVar);

                if (iColour > 0)
                    Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, Pedx.Handle, iFeature, iColour, AddColour, 0);

                int iCount = Function.Call<int>(Hash.GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS, Game.Player.Character, 2);
                int iAm = RandInt(1, iCount);
                while (iAm == 24)
                    iAm = RandInt(1, iCount);

                if (Fixtures != null)
                    Fixtures.PFHeadColour.Add(iAm);
                else
                    iAm = PedList[iReload].PFMySetting.PFHeadColour[iFeature];

                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, 2, iAm, 0, 2);//hair

                iFeature += 1;
            }
            int iHair = RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));
            int iHair2 = RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));

            if (Fixtures != null)
            {
                Fixtures.PFHair01 = iHair;
                Fixtures.PFHair02 = iHair2;
            }
            else
            {
                iHair = PedList[iReload].PFMySetting.PFHair01;
                iHair2 = PedList[iReload].PFMySetting.PFHair02;
            }

            Function.Call(Hash._SET_PED_HAIR_COLOR, Pedx.Handle, iHair, iHair2);

            if (iReload != -1)
                OnlineWardrobe(Pedx, bMale, vMyCar, iSeat, Fixtures, PedList[iReload].iLevel);
            else
                OnlineWardrobe(Pedx, bMale, vMyCar, iSeat, Fixtures, iReload);
        }
        private void OnlineWardrobe(Ped Pedx, bool bMale, Vehicle vMyCar, int iSeat, PedFixtures Fixtures, int iReload)
        {
            GetLogging("OnlineWardrobe, iSeat == " + iSeat + ", iReload == " + iReload);

            if (iReload != -1)
                iReload = ReteaveBrain(iReload);

            if (Fixtures != null)
            {
                ClothBank MyWard = new ClothBank();
                Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx);

                if (bMale)
                {
                    if (MaleCloth.Count > 0)
                        MyWard = MaleCloth[RandInt(0, MaleCloth.Count - 1)];
                    else
                    {
                        int RanChar = RandInt(1, 6);
                        if (RanChar == 1)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(12);
                            MyWard.ClothB.Add(4);

                            MyWard.ClothA.Add(1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(1);
                            MyWard.ClothB.Add(5);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(65);
                            MyWard.ClothB.Add(3);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(22);
                            MyWard.ClothB.Add(4);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(11);
                            MyWard.ClothB.Add(11);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 2)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(14);
                            MyWard.ClothB.Add(3);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(17);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(23);
                            MyWard.ClothB.Add(4);

                            MyWard.ClothA.Add(40);
                            MyWard.ClothB.Add(1);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(26);
                            MyWard.ClothB.Add(3);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(35);
                            MyWard.ClothB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 3)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(147);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(167);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(33);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(36);
                            MyWard.ClothB.Add(1);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(286);
                            MyWard.ClothB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 4)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(11);
                            MyWard.ClothB.Add(4);

                            MyWard.ClothA.Add(19);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(88);
                            MyWard.ClothB.Add(7);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(14);
                            MyWard.ClothB.Add(2);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(273);
                            MyWard.ClothB.Add(15);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 5)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(125);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(114);
                            MyWard.ClothB.Add(6);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(78);
                            MyWard.ClothB.Add(6);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(287);
                            MyWard.ClothB.Add(6);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(134);
                            MyWard.ClothB.Add(8);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(3);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(106);
                            MyWard.ClothB.Add(8);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(83);
                            MyWard.ClothB.Add(8);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(274);
                            MyWard.ClothB.Add(8);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                    }
                }
                else
                {
                    if (FemaleCloth.Count > 0)
                        MyWard = FemaleCloth[RandInt(0, FemaleCloth.Count - 1)];
                    else
                    {
                        int RanChar = RandInt(1, 5);
                        if (RanChar == 1)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(146);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(1);

                            MyWard.ClothA.Add(113);
                            MyWard.ClothB.Add(1);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(23);
                            MyWard.ClothB.Add(8);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(287);
                            MyWard.ClothB.Add(1);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 2)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(11);
                            MyWard.ClothB.Add(3);

                            MyWard.ClothA.Add(169);
                            MyWard.ClothB.Add(12);

                            MyWard.ClothA.Add(93);
                            MyWard.ClothB.Add(4);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(3);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 3)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(13);
                            MyWard.ClothB.Add(3);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(98);
                            MyWard.ClothB.Add(4);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(71);
                            MyWard.ClothB.Add(4);

                            MyWard.ClothA.Add(1);
                            MyWard.ClothB.Add(5);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(254);
                            MyWard.ClothB.Add(4);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 4)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(10);
                            MyWard.ClothB.Add(1);

                            MyWard.ClothA.Add(15);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(9);
                            MyWard.ClothB.Add(6);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(54);
                            MyWard.ClothB.Add(3);

                            MyWard.ClothA.Add(100);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(36);
                            MyWard.ClothB.Add(1);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(13);
                            MyWard.ClothB.Add(5);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else if (RanChar == 5)
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(2);
                            MyWard.ClothB.Add(3);

                            MyWard.ClothA.Add(11);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(75);
                            MyWard.ClothB.Add(1);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(20);
                            MyWard.ClothB.Add(5);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(208);
                            MyWard.ClothB.Add(4);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                        else
                        {
                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(134);
                            MyWard.ClothB.Add(8);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(13);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(113);
                            MyWard.ClothB.Add(8);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(87);
                            MyWard.ClothB.Add(8);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(-1);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(0);
                            MyWard.ClothB.Add(0);

                            MyWard.ClothA.Add(287);
                            MyWard.ClothB.Add(8);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);

                            MyWard.ExtraA.Add(0);
                            MyWard.ExtraB.Add(0);
                        }
                    }
                }

                Fixtures.PFClothA = MyWard.ClothA;
                Fixtures.PFClothB = MyWard.ClothB;

                Fixtures.PFExtraA = MyWard.ExtraA;
                Fixtures.PFExtraB = MyWard.ExtraB;

                OnlineSavedWard(Pedx, MyWard);
            }
            else
            {
                ClothBank DisBank = new ClothBank();

                DisBank.ClothA = PedList[iReload].PFMySetting.PFClothA;
                DisBank.ClothB = PedList[iReload].PFMySetting.PFClothB;

                DisBank.ExtraA = PedList[iReload].PFMySetting.PFExtraA;
                DisBank.ExtraB = PedList[iReload].PFMySetting.PFExtraB;

                OnlineSavedWard(Pedx, DisBank);
            }

            if (iReload != -1)
                NpcBrains(Pedx, vMyCar, iSeat, Fixtures, PedList[iReload].iLevel);
            else
                NpcBrains(Pedx, vMyCar, iSeat, Fixtures, iReload);
        }
        private void OnlineSavedWard(Ped Pedx, ClothBank MyCloths)
        {
            GetLogging("OnlineSavedWard");

            Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx);

            for (int i = 0; i < MyCloths.ClothA.Count; i++)
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx, i, MyCloths.ClothA[i], MyCloths.ClothB[i], 2);

            for (int i = 0; i < MyCloths.ExtraA.Count; i++)
                Function.Call(Hash.SET_PED_PROP_INDEX, Pedx, i, MyCloths.ExtraA[i], MyCloths.ExtraB[i], false);
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

            if (ButtonDown(iClearPlayList, false) && !bDisabled)
                LaggOut();
            else if (ButtonDown(iDisableMod, false))
                bDisabled = !bDisabled;
        }
        private void FireOrb(int MyBrian, Ped Target)
        {
            GetLogging("FireOrb, MyBrian == " + MyBrian);

            Ped pFired = Game.Player.Character;

            if (MyBrian != -1)
            {
                MyBrian = ReteaveBrain(MyBrian);

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

                ClearPedBlips(PedList[MyBrian].iLevel);
                PedList[MyBrian].ThisBlip = LocalBlip(FacList[RandInt(0, FacList.Count - 1)], 590, PedList[MyBrian].sMyName);

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
                    OrbLoad(PedList[MyBrian].sMyName);
                    Script.Wait(4000);
                    PedCleaning(PedList[MyBrian].iLevel, "left", false);
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
            GetLogging("WhileYouDead, string == " + Kellar + ", iKills == " + iKills + ", iKilled == " + iKilled);

            while (Game.Player.Character.GetKiller() == Peddy)
                Script.Wait(1);
            Script.Wait(1000);
            UI.Notify("You  " + iKills + " - " + iKilled + " " + Kellar);
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
                    else if (PedList[iBe].iTimeOn < Game.GameTime)
                    {
                        GetOutVehicle(PedList[iBe].ThisPed, PedList[iBe].iLevel);
                        PedCleaning(PedList[iBe].iLevel, "left", false);
                    }
                    else if (Game.Player.Character.GetKiller() == PedList[iBe].ThisPed)
                    {
                        PedList[iBe].iKills += 1;
                        WhileYouDead(PedList[iBe].sMyName, PedList[iBe].iKilled, PedList[iBe].iKills, PedList[iBe].ThisPed);
                        if (iAggression < 6)
                            PedCleaning(PedList[iBe].iLevel, "left", false);
                    }
                    else if (PedList[iBe].ThisPed.IsDead)
                    {
                        if (PedList[iBe].iDeathSequence == 0)
                        {
                            if (PedList[iBe].ThisVeh != null)
                            {
                                EmptyVeh(PedList[iBe].ThisVeh);
                                PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                PedList[iBe].ThisVeh = null;
                            }

                            int iDie = WhoShotMe(PedList[iBe].ThisPed);

                            ClearPedBlips(PedList[iBe].iLevel);

                            if (PedList[iBe].ThisPed.GetKiller() == Game.Player.Character)
                            {
                                if (PedList[iBe].bBounty)
                                    Game.Player.Money += 7000;
                                PedList[iBe].bFriendly = false;
                                PedList[iBe].iColours = 1;
                                PedList[iBe].bApprochPlayer = false;
                                PedList[iBe].bFollower = false;
                                PedList[iBe].iKilled += 1;
                                UI.Notify("You  " + PedList[iBe].iKilled + " - " + PedList[iBe].iKills + " " + PedList[iBe].sMyName);
                            }
                            else if (iDie != -1)
                                UI.Notify(PedList[iDie].sMyName + " Killed " + PedList[iBe].sMyName);
                            else
                                UI.Notify(PedList[iBe].sMyName + " died");

                            PedList[iBe].bBounty = false;
                            PedList[iBe].iDeathSequence += 1;
                            PedList[iBe].iDeathTime = Game.GameTime + 10000;
                            PedList[iBe].iTimeOn += 60000;
                        }
                        else if (PedList[iBe].iDeathSequence == 1 || PedList[iBe].iDeathSequence == 3 || PedList[iBe].iDeathSequence == 5 || PedList[iBe].iDeathSequence == 7)
                        {
                            if (PedList[iBe].iDeathTime < Game.GameTime)
                            {
                                PedList[iBe].ThisPed.Alpha = 80;
                                PedList[iBe].iDeathSequence += 1;
                                PedList[iBe].iDeathTime = Game.GameTime + 500;
                            }
                        }
                        else if (PedList[iBe].iDeathSequence == 2 || PedList[iBe].iDeathSequence == 4 || PedList[iBe].iDeathSequence == 6)
                        {
                            if (PedList[iBe].iDeathTime < Game.GameTime)
                            {
                                PedList[iBe].ThisPed.Alpha = 255;
                                PedList[iBe].iDeathSequence += 1;
                                PedList[iBe].iDeathTime = Game.GameTime + 500;
                            }
                        }
                        else if (PedList[iBe].iDeathSequence == 8)
                        {
                            if (!bStuckOnYou)
                            {
                                if (PedList[iBe].iDeathTime < Game.GameTime)
                                {
                                    if (PedList[iBe].iKilled > RandInt(13, 22) || iAggression < 2)
                                    {
                                        PedCleaning(PedList[iBe].iLevel, "left", false);
                                    }
                                    else if (PedList[iBe].iKilled > 15 && PedList[iBe].iKills == 0 && iAggression > 7)
                                        FireOrb(PedList[iBe].iLevel, Game.Player.Character);
                                    else
                                    {
                                        ClearPedBlips(PedList[iBe].iLevel);
                                        PedList[iBe].iDeathSequence = 10;
                                        PedList[iBe].ThisPed.Delete();
                                        PedList[iBe].ThisPed = null;
                                        bStuckOnYou = true;
                                        Vector3 Posy = FindAPed(Game.Player.Character.Position, 95.00f, true, PedList[iBe].iLevel);
                                    }
                                }
                            }
                        }
                    }
                    else if (PedList[iBe].ThisPed.Position.Z < -90.00f)
                    {
                        PedList[iBe].ThisPed.Kill();
                    }
                    else if (PedList[iBe].bHacker && !bHackEvent)
                    {
                        if (PedList[iBe].ThisPed.Position.DistanceTo(Game.Player.Character.Position) < 40.00f)
                        {
                            bHackEvent = true;
                            HackerTime(PedList[iBe].ThisPed);
                        }
                    }
                    else if (PedList[iBe].bSessionJumper)
                    {
                        if (PedList[iBe].ThisPed.Position.DistanceTo(Game.Player.Character.Position) < 10.00f)
                            PedCleaning(PedList[iBe].iLevel, "has disappeared", true);
                    }
                    else if (PedList[iBe].bDriver)
                    {
                        if (PedList[iBe].ThisPed.IsInVehicle())
                        {
                            if (PedList[iBe].bFollower)
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
                                        PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 225, PedList[iBe].sMyName, PedList[iBe].iColours);

                                    if (Game.Player.Character.Position.DistanceTo(PedList[iBe].ThisPed.Position) > 25.00f)
                                    {
                                        if (PedList[iBe].iFindPlayer < Game.GameTime)
                                        {
                                            PedList[iBe].iFindPlayer = Game.GameTime + 5000;
                                            DriveTooo(PedList[iBe].ThisPed, false);
                                        }
                                    }
                                }
                                else
                                {
                                    GetOutVehicle(PedList[iBe].ThisPed, PedList[iBe].iLevel);
                                    if (PedList[iBe].ThisVeh != null)
                                    {
                                        PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                        PedList[iBe].ThisVeh = null;
                                    }
                                    PedList[iBe].bPassenger = false;
                                    PedList[iBe].bDriver = false;
                                    Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, PedList[iBe].ThisPed, Game.Player.Character.Handle, 0.0f, 3.0f, 0.0f, 1.0f, -1, 0.5f, true);
                                }
                            }
                            else if (PedList[iBe].bApprochPlayer)
                            {
                                if (iAggression < 6 && PedList[iBe].bFriendly && iFollow < 8)
                                {
                                    if (Game.Player.Character.Position.DistanceTo(PedList[iBe].ThisPed.Position) < 5.00f)
                                    {
                                        if (!PedList[iBe].bHorny)
                                        {
                                            PedList[iBe].bHorny = true;
                                            PedList[iBe].ThisVeh.SoundHorn(3000);
                                            TopLeftUI("Press" + ControlSybols(23) + "to enter vehicle");
                                        }
                                        else if (!Game.Player.Character.IsInVehicle())
                                        {
                                            if (ButtonDown(23, true))
                                            {
                                                PedList[iBe].iTimeOn += 60000;
                                                EnterAnyVeh(PedList[iBe].ThisVeh, Game.Player.Character, 2.00f, -1);
                                            }
                                        }
                                        else
                                        {
                                            PedList[iBe].bApprochPlayer = false;
                                            if (Game.Player.Character.IsInVehicle(PedList[iBe].ThisVeh))
                                            {
                                                PedList[iBe].iColours = 38;
                                                PedList[iBe].bFollower = true;
                                                FolllowTheLeader(PedList[iBe].ThisPed);
                                                iFollow += 1;
                                            }
                                            DriveAround(PedList[iBe].ThisPed);
                                        }
                                    }
                                    else if (Game.Player.Character.Position.DistanceTo(PedList[iBe].ThisPed.Position) > 25.00f)
                                    {
                                        if (PedList[iBe].iFindPlayer < Game.GameTime)
                                        {
                                            PedList[iBe].iFindPlayer = Game.GameTime + 5000;
                                            DriveTooo(PedList[iBe].ThisPed, false);
                                        }
                                    }
                                }
                                else
                                {
                                    if (Game.Player.Character.Position.DistanceTo(PedList[iBe].ThisPed.Position) < 5.00f)
                                    {
                                        PedList[iBe].bApprochPlayer = false;
                                        PedList[iBe].ThisVeh.SoundHorn(3000);
                                    }
                                    else if (Game.Player.Character.Position.DistanceTo(PedList[iBe].ThisPed.Position) > 45.00f)
                                    {
                                        if (PedList[iBe].iFindPlayer < Game.GameTime)
                                        {
                                            PedList[iBe].iFindPlayer = Game.GameTime + 5000;
                                            DriveBye(PedList[iBe].ThisPed);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (PedList[iBe].ThisVeh != null)
                            {
                                PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                PedList[iBe].ThisVeh = null;
                            }
                            PedList[iBe].bApprochPlayer = false;
                            PedList[iBe].bDriver = false;
                            ClearPedBlips(PedList[iBe].iLevel);
                            PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                            PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].sMyName, PedList[iBe].iColours);
                        }
                    }
                    else if (PedList[iBe].bPassenger)
                    {
                        if (PedList[iBe].bFollower)
                        {
                            if (!Game.Player.Character.IsInVehicle())
                            {
                                PedList[iBe].bPassenger = false;
                                ClearPedBlips(PedList[iBe].iLevel);
                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].sMyName, PedList[iBe].iColours);
                                PedList[iBe].ThisPed.Task.LeaveVehicle();
                                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, PedList[iBe].ThisPed, Game.Player.Character.Handle, 0.0f, 3.0f, 0.0f, 1.0f, -1, 0.5f, true);
                            }
                        }
                        else
                        {
                            if (!PedList[iBe].ThisPed.IsInVehicle())
                            {
                                PedList[iBe].bPassenger = false;
                                ClearPedBlips(PedList[iBe].iLevel);
                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].sMyName, PedList[iBe].iColours);
                            }
                        }
                    }
                    else if (PedList[iBe].bFollower)
                    {
                        if (Game.Player.Character.IsInVehicle())
                        {
                            PedList[iBe].bPassenger = true;
                            Vehicle DisVeh = Game.Player.Character.CurrentVehicle;
                            EnterAnyVeh(DisVeh, PedList[iBe].ThisPed, 2.00f, PedList[iBe].iLevel);
                            ClearPedBlips(PedList[iBe].iLevel);
                        }
                    }
                    else if (PedList[iBe].bFriendly)
                    {
                        if (PedList[iBe].ThisPed.HasBeenDamagedBy(Game.Player.Character) || PedList[iBe].ThisPed.IsInCombatAgainst(Game.Player.Character) && iAggression > 2)
                        {
                            ClearPedBlips(PedList[iBe].iLevel);
                            ExMember(PedList[iBe].ThisPed);
                            PedList[iBe].iColours = 1;
                            FightPlayer(PedList[iBe].ThisPed, false);
                            PedList[iBe].bFriendly = false;
                            if (PedList[iBe].bFollower)
                            {
                                PedList[iBe].bFollower = false;
                                iFollow -= 1;
                            }
                            PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                            PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].sMyName, PedList[iBe].iColours);
                        }
                        else
                        {
                            if (Game.Player.Character.Position.DistanceTo(PedList[iBe].ThisPed.Position) < 7.00f && !PedList[iBe].ThisPed.IsInVehicle() && iFollow < 8)
                            {
                                if (Game.Player.Character.IsInVehicle())
                                {
                                    if (Game.Player.Character.SeatIndex == VehicleSeat.Driver)
                                    {
                                        if (!PedList[iBe].bHorny2)
                                        {
                                            TopLeftUI("Press" + ControlSybols(86) + "to attract the players attention");
                                            PedList[iBe].bHorny2 = true;
                                        }
                                        else if (ButtonDown(86, false))
                                        {
                                            if (iAggression < 9)
                                            {
                                                ClearPedBlips(PedList[iBe].iLevel);

                                                PedList[iBe].iColours = 38;
                                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].sMyName, PedList[iBe].iColours);

                                                FolllowTheLeader(PedList[iBe].ThisPed);
                                                PedList[iBe].iTimeOn += 60000;
                                                iFollow += 1;

                                                PedList[iBe].bFollower = true;
                                            }
                                            else
                                            {
                                                ClearPedBlips(PedList[iBe].iLevel);
                                                ExMember(PedList[iBe].ThisPed);
                                                FightPlayer(PedList[iBe].ThisPed, false);
                                                PedList[iBe].iColours = 3;
                                                PedList[iBe].bFriendly = false;
                                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].sMyName, PedList[iBe].iColours);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!PedList[iBe].bOffRadar && PedList[iBe].iOffRadar == -1)
                        {

                            PedList[iBe].iOffRadar = Game.GameTime + 300000;
                            ClearPedBlips(PedList[iBe].iLevel);
                            UI.Notify("~h~" + PedList[iBe].sMyName + "~s~ has gone off radar");
                        }
                        else if (PedList[iBe].bOffRadar)
                        {
                            if (PedList[iBe].iOffRadar < Game.GameTime)
                            {
                                PedList[iBe].bOffRadar = false;
                                ClearPedBlips(PedList[iBe].iLevel);
                                PedList[iBe].DirBlip = DirectionalBlimp(PedList[iBe].ThisPed);
                                PedList[iBe].ThisBlip = PedBlimp(PedList[iBe].ThisPed, 1, PedList[iBe].sMyName, PedList[iBe].iColours);
                            }
                        }
                        else if (PedList[iBe].ThisPed.Position.DistanceTo(Game.Player.Character.Position) > 350.00f && iAggression > 4 && PedList[iBe].iDeathSequence == 0 && !bStuckOnYou)
                        {
                            if (PedList[iBe].ThisVeh == null)
                                AirAttack(PedList[iBe].iLevel);
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

                    if (HouseBlip.iTimeOn < Game.GameTime)
                    {
                        DeListingBrains(false, iBlpList, true);
                        iCurrentPlayerz -= 1;
                    }
                    iBlpList += 1;
                }
                else
                    iBlpList = 0;

                if (!bStuckOnYou && bSearching)
                {
                    if (BTimer(1))
                        NewPlayer();
                }
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
                PlayerZerosAI();
        }
    }
}
