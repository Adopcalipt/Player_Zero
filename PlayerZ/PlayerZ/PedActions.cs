using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System.Collections.Generic;
using System.Linq;

namespace PlayerZero
{
    public class PedActions
    {
        public static List<Vector3> landPlane = new List<Vector3>();
        private static readonly List<Vector3> HeistDrop = new List<Vector3>
            {
                new Vector3(-1105.577f, -1692.11f, 4.345489f),      //0
                new Vector3(60.53763f, 8.939384f, 69.14648f),       //4
                new Vector3(718.9022f, -980.336f, 24.12285f),       //8
                new Vector3(1681.823f, 4817.896f, 42.01214f),       //12
                new Vector3(-1038.972f, -2736.403f, 20.16928f)     //16
            };

        public static void HeistDrips(int iMyArea)
        {
            LoggerLight.GetLogging("PedActions.HeistDrips, iMyArea == " + iMyArea);

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
            {
                PlayerBrain newBrain = new PlayerBrain();
                newBrain.PFMySetting = FreemodePed.MakeFaces();
                newBrain.MyName = ReturnValues.SillyNameList();
                BuildObjects.GenPlayerPed(VectorList[i], RandomNum.RandInt(0, 360), newBrain);
            }

            Script.Wait(1200);
            World.AddExplosion(VectorList[0], ExplosionType.Grenade, 7.00f, 15.00f, true, false);
            DataStore.bHeistPop = false;
        }
        public static int NearHiest()
        {
            LoggerLight.GetLogging("PedActions.NearHiest");

            int iNear = -1;


            for (int i = 0; i < HeistDrop.Count; i++)
            {
                if (HeistDrop[i].DistanceTo(ReturnValues.YoPoza()) < 55.00f)
                {
                    iNear = i;
                    break;
                }
            }
            return iNear;
        }
        public static void FireOrb(string sId, Ped Target, bool bPlayerStrike)
        {
            LoggerLight.GetLogging("PedActions.FireOrb, sId == " + sId);

            Ped pFired = Game.Player.Character;

            int MyBrian = PlayerAI.ReteaveBrain(sId);

            if (MyBrian != -1)
            {
                if (!bPlayerStrike)
                {
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

                    ClearUp.ClearPedBlips(DataStore.PedList[MyBrian].MyIdentity);
                    DataStore.PedList[MyBrian].ThisBlip = BuildObjects.LocalBlip(FacList[RandomNum.RandInt(0, FacList.Count - 1)], 590, DataStore.PedList[MyBrian].MyName);

                    pFired = DataStore.PedList[MyBrian].ThisPed;
                    Script.Wait(7500);
                }

                Vector3 TargetPos = Target.Position;
                if (World.GetGroundHeight(TargetPos) < TargetPos.Z)
                {
                    Vector3 TargF = Target.Position + (Target.ForwardVector * 5);
                    Vector3 TargB = Target.Position - (Target.ForwardVector * 5);
                    Vector3 TargR = Target.Position + (Target.RightVector * 5);
                    Vector3 TargL = Target.Position - (Target.RightVector * 5);
                    OrbExp(pFired, TargetPos, TargF, TargB, TargR, TargL);

                    OrbLoad(DataStore.PedList[MyBrian].MyName, bPlayerStrike);
                    Script.Wait(4000);

                    if (MyBrian != -1)
                    {
                        ClearUp.PedCleaning(DataStore.PedList[MyBrian], "left", false);
                    }
                }
            }
        }
        public static void OrbExp(Ped PFired, Vector3 Pos1, Vector3 Pos2, Vector3 Pos3, Vector3 Pos4, Vector3 Pos5)
        {
            LoggerLight.GetLogging("PedActions.OrbExp, Pos1 == " + Pos1);

            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos2.X, Pos2.Y, Pos2.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos3.X, Pos3.Y, Pos3.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos4.X, Pos4.Y, Pos4.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos5.X, Pos5.Y, Pos5.Z, 49, 1.00f, true, false, 1.00f);
            Function.Call(Hash.ADD_OWNED_EXPLOSION, PFired.Handle, Pos1.X, Pos1.Y, Pos1.Z, 54, 1.00f, true, false, 1.00f);

