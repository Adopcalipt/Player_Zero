using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;

namespace PlayerZero
{
    public class PlayerAI
    {
        private static int iNpcList = 0;
        private static int iBlpList = 0;

        public static int WhoShotMe(Ped MeDie)
        {
            LoggerLight.GetLogging("PedActions.WhoShotMe");

            int iShoot = -1;

            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (YourKilller(MeDie) == DataStore.PedList[i].ThisPed)
                {
                    iShoot = i;
                    break;
                }
            }
            return iShoot;
        }
        public static Ped YourKilller(Ped Victim)
        {
            LoggerLight.GetLogging("PedActions.YourKilller");

            Ped ThePerp = null;
            Entity ThisEnt = Function.Call<Entity>((Hash)0x93C8B64DEB84728C, Victim.Handle);
            if (ThisEnt != null)
            {
                if (Function.Call<bool>(Hash.IS_ENTITY_A_PED, ThisEnt.Handle))
                    ThePerp = new Ped(ThisEnt.Handle);

                if (Function.Call<bool>(Hash.IS_ENTITY_A_VEHICLE, ThisEnt.Handle))
                {
                    Vehicle Vic = new Vehicle(ThisEnt.Handle);
                    ThePerp = Vic.GetPedOnSeat(VehicleSeat.Driver);
                }
            }
            return ThePerp;
        }
        public static void AddToPedList(PlayerBrain myBrain)
        {
            DataStore.PedList.Add(myBrain);
            Script.Wait(10);
        }
        public static void AddToAFKList(AfkPlayer myAfker)
        {
            DataStore.AFKList.Add(myAfker);
            Script.Wait(10);
        }
        public static void RemoveFromPedList(PlayerBrain myBrain)
        {
            DataStore.PedList.Remove(myBrain);
            Script.Wait(10);
        }
        public static void RemoveFromAFKList(AfkPlayer myAfker)
        {
            DataStore.AFKList.Remove(myAfker);
            Script.Wait(10);
        }
        public static void RemoveFromPedList(int iPlace)
        {
            if (iPlace < DataStore.PedList.Count && iPlace > -1)
                DataStore.PedList.RemoveAt(iPlace);
            Script.Wait(10);
        }
        public static void RemoveFromAFKList(int iPlace)
        {
            if (iPlace < DataStore.AFKList.Count && iPlace > -1)
                DataStore.AFKList.RemoveAt(iPlace);
            Script.Wait(10);
        }
        private static void BlipingBlip(int iPos)
        {
            if (DataStore.PedList[iPos].ThisPed.IsInVehicle())
            {
                if (DataStore.PedList[iPos].DirBlip != null)
                {
                    ClearUp.ClearPedBlips(DataStore.PedList[iPos].MyIdentity);
                    if (DataStore.PedList[iPos].Driver && DataStore.PedList[iPos].ThisVeh != null)
                        DataStore.PedList[iPos].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[iPos].ThisPed, BuildObjects.OhMyBlip(DataStore.PedList[iPos].ThisVeh), DataStore.PedList[iPos].MyName, DataStore.PedList[iPos].BlipColour);
                }
            }
            else
            {
                if (DataStore.PedList[iPos].DirBlip != null)
                    BuildObjects.BlipDirect(DataStore.PedList[iPos].DirBlip, DataStore.PedList[iPos].ThisPed.Heading);
                else
                {
                    ClearUp.ClearPedBlips(DataStore.PedList[iPos].MyIdentity);
                    DataStore.PedList[iPos].Passenger = false;
                    DataStore.PedList[iPos].ThisPed.BlockPermanentEvents = false;
                    DataStore.PedList[iPos].ThisPed.AlwaysKeepTask = false;
                    DataStore.PedList[iPos].DirBlip = BuildObjects.DirectionalBlimp(DataStore.PedList[iPos].ThisPed);
                    DataStore.PedList[iPos].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[iPos].ThisPed, 1, DataStore.PedList[iPos].MyName, DataStore.PedList[iPos].BlipColour);
                    if (DataStore.PedList[iPos].Follower)
                    {
                        DataStore.PedList[iPos].ApprochPlayer = false;
                        PedActions.OhDoKeepUp(DataStore.PedList[iPos].ThisPed);
                    }
                }
            }
        }
        public static void NoMoreBlips()
        {
            for (int i = 0; i < DataStore.PedList.Count; i++)
                ClearUp.ClearPedBlips(DataStore.PedList[i].MyIdentity);

            for (int i = 0; i < DataStore.AddtoPedList.Count; i++)
            {
                if (DataStore.AddtoPedList[i].DirBlip != null)
                    DataStore.AddtoPedList[i].DirBlip.Remove();
                DataStore.AddtoPedList[i].DirBlip = null;

                if (DataStore.AddtoPedList[i].ThisBlip != null)
                    DataStore.AddtoPedList[i].ThisBlip.Remove();
                DataStore.AddtoPedList[i].ThisBlip = null;
            }
        }
        public static int ReteaveBrain(string sId)
        {
            LoggerLight.GetLogging("PlayerAI.ReteaveBrain, sId == " + sId);

            int iAm = -1;
            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].MyIdentity == sId)
                {
                    iAm = i;
                    break;
                }
            }
            LoggerLight.GetLogging("PlayerAI.ReteaveBrain, iAm == " + iAm);
            return iAm;
        }
        public static int ReteaveAfk(string sId)
        {
            LoggerLight.GetLogging("PlayerAI.ReteaveAfk, sId == " + sId);

            int iAm = -1;
            for (int i = 0; i < DataStore.AFKList.Count; i++)
            {
                if (DataStore.AFKList[i].MyIdentity == sId)
                {
                    iAm = i;
                    break;
                }
            }
            return iAm;
        }
        public static int ReteveContact(string sId)
        {
            LoggerLight.GetLogging("PlayerAI.ReteveContact, sId == " + sId);

            int iAm = -1;
            for (int i = 0; i < DataStore.PlayContList.PedBrain.Count; i++)
            {
                if (DataStore.PlayContList.PedBrain[i].MyIdentity == sId)
                {
                    iAm = i;
                    break;
                }
            }
            return iAm;
        }
        public static bool BrainNumberCheck(int iNumber)
        {
            bool bRunAgain = false;
            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].Level == iNumber)
                {
                    bRunAgain = true;
                    break;
                }
            }
            for (int i = 0; i < DataStore.AFKList.Count; i++)
            {
                if (DataStore.AFKList[i].Level == iNumber)
                {
                    bRunAgain = true;
                    break;
                }
            }
            return bRunAgain;
        }
        public static void LaggOut()
        {
            LoggerLight.GetLogging("PedActions.LaggOut");

            DataStore.bClearingUp = true;
            FindStuff.MakeFrenz.Clear();
            FindStuff.MakeCarz.Clear();
            DataStore.bPlanePort = false;

            while (DataStore.PedList.Count > 0)
            {
                PlayerDump();
                Script.Wait(100);
            }

            DataStore.iFollow = 0;
            DataStore.bClearingUp = false;
        }
        public static void PlayerDump()
        {
            LoggerLight.GetLogging("PedActions.PlayerDump");

            for (int i = 0; i < DataStore.PedList.Count; i++)
                ClearUp.PedCleaning(DataStore.PedList[i], "left", false);

            for (int i = 0; i < DataStore.AFKList.Count; i++)
                ClearUp.DeListingBrains(DataStore.AFKList[i]);

            for (int i = 0; i < DataStore.AddtoPedList.Count; i++)
                ClearUp.PedCleaning(DataStore.AddtoPedList[i], "left", false);
        }
        public static void PlayerZerosAI()
        {
            if (DataStore.bDisabled)
            {
                if (DataStore.PedList.Count > 0 || DataStore.AFKList.Count > 0)
                    LaggOut();
            }
            else
            {
                if (DataStore.PedList.Count > 0)
                {
                    if (iNpcList < DataStore.PedList.Count)
                    {
                        if (!DataStore.MySettings.NoRadar && !DataStore.PedList[iNpcList].Bounty)
                        {
                            if (!DataStore.PedList[iNpcList].OffRadarBool && DataStore.PedList[iNpcList].ThisPed != null)
                                BlipingBlip(iNpcList);
                            else
                            {
                                if (!DataStore.PedList[iNpcList].OffRadarBool && DataStore.PedList[iNpcList].OffRadar == -1)
                                {
                                    DataStore.PedList[iNpcList].OffRadar = Game.GameTime + 300000;
                                    ClearUp.ClearPedBlips(DataStore.PedList[iNpcList].MyIdentity);
                                    ScaleDisp.BottomLeft("~h~" + DataStore.PedList[iNpcList].MyName + "~s~ has gone off radar");
                                }
                                else if (!DataStore.MySettings.NoRadar)
                                {
                                    if (DataStore.PedList[iNpcList].OffRadarBool)
                                    {
                                        if (DataStore.PedList[iNpcList].OffRadar < Game.GameTime)
                                        {
                                            DataStore.PedList[iNpcList].OffRadarBool = false;
                                            ClearUp.ClearPedBlips(DataStore.PedList[iNpcList].MyIdentity);
                                            DataStore.PedList[iNpcList].DirBlip = BuildObjects.DirectionalBlimp(DataStore.PedList[iNpcList].ThisPed);
                                            DataStore.PedList[iNpcList].ThisBlip = BuildObjects.PedBlimp(DataStore.PedList[iNpcList].ThisPed, 1, DataStore.PedList[iNpcList].MyName, DataStore.PedList[iNpcList].BlipColour);
                                        }
                                    }
                                }
                            }
                        }

                        if (DataStore.PedList[iNpcList].ThisPed == null)
                        {

                        }
                        else if (DataStore.PedList[iNpcList].WanBeFriends)
                        {
                            if (DataStore.PedList[iNpcList].ThisPed.HasBeenDamagedBy(Game.Player.Character) || Game.Player.IsTargetting(DataStore.PedList[iNpcList].ThisPed))
                            {
                                DataStore.PedList[iNpcList].WanBeFriends = false;
                                DataStore.PedList[iNpcList].ThisPed.BlockPermanentEvents = false;
                                DataStore.PedList[iNpcList].ThisPed.AlwaysKeepTask = false;
                                if (DataStore.WalkFriend != null)
                                {
                                    if (DataStore.PedList[iNpcList].ThisPed.Handle == DataStore.WalkFriend.ThisPed.Handle)
                                        DataStore.WalkFriend = null;
                                }
                                if (DataStore.FlyFriend != null)
                                {
                                    if (DataStore.PedList[iNpcList].ThisPed.Handle == DataStore.FlyFriend.ThisPed.Handle)
                                        DataStore.FlyFriend = null;
                                }
                                if (DataStore.DriveFriend != null)
                                {
                                    if (DataStore.PedList[iNpcList].ThisPed.Handle == DataStore.DriveFriend.ThisPed.Handle)
                                        DataStore.DriveFriend = null;
                                }
                                if (DataStore.PedList[iNpcList].Follower)
                                {
                                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iNpcList].ThisPed.Handle);
                                    DataStore.PedList[iNpcList].Follower = false;
                                    DataStore.iFollow -= 1;
                                }

                                if (DataStore.MySettings.Aggression > 2)
                                {
                                    ClearUp.ClearPedBlips(DataStore.PedList[iNpcList].MyIdentity);
                                    DataStore.PedList[iNpcList].BlipColour = 1;
                                    DataStore.PedList[iNpcList].TimeOn += 120000;
                                    PedActions.FightPlayer(DataStore.PedList[iNpcList].ThisPed, false);
                                    DataStore.PedList[iNpcList].Friendly = false;
                                }
                                else
                                {
                                    DataStore.PedList[iNpcList].ThisPed.Task.FleeFrom(Game.Player.Character);
                                }
                            }
                        }
                        else if (DataStore.PedList[iNpcList].TimeOn < Game.GameTime)
                        {
                            LoggerLight.GetLogging("PlayerZerosAI.Time to leave");
                            Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iNpcList].ThisPed.Handle);
                            PedActions.GetOutVehicle(DataStore.PedList[iNpcList].ThisPed);
                            ClearUp.PedCleaning(DataStore.PedList[iNpcList], "left", false);
                        }
                        else if (Game.Player.Character.GetKiller() == DataStore.PedList[iNpcList].ThisPed)
                        {
                            DataStore.PedList[iNpcList].Kills += 1;
                            WhileYouDead(DataStore.PedList[iNpcList].MyName, DataStore.PedList[iNpcList].Killed, DataStore.PedList[iNpcList].Kills, DataStore.PedList[iNpcList].ThisPed);
                            if (DataStore.MySettings.Aggression < 5)
                            {
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iNpcList].ThisPed.Handle);
                                PedActions.GetOutVehicle(DataStore.PedList[iNpcList].ThisPed);
                                ClearUp.PedCleaning(DataStore.PedList[iNpcList], "left", false);
                            }
                        }
                        else if (DataStore.PedList[iNpcList].ThisPed.IsDead)
                        {
                            if (DataStore.PedList[iNpcList].DeathSequence == 0)
                            {
                                if (DataStore.PedList[iNpcList].ThisOppress != null)
                                {
                                    PedActions.EmptyVeh(DataStore.PedList[iNpcList].ThisOppress);
                                    DataStore.PedList[iNpcList].ThisOppress.Explode();
                                    DataStore.PedList[iNpcList].ThisOppress.MarkAsNoLongerNeeded();
                                    DataStore.PedList[iNpcList].ThisOppress = null;

                                    if (DataStore.PedList[iNpcList].ThisVeh != null)
                                    {
                                        PedActions.EmptyVeh(DataStore.PedList[iNpcList].ThisVeh);
                                        DataStore.PedList[iNpcList].ThisVeh.Delete();
                                        DataStore.PedList[iNpcList].ThisVeh = null;
                                    }
                                }
                                else if (DataStore.PedList[iNpcList].ThisVeh != null)
                                {
                                    PedActions.EmptyVeh(DataStore.PedList[iNpcList].ThisVeh);
                                    DataStore.PedList[iNpcList].ThisVeh.MarkAsNoLongerNeeded();
                                    DataStore.PedList[iNpcList].ThisVeh = null;
                                }

                                if (DataStore.PedList[iNpcList].PlaneLand > 0)
                                {
                                    DataStore.PedList[iNpcList].PlaneLand = -1;
                                    DataStore.bPlanePort = false;
                                }

                                int iDie = WhoShotMe(DataStore.PedList[iNpcList].ThisPed);

                                ClearUp.ClearPedBlips(DataStore.PedList[iNpcList].MyIdentity);

                                if (YourKilller(DataStore.PedList[iNpcList].ThisPed) == Game.Player.Character)
                                {
                                    if (ReteveContact(DataStore.PedList[iNpcList].MyIdentity) != -1)
                                    {
                                        OnlinePlayerz.RemoveMyFriend(DataStore.PedList[iNpcList].MyIdentity);
                                        DataStore.PedList[iNpcList].PrefredVehicle = 8;
                                    }

                                    if (DataStore.PedList[iNpcList].Bounty)
                                        Game.Player.Money += 7000;

                                    if (DataStore.PedList[iNpcList].PlaneLand != -1)
                                        DataStore.bPlanePort = false;

                                    DataStore.PedList[iNpcList].Friendly = false;
                                    DataStore.PedList[iNpcList].BlipColour = 1;
                                    DataStore.PedList[iNpcList].ApprochPlayer = false;
                                    DataStore.PedList[iNpcList].Follower = false;
                                    DataStore.PedList[iNpcList].Killed += 1;

                                    ScaleDisp.BottomLeft("You  " + DataStore.PedList[iNpcList].Killed + " - " + DataStore.PedList[iNpcList].Kills + " " + DataStore.PedList[iNpcList].MyName);
                                }
                                else if (iDie != -1)
                                    ScaleDisp.BottomLeft(DataStore.PedList[iDie].MyName + " Killed " + DataStore.PedList[iNpcList].MyName);
                                else
                                    ScaleDisp.BottomLeft(DataStore.PedList[iNpcList].MyName + " died");

                                DataStore.PedList[iNpcList].DeathSequence++;
                                DataStore.PedList[iNpcList].DeathTime = Game.GameTime + 10000;
                                DataStore.PedList[iNpcList].TimeOn += 60000;
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iNpcList].ThisPed.Handle);
                            }
                            else if (DataStore.PedList[iNpcList].DeathSequence == 1 || DataStore.PedList[iNpcList].DeathSequence == 3 || DataStore.PedList[iNpcList].DeathSequence == 5 || DataStore.PedList[iNpcList].DeathSequence == 7)
                            {
                                if (DataStore.PedList[iNpcList].DeathTime < Game.GameTime)
                                {
                                    DataStore.PedList[iNpcList].ThisPed.Alpha = 80;
                                    DataStore.PedList[iNpcList].DeathSequence ++;
                                    DataStore.PedList[iNpcList].DeathTime = Game.GameTime + 500;
                                }
                            }
                            else if (DataStore.PedList[iNpcList].DeathSequence == 2 || DataStore.PedList[iNpcList].DeathSequence == 4 || DataStore.PedList[iNpcList].DeathSequence == 6)
                            {
                                if (DataStore.PedList[iNpcList].DeathTime < Game.GameTime)
                                {
                                    DataStore.PedList[iNpcList].ThisPed.Alpha = 255;
                                    DataStore.PedList[iNpcList].DeathSequence ++;
                                    DataStore.PedList[iNpcList].DeathTime = Game.GameTime + 500;
                                }
                            }
                            else if (DataStore.PedList[iNpcList].DeathSequence == 8)
                            {
                                if (DataStore.PedList[iNpcList].DeathTime < Game.GameTime)
                                {
                                    LoggerLight.GetLogging("PlayerZerosAI.BringoutDead");
                                    ClearUp.ClearPedBlips(DataStore.PedList[iNpcList].MyIdentity);

                                    DataStore.PedList[iNpcList].Bounty = false;
                                    DataStore.PedList[iNpcList].InCombat = false;
                                    DataStore.PedList[iNpcList].SessionJumper = false;
                                    DataStore.PedList[iNpcList].EnterVehQue = false;
                                    DataStore.PedList[iNpcList].Passenger = false;
                                    DataStore.PedList[iNpcList].Befallen = false;
                                    DataStore.PedList[iNpcList].OffRadarBool = false;
                                    if (DataStore.PedList[iNpcList].Friendly || DataStore.PedList[iNpcList].Follower)
                                    {
                                        DataStore.PedList[iNpcList].ApprochPlayer = true;
                                        DataStore.PedList[iNpcList].WanBeFriends = false;
                                    }
                                    DataStore.PedList[iNpcList].DeathSequence = 0;
                                    DataStore.PedList[iNpcList].FindPlayer = 0;
                                    DataStore.PedList[iNpcList].DeathTime = 0;
                                    DataStore.PedList[iNpcList].ThisPed.Delete();
                                    DataStore.PedList[iNpcList].ThisPed = null;

                                    if (DataStore.PedList[iNpcList].Killed > RandomNum.RandInt(8, 22) || DataStore.MySettings.Aggression < 3)
                                    {
                                        ClearUp.PedCleaning(DataStore.PedList[iNpcList], "left", false);
                                    }
                                    else if (DataStore.PedList[iNpcList].Killed > 15 && DataStore.PedList[iNpcList].Kills == 0 && DataStore.MySettings.Aggression == 11)
                                        PedActions.FireOrb(DataStore.PedList[iNpcList].MyIdentity, Game.Player.Character, false);
                                    else
                                    {
                                        if (DataStore.PedList[iNpcList].PrefredVehicle > 0)
                                        {
                                            FindVeh MyFinda = new FindVeh(35.00f, 220.00f, true, false, DataStore.PedList[iNpcList]);
                                            FindStuff.MakeCarz.Add(MyFinda);
                                        }
                                        else
                                        {
                                            FindPed MyFinda = new FindPed(35.00f, 220.00f, DataStore.PedList[iNpcList]);
                                            FindStuff.MakeFrenz.Add(MyFinda);
                                        }
                                    }
                                }
                            }
                        }
                        else if (DataStore.PedList[iNpcList].ThisPed.Position.Z + 10.00f < World.GetGroundHeight(DataStore.PedList[iNpcList].ThisPed.Position))
                        {
                            if (DataStore.PedList[iNpcList].Befallen)
                            {
                                if (DataStore.PedList[iNpcList].DeathTime < Game.GameTime)
                                    DataStore.PedList[iNpcList].ThisPed.Kill();
                            }
                            else
                            {
                                DataStore.PedList[iNpcList].Befallen = true;
                                DataStore.PedList[iNpcList].DeathTime = Game.GameTime + 5000;
                            }
                        }
                        else if (DataStore.PedList[iNpcList].Befallen)
                            DataStore.PedList[iNpcList].Befallen = false;
                        else if (DataStore.PedList[iNpcList].Hacker)
                        {

                        }
                        else if (DataStore.PedList[iNpcList].SessionJumper)
                        {
                            if (DataStore.PedList[iNpcList].ThisPed.Position.DistanceTo(ReturnValues.YoPoza()) < 10.00f)
                            {
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iNpcList].ThisPed.Handle);
                                ClearUp.PedCleaning(DataStore.PedList[iNpcList], "has disappeared", true);
                            }
                        }
                        else if (DataStore.PedList[iNpcList].Follower)
                        {
                            if (DataStore.PedList[iNpcList].ApprochPlayer)
                            {
                                if (DataStore.PedList[iNpcList].ThisVeh == null)
                                {
                                    DataStore.PedList[iNpcList].ApprochPlayer = false;
                                    DataStore.PedList[iNpcList].Driver = false;
                                }
                                else if (!DataStore.PedList[iNpcList].WanBeFriends && ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iNpcList].ThisPed.Position) < 25f)
                                {
                                    if (DataStore.WalkFriend != null)
                                    {
                                        ScaleDisp.YourNoQuiteDumped(DataStore.WalkFriend.MyIdentity);
                                        DataStore.WalkFriend = null;
                                    }
                                    JoinMe joinX = new JoinMe(DataStore.PedList[iNpcList].MyIdentity, DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, false, 2);
                                    DataStore.PedList[iNpcList].WanBeFriends = true;
                                    DataStore.PedList[iNpcList].ApprochPlayer = false;
                                    DataStore.PedList[iNpcList].ThisPed.BlockPermanentEvents = true;
                                    DataStore.PedList[iNpcList].ThisPed.AlwaysKeepTask = true;
                                    DataStore.WalkFriend = joinX;
                                }
                                else
                                {
                                    if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                    {
                                        if (DataStore.PedList[iNpcList].IsPlane)
                                            DataStore.PedList[iNpcList].ApprochPlayer = false;
                                        else if (DataStore.PedList[iNpcList].IsHeli)
                                            PedActions.LandNearHeli(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, ReturnValues.YoPoza() + (Game.Player.Character.ForwardVector * 5));
                                        else
                                            PedActions.DriveToooPlayer(DataStore.PedList[iNpcList].ThisPed, false);

                                        DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                    }
                                }
                            }
                            else if (DataStore.PedList[iNpcList].EnterVehQue)
                            {
                                if (Game.Player.Character.IsInVehicle())
                                {
                                    if (DataStore.PedList[iNpcList].ThisPed.IsInVehicle())
                                    {
                                        if (DataStore.PedList[iNpcList].ThisPed.SeatIndex == VehicleSeat.Driver)
                                        {
                                            DataStore.PedList[iNpcList].ApprochPlayer = true;
                                            DataStore.PedList[iNpcList].Passenger = false;
                                            DataStore.PedList[iNpcList].Driver = true;
                                        }
                                        else
                                        {
                                            DataStore.PedList[iNpcList].Passenger = true;
                                            DataStore.PedList[iNpcList].Driver = false;
                                        }
                                        DataStore.PedList[iNpcList].EnterVehQue = false;
                                    }
                                }
                                else
                                    DataStore.PedList[iNpcList].EnterVehQue = false;
                            }
                            else if (Game.Player.Character.IsInVehicle())
                            {
                                if (DataStore.PedList[iNpcList].Driver)
                                {
                                    if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                    {
                                        if (Game.IsWaypointActive)
                                        {
                                            if (World.GetWaypointPosition() != DataStore.LetsGoHere)
                                            {
                                                DataStore.LetsGoHere = World.GetWaypointPosition();

                                                if (DataStore.PedList[iNpcList].IsPlane)
                                                {

                                                }
                                                else if (DataStore.PedList[iNpcList].IsHeli)
                                                {
                                                    Vector3 vecHigh = new Vector3(DataStore.LetsGoHere.X, DataStore.LetsGoHere.Y, DataStore.LetsGoHere.Z + 50f);
                                                    PedActions.FlyHeli(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, vecHigh, 45f, 0f);
                                                }
                                                else
                                                    PedActions.DriveToooDest(DataStore.PedList[iNpcList].ThisPed, DataStore.LetsGoHere, 55f);
                                            }
                                            else if (DataStore.PedList[iNpcList].IsHeli)
                                            {
                                                if (DataStore.PedList[iNpcList].ThisPed.Position.DistanceTo(DataStore.LetsGoHere) < 45f)
                                                    PedActions.LandNearHeli(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, DataStore.LetsGoHere);
                                            }
                                        }
                                        else
                                        {
                                            if (DataStore.PedList[iNpcList].ThisEnemy != null)
                                            {
                                                if (!DataStore.PedList[iNpcList].ThisEnemy.Exists())
                                                    DataStore.PedList[iNpcList].ThisEnemy = null;
                                                else if (DataStore.PedList[iNpcList].ThisEnemy.IsDead)
                                                    DataStore.PedList[iNpcList].ThisEnemy = null;
                                            }
                                            else
                                            {
                                                DataStore.PedList[iNpcList].ThisEnemy = PedActions.FindAFight(DataStore.PedList[iNpcList].ThisPed);
                                                PedActions.PickFight(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, DataStore.PedList[iNpcList].ThisEnemy, DataStore.PedList[iNpcList].PrefredVehicle);
                                            }
                                        }
                                        DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                    }
                                }
                                else if (DataStore.PedList[iNpcList].Passenger)
                                {

                                }
                                else
                                {
                                    DataStore.PedList[iNpcList].EnterVehQue = true;
                                    PedActions.PedDoGetIn(Game.Player.Character.CurrentVehicle, DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].MyIdentity);
                                    DataStore.PedList[iNpcList].TimeOn += 60000;
                                }
                            }
                            else
                            {
                                if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                {
                                    if (DataStore.PedList[iNpcList].Driver || DataStore.PedList[iNpcList].Passenger)
                                    {
                                        if (!Game.Player.Character.IsInVehicle())
                                            PedActions.GetOutVehicle(DataStore.PedList[iNpcList].ThisPed);
                                        DataStore.PedList[iNpcList].Driver = false;
                                        DataStore.PedList[iNpcList].Passenger = false;
                                        DataStore.PedList[iNpcList].ThisPed.BlockPermanentEvents = false;
                                        DataStore.PedList[iNpcList].ThisPed.AlwaysKeepTask = false;
                                    }
                                    else if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iNpcList].ThisPed.Position) > 150.00f)
                                    {
                                        DataStore.PedList[iNpcList].ThisPed.Position = ReturnValues.YoPoza() + (Game.Player.Character.RightVector * 2);
                                        PedActions.OhDoKeepUp(DataStore.PedList[iNpcList].ThisPed);
                                    }
                                    DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                }
                            }
                        }
                        else if (DataStore.PedList[iNpcList].Friendly)
                        {
                            if (DataStore.PedList[iNpcList].ThisPed.HasBeenDamagedBy(Game.Player.Character) || DataStore.PedList[iNpcList].ThisPed.IsInCombatAgainst(Game.Player.Character) || Game.Player.IsTargetting(DataStore.PedList[iNpcList].ThisPed))
                            {
                                DataStore.PedList[iNpcList].ApprochPlayer = false;
                                DataStore.PedList[iNpcList].WanBeFriends = false;
                                DataStore.PedList[iNpcList].ThisPed.BlockPermanentEvents = false;
                                DataStore.PedList[iNpcList].ThisPed.AlwaysKeepTask = false;
                                if (DataStore.PedList[iNpcList].Follower)
                                {
                                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iNpcList].ThisPed.Handle);
                                    DataStore.PedList[iNpcList].Follower = false;
                                    DataStore.iFollow -= 1;
                                }

                                if (DataStore.MySettings.Aggression > 2)
                                {
                                    ClearUp.ClearPedBlips(DataStore.PedList[iNpcList].MyIdentity);
                                    DataStore.PedList[iNpcList].BlipColour = 1;
                                    DataStore.PedList[iNpcList].TimeOn += 120000;
                                    PedActions.FightPlayer(DataStore.PedList[iNpcList].ThisPed, false);
                                    DataStore.PedList[iNpcList].Friendly = false;
                                }
                                else
                                {
                                    DataStore.PedList[iNpcList].ThisPed.Task.FleeFrom(Game.Player.Character);
                                }
                            }
                            else
                            {
                                if (DataStore.PedList[iNpcList].PlaneLand > 0)
                                {
                                    if (DataStore.PedList[iNpcList].PlaneLand == 10)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisPed.IsInVehicle(DataStore.PedList[iNpcList].ThisVeh))
                                        {
                                            if (PedActions.landPlane.Count == 0)
                                                PedActions.landPlane = PedActions.BuildFlightPath(ReturnValues.YoPoza());
                                            DataStore.PedList[iNpcList].ThisVeh.FreezePosition = false;
                                            DataStore.PedList[iNpcList].ThisPed.BlockPermanentEvents = true;
                                            DataStore.PedList[iNpcList].ThisPed.AlwaysKeepTask = true;
                                            DataStore.PedList[iNpcList].ThisPed.CanBeDraggedOutOfVehicle = false;
                                            Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, DataStore.PedList[iNpcList].ThisPed.Handle, 16777216);
                                            DataStore.PedList[iNpcList].FlightPath = 0;
                                            PedActions.FlyPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath], null);
                                            DataStore.PedList[iNpcList].PlaneLand--;
                                        }
                                    }
                                    else if (DataStore.PedList[iNpcList].PlaneLand == 9)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisVeh.Position.DistanceTo(PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath]) < 150f)
                                        {
                                            PedActions.LandNearPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, PedActions.landPlane[1], PedActions.landPlane[2]);
                                            DataStore.PedList[iNpcList].WanBeFriends = false;
                                            DataStore.PedList[iNpcList].ApprochPlayer = true;
                                            DataStore.PedList[iNpcList].FlightPath++;
                                            DataStore.PedList[iNpcList].FlightPath++;
                                            DataStore.PedList[iNpcList].PlaneLand--;
                                        }
                                        else
                                            PedActions.FlyPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath], null);
                                    }
                                    else if (DataStore.PedList[iNpcList].PlaneLand == 8)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisVeh.Position.DistanceTo(PedActions.landPlane[2]) < 25)
                                        {
                                            DataStore.PedList[iNpcList].ThisVeh.FreezePosition = true;
                                            DataStore.PedList[iNpcList].PlaneLand--;
                                        }
                                    }
                                    else if (DataStore.PedList[iNpcList].PlaneLand == 7)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisVeh.Position.DistanceTo(ReturnValues.YoPoza()) < 50)
                                        {
                                            BuildObjects.StayOnGround(DataStore.PedList[iNpcList].ThisVeh);

                                            JoinMe joinX = new JoinMe(DataStore.PedList[iNpcList].MyIdentity, DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, false, 4);
                                            DataStore.PedList[iNpcList].WanBeFriends = true;
                                            DataStore.PedList[iNpcList].ApprochPlayer = false;
                                            DataStore.FlyFriend = joinX;
                                            DataStore.PedList[iNpcList].PlaneLand--;
                                        }
                                    }
                                    else if (DataStore.PedList[iNpcList].PlaneLand == 5)
                                    {
                                        if (Game.Player.Character.IsInVehicle(DataStore.PedList[iNpcList].ThisVeh))
                                        {
                                            DataStore.PedList[iNpcList].ThisVeh.FreezePosition = false;
                                            DataStore.PedList[iNpcList].FlightPath++;
                                            PedActions.DriveDirect(DataStore.PedList[iNpcList].ThisPed, PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath], 5f);
                                            DataStore.PedList[iNpcList].PlaneLand--;
                                        }
                                    }
                                    else if (DataStore.PedList[iNpcList].PlaneLand == 4)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisVeh.Position.DistanceTo(PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath]) < 5f)
                                        {
                                            DataStore.PedList[iNpcList].FlightPath++;
                                            PedActions.DriveDirect(DataStore.PedList[iNpcList].ThisPed, PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath], 5f);
                                            DataStore.PedList[iNpcList].PlaneLand--;
                                        }
                                    }
                                    else if (DataStore.PedList[iNpcList].PlaneLand == 3)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisVeh.Position.DistanceTo(PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath]) < 5f)
                                        {
                                            DataStore.PedList[iNpcList].FlightPath++;
                                            PedActions.FlyPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath], null);
                                            DataStore.PedList[iNpcList].PlaneLand--;
                                        }
                                    }
                                    else if (DataStore.PedList[iNpcList].PlaneLand == 2)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisVeh.Position.DistanceTo(PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath]) < 150f)
                                        {
                                            if (DataStore.PedList[iNpcList].ThisVeh.LandingGear == VehicleLandingGear.Deployed)
                                                DataStore.PedList[iNpcList].ThisVeh.LandingGear = VehicleLandingGear.Closing;

                                            DataStore.PedList[iNpcList].FlightPath++;
                                            if (DataStore.PedList[iNpcList].FlightPath < PedActions.landPlane.Count)
                                                PedActions.FlyPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath], null);
                                            else
                                            {
                                                DataStore.PedList[iNpcList].FlightPath = 0;
                                                PedActions.landPlane = PedActions.BuildFlightPath(DataStore.FlyMeToo);
                                                PedActions.FlyPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, PedActions.landPlane[0], null);
                                                DataStore.PedList[iNpcList].PlaneLand = 9;

                                            }
                                        }
                                        else 
                                            PedActions.FlyPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, PedActions.landPlane[DataStore.PedList[iNpcList].FlightPath], null);
                                    }
                                }
                                else if (DataStore.PedList[iNpcList].Passenger)
                                {
                                    LoggerLight.GetLogging("PlayerZerosAI--HereItCrash?");

                                    if (ReturnValues.InSameVeh(DataStore.PedList[iNpcList].ThisPed) && DataStore.iFollow < 7)
                                    {
                                        DataStore.PedList[iNpcList].Follower = true;
                                        DataStore.PedList[iNpcList].ApprochPlayer = false;
                                        DataStore.PedList[iNpcList].WanBeFriends = false;
                                        DataStore.PedList[iNpcList].BlipColour = 38;
                                        PedActions.FolllowTheLeader(DataStore.PedList[iNpcList].ThisPed);
                                        DataStore.PedList[iNpcList].TimeOn += 600000;
                                        DataStore.iFollow += 1;
                                    }
                                }
                                else if (DataStore.PedList[iNpcList].Driver)
                                {
                                    if (DataStore.PedList[iNpcList].ApprochPlayer && DataStore.DriveFriend == null)
                                    {
                                        if (!DataStore.PedList[iNpcList].WanBeFriends)
                                        {
                                            if (!DataStore.PedList[iNpcList].WanBeFriends && ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iNpcList].ThisPed.Position) < 25f)
                                            {
                                                JoinMe joinX = new JoinMe(DataStore.PedList[iNpcList].MyIdentity, DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, true, 2);
                                                DataStore.PedList[iNpcList].WanBeFriends = true;
                                                DataStore.PedList[iNpcList].ApprochPlayer = false;
                                                DataStore.DriveFriend = joinX;
                                            }
                                            else
                                            {
                                                if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                                {
                                                    if (DataStore.iFollow < 7)
                                                    {
                                                        if (DataStore.PedList[iNpcList].IsPlane && DataStore.PedList[iNpcList].PlaneLand > 0)
                                                            DataStore.PedList[iNpcList].ApprochPlayer = false;
                                                        else if (DataStore.PedList[iNpcList].IsHeli)
                                                            PedActions.LandNearHeli(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, ReturnValues.YoPoza() + (Game.Player.Character.ForwardVector * 5));
                                                        else
                                                            PedActions.DriveToooPlayer(DataStore.PedList[iNpcList].ThisPed, false);
                                                    }
                                                    else
                                                        DataStore.PedList[iNpcList].ApprochPlayer = false;
                                                    DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iNpcList].ThisPed.Position) < 25f)
                                        {
                                            if (DataStore.WalkFriend == null && DataStore.PedList[iNpcList].ApprochPlayer)
                                            {
                                                JoinMe joinX = new JoinMe(DataStore.PedList[iNpcList].MyIdentity, DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, true, 2);
                                                DataStore.PedList[iNpcList].WanBeFriends = true;
                                                DataStore.PedList[iNpcList].ApprochPlayer = false;
                                                DataStore.WalkFriend = joinX;
                                            }
                                        }
                                        else if (DataStore.MySettings.Aggression < 2)
                                        {
                                            if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                            {
                                                PedActions.DriveAround(DataStore.PedList[iNpcList].ThisPed);
                                                DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                            }
                                        }
                                        else
                                        {
                                            if (DataStore.PedList[iNpcList].ThisEnemy != null)
                                            {
                                                if (!DataStore.PedList[iNpcList].ThisEnemy.Exists())
                                                    DataStore.PedList[iNpcList].ThisEnemy = null;
                                                else if (DataStore.PedList[iNpcList].ThisEnemy.IsDead)
                                                    DataStore.PedList[iNpcList].ThisEnemy = null;
                                            }
                                            else
                                            {
                                                if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                                {
                                                    DataStore.PedList[iNpcList].ThisEnemy = PedActions.FindAFight(DataStore.PedList[iNpcList].ThisPed);
                                                    PedActions.PickFight(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, DataStore.PedList[iNpcList].ThisEnemy, DataStore.PedList[iNpcList].PrefredVehicle);
                                                    DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iNpcList].ThisPed.Position) < 7.00f && !DataStore.PedList[iNpcList].ThisPed.IsInVehicle() && DataStore.iFollow < 7)
                                    {
                                        if (!DataStore.PedList[iNpcList].WanBeFriends && DataStore.PedList[iNpcList].ApprochPlayer)
                                        {
                                            if (DataStore.WalkFriend == null)
                                            {
                                                if (Game.Player.Character.IsInVehicle())
                                                {
                                                    if (Game.Player.Character.SeatIndex == VehicleSeat.Driver)
                                                    {
                                                        if (!DataStore.PedList[iNpcList].WanBeFriends)
                                                        {
                                                            JoinMe joinX = new JoinMe(DataStore.PedList[iNpcList].MyIdentity, DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, true, 1);
                                                            DataStore.PedList[iNpcList].WanBeFriends = true;
                                                            DataStore.PedList[iNpcList].ApprochPlayer = false;
                                                            DataStore.WalkFriend = joinX;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (!DataStore.PedList[iNpcList].WanBeFriends)
                                                    {
                                                        JoinMe joinX = new JoinMe(DataStore.PedList[iNpcList].MyIdentity, DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, true, 3);
                                                        DataStore.PedList[iNpcList].WanBeFriends = true;
                                                        DataStore.PedList[iNpcList].ApprochPlayer = false;
                                                        DataStore.PedList[iNpcList].ThisPed.BlockPermanentEvents = true;
                                                        DataStore.PedList[iNpcList].ThisPed.AlwaysKeepTask = true;

                                                        DataStore.WalkFriend = joinX;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (DataStore.MySettings.Aggression > 4)
                                    {
                                        if (DataStore.PedList[iNpcList].ThisEnemy != null)
                                        {
                                            if (!DataStore.PedList[iNpcList].ThisEnemy.Exists())
                                                DataStore.PedList[iNpcList].ThisEnemy = null;
                                            else if (DataStore.PedList[iNpcList].ThisEnemy.IsDead)
                                                DataStore.PedList[iNpcList].ThisEnemy = null;
                                        }
                                        else
                                        {
                                            if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                            {
                                                DataStore.PedList[iNpcList].ThisEnemy = PedActions.FindAFight(DataStore.PedList[iNpcList].ThisPed);
                                                PedActions.GreefWar(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisEnemy);
                                                DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (DataStore.PedList[iNpcList].Driver)
                            {
                                if (DataStore.PedList[iNpcList].FindPlayer < Game.GameTime)
                                {
                                    DataStore.PedList[iNpcList].FindPlayer = Game.GameTime + 5000;
                                    if (DataStore.PedList[iNpcList].PrefredVehicle == 1 || DataStore.PedList[iNpcList].PrefredVehicle == 6)
                                        PedActions.DriveBye(DataStore.PedList[iNpcList].ThisPed, Game.Player.Character);
                                    else if (DataStore.PedList[iNpcList].PrefredVehicle == 2 || DataStore.PedList[iNpcList].PrefredVehicle == 4)
                                        PedActions.FlyHeli(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, Game.Player.Character.Position, 45f, 0f);
                                    else
                                        PedActions.FlyPlane(DataStore.PedList[iNpcList].ThisPed, DataStore.PedList[iNpcList].ThisVeh, Game.Player.Character.Position, Game.Player.Character);
                                }
                            }
                        }

                        iNpcList ++;
                    }
                    else
                        iNpcList = 0;
                }

                if (DataStore.AFKList.Count > 0)
                {
                    if (iBlpList < DataStore.AFKList.Count)
                    {
                        AfkPlayer HouseBlip = DataStore.AFKList[iBlpList];

                        if (HouseBlip.TimeOn < Game.GameTime)
                        {
                            ClearUp.DeListingBrains(HouseBlip);
                        }
                        iBlpList ++;
                    }
                    else
                        iBlpList = 0;
                }

                for (int i = 0; i < DataStore.AddtoPedList.Count; i++)
                {
                    LoggerLight.GetLogging("AddtoPedList");
                    AddToPedList(DataStore.AddtoPedList[i]);
                    DataStore.AddtoPedList.RemoveAt(i);
                }

                if (DataStore.iNextPlayer < Game.GameTime && ReturnValues.PlayerZinSesh() < DataStore.MySettings.MaxPlayers)
                    BuildObjects.NewPlayer();

                DataStore.bScan = false;
            }
        }
        public static void WhileYouDead(string Kellar, int iKills, int iKilled, Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.WhileYouDead, string == " + Kellar + ", iKills == " + iKills + ", iKilled == " + iKilled);

            while (Game.Player.Character.GetKiller() == Peddy)
                Script.Wait(1);
            Script.Wait(1000);
            ScaleDisp.BottomLeft("You  " + iKills + " - " + iKilled + " " + Kellar);
        }
    }
}
