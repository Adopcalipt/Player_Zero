using GTA;
using GTA.Native;
using System.Linq;
using System;

namespace PlayerZero
{
    public class ScaleDisp : Script
    {
        private static bool bPlayerList = false;
        private static int iDisplay = 0;
        private static bool bPressHorn = true;

        public ScaleDisp()
        {
            Tick += ShowList;
            Interval = 1;
        }
        private void ShowList(object sender, EventArgs e)
        {
            if (bPlayerList)
            {
                if (iDisplay > Game.GameTime && !DataStore.bMenuOpen)
                {
                    Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, DataStore.iScale, 255, 255, 255, 255);

                    if (ReturnValues.ButtonDown(DataStore.MySettings.GetlayList, false) && ReturnValues.ButtonDown(DataStore.MySettings.DisableMod, true))
                        HackazMenu.HackerZMenu();
                }
                else
                {
                    CloseBaseHelpBar();
                    DataStore.bShowPlayerz = false;
                }
            }
            else if (IsIsSafe())
                Scaleform_PLAYER_LIST();
            else
            {
                if (DataStore.WalkFriend != null)
                    WalkFriend();
                else if (DataStore.FlyFriend != null)
                    FlyFriend();
                else if (DataStore.DriveFriend != null)
                    DriveFriend();
            }
        }
        private void FollowOn(string sPed, int iDes, bool addTo, Vehicle vic)
        {
            int ThisBrain = PlayerAI.ReteaveBrain(sPed);

            if (ThisBrain != -1)
            {
                DataStore.PedList[ThisBrain].WanBeFriends = false;
                DataStore.PedList[ThisBrain].ApprochPlayer = false;
                DataStore.PedList[ThisBrain].TimeOn += 600000;

                if (addTo)
                {
                    ClearUp.ClearPedBlips(DataStore.PedList[ThisBrain].MyIdentity);

                    DataStore.PedList[ThisBrain].Follower = true;
                    DataStore.PedList[ThisBrain].BlipColour = 38;
                    PedActions.FolllowTheLeader(DataStore.PedList[ThisBrain].ThisPed);
                    DataStore.iFollow += 1;
                }

                if (iDes == 1)
                {
                    DataStore.PedList[ThisBrain].EnterVehQue = true;
                    PedActions.PedDoGetIn(Game.Player.Character.CurrentVehicle, DataStore.PedList[ThisBrain].ThisPed, DataStore.PedList[ThisBrain].MyIdentity);
                    DataStore.PedList[ThisBrain].DirBlip = BuildObjects.DirectionalBlimp(DataStore.PedList[ThisBrain].ThisPed);
                    DataStore.PedList[ThisBrain].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[ThisBrain].ThisPed, 1, DataStore.PedList[ThisBrain].MyName, DataStore.PedList[ThisBrain].BlipColour);
                }
                else if (iDes == 2)
                {
                    DataStore.PedList[ThisBrain].EnterVehQue = false;
                    DataStore.PedList[ThisBrain].FindPlayer = Game.GameTime + 7000;
                    PedActions.PlayerEnterVeh(vic, false);
                    DataStore.PedList[ThisBrain].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[ThisBrain].ThisPed, BuildObjects.OhMyBlip(vic), DataStore.PedList[ThisBrain].MyName, DataStore.PedList[ThisBrain].BlipColour);

                }
                else if (iDes == 3)
                {
                    DataStore.PedList[ThisBrain].EnterVehQue = false;
                    DataStore.PedList[ThisBrain].DirBlip = BuildObjects.DirectionalBlimp(DataStore.PedList[ThisBrain].ThisPed);
                    DataStore.PedList[ThisBrain].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[ThisBrain].ThisPed, 1, DataStore.PedList[ThisBrain].MyName, DataStore.PedList[ThisBrain].BlipColour);
                    PedActions.OhDoKeepUp(DataStore.PedList[ThisBrain].ThisPed);
                }
                else if (iDes == 4)
                {
                    PedActions.PlayerEnterVeh(vic, true);
                    DataStore.PedList[ThisBrain].EnterVehQue = false;
                    DataStore.PedList[ThisBrain].PlaneLand = 5;
                }
            }
            bPressHorn = true;
            Script.Wait(2000);
        }
        private void YourDumped(string sPed)
        {
            int ThisBrain = PlayerAI.ReteaveBrain(sPed);
            if (ThisBrain != -1)
            {
                DataStore.PedList[ThisBrain].WanBeFriends = false;
                DataStore.PedList[ThisBrain].ThisPed.AlwaysKeepTask = false;
                DataStore.PedList[ThisBrain].ThisPed.BlockPermanentEvents = false;
                if (DataStore.PedList[ThisBrain].PlaneLand > 0)
                    DataStore.PedList[ThisBrain].PlaneLand = 8;
            }
            bPressHorn = true;
            Script.Wait(2000);
        }
        public static void YourNoQuiteDumped(string sPed)
        {
            int ThisBrain = PlayerAI.ReteaveBrain(sPed);
            if (ThisBrain != -1)
            {
                DataStore.PedList[ThisBrain].WanBeFriends = false;
                DataStore.PedList[ThisBrain].ApprochPlayer = true;
            }
            bPressHorn = true;
            Script.Wait(2000);
        }
        private void TurnOnYou(string sPed)
        {
            int ThisBrain = PlayerAI.ReteaveBrain(sPed);

            if ( ThisBrain != -1)
            {
                ClearUp.ClearPedBlips(DataStore.PedList[ThisBrain].MyIdentity);
                PedActions.FightPlayer(DataStore.PedList[ThisBrain].ThisPed, false);
                DataStore.PedList[ThisBrain].BlipColour = 3;
                DataStore.PedList[ThisBrain].Friendly = false;
                DataStore.PedList[ThisBrain].ApprochPlayer = false;
                DataStore.PedList[ThisBrain].WanBeFriends = false;
                DataStore.PedList[ThisBrain].DirBlip = BuildObjects.DirectionalBlimp(DataStore.PedList[ThisBrain].ThisPed);
                DataStore.PedList[ThisBrain].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[ThisBrain].ThisPed, 1, DataStore.PedList[ThisBrain].MyName, DataStore.PedList[ThisBrain].BlipColour);
            }
            bPressHorn = true;
        }
        private void FlyFriend()
        {
            if (!DataStore.FlyFriend.ThisPed.Exists())
            {
                YourDumped(DataStore.FlyFriend.MyIdentity);
                DataStore.FlyFriend = null;
            }
            else if (DataStore.FlyFriend.ThisPed.IsDead)
            {
                YourDumped(DataStore.FlyFriend.MyIdentity);
                DataStore.FlyFriend = null;
            }
            else if (DataStore.FlyFriend.ThisVeh.IsDead)
            {
                YourDumped(DataStore.FlyFriend.MyIdentity);
                DataStore.FlyFriend = null;
            }
            else if (!DataStore.FlyFriend.ThisPed.IsInVehicle())
            {
                YourDumped(DataStore.FlyFriend.MyIdentity);
                DataStore.FlyFriend = null;
            }
            else if (!Game.Player.Character.IsInVehicle() && ReturnValues.YoPoza().DistanceTo(DataStore.FlyFriend.ThisPed.Position) < 25f)
            {
                TopLeftUI("Press" + DataStore.ControlSym[23] + "to enter vehicle");

                if (ReturnValues.ButtonDown(23, true))
                {
                    FollowOn(DataStore.FlyFriend.MyIdentity, DataStore.FlyFriend.InCarOutCarFoot, false, DataStore.FlyFriend.ThisVeh);
                    DataStore.FlyFriend = null;
                }
            }
        }
        private void DriveFriend()
        {
            if (DataStore.iFollow < 7)
            {
                if (!DataStore.DriveFriend.ThisPed.Exists())
                {
                    YourDumped(DataStore.DriveFriend.MyIdentity);
                    DataStore.DriveFriend = null;
                }
                else if (DataStore.DriveFriend.ThisPed.IsDead)
                {
                    YourNoQuiteDumped(DataStore.DriveFriend.MyIdentity);
                    DataStore.DriveFriend = null;
                }
                else if (DataStore.DriveFriend.ThisVeh.IsDead)
                {
                    YourNoQuiteDumped(DataStore.DriveFriend.MyIdentity);
                    DataStore.DriveFriend = null;
                }
                else if (bPressHorn)
                {
                    if (ReturnValues.YoPoza().DistanceTo(DataStore.DriveFriend.ThisPed.Position) < 25f)
                    {
                        bPressHorn = false;
                        DataStore.DriveFriend.ThisVeh.SoundHorn(3000);
                    }
                }
                else if (!Game.Player.Character.IsInVehicle() && DataStore.DriveFriend.ThisPed.IsInVehicle() && ReturnValues.YoPoza().DistanceTo(DataStore.DriveFriend.ThisPed.Position) < 30f)
                {
                    TopLeftUI("Press" + DataStore.ControlSym[23] + "to enter vehicle, Press" + DataStore.ControlSym[46] + "to dismiss");

                    if (ReturnValues.ButtonDown(23, true))
                    {
                        FollowOn(DataStore.DriveFriend.MyIdentity, DataStore.DriveFriend.InCarOutCarFoot, true, DataStore.DriveFriend.ThisVeh);
                        DataStore.DriveFriend = null;
                    }
                    else if (ReturnValues.ButtonDown(46, false))
                    {
                        YourDumped(DataStore.DriveFriend.MyIdentity);
                        DataStore.DriveFriend = null;
                    }
                }
                else
                {
                    YourDumped(DataStore.DriveFriend.MyIdentity);
                    DataStore.DriveFriend = null;
                }
            }
            else
            {
                YourDumped(DataStore.DriveFriend.MyIdentity);
                DataStore.DriveFriend = null;
            }
        }
        private void WalkFriend()
        {
            if (!DataStore.WalkFriend.ThisPed.Exists())
            {
                YourDumped(DataStore.WalkFriend.MyIdentity);
                DataStore.WalkFriend = null;
            }
            else if (DataStore.WalkFriend.ThisPed.IsDead)
            {
                YourNoQuiteDumped(DataStore.WalkFriend.MyIdentity);
                DataStore.WalkFriend = null;
            }
            else if (ReturnValues.YoPoza().DistanceTo(DataStore.WalkFriend.ThisPed.Position) < 15f)
            {
                if (DataStore.iFollow < 7)
                {
                    if (DataStore.WalkFriend.ThisVeh == null)
                    {
                        if (DataStore.WalkFriend.InCarOutCarFoot == 1)
                        {
                            TopLeftUI("Press" + DataStore.ControlSym[86] + "to attract the players attention");
                            if (bPressHorn)
                                bPressHorn = false;
                            else if (ReturnValues.ButtonDown(86, false))
                            {
                                if (DataStore.MySettings.Aggression < 9)
                                    FollowOn(DataStore.WalkFriend.MyIdentity, DataStore.WalkFriend.InCarOutCarFoot, true, null);
                                else
                                    TurnOnYou(DataStore.WalkFriend.MyIdentity);
                                DataStore.WalkFriend = null;
                            }
                        }
                        else if (DataStore.WalkFriend.InCarOutCarFoot == 3)
                        {
                            TopLeftUI("Press" + DataStore.ControlSym[46] + "to invte this player");
                            if (bPressHorn)
                                bPressHorn = false;
                            else if (ReturnValues.ButtonDown(46, false))
                            {
                                if (DataStore.MySettings.Aggression < 9)
                                    FollowOn(DataStore.WalkFriend.MyIdentity, DataStore.WalkFriend.InCarOutCarFoot, true, null);
                                else
                                    TurnOnYou(DataStore.WalkFriend.MyIdentity);
                                DataStore.WalkFriend = null;
                            }
                        }
                    }
                    else
                    {
                        if (!DataStore.WalkFriend.ThisPed.Exists())
                        {
                            YourDumped(DataStore.WalkFriend.MyIdentity);
                            DataStore.WalkFriend = null;
                        }
                        else if (DataStore.WalkFriend.ThisPed.IsDead)
                        {
                            YourNoQuiteDumped(DataStore.WalkFriend.MyIdentity);
                            DataStore.WalkFriend = null;
                        }
                        else if (DataStore.WalkFriend.ThisVeh.IsDead)
                        {
                            YourNoQuiteDumped(DataStore.WalkFriend.MyIdentity);
                            DataStore.WalkFriend = null;
                        }
                        else if (bPressHorn)
                        {
                            if (ReturnValues.YoPoza().DistanceTo(DataStore.WalkFriend.ThisPed.Position) < 25f)
                            {
                                bPressHorn = false;
                                DataStore.WalkFriend.ThisVeh.SoundHorn(3000);
                            }
                        }
                        else if (!Game.Player.Character.IsInVehicle() && DataStore.WalkFriend.ThisPed.IsInVehicle() && ReturnValues.YoPoza().DistanceTo(DataStore.WalkFriend.ThisPed.Position) < 30f)
                        {
                            TopLeftUI("Press" + DataStore.ControlSym[23] + "to enter vehicle, Press" + DataStore.ControlSym[46] + "to dismiss");

                            if (ReturnValues.ButtonDown(23, true))
                            {
                                FollowOn(DataStore.WalkFriend.MyIdentity, DataStore.WalkFriend.InCarOutCarFoot, true, DataStore.WalkFriend.ThisVeh);
                                DataStore.WalkFriend = null;
                            }
                            else if (ReturnValues.ButtonDown(46, false))
                            {
                                YourDumped(DataStore.WalkFriend.MyIdentity);
                                DataStore.WalkFriend = null;
                            }
                        }
                        else
                        {
                            YourDumped(DataStore.WalkFriend.MyIdentity);
                            DataStore.WalkFriend = null;
                        }
                    }
                }
                else
                {
                    YourDumped(DataStore.WalkFriend.MyIdentity);
                    DataStore.WalkFriend = null;
                }
            }
            else if (ReturnValues.YoPoza().DistanceTo(DataStore.WalkFriend.ThisPed.Position) > 45f)
            {
                YourNoQuiteDumped(DataStore.WalkFriend.MyIdentity);
                DataStore.WalkFriend = null;
            }
        }
        private bool IsIsSafe()
        {
            bool isSafe = false;
            if (!Function.Call<bool>(Hash._HAS_NAMED_SCALEFORM_MOVIE_LOADED, "instructional_buttons") || !Function.Call<bool>(Hash.IS_PED_RUNNING_MOBILE_PHONE_TASK, Game.Player.Character))
            {
                if (ReturnValues.ButtonDown(DataStore.MySettings.GetlayList, false) && !bPlayerList && !DataStore.bPlayerPiggyBack && !DataStore.bShowPlayerz && !DataStore.bMenuOpen && !DataStore.bClearingUp)
                    isSafe = true;
            }
            return isSafe;
        }
        private void Scaleform_PLAYER_LIST()
        {
            LoggerLight.GetLogging("ScaleDisp.Scaleform_PLAYER_LIST");

            DataStore.iScale = Function.Call<int>((Hash)0x11FE353CF9733E6F, "INSTRUCTIONAL_BUTTONS");

            while (!Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, DataStore.iScale))
                Script.Wait(1);

            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_VOID, DataStore.iScale, "CLEAR_ALL");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, DataStore.iScale, "TOGGLE_MOUSE_BUTTONS");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_BOOL, 0);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            Function.Call(Hash._CALL_SCALEFORM_MOVIE_FUNCTION_VOID, DataStore.iScale, "CREATE_CONTAINER");
            int iAddOns = 0;

            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                int iFailed = 0;
                string sPlayer = DataStore.PedList[i].MyName;
                while (sPlayer.Count() < 14 && iFailed < 10)
                {
                    sPlayer = sPlayer + " ";
                    Script.Wait(1);
                    iFailed += 1;
                }

                sPlayer = sPlayer + DataStore.PedList[i].Level;
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, DataStore.iScale, "SET_DATA_SLOT");
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, sPlayer);
                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                iAddOns += 1;
            }
            for (int i = 0; i < DataStore.AFKList.Count; i++)
            {
                int iFailed = 0;
                string sPlayer = DataStore.AFKList[i].MyName;
                while (sPlayer.Count() < 14 && iFailed < 10)
                {
                    sPlayer = sPlayer + " ";
                    Script.Wait(1);
                    iFailed += 1;
                }

                sPlayer = sPlayer + DataStore.AFKList[i].Level;
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, DataStore.iScale, "SET_DATA_SLOT");
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, sPlayer);
                Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);
                iAddOns += 1;
            }

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, DataStore.iScale, "SET_DATA_SLOT");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, iAddOns);

            if (DataStore.bDisabled)
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, "Mod Disabled");
            else
                Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_STRING, "Players in Session ;  ");

            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, DataStore.iScale, "DRAW_INSTRUCTIONAL_BUTTONS");
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, 1);
            Function.Call(Hash._POP_SCALEFORM_MOVIE_FUNCTION_VOID);

            iDisplay = Game.GameTime + 8000;
            TopLeftUI("Press" + DataStore.ControlSym[DataStore.MySettings.DisableMod] + "for hackers menu mod");

            bPlayerList = true;
        }
        private void CloseBaseHelpBar()
        {
            LoggerLight.GetLogging("ScaleDisp.CloseBaseHelpBar");

            unsafe
            {
                int SF = DataStore.iScale;
                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &SF);
            }

            bPlayerList = false;
        }
        public static void TopLeftUI(string sText)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, sText);
            Function.Call(Hash._0x238FFE5C7B0498A6, false, false, false, 5000);
        }
        public static void BottomLeft(string sText)
        {
            if (!DataStore.MySettings.NoNotify)
                UI.Notify(sText);
        }
    }
}