            Function.Call(Hash.PLAY_SOUND_FROM_COORD, -1, "DLC_XM_Explosions_Orbital_Cannon", Pos1.X, Pos1.Y, Pos1.Z, 0, 0, 1, 0);
            Function.Call((Hash)0x6C38AF3693A69A91, "scr_xm_orbital");
        }
        public static void OrbLoad(string sWhoDidit, bool bPlayerStrike)
        {
            LoggerLight.GetLogging("PedActions.OrbLoad, sWhoDidit == " + sWhoDidit);

            if (bPlayerStrike)
                ScaleDisp.BottomLeft("You obliterated "+ sWhoDidit + " with the Orbital Cannon.");
            else
                ScaleDisp.BottomLeft(sWhoDidit + " obliterated you with the Orbital Cannon.");

            DataStore.iScale = Function.Call<int>((Hash)0x11FE353CF9733E6F, "MIDSIZED_MESSAGE");
            Script.Wait(1500);
            while (!Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, DataStore.iScale))
                Script.Wait(1);

            Function.Call(Hash._START_SCREEN_EFFECT, "SuccessNeutral", 8500, false);
            Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, DataStore.iScale, "SHOW_SHARD_MIDSIZED_MESSAGE");
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
                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, DataStore.iScale, 255, 255, 255, 255);
                Script.Wait(1);
            }

            unsafe
            {
                int SF = DataStore.iScale;
                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &SF);
            }
        }
        public static void WarptoAnyVeh(Vehicle Vhic, Ped Peddy, int iSeat)
        {
            LoggerLight.GetLogging("PedActions.WarptoAnyVeh, iSeat == " + iSeat);

            Function.Call(Hash.SET_PED_INTO_VEHICLE, Peddy.Handle, Vhic.Handle, iSeat);
        }
        public static void GetOutVehicle(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.GetOutVehicle");

            if (Peddy.IsInVehicle())
            {
                Vehicle PedVeh = Peddy.CurrentVehicle;
                Function.Call(Hash.TASK_LEAVE_VEHICLE, Peddy.Handle, PedVeh.Handle, 4160);
            }
        }
        public static void PlayerEnterVeh(Vehicle Vhick, bool bPass)
        {
            LoggerLight.GetLogging("PedActions.PlayerEnterVeh");

            DataStore.iFindingTime = Game.GameTime + 1000;

            int iSeats = ReturnValues.FindUSeat(Vhick, bPass);
                
            if (iSeats != -1)
            {
                int ThreePass = 3;
                while (!Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, Game.Player.Character.Handle) && ThreePass > 0)
                {
                    Script.Wait(1000);
                    Function.Call(Hash.TASK_ENTER_VEHICLE, Game.Player.Character.Handle, Vhick.Handle, -1, iSeats, 1.50f, 1, 0);
                    ThreePass -= 1;
                }

                if (ThreePass < 1)
                    WarptoAnyVeh(Vhick, Game.Player.Character, iSeats);
            }
        }
        public static void EmptyVeh(Vehicle Vhic)
        {
            LoggerLight.GetLogging("PedActions.EmptyVeh");

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
        public static void PedDoGetIn(Vehicle GetV, Ped Peddy, string sId)
        {
            LoggerLight.GetLogging("PedActions.PedDoGetIn");

            int iSeats = ReturnValues.FindUSeat(GetV, true);
            int iThree = 3;

            if (iSeats > -1)
            {
                if (Peddy.Position.DistanceTo(GetV.Position) < 65.00f)
                {
                    while (!Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, Peddy.Handle) && iThree > 0)
                    {
                        iThree--;
                        Function.Call(Hash.TASK_ENTER_VEHICLE, Peddy.Handle, GetV.Handle, -1, iSeats, 1.50f, 1, 0);
                        Script.Wait(1000);
                    }

                    if (iThree < 1)
                        WarptoAnyVeh(GetV, Peddy, iSeats);
                }
                else
                    WarptoAnyVeh(GetV, Peddy, iSeats);
            }
            else
            {
                int iBPed = PlayerAI.ReteaveBrain(sId);
                if (iBPed != -1)
                {
                    if (DataStore.PedList[iBPed].ThisVeh != null)
                        DataStore.PedList[iBPed].ThisVeh.MarkAsNoLongerNeeded();

                    DataStore.PedList[iBPed].Passenger = false;
                    DataStore.PedList[iBPed].Driver = true;
                    if (DataStore.PedList[iBPed].PrefredVehicle == 0)
                        DataStore.PedList[iBPed].PrefredVehicle = 1;
                    FindVeh MyFinda = new FindVeh(1.00f, 95.00f, false, false, DataStore.PedList[iBPed]);
                    FindStuff.MakeCarz.Add(MyFinda);
                }
            }
        }
        public static void FolllowTheLeader(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.FolllowTheLeader");

            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, DataStore.iFollowMe);
            Peddy.RelationshipGroup = DataStore.Gp_Follow;
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);

            if (DataStore.MySettings.Aggression > 5)
                Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_PLAYER, Peddy.Handle, Game.Player.Character.Handle, true);
            else
                Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_PLAYER, Peddy.Handle, Game.Player.Character.Handle, false);
        }
        public static void OhDoKeepUp(Ped Peddy)
        {
            Peddy.Task.ClearAll();

            float fXpos = -2.50f;
            float fYpos = 1.50f;

            DataStore.iFolPos += 1;
            if (DataStore.iFolPos == 1)
            {
                fXpos = -2.50f;
                fYpos = 0.00f;
            }
            else if (DataStore.iFolPos == 2)
            {
                fXpos = -2.50f;
                fYpos = -2.50f;
            }
            else if (DataStore.iFolPos == 3)
            {
                fXpos = 2.50f;
                fYpos = 0.00f;
            }
            else if (DataStore.iFolPos == 4)
            {
                fXpos = 1.50f;
                fYpos = 0.00f;
            }
            else if (DataStore.iFolPos == 5)
            {
                fXpos = -1.50f;
                fYpos = 0.00f;
            }
            else if (DataStore.iFolPos == 6)
            {
                fXpos = 2.50f;
                fYpos = -2.50f;
            }
            else if (DataStore.iFolPos == 7)
            {
                fXpos = -1.50f;
                fYpos = -2.50f;
                DataStore.iFolPos = 0;
            }

            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, Peddy.Handle, Game.Player.Character.Handle, fXpos, fYpos, 0.0f, 1.0f, -1, 2.5f, true);

            Peddy.BlockPermanentEvents = false;
        }
        public static void DriveBye(Ped Peddy, Ped Target)
        {
            LoggerLight.GetLogging("PedActions.DriveBye");

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    if (Target.IsInVehicle())
                        Peddy.Task.VehicleChase(Target);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, Target.Position, 10.00f, 45.00f, 0);
                }
                else
                    Peddy.Task.VehicleShootAtPed(Target);
            }
        }
        public static void DriveAround(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.DriveAround");

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                    Peddy.Task.CruiseWithVehicle(Peddy.CurrentVehicle, 85.00f, 262972);
                Peddy.AlwaysKeepTask = true;
                Peddy.BlockPermanentEvents = true;
            }
        }
        public static void DriveToooPlayer(Ped Peddy, bool bRunOver)
        {
            LoggerLight.GetLogging("PedActions.DriveTooo, bRunOver == " + bRunOver);

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {

                    if (bRunOver)
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, ReturnValues.YoPoza(), 1.00f, 45.00f, 0);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, ReturnValues.YoPoza(), 10.00f, 25.00f, 0);

                    Peddy.AlwaysKeepTask = true;
                    Peddy.BlockPermanentEvents = true;
                }
            }
        }
        public static void LandNearHeli(Ped Peddy, Vehicle vHick, Vector3 vTarget)
        {
            float HeliDesX = vTarget.X;
            float HeliDesY = vTarget.Y;
            float HeliDesZ = vTarget.Z;
            float HeliSpeed = 35f;
            float HeliLandArea = 10f;

            float dx = vHick.Position.X - HeliDesX;
            float dy = vHick.Position.Y - HeliDesY;
            float HeliDirect = Function.Call<float>(Hash.GET_HEADING_FROM_VECTOR_2D, dx, dy) - 180.00f;

            Function.Call(Hash.TASK_HELI_MISSION, Peddy.Handle, vHick.Handle, 0, 0, HeliDesX, HeliDesY, HeliDesZ, 20, HeliSpeed, HeliLandArea, HeliDirect, -1, -1, -1, 0);

            Function.Call(Hash.SET_PED_FIRING_PATTERN, Peddy.Handle, Function.Call<int>(Hash.GET_HASH_KEY, "FIRING_PATTERN_BURST_FIRE_HELI"));
            Peddy.AlwaysKeepTask = true;
            Peddy.BlockPermanentEvents = true;
        }
        public static void LandNearPlane(Ped Peddy, Vehicle vHick, Vector3 vStart, Vector3 vFinish)
        {
            Function.Call(Hash.TASK_PLANE_LAND, Peddy.Handle, vHick.Handle, vStart.X, vStart.Y, vStart.Z, vFinish.X, vFinish.Y, vFinish.Z);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
        }
        public static List<Vector3> BuildFlightPath(Vector3 vStart)
        {
            landPlane.Clear();

            List<Vector3> landSand = new List<Vector3>
            {
                 new Vector3(225.8934f, 2841.527f, 200.0402f),
                 new Vector3(796.0992f, 3011.926f, 90.13193f),
                 new Vector3(1495.307f, 3187.998f, 41.04951f),
                 new Vector3(1655.049f, 3249.205f, 41.21964f),
                 new Vector3(1561.392f, 3160.818f, 41.1649f),
                 new Vector3(1334.507f, 2924.953f, 98.35621f),
                 new Vector3(798.5253f, 2388.362f, 282.331f),
                 new Vector3(413.3483f, 2034.074f, 425.4946f),
                 new Vector3(-175.5016f, 1448.899f, 598.845f),
                 new Vector3(-349.6658f, -187.2563f, 398.4032f)
            };
            List<Vector3> landLS = new List<Vector3>
            {
                 new Vector3(-1002.727f, -1650.774f, 134.2087f),
                 new Vector3(-1193.304f, -1941.04f, 59.51603f),
                 new Vector3(-1571.467f, -2617.15f, 14.57554f),
                 new Vector3(-1612.011f, -2789.524f, 14.62421f),
                 new Vector3(-1532.074f, -2835.987f, 14.58676f),
                 new Vector3(-991.79f, -3147.605f, 90.8317f),
                 new Vector3(-440.5424f, -3123.56f, 232.0305f),
                 new Vector3(-88.80471f, -2403.97f, 234.262f),
                 new Vector3(-18.7144f, -1591.593f, 351.0859f),
                 new Vector3(29.53166f, 219.7558f, 581.6113f),
                 new Vector3(-169.9742f, 1746.14f, 484.2034f)
            };

            float f1 = landSand[0].DistanceTo(vStart);
            float f2 = landLS[0].DistanceTo(vStart);

            if (f1 < f2)
            {
                DataStore.FlyMeToo = landLS[0];
                return landSand;
            }
            else
            {
                DataStore.FlyMeToo = landSand[0];
                return landLS;
            }
        }
        public static void DriveToooDest(Ped Peddy, Vector3 Vme, float fSpeed)
        {
            LoggerLight.GetLogging("PedActions.DriveToooDest, Vme == " + Vme);

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    Peddy.Task.DriveTo(Peddy.CurrentVehicle, Vme, 1.00f, fSpeed, 262972);
                }
            }
        }
        public static void DriveDirect(Ped Peddy, Vector3 Vme, float fSpeed)
        {
            LoggerLight.GetLogging("PedActions.DriveDirect, Vme == " + Vme);

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    Peddy.Task.DriveTo(Peddy.CurrentVehicle, Vme, 1.00f, fSpeed, 16777216);
                }
            }
        }
        public static void FightPlayer(Ped Peddy, bool bInVeh)
        {
            LoggerLight.GetLogging("PedActions.FightPlayer");

            Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
            Peddy.RelationshipGroup = DataStore.GP_Attack;
            Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy.Handle);
            Peddy.IsEnemy = true;
            Peddy.CanBeTargette﻿d﻿ = true;
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 46, true);
            if (!bInVeh)
                Peddy.Task.FightAgainst(Game.Player.Character);
            else
                DriveBye(Peddy, Game.Player.Character);
        }
        public static void GreefWar(Ped Peddy, Ped Victim)
        {
            LoggerLight.GetLogging("PedActions.GreefWar");
            if (Victim != null)
            {
                Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy.Handle);
                Victim.IsEnemy = true;
                Victim.CanBeTargette﻿d﻿ = true;
                Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 46, true);

                Peddy.Task.FightAgainst(Victim);
            }
        }
        public static void FlyHeli(Ped Pedd, Vehicle Vhick, Vector3 vHeliDest, float fSpeed, float flanding)
        {
            LoggerLight.GetLogging("PedActions.FlyHeli");

            Vhick.FreezePosition = false;

            float HeliDesX = vHeliDest.X;
            float HeliDesY = vHeliDest.Y;
            float HeliDesZ = vHeliDest.Z;
            float HeliSpeed = fSpeed;
            float HeliLandArea = flanding;

            float dx = Pedd.Position.X - HeliDesX;
            float dy = Pedd.Position.Y - HeliDesY;
            float HeliDirect = Function.Call<float>(Hash.GET_HEADING_FROM_VECTOR_2D, dx, dy) - 180.00f;

            Function.Call(Hash.TASK_HELI_MISSION, Pedd.Handle, Vhick.Handle, 0, 0, HeliDesX, HeliDesY, HeliDesZ, 9, HeliSpeed, HeliLandArea, HeliDirect, -1, -1, -1, 0);
            Function.Call(Hash.SET_PED_FIRING_PATTERN, Pedd.Handle, Function.Call<int>(Hash.GET_HASH_KEY, "FIRING_PATTERN_BURST_FIRE_HELI"));
            Pedd.AlwaysKeepTask = true;
            Pedd.BlockPermanentEvents = true;
        }
        public static void FlyPlane(Ped Pedd, Vehicle Vhick, Vector3 vPlaneDest, Ped AttackPLayer)
        {
            LoggerLight.GetLogging("PedActions.FlyPlane");

            float fAngle = Vector3.Angle(Vhick.Position, vPlaneDest);
            if (AttackPLayer != null)
            {
                Function.Call(Hash.TASK_PLANE_MISSION, Pedd.Handle, Vhick.Handle, 0, AttackPLayer, 0, 0, 0, 6, 0.0f, 0.0f, fAngle, 1000.0f, -5000.0f);
                Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Pedd.Handle, 0, true);
                Pedd.RelationshipGroup = DataStore.GP_Mental;
                Pedd.BlockPermanentEvents = false;
            }
            else
                Function.Call(Hash.TASK_PLANE_MISSION, Pedd.Handle, Vhick.Handle, 0, 0, vPlaneDest.X, vPlaneDest.Y, vPlaneDest.Z, 6, 20f, 50f, -1f, fAngle, 50, 1);
        }
        public static void MoneyDrops(string sId)
        {
            int MyPed = PlayerAI.ReteaveBrain(sId);

            if (DataStore.Plops.Count < 6 && DataStore.iMoneyDropRate < Game.GameTime)
            {
                DataStore.iMoneyDropRate = Game.GameTime + RandomNum.RandInt(200, 800);
                Vector3 vTarget = DataStore.PedList[MyPed].ThisPed.Position.Around(4f);

                BuildObjects.BuildProps("prop_money_bag_01", vTarget, Vector3.Zero, true, true);

                DataStore.PedList[MyPed].ThisPed.Task.RunTo(vTarget, true);
            }
            else
            {
                for (int i = 0; i < DataStore.Plops.Count; i++)
                {
                    if (DataStore.PedList[MyPed].ThisPed.Position.DistanceTo(DataStore.Plops[i].Position) < 1.00f)
                    {
                        if (i > 0)
                            DataStore.PedList[MyPed].ThisPed.Task.RunTo(DataStore.Plops[i - 1].Position, true);

                        DataStore.Plops[i].Delete();
                        DataStore.Plops.RemoveAt(i);

                        break;
                    }
                }
            }
        }
        public static void RemoveMoneyDrop(string sId)
        {
            DataStore.iMoneyDrops = -1;
            DataStore.iMoneyDropRate = 0;
            DataStore.sMoneyPicker = "";
            int MyPed = PlayerAI.ReteaveBrain(sId);
            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].Follower && DataStore.PedList[i].ThisPed != null)
                    DataStore.PedList[i].ThisPed.Task.FightAgainst(DataStore.PedList[MyPed].ThisPed, -1);
            }
            ClearUp.ClearProps();
        }
        public static void EclipsWindMill()
        {
            LoggerLight.GetLogging("PedActions.AddEclipsWindMill");
            if (DataStore.WindMill == null)
                DataStore.WindMill = BuildObjects.BuildProps("prop_windmill_01", new Vector3(-832.50f, 290.95f, 82.00f), new Vector3(-90.00f, 94.72f, 0.00f), false, false);
            else
            {
                DataStore.WindMill.Delete();
                DataStore.WindMill = null;
            }
        }
        public static void HackerTime(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.HackerTime");

            if (ReturnValues.YoPoza().DistanceTo(new Vector3(-778.81F, 312.66F, 84.70F)) < 80.00f && DataStore.WindMill == null)
            {
                EclipsWindMill();
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
                DataStore.bPiggyBack = true;
            }// MegaMonster
            else
            {
                ForceAnim(Peddy, "amb@code_human_in_bus_passenger_idles@female@sit@idle_a", "idle_a", Peddy.Position, Peddy.Rotation);
                Peddy.AttachTo(Game.Player.Character, 31086, new Vector3(0.10f, 0.15f, 0.61f), new Vector3(0.00f, 0.00f, 180.00f));
                DataStore.bPiggyBack = true;
            }// Clones

            //Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, Peddy.Handle);
        }
        public static void RoBoCar(Vehicle Atchoo)
        {
            LoggerLight.GetLogging("PedActions.RoBoCar");

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
                DataStore.Vicks.Add(new Vehicle(Tanks.Handle));
            }
            Vehicle Planes = World.CreateVehicle(VehicleHash.Hydra, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(DataStore.Vicks[iHyd_01], 0, Hyda_01, Hydb_01);
            DataStore.Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Hydra, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(DataStore.Vicks[iHyd_02], 0, Hyda_02, Hydb_02);
            DataStore.Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Skylift, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(DataStore.Vicks[iSky], 0, Skylifta, Skyliftb);
            DataStore.Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Lazer, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(DataStore.Vicks[Las_01], 0, Lasa_01, Lasa_01);
            DataStore.Vicks.Add(new Vehicle(Planes.Handle));

            Planes = World.CreateVehicle(VehicleHash.Lazer, new Vector3(0.00f, 0.00f, 150.00f));
            Planes.IsPersistent = true;
            Planes.AttachTo(DataStore.Vicks[Las_02], 0, Lasa_02, Lasa_02);
            DataStore.Vicks.Add(new Vehicle(Planes.Handle));
        }
        public static void ForceAnim(Ped peddy, string sAnimDict, string sAnimName, Vector3 AnPos, Vector3 AnRot)
        {
            LoggerLight.GetLogging("PedActions.ForceAnim, sAnimName == " + sAnimName);

            peddy.Task.ClearAll();
            Function.Call(Hash.REQUEST_ANIM_DICT, sAnimDict);
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, sAnimDict))
                Script.Wait(1);
            Function.Call(Hash.TASK_PLAY_ANIM_ADVANCED, peddy.Handle, sAnimDict, sAnimName, AnPos.X, AnPos.Y, AnPos.Z, AnRot.X, AnRot.Y, AnRot.Z, 8.0f, 0.00f, -1, 1, 0.01f, 0, 0);
            Function.Call(Hash.REMOVE_ANIM_DICT, sAnimDict);
        }
        public static void GunningIt(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.GunningIt");

            List<string> sWeapList = new List<string>();

            int iGun = 0;

            if (DataStore.MySettings.Aggression > 1)
            {
                if (DataStore.MySettings.Aggression < 3)
                    iGun = 1;
                else
                {
                    if (DataStore.MySettings.SpaceWeaps)
                        iGun = RandomNum.FindRandom(14, 2, 10);
                    else
                        iGun = RandomNum.FindRandom(14, 2, 9);
                }
            }

            if (iGun == 1)
            {
                sWeapList.Add("WEAPON_dagger");  //0x92A27487",
                sWeapList.Add("WEAPON_hammer");  //0x4E875F73",
                sWeapList.Add("WEAPON_battleaxe");  //0xCD274149",
                sWeapList.Add("WEAPON_golfclub");  //0x440E4788",
                sWeapList.Add("WEAPON_machete");  //0xDD5DF8D9",
            }
            else if (iGun == 2)
            {
                sWeapList.Add("WEAPON_dagger");  //0x92A27487",
                sWeapList.Add("WEAPON_pipebomb");  //0xBA45E8B8",
                sWeapList.Add("WEAPON_navyrevolver");  //0x917F6C8C"
                sWeapList.Add("WEAPON_combatpdw");  //0xA3D4D34",
                sWeapList.Add("WEAPON_sawnoffshotgun");  //0x7846A318",
                sWeapList.Add("WEAPON_sniperrifle");  //0x5FC3C11",
            }
            else if (iGun == 3)
            {
                sWeapList.Add("WEAPON_hammer");  //0x4E875F73",
                sWeapList.Add("WEAPON_revolver");  //0xC1B3C3D1",
                sWeapList.Add("WEAPON_smg");  //0x2BE6766B",
                sWeapList.Add("WEAPON_pumpshotgun");  //0x1D073A89",
                sWeapList.Add("WEAPON_advancedrifle");  //0xAF113F99",
            }
            else if (iGun == 4)
            {
                sWeapList.Add("WEAPON_battleaxe");  //0xCD274149",
                sWeapList.Add("WEAPON_molotov");  //0x24B17070",
                sWeapList.Add("WEAPON_stungun");  //0x3656C8C1",
                sWeapList.Add("WEAPON_microsmg");  //0x13532244",
                sWeapList.Add("WEAPON_musket");  //0xA89CB99E",
                sWeapList.Add("WEAPON_gusenberg");  //0x61012683"--69
            }
            else if (iGun == 5)
            {
                sWeapList.Add("WEAPON_golfclub");  //0x440E4788",
                sWeapList.Add("WEAPON_grenade");  //0x93E220BD",
                sWeapList.Add("WEAPON_appistol");  //0x22D8FE39",
                sWeapList.Add("WEAPON_assaultshotgun");  //0xE284C527",
                sWeapList.Add("WEAPON_mg");  //0x9D07F764",
            }
            else if (iGun == 6)
            {
                sWeapList.Add("WEAPON_machete");  //0xDD5DF8D9",
                sWeapList.Add("WEAPON_heavypistol");  //0xD205520E",
                sWeapList.Add("WEAPON_microsmg");  //0x13532244",
                sWeapList.Add("WEAPON_specialcarbine");  //0xC0A3098D",

            }
            else if (iGun == 7)
            {
                sWeapList.Add("WEAPON_flashlight");  //0x8BB05FD7",
                sWeapList.Add("WEAPON_GADGETPISTOL");  //0xAF3696A1",--new to cayo ppero
                sWeapList.Add("WEAPON_MILITARYRIFLE");  //0x624FE830"--65
                sWeapList.Add("WEAPON_COMBATSHOTGUN");  //0x5A96BA4--54
            }
            else if (iGun == 8)
            {
                sWeapList.Add("WEAPON_marksmanrifle");  //0xC734385A",
            }
            else if (iGun == 9)
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
            else if (iGun == 10)
            {
                sWeapList.Add("WEAPON_raypistol");  //0xAF3696A1",--36
                sWeapList.Add("WEAPON_raycarbine");  //0x476BF155"--44
                sWeapList.Add("weapon_rayminigun");
            }

            for (int i = 0; i < sWeapList.Count; i++)
                Function.Call(Hash.GIVE_WEAPON_TO_PED, Peddy, Function.Call<int>(Hash.GET_HASH_KEY, sWeapList[i]), 9999, false, true);
        }
        public static Ped FindAFight(Ped Attacker)
        {
            Ped Fight = null;

            for (int i = 0; i < DataStore.PedList.Count;i++)
            {
                if (DataStore.MySettings.Aggression < 4)
                {
                    if (DataStore.PedList[i].ThisPed != Attacker)
                    {
                        Fight = DataStore.PedList[i].ThisPed;
                        break;
                    }
                }
                else
                {
                    if (!DataStore.PedList[i].Friendly)
                    {
                        Fight = DataStore.PedList[i].ThisPed;
                        break;
                    }
                }
            }

            return Fight;
        }
        public static void PickFight(Ped Attacker, Vehicle Plane, Ped fight, int iVehType)
        {
            if (fight != null)
            {
                if (iVehType == 1 || iVehType == 6) 
                    DriveBye(Attacker, fight);
                else if (iVehType == 2 || iVehType == 4)
                    FlyHeli(Attacker, Plane, fight.Position, 45.00f, 0.00f);
                else
                    FlyPlane(Attacker, Plane, Vector3.Zero, fight);
            }
        }
    }
}
