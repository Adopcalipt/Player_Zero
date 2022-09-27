using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerZero
{
    public class FindStuff : Script
    {
        public static List<FindVeh> MakeCarz { get; set; }
        public static List<FindPed> MakeFrenz { get; set; }
        private static readonly Vector4 ZeroV = new Vector4(0f, 0f, 0f, 0f);

        public FindStuff()
        {
            LoggerLight.GetLogging("FindStuff Loadin");
            MakeCarz = new List<FindVeh>();
            MakeFrenz = new List<FindPed>();

            Tick += OnTick;
            Interval = 1000;
        }

        private static Vector4 GoGetVeh(FindVeh ThisVeh)
        {
            Vector4 vPlace = new Vector4(0f, 0f, 0f, 0f);

            if (ThisVeh.Brains.IsPlane)
            {
                Vector3 vArea = new Vector3(0f, 0f, 0f).Around(300f);
                vPlace = new Vector4(vArea.X, vArea.Y, 750f, ReturnValues.RandFlow(1, 360));
            }
            else if (ThisVeh.Brains.IsHeli)
            {
                Vector3 Vpos = ReturnValues.YoPoza().Around(300f);
                vPlace = new Vector4(Vpos.X, Vpos.Y, Vpos.Z + 50f, ReturnValues.RandFlow(1, 359));
            }
            else
            {
                LoggerLight.GetLogging("FindStuff.GetVehPos");
                DataStore.iFindingTime = Game.GameTime + 1000;
                Vector3 vArea = Game.Player.Character.Position - (Game.Player.Character.ForwardVector * 15);
                Vehicle[] CarSpot = World.GetNearbyVehicles(vArea, ThisVeh.MaxRadi);
                int iTotal = CarSpot.Count();
                for (int i = 0; i < iTotal; i++)
                {
                    Vehicle Veh = CarSpot[i];
                    if (ClearUp.VehExists(Veh, i, iTotal))
                    {
                        if (Veh.IsPersistent == false && Veh.Position.DistanceTo(Game.Player.Character.Position) > ThisVeh.MinRadi && Veh.ClassType != VehicleClass.Boats && Veh.ClassType != VehicleClass.Cycles && Veh.ClassType != VehicleClass.Helicopters && Veh.ClassType != VehicleClass.Planes && Veh.ClassType != VehicleClass.Trains && Veh != Game.Player.Character.CurrentVehicle && !Veh.IsOnScreen && Veh.EngineRunning)
                        {
                            vPlace = new Vector4(Veh.Position.X, Veh.Position.Y, Veh.Position.Z, Veh.Heading);
                            Veh.Delete();
                            break;
                        }
                    }
                    Script.Wait(1);
                }
            }

            return vPlace;
        }
        private static Vector4 GoGetPeds(FindPed ThisPed)
        {
            Vector4 vPlace = new Vector4(0f, 0f, 0f, 0f);
            LoggerLight.GetLogging("FindStuff.GetPedPos");
            Vector3 vArea = Game.Player.Character.Position - (Game.Player.Character.ForwardVector * 15);
            DataStore.iFindingTime = Game.GameTime + 500;
            Ped[] MadPeds = World.GetNearbyPeds(vArea, ThisPed.MaxRadi);
            int iTotal = MadPeds.Count();
            for (int i = 0; i < iTotal; i++)
            {
                Ped MadP = MadPeds[i];
                if (ClearUp.PedExists(MadP, i, iTotal))
                {
                    if (!MadP.IsOnScreen && !MadP.IsInVehicle() && !MadP.IsDead && Function.Call<int>(Hash.GET_PED_TYPE, MadP.Handle) != 28 && MadP != Game.Player.Character && !MadP.IsPersistent && MadP.Position.DistanceTo(Game.Player.Character.Position) > ThisPed.MinRadi)
                    {
                        vPlace = new Vector4(MadP.Position.X, MadP.Position.Y, MadP.Position.Z, MadP.Heading);
                        MadP.Delete();
                        break;
                    }
                }
                Script.Wait(1);
            }

            return vPlace;
        }
        private void OnTick(object sender, EventArgs e)
        {
            if (!DataStore.bDisabled)
            {               
                if (MakeFrenz.Count > 0)
                {
                    Vector4 Frend = GoGetPeds(MakeFrenz[0]);

                    if (ReturnValues.NotTheSame(ZeroV, Frend) && MakeFrenz.Count > 0)
                    {
                        BuildObjects.GenPlayerPed(new Vector3(Frend.X, Frend.Y, Frend.Z), Frend.R, MakeFrenz[0].Brains);
                        MakeFrenz.RemoveAt(0);
                    }
                }
                
                if (MakeCarz.Count > 0)
                {
                    Vector4 Carz = GoGetVeh(MakeCarz[0]);

                    if (ReturnValues.NotTheSame(ZeroV, Carz) && MakeCarz.Count > 0)
                    {
                        BuildObjects.VehicleSpawn(MakeCarz[0].Brains.PrefredVehicle, new Vector3(Carz.X, Carz.Y, Carz.Z), Carz.R, MakeCarz[0].AddPlayer, MakeCarz[0].Brains, false, MakeCarz[0].CanFill);
                        MakeCarz.RemoveAt(0);
                    }
                }
            }
        } 
    }
}