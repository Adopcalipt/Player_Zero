using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System;
using System.Linq;

namespace PlayerZero
{
    public class FindStuff : Script
    {
        public FindStuff()
        {
            LoggerLight.GetLogging("FindStuff Loadin");

            Tick += OnTick;
            Interval = 1000;
        }
        private static Vehicle LookNear(Vector3 Vec3)
        {
            LoggerLight.GetLogging("FindStuff.LookNear");
            Vehicle Vickary = null;

            if (World.GetNextPositionOnStreet(ReturnValues.YoPoza()).DistanceTo(ReturnValues.YoPoza()) < 95.00f)
            {
                Vehicle[] CarSpot = World.GetNearbyVehicles(Vec3, 200.00f);
                for (int i = 0; i < CarSpot.Count(); i++)
                {
                    if (ClearUp.VehExists(CarSpot, i) && Vickary == null)
                    {
                        Vehicle Veh = CarSpot[i];
                        if (!Veh.IsPersistent && Veh.Position.DistanceTo(ReturnValues.YoPoza()) > 15.00f && Veh.ClassType != VehicleClass.Boats && Veh.ClassType != VehicleClass.Cycles && Veh.ClassType != VehicleClass.Helicopters && Veh.ClassType != VehicleClass.Planes && Veh.ClassType != VehicleClass.Trains && !Veh.IsOnScreen)
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
        private static PositionDirect GetVehPos(float fMinRadi, float fMaxRadi)
        {
            LoggerLight.GetLogging("FindStuff.GetVehPos");
            DataStore.iFindingTime = Game.GameTime + 1000;
            Vector3 vArea = Game.Player.Character.Position - (Game.Player.Character.ForwardVector * 15);
            PositionDirect MyPosDir = null;
            Vehicle[] CarSpot = World.GetNearbyVehicles(vArea, fMaxRadi);
            for (int i = 0; i < CarSpot.Count(); i++)
            {
                if (ClearUp.VehExists(CarSpot, i))
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
        private static void VehRelpace(PositionDirect MyPos, FindVeh MyVeh)
        {
            LoggerLight.GetLogging("FindStuff.VehRelpace");
            VehActions.VehicleSpawn(MyVeh.VehModel, MyPos.Pos, MyPos.Dir, MyVeh.AddPlayer, MyVeh.AddtoBrain, false, false);
        }
        public static void SearchVeh(float fMin, float fMax, string sVehModel, bool bAddPlayer, int iBrainLevel)
        {
            LoggerLight.GetLogging("FindStuff.SearchVeh, sVehModel == " + sVehModel + ",iBrainLevel == " + iBrainLevel);
            FindVeh MyFinda = new FindVeh
            {
                MinRadi = fMin,
                MaxRadi = fMax,
                VehModel = sVehModel,
                AddPlayer = bAddPlayer,
                AddtoBrain = iBrainLevel
            };
            DataStore.MakeCarz.Add(MyFinda);
        }
        private static PositionDirect GetPedPos(float fMinRadi, float fMaxRadi)
        {
            LoggerLight.GetLogging("FindStuff.GetPedPos");
            Vector3 vArea = Game.Player.Character.Position - (Game.Player.Character.ForwardVector * 15);
            DataStore.iFindingTime = Game.GameTime + 500;
            PositionDirect MyPosDir = null;
            Ped[] MadPeds = World.GetNearbyPeds(vArea, fMaxRadi);
            for (int i = 0; i < MadPeds.Count(); i++)
            {
                if (ClearUp.PedExists(MadPeds, i))
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
        private static void PedRelpace(PositionDirect MyPos, FindPed MyPeds)
        {
            LoggerLight.GetLogging("FindStuff.PedRelpace");
            PedActions.GenPlayerPed(MyPos.Pos, MyPos.Dir, MyPeds.MyCar, MyPeds.Seat, MyPeds.Reload, "");
        }
        public static void SearchPed(float fMin, float fMax, Vehicle vMyCar, int iSeat, int iReload)
        {
            LoggerLight.GetLogging("FindStuff.SearchPed, iSeat == " + iSeat + ",iReload == " + iReload);
            FindPed MyFinda = new FindPed
            {
                MinRadi = fMin,
                MaxRadi = fMax,
                MyCar = vMyCar,
                Seat = iSeat,
                Reload = iReload
            };
            DataStore.MakeFrenz.Add(MyFinda);
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (DataStore.bLoadUp)
            {
                if (DataStore.GetInQUe.Count > 0)
                {
                    if (!DataStore.GetInQUe[0].Peddy.IsInVehicle(DataStore.GetInQUe[0].Vhic) && !DataStore.GetInQUe[0].Peddy.IsDead && !DataStore.GetInQUe[0].Peddy.IsFalling)
                    {
                        if (DataStore.iFindingTime < Game.GameTime)
                            PedActions.PedDoGetIn(DataStore.GetInQUe[0]);
                    }
                    else
                    {
                        int iBPed = PedActions.ReteaveBrain(DataStore.GetInQUe[0].PedLevel);
                        DataStore.PedList[iBPed].EnterVehQue = false;
                        if (DataStore.PedList[iBPed].ThisBlip != null)
                            ClearUp.ClearPedBlips(DataStore.GetInQUe[0].PedLevel);
                        DataStore.GetInQUe.RemoveAt(0);
                    }
                }

                if (DataStore.MakeFrenz.Count > 0)
                {
                    if (DataStore.FindMe == null)
                    {
                        if (DataStore.iFindingTime < Game.GameTime)
                            DataStore.FindMe = GetPedPos(DataStore.MakeFrenz[0].MinRadi, DataStore.MakeFrenz[0].MaxRadi);
                    }
                    else
                    {
                        PedRelpace(DataStore.FindMe, DataStore.MakeFrenz[0]);
                        DataStore.MakeFrenz.RemoveAt(0);
                        DataStore.FindMe = null;
                    }
                }

                if (DataStore.MakeCarz.Count > 0)
                {
                    if (DataStore.FindMe == null)
                    {
                        if (DataStore.iFindingTime < Game.GameTime)
                            DataStore.FindMe = GetVehPos(DataStore.MakeCarz[0].MinRadi, DataStore.MakeCarz[0].MaxRadi);
                    }
                    else
                    {
                        VehRelpace(DataStore.FindMe, DataStore.MakeCarz[0]);
                        DataStore.MakeCarz.RemoveAt(0);
                        DataStore.FindMe = null;
                    }
                }
            }
        } 
    }
}