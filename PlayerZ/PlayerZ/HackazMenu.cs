using GTA;
using GTA.Math;
using GTA.Native;
using System.Collections.Generic;
using PlayerZero.Classes;
using NativeUI; 

namespace PlayerZero
{
    public class HackazMenu
    {
        public static MenuPool PzPool;

        private static void CompileMenuTotals(List<dynamic> dList, int iTotal, int iBZero)
        {
            LoggerLight.GetLogging("CompileMenuTotals");

            while (iBZero < iTotal)
            {
                dList.Add("- " + iBZero + " -");
                iBZero = iBZero + 1;
            }
        }
        private static int SecondMiniute(bool bMin, int iSec,bool bAdd)
        {
            int MyI = 0;
            if (bAdd)
            {
                MyI = iSec * 1000;

                if (bMin)
                    MyI = MyI * 60;
            }
            else
            {
                MyI = iSec / 1000;

                if (bMin)
                    MyI = MyI / 60;
            }

            return MyI;
        }
        public static void HackerZMenu()
        {
            LoggerLight.GetLogging("HackerZMenu");

            PzPool = new MenuPool();
            var mainMenu = new UIMenu("HackarZ Menu", "");
            PzPool.Add(mainMenu);

            TrollPlayerzMenu(mainMenu);
            AddWindMill(mainMenu);
            if (DataStore.PlayContList.PedBrain.Count > 0)
                ContackzMenu(mainMenu);
            SetPlayerAgg(mainMenu);
            SetPlayerNumb(mainMenu);
            MinWait(mainMenu);
            MaxWait(mainMenu);
            MinSession(mainMenu);
            MaxSession(mainMenu);
            PlayerMinAcc(mainMenu);
            PlayerMaxAcc(mainMenu);
            ClearSession(mainMenu);
            InviteOnly(mainMenu);
            SetNotify(mainMenu);
            SetBlipies(mainMenu);

            PzPool.RefreshIndex();
            DataStore.bMenuOpen = true;
            mainMenu.Visible = !mainMenu.Visible;
        }
        private static void ClearSession(UIMenu XMen)
        {
            LoggerLight.GetLogging("ClearSession");
            var ClearSesh = new UIMenuItem("Clear session", "");
            XMen.AddItem(ClearSesh);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == ClearSesh)
                {
                    PlayerAI.LaggOut();
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void InviteOnly(UIMenu XMen)
        {
            LoggerLight.GetLogging("InviteOnly");
            string sTitle = "Invite only session";
            if (DataStore.bDisabled)
                sTitle = "Public session";

            var inviteOnly = new UIMenuItem(sTitle, "");
            XMen.AddItem(inviteOnly);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == inviteOnly)
                {
                    DataStore.bDisabled = !DataStore.bDisabled;
                    if (DataStore.bScan)
                        DataStore.bScan = false;
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void TrollPlayerzMenu(UIMenu XMen)
        {
            LoggerLight.GetLogging("TrollPlayerz");

            var trollinList = PzPool.AddSubMenu(XMen, "Playerz");

            List<dynamic> Playerz = new List<dynamic>();
            int iCount = 0;

            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                Playerz.Add(i);
                var newitem = new UIMenuItem(DataStore.PedList[i].MyName);
                trollinList.AddItem(newitem);
                iCount++;
            }

            for (int i = 0; i < DataStore.AFKList.Count; i++)
            {
                Playerz.Add(i);
                var newitem = new UIMenuItem(DataStore.AFKList[i].MyName);
                trollinList.AddItem(newitem);
            }

            trollinList.OnItemSelect += (sender, item, index) =>
            {
                if (sender == trollinList)
                {
                    DataStore.bMenuOpen = false;
                    Vehicle Vhich = null;  

                    if (index < iCount)
                    {
                        Vhich = DataStore.PedList[index].ThisVeh;
                        TrollList(Playerz[index], Vhich);
                    }
                    else
                        TrolAfkerslList(Playerz[index]);
                }
            };
        }
        private static void ContackzMenu(UIMenu XMen)
        {
            LoggerLight.GetLogging("ContackzMenu");

            var trollinList = PzPool.AddSubMenu(XMen, "Contacts");

            List<string> PlayerzId = new List<string>();
            int iCount = 0;

            for (int i = 0; i < DataStore.PlayContList.PedBrain.Count; i++)
            {
                PlayerzId.Add(DataStore.PlayContList.PedBrain[i].MyIdentity);
                var newitem = new UIMenuItem(DataStore.PlayContList.PedBrain[i].MyName);
                trollinList.AddItem(newitem);
                iCount++;
            }

            trollinList.OnItemSelect += (sender, item, index) =>
            {
                if (sender == trollinList)
                {
                    DataStore.bMenuOpen = false;
                    MyContactMenu(PlayerzId[index]);
                }
            };
        }
        private static void MyContactMenu(string sMyId)
        {
            LoggerLight.GetLogging("MyContactMenu sMyId == " + sMyId);

            int iPlace = PlayerAI.ReteveContact(sMyId);
            if (iPlace != -1)
            {
                PzPool = new MenuPool();
                var contMenu = new UIMenu(DataStore.PlayContList.PedBrain[iPlace].MyName, "");
                PzPool.Add(contMenu);

                RemovePed(contMenu, sMyId);

                PzPool.RefreshIndex();
                DataStore.bMenuOpen = true;
                contMenu.Visible = !contMenu.Visible;
            }
        }
        private static void RemovePed(UIMenu XMen, string sMyId)
        {
            LoggerLight.GetLogging("RemovePed");
            var comehere = new UIMenuItem("Remove player contact", "");
            XMen.AddItem(comehere);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                OnlinePlayerz.RemoveMyFriend(sMyId);
                PzPool.CloseAllMenus();
            };
        }
        private static void AddWindMill(UIMenu XMen)
        {
            LoggerLight.GetLogging("AddWindMill");

            string sTitle = "Add eclipse windMill";
            if (DataStore.WindMill != null)
                sTitle = "Remove eclipse windMill";
            var addWindmill = new UIMenuItem(sTitle, "");
            XMen.AddItem(addWindmill);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == addWindmill)
                {
                    PedActions.EclipsWindMill();

                    if (DataStore.WindMill == null)
                        addWindmill.Text = "Add eclipse windMill";
                    else
                        addWindmill.Text = "Remove eclipse windMill";
                }
            };
        }
        private static void SetNotify(UIMenu XMen)
        {
            LoggerLight.GetLogging("SetNotify");

            string sTitle = "Add Notification";
            if (!DataStore.MySettings.NoNotify)
                sTitle = "Remove Notification";
            var notes = new UIMenuItem(sTitle, "");
            XMen.AddItem(notes);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == notes)
                {
                    DataStore.MySettings.NoNotify = !DataStore.MySettings.NoNotify;
                    if (DataStore.MySettings.NoNotify)
                        notes.Text = "Add Notification";
                    else
                        notes.Text = "Remove Notification";
                }
            };
        }
        private static void SetBlipies(UIMenu XMen)
        {
            LoggerLight.GetLogging("SetNotify");

            string sTitle = "Player Blips On";
            if (!DataStore.MySettings.NoRadar)
                sTitle = "Player Blips Off";
            var radar = new UIMenuItem(sTitle, "");
            XMen.AddItem(radar);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == radar)
                {
                    DataStore.MySettings.NoRadar = !DataStore.MySettings.NoRadar;
                    if (DataStore.MySettings.NoRadar)
                    {
                        PlayerAI.NoMoreBlips();
                        radar.Text = "Player Blips On";
                    }
                    else
                        radar.Text = "Player Blips Off";
                }
            };
        }
        private static void SetPlayerAgg(UIMenu XMen)
        {
            LoggerLight.GetLogging("SetPlayerAgg");

            List<dynamic> AggList = new List<dynamic>();

            int iCount = 12;
            CompileMenuTotals(AggList, iCount, 1);
            var Aggroitem = new UIMenuListItem("Player agression", AggList, 0);
            Aggroitem.Index = DataStore.MySettings.Aggression - 1;
            XMen.AddItem(Aggroitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == Aggroitem)
                {
                    DataStore.MySettings.Aggression = index + 1;
                }
            };
        }
        private static void SetPlayerNumb(UIMenu XMen)
        {
            LoggerLight.GetLogging("SetPlayerNumb");

            List<dynamic> NumbList = new List<dynamic>();

            int iCount = 30;
            CompileMenuTotals(NumbList, iCount, 5);
            var Numbitem = new UIMenuListItem("Players in session", NumbList, 0);
            Numbitem.Index = DataStore.MySettings.MaxPlayers - 5;
            XMen.AddItem(Numbitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == Numbitem)
                {
                    DataStore.MySettings.MaxPlayers = index + 5;
                }
            };
        }
        private static void MinWait(UIMenu XMen)
        {
            LoggerLight.GetLogging("MinWait");

            List<dynamic> MinWaitList = new List<dynamic>();

            int iCount = SecondMiniute(false, DataStore.MySettings.MaxWait, false) + 1;
            CompileMenuTotals(MinWaitList, iCount, 1);
            var MinSeshitem = new UIMenuListItem("Min login time (sec)", MinWaitList, 1);
            MinSeshitem.Index = SecondMiniute(false, DataStore.MySettings.MinWait, false) - 1;
            XMen.AddItem(MinSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MinSeshitem)
                {
                    DataStore.MySettings.MinWait = SecondMiniute(false, index, true) + 1;

                }
            };
        }
        private static void MaxWait(UIMenu XMen)
        {
            LoggerLight.GetLogging("MaxWait");

            List<dynamic> MaxWaitList = new List<dynamic>();

            int iCount = 542;
            CompileMenuTotals(MaxWaitList, iCount, 1);
            var MaxSeshitem = new UIMenuListItem("Max login time (sec)", MaxWaitList, 1);
            MaxSeshitem.Index = SecondMiniute(false, DataStore.MySettings.MaxWait, false) - 1;
            XMen.AddItem(MaxSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MaxSeshitem)
                {
                    DataStore.MySettings.MaxWait = SecondMiniute(false, index, true) + 1;
                }
            };
        }
        private static void MinSession(UIMenu XMen)
        {
            LoggerLight.GetLogging("MinSession");

            List<dynamic> MinSeshList = new List<dynamic>();

            int iCount = SecondMiniute(true, DataStore.MySettings.MaxSession, false) + 1;
            CompileMenuTotals(MinSeshList, iCount, 1);
            var MinSeshitem = new UIMenuListItem("Min session time (min)", MinSeshList, 1);
            MinSeshitem.Index = SecondMiniute(true, DataStore.MySettings.MinSession, false) - 1;
            XMen.AddItem(MinSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MinSeshitem)
                {
                    DataStore.MySettings.MinSession = SecondMiniute(true, index, true) + 1;
                }
            };
        }
        private static void MaxSession(UIMenu XMen)
        {
            LoggerLight.GetLogging("MaxWait");

            List<dynamic> MaxWaitList = new List<dynamic>();

            int iCount = 542;
            CompileMenuTotals(MaxWaitList, iCount, 1);
            var MaxSeshitem = new UIMenuListItem("Max session time (min)", MaxWaitList, 1);
            MaxSeshitem.Index = SecondMiniute(true, DataStore.MySettings.MaxSession, false) - 1;
            XMen.AddItem(MaxSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MaxSeshitem)
                {
                    DataStore.MySettings.MaxSession = SecondMiniute(true, index, true) + 1;
                }
            };
        }
        private static void PlayerMinAcc(UIMenu XMen)
        {
            LoggerLight.GetLogging("PlayerMinAcc");

            List<dynamic> MinAccList = new List<dynamic>();

            int iCount = DataStore.MySettings.AccMax + 1;
            CompileMenuTotals(MinAccList, iCount, 1);
            var MinAcc = new UIMenuListItem("Player aim accuracy minimum", MinAccList, 1);
            MinAcc.Index = DataStore.MySettings.AccMin - 1;
            XMen.AddItem(MinAcc);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MinAcc)
                {
                    DataStore.MySettings.AccMin = index + 1;
                }
            };
        }
        private static void PlayerMaxAcc(UIMenu XMen)
        {
            LoggerLight.GetLogging("PlayerMaxAcc");

            List<dynamic> MaxAccList = new List<dynamic>();

            int iCount = 100;
            CompileMenuTotals(MaxAccList, iCount, 1);
            var MaxAcc = new UIMenuListItem("Player aim accuracy maximum", MaxAccList, 1);
            MaxAcc.Index = DataStore.MySettings.AccMax - 1;
            XMen.AddItem(MaxAcc);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MaxAcc)
                {
                    DataStore.MySettings.AccMax = index + 1;
                }
            };
        }
        private static void TrolAfkerslList(int iPedId)
        {
            LoggerLight.GetLogging("TrollList iPedId == " + iPedId);

            if (iPedId != -1 && iPedId < DataStore.AFKList.Count)
            {
                PzPool = new MenuPool();
                var trollPMenu = new UIMenu(DataStore.AFKList[iPedId].MyName, "");
                PzPool.Add(trollPMenu);

                BringPed(trollPMenu, iPedId, true);
                BurnPed(trollPMenu, iPedId, true);
                CrashPed(trollPMenu, iPedId, true);
                DropObjectOnPed(trollPMenu, iPedId, true);
                OrbitalStrikeOnPed(trollPMenu, iPedId, true);
                PiggyBack(trollPMenu, iPedId, true);
                AddBountyPed(trollPMenu, iPedId, true);
                MoneyDropOnPed(trollPMenu, iPedId, true);

                PzPool.RefreshIndex();
                DataStore.bMenuOpen = true;
                trollPMenu.Visible = !trollPMenu.Visible;
            }
        }
        private static void TrollList(int iPedId, Vehicle hasVeh)
        {
            LoggerLight.GetLogging("TrollList iPedId == " + iPedId);

            if (iPedId != -1 && iPedId < DataStore.PedList.Count)
            {
                PzPool = new MenuPool();
                var trollPMenu = new UIMenu(DataStore.PedList[iPedId].MyName, "");
                PzPool.Add(trollPMenu);

                BringPed(trollPMenu, iPedId, false);
                if (ReturnValues.HasASeat(hasVeh))
                    EnterPeddVeh(trollPMenu, iPedId, hasVeh);
                BurnPed(trollPMenu, iPedId, false);
                CrashPed(trollPMenu, iPedId, false);
                DropObjectOnPed(trollPMenu, iPedId, false);
                OrbitalStrikeOnPed(trollPMenu, iPedId, false);
                PiggyBack(trollPMenu, iPedId, false);
                MoneyDropOnPed(trollPMenu, iPedId, false);
                AddBountyPed(trollPMenu, iPedId, false);

                PzPool.RefreshIndex();
                DataStore.bMenuOpen = true;
                trollPMenu.Visible = !trollPMenu.Visible;
            }
        }
        private static void BringPed(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("BringPed");
            var comehere = new UIMenuItem("Move player to self", "");
            XMen.AddItem(comehere);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == comehere)
                {
                    Vector3 LandHere = Game.Player.Character.Position + (Game.Player.Character.ForwardVector * 3);
                    if (bAfk)
                    {
                        if (iPedId < DataStore.AFKList.Count)
                        {
                            DataStore.AFKList[iPedId].ThisBlip.Remove();
                            string sNAmes = DataStore.AFKList[iPedId].MyName;
                            int iLev = DataStore.AFKList[iPedId].Level;
                            PlayerAI.RemoveFromAFKList(iPedId);

                            PlayerBrain newBrain = new PlayerBrain();
                            newBrain.PFMySetting = FreemodePed.MakeFaces();
                            newBrain.MyName = sNAmes;
                            newBrain.Level = iLev;
                            newBrain.TimeOn = Game.GameTime + RandomNum.RandInt(DataStore.MySettings.MinSession, DataStore.MySettings.MaxSession);
                            newBrain.Friendly = false;

                            Ped MyPed = BuildObjects.GenPlayerPed(LandHere, RandomNum.RandInt(0, 360), newBrain);
                        }
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            if (DataStore.PedList[iPedId].ThisPed != null)
                            {
                                DataStore.PedList[iPedId].ThisPed.Position = LandHere;
                                DataStore.PedList[iPedId].ApprochPlayer = true;
                                DataStore.PedList[iPedId].Driver = false;
                                DataStore.PedList[iPedId].Passenger = false;
                            }
                        }
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void EnterPeddVeh(UIMenu XMen, int iPedId, Vehicle vHick)
        {
            LoggerLight.GetLogging("BringPed");
            var comehere = new UIMenuItem("Enter Players Vehicle", "");
            XMen.AddItem(comehere);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == comehere)
                {
                    if (iPedId < DataStore.PedList.Count)
                    {
                        PedActions.PlayerEnterVeh(vHick, true);
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void BurnPed(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("BurnPed");
            var burningMan = new UIMenuItem("Burn player", "");
            XMen.AddItem(burningMan);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == burningMan)
                {
                    if (bAfk)
                    {
                        if (iPedId < DataStore.AFKList.Count)
                        {
                            ScaleDisp.BottomLeft(DataStore.AFKList[iPedId].MyName + " died");
                            DataStore.AFKList[iPedId].TimeOn = 0;
                        }
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            if (DataStore.PedList[iPedId].ThisPed != null)
                            {
                                Function.Call(Hash.START_ENTITY_FIRE, DataStore.PedList[iPedId].ThisPed);
                                ClearUp.ClearPedBlips(DataStore.PedList[iPedId].MyIdentity);
                                DataStore.PedList[iPedId].BlipColour = 1;
                                if (DataStore.MySettings.Aggression < 5)
                                    DataStore.PedList[iPedId].TimeOn = Game.GameTime + 120000;
                                else
                                    DataStore.PedList[iPedId].TimeOn += 120000;

                                PedActions.FightPlayer(DataStore.PedList[iPedId].ThisPed, false);
                                DataStore.PedList[iPedId].Friendly = false;
                                if (DataStore.PedList[iPedId].Follower)
                                {
                                    if (PlayerAI.ReteveContact(DataStore.PedList[iPedId].MyIdentity) != -1)
                                        OnlinePlayerz.RemoveMyFriend(DataStore.PedList[iPedId].MyIdentity);
                                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iPedId].ThisPed.Handle);
                                    DataStore.PedList[iPedId].Follower = false;
                                    DataStore.iFollow -= 1;
                                }
                                DataStore.PedList[iPedId].DirBlip = BuildObjects.DirectionalBlimp(DataStore.PedList[iPedId].ThisPed);
                                DataStore.PedList[iPedId].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[iPedId].ThisPed, 1, DataStore.PedList[iPedId].MyName, DataStore.PedList[iPedId].BlipColour);
                            }
                        }
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void CrashPed(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("CrashPed");
            var dumpPlayer = new UIMenuItem("Kick player out of session", "");
            XMen.AddItem(dumpPlayer);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == dumpPlayer)
                {
                    if (bAfk)
                    {
                        if (iPedId < DataStore.AFKList.Count)
                            DataStore.AFKList[iPedId].TimeOn = 0;
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                            DataStore.PedList[iPedId].TimeOn = 0;

                        if (PlayerAI.ReteveContact(DataStore.PedList[iPedId].MyIdentity) != -1)
                            OnlinePlayerz.RemoveMyFriend(DataStore.PedList[iPedId].MyIdentity);
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void DropObjectOnPed(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("DropObjectOnPed");
            var dropShit = new UIMenuItem("Drop items on player", "");
            XMen.AddItem(dropShit);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == dropShit)
                {
                    if (bAfk)
                    {

                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            if (DataStore.PedList[iPedId].ThisPed != null)
                            {
                                Vector3 Above = DataStore.PedList[iPedId].ThisPed.Position + (DataStore.PedList[iPedId].ThisPed.ForwardVector * 3);
                                Above.Z = Above.Z + 10;
                                BuildObjects.DropObjects(Above);
                                ClearUp.ClearPedBlips(DataStore.PedList[iPedId].MyIdentity);
                                DataStore.PedList[iPedId].BlipColour = 1;
                                if (DataStore.MySettings.Aggression < 5)
                                    DataStore.PedList[iPedId].TimeOn = Game.GameTime + 120000;
                                else
                                    DataStore.PedList[iPedId].TimeOn += 120000;

                                PedActions.FightPlayer(DataStore.PedList[iPedId].ThisPed, false);
                                DataStore.PedList[iPedId].Friendly = false;
                                if (DataStore.PedList[iPedId].Follower)
                                {
                                    if (PlayerAI.ReteveContact(DataStore.PedList[iPedId].MyIdentity) != -1)
                                        OnlinePlayerz.RemoveMyFriend(DataStore.PedList[iPedId].MyIdentity);

                                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iPedId].ThisPed.Handle);
                                    DataStore.PedList[iPedId].Follower = false;
                                    DataStore.iFollow -= 1;
                                }
                                DataStore.PedList[iPedId].DirBlip = BuildObjects.DirectionalBlimp(DataStore.PedList[iPedId].ThisPed);
                                DataStore.PedList[iPedId].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[iPedId].ThisPed, 1, DataStore.PedList[iPedId].MyName, DataStore.PedList[iPedId].BlipColour);
                            }
                        }
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void OrbitalStrikeOnPed(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("OrbitalStrikeOnPed");
            var orbStrike = new UIMenuItem("'Orbital Strike' on player", "");
            XMen.AddItem(orbStrike);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == orbStrike)
                {
                    if (bAfk)
                    {

                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            if (DataStore.PedList[iPedId].ThisPed != null)
                            {
                                PedActions.FireOrb(DataStore.PedList[iPedId].MyIdentity, DataStore.PedList[iPedId].ThisPed, true);
                                if (DataStore.PedList[iPedId].Bounty)
                                    Game.Player.Money += 7000;
                                if (DataStore.PedList[iPedId].Follower)
                                {
                                    if (PlayerAI.ReteveContact(DataStore.PedList[iPedId].MyIdentity) != -1)
                                        OnlinePlayerz.RemoveMyFriend(DataStore.PedList[iPedId].MyIdentity);

                                    DataStore.PedList[iPedId].Follower = false;
                                    DataStore.iFollow -= 1;
                                }
                                DataStore.PedList[iPedId].Friendly = false;
                                DataStore.PedList[iPedId].BlipColour = 1;
                                DataStore.PedList[iPedId].ApprochPlayer = false;
                                DataStore.PedList[iPedId].Killed ++;
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iPedId].ThisPed.Handle);
                                DataStore.PedList[iPedId].DeathSequence = 1;
                                DataStore.PedList[iPedId].DeathTime = Game.GameTime + 10000;
                            }
                        }
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void PiggyBack(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("PiggyBack");
            var piggyBacker = new UIMenuItem("Piggy back player", "");
            XMen.AddItem(piggyBacker);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == piggyBacker)
                {
                    Ped Peddy = Game.Player.Character;
                    if (bAfk)
                    {
                        Vector3 LandHere = Peddy.Position + (Peddy.ForwardVector * 3);

                        DataStore.AFKList[iPedId].ThisBlip.Remove();
                        string sNAmes = DataStore.AFKList[iPedId].MyName;
                        PlayerAI.RemoveFromAFKList(iPedId);

                        PlayerBrain newBrain = new PlayerBrain();
                        newBrain.PFMySetting = FreemodePed.MakeFaces();
                        newBrain.MyName = ReturnValues.SillyNameList();
                        newBrain.Friendly = false;

                        Ped MyPed = BuildObjects.GenPlayerPed(LandHere, RandomNum.RandInt(0, 360), newBrain);

                        PedActions.ForceAnim(Peddy, "amb@code_human_in_bus_passenger_idles@female@sit@idle_a", "idle_a", Peddy.Position, Peddy.Rotation);
                        Peddy.AttachTo(MyPed, 31086, new Vector3(0.10f, 0.15f, 0.61f), new Vector3(0.00f, 0.00f, 180.00f));
                        DataStore.bPlayerPiggyBack = true;
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            if (DataStore.PedList[iPedId].ThisPed != null)
                            {
                                PedActions.ForceAnim(Peddy, "amb@code_human_in_bus_passenger_idles@female@sit@idle_a", "idle_a", Peddy.Position, Peddy.Rotation);
                                Peddy.AttachTo(DataStore.PedList[iPedId].ThisPed, 31086, new Vector3(0.10f, 0.15f, 0.61f), new Vector3(0.00f, 0.00f, 180.00f));
                                DataStore.bPlayerPiggyBack = true;
                            }
                        }
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void MoneyDropOnPed(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("MoneyDropOnPed");
            var moneyDrops = new UIMenuItem("Money drop", "");
            XMen.AddItem(moneyDrops);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == moneyDrops)
                {
                    if (bAfk)
                    {

                    }
                    else
                    {
                        if (DataStore.PedList[iPedId].ThisPed != null)
                        {
                            ClearUp.ClearProps();

                            DataStore.sMoneyPicker = DataStore.PedList[iPedId].MyIdentity;
                            DataStore.iMoneyDrops = Game.GameTime + RandomNum.RandInt(4000, 8000);
                        }
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
        private static void AddBountyPed(UIMenu XMen, int iPedId, bool bAfk)
        {
            LoggerLight.GetLogging("MoneyDropOnPed");
            var addbounty = new UIMenuItem("Add bounty to player", "");
            XMen.AddItem(addbounty);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == addbounty)
                {
                    if (bAfk)
                    {
                        if (iPedId < DataStore.AFKList.Count)
                            DataStore.AFKList[iPedId].TimeOn = 0;
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            if (DataStore.PedList[iPedId].ThisPed != null)
                            {
                                if (DataStore.PedList[iPedId].ThisBlip != null)
                                {
                                    DataStore.PedList[iPedId].ThisBlip.Remove();
                                    DataStore.PedList[iPedId].ThisBlip = null;
                                }
                                if (DataStore.PedList[iPedId].DirBlip != null)
                                {
                                    DataStore.PedList[iPedId].DirBlip.Remove();
                                    DataStore.PedList[iPedId].DirBlip = null;
                                }
                                DataStore.PedList[iPedId].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[iPedId].ThisPed, 303, DataStore.PedList[iPedId].MyName, 1);
                                DataStore.PedList[iPedId].Bounty = true;
                            }
                        }
                    }
                    PzPool.CloseAllMenus();
                }
            };
        }
    }
}
