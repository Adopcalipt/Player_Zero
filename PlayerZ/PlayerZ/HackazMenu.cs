using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System.Collections.Generic;
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

            ClearSession(mainMenu);
            InviteOnly(mainMenu);
            TrollPlayerzMenu(mainMenu);
            AddWindMill(mainMenu);
            SetPlayerAgg(mainMenu);
            SetPlayerNumb(mainMenu);
            MinWait(mainMenu);
            MaxWait(mainMenu);
            MinSession(mainMenu);
            MaxSession(mainMenu);
            PlayerMinAcc(mainMenu);
            PlayerMaxAcc(mainMenu);

            PzPool.RefreshIndex();
            DataStore.bMenuOpen = true;
            mainMenu.Visible = !mainMenu.Visible;
        }
        private static void AddWindMill(UIMenu XMen)
        {
            LoggerLight.GetLogging("AddWindMill");

            string sTitle = "Add eclipse windMill";
            if (DataStore.bEclipceWind)
                sTitle = "Remove eclipse windMill";
            var addWindmill = new UIMenuItem(sTitle, "");
            XMen.AddItem(addWindmill);

            XMen.OnItemSelect += (sender, item, index) =>
            {
                if (item == addWindmill)
                {
                    if (DataStore.bEclipceWind)
                    {
                        for (int i = 0; i < DataStore.Plops.Count; i++)
                        {
                            DataStore.Plops[i].MarkAsNoLongerNeeded();
                            DataStore.bEclipceWind = false;
                        }
                        addWindmill.Text = "Add eclipse windMill";
                    }
                    else
                    {
                        PedActions.AddEclipsWindMill();
                        addWindmill.Text = "Remove eclipse windMill";
                    }
                }
            };
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
                    PedActions.LaggOut();
                    DataStore.bMenuOpen = false;
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
                    DataStore.bMenuOpen = false;
                    PzPool.CloseAllMenus();
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
            Aggroitem.Index = DataStore.MySettings.iAggression - 1;
            XMen.AddItem(Aggroitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == Aggroitem)
                {
                    DataStore.MySettings.iAggression = index + 1;
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
            Numbitem.Index = DataStore.MySettings.iMaxPlayers - 5;
            XMen.AddItem(Numbitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == Numbitem)
                {
                    DataStore.MySettings.iMaxPlayers = index + 5;
                }
            };
        }
        private static void MinWait(UIMenu XMen)
        {
            LoggerLight.GetLogging("MinWait");

            List<dynamic> MinWaitList = new List<dynamic>();

            int iCount = SecondMiniute(false, DataStore.MySettings.iMaxWait, false) + 1;
            CompileMenuTotals(MinWaitList, iCount, 1);
            var MinSeshitem = new UIMenuListItem("Min login time (sec)", MinWaitList, 1);
            MinSeshitem.Index = SecondMiniute(false, DataStore.MySettings.iMinWait, false) - 1;
            XMen.AddItem(MinSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MinSeshitem)
                {
                    DataStore.MySettings.iMinWait = SecondMiniute(false, index, true) + 1;
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
            MaxSeshitem.Index = SecondMiniute(false, DataStore.MySettings.iMaxWait, false) - 1;
            XMen.AddItem(MaxSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MaxSeshitem)
                {
                    DataStore.MySettings.iMinWait = SecondMiniute(false, index, true) + 1;
                }
            };
        }
        private static void MinSession(UIMenu XMen)
        {
            LoggerLight.GetLogging("MinSession");

            List<dynamic> MinSeshList = new List<dynamic>();

            int iCount = SecondMiniute(true, DataStore.MySettings.iMaxSession, false) + 1;
            CompileMenuTotals(MinSeshList, iCount, 1);
            var MinSeshitem = new UIMenuListItem("Min session time (min)", MinSeshList, 1);
            MinSeshitem.Index = SecondMiniute(true, DataStore.MySettings.iMinSession, false) - 1;
            XMen.AddItem(MinSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MinSeshitem)
                {
                    DataStore.MySettings.iMinSession = SecondMiniute(true, index, true) + 1;
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
            MaxSeshitem.Index = SecondMiniute(true, DataStore.MySettings.iMaxSession, false) - 1;
            XMen.AddItem(MaxSeshitem);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MaxSeshitem)
                {
                    DataStore.MySettings.iMaxSession = SecondMiniute(true, index, true) + 1;
                }
            };
        }
        private static void PlayerMinAcc(UIMenu XMen)
        {
            LoggerLight.GetLogging("PlayerMinAcc");

            List<dynamic> MinAccList = new List<dynamic>();

            int iCount = DataStore.MySettings.iAccMax + 1;
            CompileMenuTotals(MinAccList, iCount, 1);
            var MinAcc = new UIMenuListItem("Player aim accuracy minimum", MinAccList, 1);
            MinAcc.Index = DataStore.MySettings.iAccMin - 1;
            XMen.AddItem(MinAcc);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MinAcc)
                {
                    DataStore.MySettings.iAccMin = index + 1;
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
            MaxAcc.Index = DataStore.MySettings.iAccMax - 1;
            XMen.AddItem(MaxAcc);

            XMen.OnListChange += (sender, item, index) =>
            {
                if (item == MaxAcc)
                {
                    DataStore.MySettings.iAccMax = index + 1;
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
                    PzPool.CloseAllMenus();

                    if (index < iCount)
                        TrollList(Playerz[index]);
                    else
                        TrolAfkerslList(Playerz[index]);
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
                //MoneyDropOnPed(trollPMenu, iPedId, true);

                PzPool.RefreshIndex();
                DataStore.bMenuOpen = true;
                trollPMenu.Visible = !trollPMenu.Visible;
            }
        }
        private static void TrollList(int iPedId)
        {
            LoggerLight.GetLogging("TrollList iPedId == " + iPedId);

            if (iPedId != -1 && iPedId < DataStore.PedList.Count)
            {
                PzPool = new MenuPool();
                var trollPMenu = new UIMenu(DataStore.PedList[iPedId].MyName, "");
                PzPool.Add(trollPMenu);

                BringPed(trollPMenu, iPedId, false);
                BurnPed(trollPMenu, iPedId, false);
                CrashPed(trollPMenu, iPedId, false);
                DropObjectOnPed(trollPMenu, iPedId, false);
                OrbitalStrikeOnPed(trollPMenu, iPedId, false);
                PiggyBack(trollPMenu, iPedId, false);
                //MoneyDropOnPed(trollPMenu, iPedId, false);
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
                            DataStore.AFKList.RemoveAt(iPedId);

                            PedActions.GenPlayerPed(LandHere, ReturnValues.RandInt(1, 360), null, 0, -1, sNAmes);
                        }
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                            DataStore.PedList[iPedId].ThisPed.Position = LandHere;
                    }
                    DataStore.bMenuOpen = false;
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
                            UI.Notify(DataStore.AFKList[iPedId].MyName + " died");
                            DataStore.AFKList[iPedId].TimeOn = 0;
                        }
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            Function.Call(Hash.START_ENTITY_FIRE, DataStore.PedList[iPedId].ThisPed);
                            ClearUp.ClearPedBlips(DataStore.PedList[iPedId].Level);
                            DataStore.PedList[iPedId].Colours = 1;
                            if (DataStore.MySettings.iAggression < 5)
                                DataStore.PedList[iPedId].TimeOn = Game.GameTime + 120000;
                            else
                                DataStore.PedList[iPedId].TimeOn += 120000;

                            PedActions.FightPlayer(DataStore.PedList[iPedId].ThisPed, false);
                            DataStore.PedList[iPedId].Friendly = false;
                            if (DataStore.PedList[iPedId].Follower)
                            {
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iPedId].ThisPed.Handle);
                                DataStore.PedList[iPedId].Follower = false;
                                DataStore.iFollow -= 1;
                            }
                            DataStore.PedList[iPedId].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iPedId].ThisPed);
                            DataStore.PedList[iPedId].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iPedId].ThisPed, 1, DataStore.PedList[iPedId].MyName, DataStore.PedList[iPedId].Colours);
                        }
                    }
                    DataStore.bMenuOpen = false;
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
                    }
                    DataStore.bMenuOpen = false;
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
                            Vector3 Above = DataStore.PedList[iPedId].ThisPed.Position + (DataStore.PedList[iPedId].ThisPed.ForwardVector * 3);
                            Above.Z = Above.Z + 10;
                            PedActions.DropObjects(Above);
                            ClearUp.ClearPedBlips(DataStore.PedList[iPedId].Level);
                            DataStore.PedList[iPedId].Colours = 1;
                            if (DataStore.MySettings.iAggression < 5)
                                DataStore.PedList[iPedId].TimeOn = Game.GameTime + 120000;
                            else
                                DataStore.PedList[iPedId].TimeOn += 120000;

                            PedActions.FightPlayer(DataStore.PedList[iPedId].ThisPed, false);
                            DataStore.PedList[iPedId].Friendly = false;
                            if (DataStore.PedList[iPedId].Follower)
                            {
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iPedId].ThisPed.Handle);
                                DataStore.PedList[iPedId].Follower = false;
                                DataStore.iFollow -= 1;
                            }
                            DataStore.PedList[iPedId].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iPedId].ThisPed);
                            DataStore.PedList[iPedId].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iPedId].ThisPed, 1, DataStore.PedList[iPedId].MyName, DataStore.PedList[iPedId].Colours);
                        }
                    }
                    DataStore.bMenuOpen = false;
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
                            PedActions.FireOrb(DataStore.PedList[iPedId].Level, DataStore.PedList[iPedId].ThisPed, true);
                            if (DataStore.PedList[iPedId].Bounty)
                                Game.Player.Money += 7000;
                            DataStore.PedList[iPedId].Friendly = false;
                            DataStore.PedList[iPedId].Colours = 1;
                            DataStore.PedList[iPedId].ApprochPlayer = false;
                            Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iPedId].ThisPed.Handle);
                            DataStore.PedList[iPedId].Follower = false;
                            DataStore.PedList[iPedId].Killed += 1;
                        }
                    }
                    DataStore.bMenuOpen = false;
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
                        DataStore.AFKList.RemoveAt(iPedId);

                        Ped MyPed = PedActions.GenPlayerPed(LandHere, ReturnValues.RandInt(1, 360), null, 0, -1, sNAmes);

                        PedActions.ForceAnim(Peddy, "amb@code_human_in_bus_passenger_idles@female@sit@idle_a", "idle_a", Peddy.Position, Peddy.Rotation);
                        Peddy.AttachTo(MyPed, 31086, new Vector3(0.10f, 0.15f, 0.61f), new Vector3(0.00f, 0.00f, 180.00f));
                        DataStore.bPlayerPiggyBack = true;
                    }
                    else
                    {
                        if (iPedId < DataStore.PedList.Count)
                        {
                            PedActions.ForceAnim(Peddy, "amb@code_human_in_bus_passenger_idles@female@sit@idle_a", "idle_a", Peddy.Position, Peddy.Rotation);
                            Peddy.AttachTo(DataStore.PedList[iPedId].ThisPed, 31086, new Vector3(0.10f, 0.15f, 0.61f), new Vector3(0.00f, 0.00f, 180.00f));
                            DataStore.bPlayerPiggyBack = true;
                        }
                    }
                    DataStore.bMenuOpen = false;
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
                    DataStore.bMenuOpen = false;
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
                            DataStore.PedList[iPedId].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iPedId].ThisPed, 303, DataStore.PedList[iPedId].MyName, 1);
                            DataStore.PedList[iPedId].Bounty = true;
                        }
                    }
                    DataStore.bMenuOpen = false;
                    PzPool.CloseAllMenus();
                }
            };
        }
    }
}
