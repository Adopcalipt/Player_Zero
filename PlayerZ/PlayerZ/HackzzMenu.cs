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
        private static void NewPlayer()
        {
            LoggerLight.GetLogging("PedActions.NewPlayer, iCurrentPlayerz == " + DataStore.iCurrentPlayerz);
            ReturnValues.SetBTimer(ReturnValues.RandInt(DataStore.MySettings.iMinWait, DataStore.MySettings.iMaxWait), 1);
            IniSettings.LoadIniSetts();

            int iHeister = NearHiest();

            if (DataStore.iCurrentPlayerz + 5 < DataStore.MySettings.iMaxPlayers && iHeister != -1 && DataStore.bHeistPop)
                HeistDrips(iHeister);
            else if (DataStore.iCurrentPlayerz < DataStore.MySettings.iMaxPlayers)
            {
                if (ReturnValues.FindRandom(0, 1, 10) < 6)
                {
                    if (ReturnValues.FindRandom(1, 1, 10) < 6)
                        FindStuff.SearchPed(35.00f, 220.00f, null, 0, -1);
                    else
                    {
                        int iTypeO = ReturnValues.FindRandom(2, 1, 9);
                        FindStuff.SearchVeh(15.00f, 145.00f, VehActions.RandVeh(iTypeO), true, -1);
                    }
                }
                else
                    InABuilding();
                DataStore.iCurrentPlayerz += 1;
            }
        }
        private static void InABuilding()
        {
            LoggerLight.GetLogging("PedActions.InABuilding");

            List<int> iKeepItReal = new List<int>();

            for (int i = 0; i < DataStore.AFKList.Count(); i++)
                iKeepItReal.Add(DataStore.AFKList[i].App);

            int iMit = ReturnValues.RandInt(0, DataStore.AFKPlayers.Count - 1);

            while (iKeepItReal.Contains(iMit))
                iMit = ReturnValues.RandInt(0, DataStore.AFKPlayers.Count - 1);

            string sName = ReturnValues.SillyNameList();
            Blip FakeB = BlipActions.LocalBlip(DataStore.AFKPlayers[iMit], 417, sName);

            AfkPlayer MyAfk = new AfkPlayer
            {
                ThisBlip = FakeB,
                App = iMit,
                Level = ReturnValues.FindRandom(12, 1, 400),
                TimeOn = Game.GameTime + ReturnValues.RandInt(DataStore.MySettings.iMinSession, DataStore.MySettings.iMaxSession),
                MyName = sName
            };
            DataStore.AFKList.Add(MyAfk);

            ClearUp.BackItUpBrain();
        }
        private static void HeistDrips(int iMyArea)
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
                PedActions.GenPlayerPed(VectorList[i], 90.00f, null, 0, -1);
            DataStore.iCurrentPlayerz += 4;
            Script.Wait(1200);
            World.AddExplosion(VectorList[0], ExplosionType.Grenade, 7.00f, 15.00f, true, false);
            DataStore.bHeistPop = false;
        }
        private static int NearHiest()
        {
            LoggerLight.GetLogging("PedActions.NearHiest");

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
                if (VectorList[i].DistanceTo(ReturnValues.YoPoza()) < 55.00f)
                {
                    iNear = i;
                    break;
                }
            }
            return iNear;
        }
        public static void FireOrb(int MyBrian, Ped Target)
        {
            LoggerLight.GetLogging("PedActions.FireOrb, MyBrian == " + MyBrian);

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

                ClearUp.ClearPedBlips(DataStore.PedList[MyBrian].Level);
                DataStore.PedList[MyBrian].ThisBlip = BlipActions.LocalBlip(FacList[ReturnValues.RandInt(0, FacList.Count - 1)], 590, DataStore.PedList[MyBrian].MyName);

                pFired = DataStore.PedList[MyBrian].ThisPed;
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
                    OrbLoad(DataStore.PedList[MyBrian].MyName);
                    Script.Wait(4000);
                    ClearUp.PedCleaning(DataStore.PedList[MyBrian].Level, "left", false);
                }
            }
        }
        private static void OrbExp(Ped PFired, Vector3 Pos1, Vector3 Pos2, Vector3 Pos3, Vector3 Pos4, Vector3 Pos5)
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
        private static void OrbLoad(string sWhoDidit)
        {
            LoggerLight.GetLogging("PedActions.OrbLoad, sWhoDidit == " + sWhoDidit);

            UI.Notify(sWhoDidit + " obliterated you with the Orbital Cannon.");

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
        public static Ped GenPlayerPed(Vector3 vLocal, float fAce, Vehicle vMyCar, int iSeat, int iReload)
        {
            LoggerLight.GetLogging("PedActions.GenPlayerPed, iSeat == " + iSeat + ", iReload == " + iReload);

            Ped MyPed = null;
            bool bMale = false;
            int iOldPed = ReteaveBrain(iReload);
            string sPeddy = "mp_f_freemode_01";

            if (iOldPed == -1)
            {
                if (ReturnValues.FindRandom(13, 0, 20) < 10)
                {
                    bMale = true;
                    sPeddy = "mp_m_freemode_01";
                }
            }
            else
            {
                if (DataStore.PedList[iOldPed].PFMySetting.PFMale)
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
                    Script.Wait(1);

                MyPed = Function.Call<Ped>(Hash.CREATE_PED, 4, model, vLocal.X, vLocal.Y, vLocal.Z, fAce, true, false);
                Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, model.Hash);

                if (MyPed.Exists())
                {
                    int iAccuracy = ReturnValues.RandInt(DataStore.MySettings.iAccMin, DataStore.MySettings.iAccMax);
                    Function.Call(Hash.SET_PED_ACCURACY, MyPed.Handle, iAccuracy);
                    MyPed.MaxHealth = ReturnValues.RandInt(200, 400);
                    MyPed.Health = MyPed.MaxHealth;

                    if (iOldPed == -1)
                        OnlineFaceTypes(MyPed, bMale, vMyCar, iSeat, null, -1);
                    else
                        OnlineFaceTypes(MyPed, bMale, vMyCar, iSeat, DataStore.PedList[iOldPed].PFMySetting, iReload);
                }
                else
                    MyPed = null;
            }
            else
                MyPed = null;

            return MyPed;
        }
        public static int ReteaveBrain(int iNumber)
        {
            LoggerLight.GetLogging("PedActions.ReteaveBrain, iNumber == " + iNumber);

            int iAm = -1;
            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].Level == iNumber)
                {
                    iAm = i;
                    break;
                }
            }
            return iAm;
        }
        private static void OnlineFaceTypes(Ped Pedx, bool bMale, Vehicle vMyCar, int iSeat, PedFixtures Fixtures, int iReload)
        {
            LoggerLight.GetLogging("PedActions.OnlineFaceTypes, iSeat == " + iSeat + ", iReload == " + iReload);

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
                    shapeFirstID = ReturnValues.RandInt(0, 20);
                    shapeSecondID = ReturnValues.RandInt(0, 20);
                    shapeThirdID = shapeFirstID;
                    skinFirstID = shapeFirstID;
                    skinSecondID = shapeSecondID;
                    skinThirdID = shapeThirdID;
                }
                else
                {
                    MyNewFixings.PFMale = false;
                    shapeFirstID = ReturnValues.RandInt(21, 41);
                    shapeSecondID = ReturnValues.RandInt(21, 41);
                    shapeThirdID = shapeFirstID;
                    skinFirstID = shapeFirstID;
                    skinSecondID = shapeSecondID;
                    skinThirdID = shapeThirdID;
                }
                shapeMix = ReturnValues.RandFlow(-0.9f, 0.9f);
                skinMix = ReturnValues.RandFlow(0.9f, 0.99f);
                thirdMix = ReturnValues.RandFlow(-0.9f, 0.9f);

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
                int iChange = ReturnValues.RandInt(0, Function.Call<int>(Hash._GET_NUM_HEAD_OVERLAY_VALUES, iFeature));
                float fVar = ReturnValues.RandFlow(0.45f, 0.99f);

                if (iFeature == 0)
                {
                    iChange = ReturnValues.RandInt(0, iChange);
                }//Blemishes
                else if (iFeature == 1)
                {
                    if (bMale)
                        iChange = ReturnValues.RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }//Facial Hair
                else if (iFeature == 2)
                {
                    iChange = ReturnValues.RandInt(0, iChange);
                    iColour = 1;
                }//Eyebrows
                else if (iFeature == 3)
                {
                    iChange = 255;
                }//Ageing
                else if (iFeature == 4)
                {
                    if (ReturnValues.RandInt(0, 50) < 40)
                    {
                        iChange = ReturnValues.RandInt(0, iChange);
                    }
                    else
                        iChange = 255;
                }//Makeup
                else if (iFeature == 5)
                {
                    if (!bMale)
                        iChange = ReturnValues.RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 2;
                }//Blush
                else if (iFeature == 6)
                {
                    iChange = ReturnValues.RandInt(0, iChange);
                }//Complexion
                else if (iFeature == 7)
                {
                    iChange = 255;
                }//Sun Damage
                else if (iFeature == 8)
                {
                    if (!bMale)
                        iChange = ReturnValues.RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 2;
                }//Lipstick
                else if (iFeature == 9)
                {
                    iChange = ReturnValues.RandInt(0, iChange);
                }//Moles/Freckles
                else if (iFeature == 10)
                {
                    if (bMale)
                        iChange = ReturnValues.RandInt(0, iChange);
                    else
                        iChange = 255;
                    iColour = 1;
                }//Chest Hair
                else if (iFeature == 11)
                {
                    iChange = ReturnValues.RandInt(0, iChange);
                }//Body Blemishes

                int AddColour = ReturnValues.RandInt(0, 64);

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
                    MyNewWard = DataStore.MaleCloth[ReturnValues.RandInt(0, DataStore.MaleCloth.Count - 1)];
                else
                    MyNewWard = DataStore.FemaleCloth[ReturnValues.RandInt(0, DataStore.FemaleCloth.Count - 1)];

                MyNewFixings.PedClothB = MyNewWard;

                OnlineSavedWard(Pedx, MyNewWard);
            }
            else
                OnlineSavedWard(Pedx, Fixtures.PedClothB);


            int iHairStyle = 0;
            if (Fixtures == null)
            {
                if (bMale)
                    iHairStyle = ReturnValues.RandInt(25, 76);
                else
                    iHairStyle = ReturnValues.RandInt(25, 80);

                MyNewFixings.iHairCut = iHairStyle;
            }
            else
                iHairStyle = Fixtures.iHairCut;

            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx.Handle, 2, iHairStyle, 0, 2);//hair

            int iHair = ReturnValues.RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));
            int iHair2 = ReturnValues.RandInt(0, Function.Call<int>(Hash._GET_NUM_HAIR_COLORS));

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
            {
                if (ReturnValues.RandInt(1, 10) < 5)
                {
                    List<Tattoo> Tatty = ReturnValues.AddRandTats(bMale);
                    int iCount = ReturnValues.RandInt(1, Tatty.Count);

                    for (int i = 0; i < iCount; i++)
                    {
                        int iTat = ReturnValues.RandInt(0, Tatty.Count - 1);
                        Function.Call(Hash._SET_PED_DECORATION, Pedx.Handle, Function.Call<int>(Hash.GET_HASH_KEY, Tatty[iTat].BaseName), Function.Call<int>(Hash.GET_HASH_KEY, Tatty[iTat].TatName));
                        MyNewFixings.TatBase.Add(Tatty[iTat].BaseName);
                        MyNewFixings.TatName.Add(Tatty[iTat].TatName);
                        Tatty.RemoveAt(iTat);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Fixtures.TatBase.Count; i++)
                    Function.Call(Hash._SET_PED_DECORATION, Pedx.Handle, Function.Call<int>(Hash.GET_HASH_KEY, Fixtures.TatBase[i]), Function.Call<int>(Hash.GET_HASH_KEY, Fixtures.TatName[i]));
            }

            if (Fixtures == null)
                NpcBrains(Pedx, vMyCar, iSeat, MyNewFixings, -1);
            else
                NpcBrains(Pedx, vMyCar, iSeat, null, iReload);
        }
        private static void OnlineSavedWard(Ped Pedx, ClothBank MyCloths)
        {
            LoggerLight.GetLogging("PedActions.OnlineSavedWard");

            Function.Call(Hash.CLEAR_ALL_PED_PROPS, Pedx.Handle);

            for (int i = 0; i < MyCloths.ClothA.Count; i++)
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Pedx.Handle, i, MyCloths.ClothA[i], MyCloths.ClothB[i], 2);

            for (int i = 0; i < MyCloths.ExtraA.Count; i++)
                Function.Call(Hash.SET_PED_PROP_INDEX, Pedx.Handle, i, MyCloths.ExtraA[i], MyCloths.ExtraB[i], false);
        }
        public static void WarptoAnyVeh(Vehicle Vhic, Ped Peddy, int iSeat)
        {
            LoggerLight.GetLogging("PedActions.WarptoAnyVeh, iSeat == " + iSeat);

            Function.Call(Hash.SET_PED_INTO_VEHICLE, Peddy.Handle, Vhic.Handle, iSeat);
        }
        private static void GetOutVehicle(Ped Peddy, int iPed)
        {
            LoggerLight.GetLogging("PedActions.GetOutVehicle");

            if (Peddy.IsInVehicle())
            {
                Vehicle PedVeh = Peddy.CurrentVehicle;
                Function.Call(Hash.TASK_LEAVE_VEHICLE, Peddy.Handle, PedVeh.Handle, 4160);
            }
            if (iPed != -1)
            {
                iPed = ReteaveBrain(iPed);
                ClearUp.ClearPedBlips(DataStore.PedList[iPed].Level);
                DataStore.PedList[iPed].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iPed].ThisPed);
                DataStore.PedList[iPed].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iPed].ThisPed, 1, DataStore.PedList[iPed].MyName, DataStore.PedList[iPed].Colours);
            }
        }
        private static void PlayerEnterVeh(Vehicle Vhick)
        {
            LoggerLight.GetLogging("PedActions.PlayerEnterVeh");

            DataStore.iFindingTime = Game.GameTime + 1000;
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
        private static void FolllowTheLeader(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.FolllowTheLeader");

            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Peddy.Handle, DataStore.iFollowMe);
            Peddy.RelationshipGroup = DataStore.Gp_Follow;
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.BlockPermanentEvents = false;
            Peddy.AlwaysKeepTask = true;

            if (DataStore.MySettings.iAggression > 5)
                Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_PLAYER, Peddy.Handle, Game.Player.Character.Handle, true);
            else
                Function.Call(Hash.SET_PED_CAN_BE_TARGETTED_BY_PLAYER, Peddy.Handle, Game.Player.Character.Handle, false);
        }
        private static void OhDoKeepUp(Ped Peddy)
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
        }
        private static void DriveBye(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.DriveBye");

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    if (Game.Player.Character.IsInVehicle())
                        Peddy.Task.VehicleChase(Game.Player.Character);
                    else
                        Peddy.Task.DriveTo(Peddy.CurrentVehicle, ReturnValues.YoPoza(), 10.00f, 45.00f, 0);

                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
                else
                    Peddy.Task.VehicleShootAtPed(Game.Player.Character);
            }
        }
        private static void DriveAround(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.DriveAround");

            if (Peddy.IsInVehicle())
            {
                if (Peddy.SeatIndex == VehicleSeat.Driver)
                {
                    float fAggi = DataStore.MySettings.iAggression / 100;
                    Peddy.Task.CruiseWithVehicle(Peddy.CurrentVehicle, 85.00f, 2883621);
                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_DRIVER_AGGRESSIVENESS, Peddy.Handle, fAggi);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
            }
        }
        public static void DriveTooo(Ped Peddy, bool bRunOver)
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

                    Function.Call(Hash.SET_DRIVER_ABILITY, Peddy.Handle, 1.00f);
                    Function.Call(Hash.SET_PED_STEERS_AROUND_VEHICLES, Peddy.Handle, true);
                }
            }
        }
        private static void DriveToooDest(Ped Peddy, Vector3 Vme)
        {
            LoggerLight.GetLogging("PedActions.DriveToooDest, Vme == " + Vme);

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
        private static void FightPlayer(Ped Peddy, bool bInVeh)
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
                DriveBye(Peddy);
        }
        private static void AirAttack(int iPed)
        {
            LoggerLight.GetLogging("PedActions.AirAttack, iPed == " + iPed);

            int iFinder = ReturnValues.FindRandom(21, 1, 100);

            if (iFinder < 33)
                HeliFighter(iPed);
            else if (iFinder < 66)
                LaserFighter(iPed);
            else
                OppresorFighter(iPed);
        }
        private static void HeliFighter(int iPed)
        {
            LoggerLight.GetLogging("PedActions.HeliFighter, iPed == " + iPed);

            int MyPed = ReteaveBrain(iPed);
            Ped Peddy = DataStore.PedList[MyPed].ThisPed;
            DataStore.PedList[MyPed].AirAttack = 1;
            string sVeh = VehActions.RandVeh(13);
            Vehicle vHeli = VehActions.VehicleSpawn(sVeh, new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 250.00f), 0.00f, false, iPed, true);
            FlyAway(DataStore.PedList[MyPed].ThisPed, ReturnValues.YoPoza(), 250.00f, 0.00f);
            Function.Call(Hash.SET_PED_FIRING_PATTERN, Peddy.Handle, Function.Call<int>(Hash.GET_HASH_KEY, "FIRING_PATTERN_BURST_FIRE_HELI"));
            Peddy.RelationshipGroup = DataStore.GP_Mental;
            Peddy.BlockPermanentEvents = false;
        }
        private static void LaserFighter(int iPed)
        {
            LoggerLight.GetLogging("PedActions.LaserFighter, iPed == " + iPed);

            int MyPed = ReteaveBrain(iPed);
            Ped Peddy = DataStore.PedList[MyPed].ThisPed;
            DataStore.PedList[MyPed].AirAttack = 2;
            DataStore.PedList[MyPed].AirDirect = (float)ReturnValues.RandInt(0, 360);
            string sVeh = VehActions.RandVeh(12);
            Vehicle vPlane = VehActions.VehicleSpawn(sVeh, new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 1550.00f), 0.00f, false, iPed, true);
            Function.Call(Hash.TASK_PLANE_MISSION, DataStore.PedList[MyPed].ThisPed.Handle, DataStore.PedList[MyPed].ThisVeh.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, DataStore.PedList[MyPed].AirDirect, 1000.0f, -5000.0f);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.RelationshipGroup = DataStore.GP_Mental;
            Peddy.BlockPermanentEvents = false;
        }
        private static void OppresorFighter(int iPed)
        {
            LoggerLight.GetLogging("PedActions.OppresorFighter, iPed == " + iPed);


            int MyPed = ReteaveBrain(iPed);
            Ped Peddy = DataStore.PedList[MyPed].ThisPed;
            DataStore.PedList[MyPed].AirAttack = 3;
            DataStore.PedList[MyPed].AirDirect = (float)ReturnValues.RandInt(0, 360);
            Vehicle Planes = VehActions.VehicleSpawn("hydra", new Vector3(Peddy.Position.X, Peddy.Position.Y, Peddy.Position.Z + 1550.00f), 0.00f, false, iPed, true);
            ClearUp.ClearPedBlips(iPed);
            DataStore.PedList[MyPed].ThisBlip = BlipActions.PedBlimp(Peddy, 639, DataStore.PedList[MyPed].MyName, DataStore.PedList[MyPed].Colours);
            Function.Call(Hash.TASK_PLANE_MISSION, Peddy.Handle, Planes.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, DataStore.PedList[MyPed].AirDirect, 300.0f, -5000.0f);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Peddy.Handle, 0, true);
            Peddy.RelationshipGroup = DataStore.GP_Mental;
            Peddy.BlockPermanentEvents = false;

            Vehicle Bike = World.CreateVehicle(VehicleHash.Oppressor2, Planes.Position);
            Bike.IsPersistent = true;
            DataStore.PedList[MyPed].ThisOppress = Bike;
            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, Bike.Handle, Planes.Handle, Function.Call<int>(Hash.GET_PED_BONE_INDEX, Planes.Handle, 0), 0.00f, 3.32999945f, -0.10f, 0.00f, 0.00f, 0.00f, false, false, false, false, 2, true);

            Planes.IsVisible = false;
            Bike.IsVisible = true;
            Peddy.IsVisible = true;
            //VehAnim(Peddy, "veh@bike@sport@front@base", "sit");
        }
        private static void FlyAway(Ped Pedd, Vector3 vHeliDest, float fSpeed, float flanding)
        {
            LoggerLight.GetLogging("PedActions.FlyAway");

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
        private static void HackerTime(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.HackerTime");

            if (ReturnValues.YoPoza().DistanceTo(new Vector3(-778.81F, 312.66F, 84.70F)) < 80.00f)
            {
                Prop Plop = World.CreateProp("prop_windmill_01", new Vector3(-832.50f, 290.95f, 82.00f), new Vector3(-90.00f, 94.72f, 0.00f), true, false);
                DataStore.Plops.Add(new Prop(Plop.Handle));
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
        private static void RoBoCar(Vehicle Atchoo)
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
        private static void ForceAnim(Ped peddy, string sAnimDict, string sAnimName, Vector3 AnPos, Vector3 AnRot)
        {
            LoggerLight.GetLogging("PedActions.ForceAnim, sAnimName == " + sAnimName);

            peddy.Task.ClearAll();
            Function.Call(Hash.REQUEST_ANIM_DICT, sAnimDict);
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, sAnimDict))
                Script.Wait(1);
            Function.Call(Hash.TASK_PLAY_ANIM_ADVANCED, peddy.Handle, sAnimDict, sAnimName, AnPos.X, AnPos.Y, AnPos.Z, AnRot.X, AnRot.Y, AnRot.Z, 8.0f, 0.00f, -1, 1, 0.01f, 0, 0);
            Function.Call(Hash.REMOVE_ANIM_DICT, sAnimDict);
        }
        private static void NpcBrains(Ped Peddy, Vehicle VeHic, int iSeat, PedFixtures Fixtures, int iReload)
        {
            LoggerLight.GetLogging("PedActions.NpcBrains, iSeat == " + iSeat + ", iReload == " + iReload);

            if (iReload != -1)
            {
                int iPosNum = ReteaveBrain(iReload);
                if (iPosNum == -1)
                {
                    Peddy.Delete();
                }
                else
                {
                    DataStore.PedList[iPosNum].ThisPed = Peddy;
                    DataStore.PedList[iPosNum].DirBlip = BlipActions.DirectionalBlimp(Peddy);
                    DataStore.PedList[iPosNum].ThisBlip = BlipActions.PedBlimp(Peddy, 1, DataStore.PedList[iPosNum].MyName, DataStore.PedList[iPosNum].Colours);
                    DataStore.PedList[iPosNum].DeathSequence = 0;
                    DataStore.PedList[iPosNum].EnterVehQue = false;
                    DataStore.PedList[iPosNum].Befallen = false;

                    Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, Peddy.Handle, 2);
                    Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, Peddy.Handle, true);
                    Function.Call(Hash.SET_PED_PATH_PREFER_TO_AVOID_WATER, Peddy.Handle, false);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 0, true);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 1, true);
                    if (DataStore.MySettings.iAggression > 2)
                        Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 2, true);
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 3, true);
                    Peddy.CanBeTargetted = true;

                    if (!DataStore.PedList[iPosNum].Friendly)
                    {
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                        Peddy.RelationshipGroup = DataStore.GP_Attack;
                        if (DataStore.PedList[iPosNum].OffRadar == 0 && ReturnValues.RandInt(0, 40) < 10)
                            DataStore.PedList[iPosNum].OffRadar = -1;
                        FightPlayer(Peddy, false);
                    }
                    else
                    {
                        if (DataStore.PedList[iPosNum].Follower)
                        {
                            FolllowTheLeader(Peddy);
                            OhDoKeepUp(Peddy);
                        }
                        else
                        {
                            Peddy.Task.WanderAround();
                            Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                            Peddy.RelationshipGroup = DataStore.Gp_Friend;
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
                    TimeOn = Game.GameTime + ReturnValues.RandInt(DataStore.MySettings.iMinSession, DataStore.MySettings.iMaxSession),
                    MyName = ReturnValues.SillyNameList(),
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
                if (DataStore.MySettings.iAggression > 3)
                    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 2, true);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Peddy.Handle, 3, true);
                Peddy.CanBeTargetted = true;

                int iBrain = 1;

                if (DataStore.MySettings.iAggression < 4 && VeHic == null)
                {
                    if (ReturnValues.FindRandom(16, 0, 40) < 10)
                        iBrain = 2;
                }
                else if (DataStore.MySettings.iAggression < 6)
                {
                    if (ReturnValues.FindRandom(17, 0, 60) < 5)
                        iBrain = 3;
                    else if (VeHic == null)
                    {
                        if (ReturnValues.FindRandom(18, 0, 40) < 10)
                            iBrain = 2;
                    }
                }
                else if (DataStore.MySettings.iAggression < 8)
                {
                    if (ReturnValues.FindRandom(19, 0, 60) < 40)
                        iBrain = 3;
                    else if (VeHic == null)
                    {
                        if (ReturnValues.FindRandom(20, 0, 40) < 10)
                            iBrain = 2;
                    }
                }
                else if (DataStore.MySettings.iAggression < 11)
                {
                    iBrain = 3;
                }
                else
                {
                    if (!DataStore.bHackerIn)
                        iBrain = 4;
                    else
                        iBrain = 3;

                }

                if (iBrain == 1)
                {
                    Peddy.Task.WanderAround();
                    MyBrains.DirBlip = BlipActions.DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = BlipActions.PedBlimp(Peddy, 1, MyBrains.MyName, 0);
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                    Peddy.RelationshipGroup = DataStore.Gp_Friend;
                }            //Friend
                else if (iBrain == 2)
                {
                    Peddy.Task.WanderAround();
                    MyBrains.DirBlip = BlipActions.DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = BlipActions.PedBlimp(Peddy, 1, MyBrains.MyName, 0);
                    MyBrains.SessionJumper = true;
                }       //Disconect
                else if (iBrain == 3)
                {
                    FightPlayer(Peddy, false);
                    MyBrains.DirBlip = BlipActions.DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = BlipActions.PedBlimp(Peddy, 1, MyBrains.MyName, 1);
                    MyBrains.Colours = 1;
                    MyBrains.Friendly = false;
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                    Peddy.RelationshipGroup = DataStore.GP_Mental;
                }       //Enemy
                else
                {
                    DataStore.bHackerIn = true;
                    MyBrains.DirBlip = BlipActions.DirectionalBlimp(Peddy);
                    MyBrains.ThisBlip = BlipActions.PedBlimp(Peddy, 163, MyBrains.MyName, 1);
                    MyBrains.TimeOn = Game.GameTime + 60000;
                    MyBrains.Colours = 1;
                    DataStore.bHackEvent = false;
                    MyBrains.Friendly = false;
                    MyBrains.Hacker = true;
                    Peddy.IsInvincible = true;
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, Peddy.Handle);
                    Peddy.RelationshipGroup = DataStore.GP_Mental;
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

                        MyBrains.ThisBlip = BlipActions.PedBlimp(Peddy, VehActions.OhMyBlip(VeHic), MyBrains.MyName, MyBrains.Colours);

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
                else if (ReturnValues.RandInt(0, 40) < 10 && DataStore.MySettings.iAggression > 5)
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
                    MyBrains.ThisBlip = BlipActions.PedBlimp(Peddy, 303, MyBrains.MyName, 1);
                    MyBrains.Bounty = true;
                }

                DataStore.PedList.Add(MyBrains);

                ClearUp.BackItUpBrain();

                if (DataStore.MySettings.iAggression > 1)
                    GunningIt(Peddy);
            }
        }
        private static PlayerBrain ThisBrian(int iCurrent)
        {
            PlayerBrain Brains = null;

            if (iCurrent < DataStore.PedList.Count)
                Brains = DataStore.PedList[iCurrent];

            return Brains;
        }
        private static AfkPlayer ThisAFKer(int iCurrent)
        {
            AfkPlayer Afker = null;

            if (iCurrent < DataStore.AFKList.Count)
                Afker = DataStore.AFKList[iCurrent];

            return Afker;
        }
        public static void LaggOut()
        {
            LoggerLight.GetLogging("PedActions.LaggOut");

            PlayerDump();
            bool bSearching = true;
            while (bSearching)
            {
                Script.Wait(100);
                PlayerDump();
                if (DataStore.PedList.Count == 0)
                    bSearching = false;
            }
            DataStore.iFollow = 0;
        }
        private static void PlayerDump()
        {
            LoggerLight.GetLogging("PedActions.PlayerDump");

            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].ThisPed != null)
                {
                    if (DataStore.PedList[i].ThisPed.Exists())
                    {
                        GetOutVehicle(DataStore.PedList[i].ThisPed, DataStore.PedList[i].Level);
                        ClearUp.PedCleaning(DataStore.PedList[i].Level, "left", false);
                    }
                    else
                        DataStore.PedList.RemoveAt(i);
                }
                else
                    DataStore.PedList.RemoveAt(i);
            }

            for (int i = 0; i < DataStore.AFKList.Count; i++)
            {
                ClearUp.DeListingBrains(false, i, true);
                DataStore.iCurrentPlayerz -= 1;
            }

            DataStore.MakeFrenz.Clear();
            DataStore.MakeCarz.Clear();
            DataStore.GetInQUe.Clear();
        }
        private static void GunningIt(Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.GunningIt");

            List<string> sWeapList = new List<string>();

            int iGun = 0;

            if (DataStore.MySettings.bSpaceWeaps)
                iGun = ReturnValues.RandInt(0, 8);
            else
                iGun = ReturnValues.RandInt(0, 7);

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
        private static int UniqueLevels()
        {
            LoggerLight.GetLogging("PedActions.UniqueLevels");

            int iNumber = ReturnValues.FindRandom(14, 1, 1000);

            while (BrainNumberCheck(iNumber))
            {
                iNumber = ReturnValues.FindRandom(15, 1, 400);
            }
            return iNumber;
        }
        private static bool BrainNumberCheck(int iNumber)
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
            return bRunAgain;
        }
        private static int WhoShotMe(Ped MeDie)
        {
            LoggerLight.GetLogging("PedActions.WhoShotMe");

            int iShoot = -1;

            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (MeDie.GetKiller() == DataStore.PedList[i].ThisPed)
                {
                    iShoot = i;
                    break;
                }
            }
            return iShoot;
        }
        private static void SearchSeats(Ped pPeddy, Vehicle vVhic, int iPedLevel)
        {
            LoggerLight.GetLogging("PedActions.SearchSeats, iPedLevel == " + iPedLevel);
            GetInAveh MyFinda = new GetInAveh
            {
                Peddy = pPeddy,
                Vhic = vVhic,
                PedLevel = iPedLevel,
                Seats = -1
            };
            DataStore.GetInQUe.Add(MyFinda); ;
        }
        public static void PedDoGetIn(GetInAveh GetingOn)
        {
            LoggerLight.GetLogging("PedActions.PedDoGetIn");

            DataStore.iFindingTime = Game.GameTime + 1000;
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
                    DataStore.GetInQUe[0].Seats = iSeats;
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
                    if (DataStore.PedList[iBPed].ThisVeh != null)
                        DataStore.PedList[iBPed].ThisVeh.MarkAsNoLongerNeeded();
                    DataStore.GetInQUe.RemoveAt(0);
                    FindStuff.SearchVeh(1.00f, 95.00f, VehActions.RandVeh(ReturnValues.RandInt(1, 9)), false, GetingOn.PedLevel);
                }
            }
        }
        public static void PlayerZerosAI()
        {
            if (DataStore.bDisabled)
            {
                if (ThisBrian(0) != null || ThisAFKer(0) != null)
                    LaggOut();
            }
            else
            {
                if (ThisBrian(DataStore.iNpcList) != null)
                {
                    int iBe = DataStore.iNpcList;
                    if (DataStore.PedList[iBe].DirBlip != null)
                        BlipActions.BlipDirect(DataStore.PedList[iBe].DirBlip, DataStore.PedList[iBe].ThisPed.Heading);

                    if (DataStore.PedList[iBe].ThisPed == null)
                    {

                    }
                    else if (DataStore.PedList[iBe].EnterVehQue)
                    {
                        if (!Game.Player.Character.IsInVehicle())
                        {
                            DataStore.PedList[iBe].EnterVehQue = false;
                            DataStore.GetInQUe.Clear();
                        }
                    }
                    else if (DataStore.PedList[iBe].TimeOn < Game.GameTime)
                    {
                        DataStore.PedList[iBe].AirAttack = 0;
                        Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iBe].ThisPed.Handle);
                        GetOutVehicle(DataStore.PedList[iBe].ThisPed, DataStore.PedList[iBe].Level);
                        ClearUp.PedCleaning(DataStore.PedList[iBe].Level, "left", false);
                    }
                    else if (Game.Player.Character.GetKiller() == DataStore.PedList[iBe].ThisPed)
                    {
                        DataStore.PedList[iBe].Kills += 1;
                        WhileYouDead(DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Killed, DataStore.PedList[iBe].Kills, DataStore.PedList[iBe].ThisPed);
                        if (DataStore.MySettings.iAggression < 6)
                            ClearUp.PedCleaning(DataStore.PedList[iBe].Level, "left", false);
                    }
                    else if (DataStore.PedList[iBe].ThisPed.IsDead)
                    {
                        if (DataStore.PedList[iBe].DeathSequence == 0)
                        {
                            DataStore.PedList[iBe].AirAttack = 0;

                            if (DataStore.PedList[iBe].ThisOppress != null)
                            {
                                EmptyVeh(DataStore.PedList[iBe].ThisOppress);
                                DataStore.PedList[iBe].ThisOppress.Explode();
                                DataStore.PedList[iBe].ThisOppress.MarkAsNoLongerNeeded();
                                DataStore.PedList[iBe].ThisOppress = null;

                                if (DataStore.PedList[iBe].ThisVeh != null)
                                {
                                    EmptyVeh(DataStore.PedList[iBe].ThisVeh);
                                    DataStore.PedList[iBe].ThisVeh.Delete();
                                    DataStore.PedList[iBe].ThisVeh = null;
                                }
                            }
                            else if (DataStore.PedList[iBe].ThisVeh != null)
                            {
                                EmptyVeh(DataStore.PedList[iBe].ThisVeh);
                                DataStore.PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                DataStore.PedList[iBe].ThisVeh = null;
                            }

                            int iDie = WhoShotMe(DataStore.PedList[iBe].ThisPed);

                            ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);

                            if (DataStore.PedList[iBe].ThisPed.GetKiller() == Game.Player.Character)
                            {
                                if (DataStore.PedList[iBe].Bounty)
                                    Game.Player.Money += 7000;
                                DataStore.PedList[iBe].Friendly = false;
                                DataStore.PedList[iBe].Colours = 1;
                                DataStore.PedList[iBe].ApprochPlayer = false;
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iBe].ThisPed.Handle);
                                DataStore.PedList[iBe].Follower = false;
                                DataStore.PedList[iBe].Killed += 1;
                                UI.Notify("You  " + DataStore.PedList[iBe].Killed + " - " + DataStore.PedList[iBe].Kills + " " + DataStore.PedList[iBe].MyName);
                            }
                            else if (iDie != -1)
                                UI.Notify(DataStore.PedList[iDie].MyName + " Killed " + DataStore.PedList[iBe].MyName);
                            else
                                UI.Notify(DataStore.PedList[iBe].MyName + " died");

                            DataStore.PedList[iBe].Bounty = false;
                            DataStore.PedList[iBe].DeathSequence += 1;
                            DataStore.PedList[iBe].DeathTime = Game.GameTime + 10000;
                            DataStore.PedList[iBe].TimeOn += 60000;
                            Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iBe].ThisPed.Handle);
                        }
                        else if (DataStore.PedList[iBe].DeathSequence == 1 || DataStore.PedList[iBe].DeathSequence == 3 || DataStore.PedList[iBe].DeathSequence == 5 || DataStore.PedList[iBe].DeathSequence == 7)
                        {
                            if (DataStore.PedList[iBe].DeathTime < Game.GameTime)
                            {
                                DataStore.PedList[iBe].ThisPed.Alpha = 80;
                                DataStore.PedList[iBe].DeathSequence += 1;
                                DataStore.PedList[iBe].DeathTime = Game.GameTime + 500;
                            }
                        }
                        else if (DataStore.PedList[iBe].DeathSequence == 2 || DataStore.PedList[iBe].DeathSequence == 4 || DataStore.PedList[iBe].DeathSequence == 6)
                        {
                            if (DataStore.PedList[iBe].DeathTime < Game.GameTime)
                            {
                                DataStore.PedList[iBe].ThisPed.Alpha = 255;
                                DataStore.PedList[iBe].DeathSequence += 1;
                                DataStore.PedList[iBe].DeathTime = Game.GameTime + 500;
                            }
                        }
                        else if (DataStore.PedList[iBe].DeathSequence == 8)
                        {
                            if (DataStore.PedList[iBe].DeathTime < Game.GameTime)
                            {
                                if (DataStore.PedList[iBe].Killed > ReturnValues.RandInt(13, 22) || DataStore.MySettings.iAggression < 2)
                                {
                                    ClearUp.PedCleaning(DataStore.PedList[iBe].Level, "left", false);
                                }
                                else if (DataStore.PedList[iBe].Killed > 15 && DataStore.PedList[iBe].Kills == 0 && DataStore.MySettings.iAggression > 7)
                                    FireOrb(DataStore.PedList[iBe].Level, Game.Player.Character);
                                else
                                {
                                    ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                    DataStore.PedList[iBe].DeathSequence = 10;
                                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iBe].ThisPed.Handle);
                                    DataStore.PedList[iBe].ThisPed.Delete();
                                    DataStore.PedList[iBe].ThisPed = null;
                                    FindStuff.SearchPed(35.00f, 220.00f, null, 0, DataStore.PedList[iBe].Level);
                                }
                            }
                        }
                    }
                    else if (DataStore.PedList[iBe].ThisPed.Position.Z + 10.00f < World.GetGroundHeight(DataStore.PedList[iBe].ThisPed.Position))
                    {
                        if (DataStore.PedList[iBe].Befallen)
                        {
                            if (DataStore.PedList[iBe].DeathTime < Game.GameTime)
                                DataStore.PedList[iBe].ThisPed.Kill();
                        }
                        else
                        {
                            DataStore.PedList[iBe].Befallen = true;
                            DataStore.PedList[iBe].DeathTime = Game.GameTime + 5000;
                        }
                    }
                    else if (DataStore.PedList[iBe].Befallen)
                        DataStore.PedList[iBe].Befallen = false;
                    else if (DataStore.PedList[iBe].Hacker && !DataStore.bHackEvent)
                    {
                        if (DataStore.PedList[iBe].ThisPed.Position.DistanceTo(ReturnValues.YoPoza()) < 40.00f)
                        {
                            DataStore.bHackEvent = true;
                            HackerTime(DataStore.PedList[iBe].ThisPed);
                        }
                    }
                    else if (DataStore.PedList[iBe].SessionJumper)
                    {
                        if (DataStore.PedList[iBe].ThisPed.Position.DistanceTo(ReturnValues.YoPoza()) < 10.00f)
                            ClearUp.PedCleaning(DataStore.PedList[iBe].Level, "has disappeared", true);
                    }
                    else if (DataStore.PedList[iBe].AirAttack != 0)
                    {
                        if (!DataStore.PedList[iBe].ThisPed.IsInVehicle())
                            DataStore.PedList[iBe].AirAttack = 0;
                        else if (DataStore.PedList[iBe].FindPlayer < Game.GameTime)
                        {
                            DataStore.PedList[iBe].FindPlayer = Game.GameTime + 5000;
                            if (Game.Player.Character.IsInVehicle())
                            {
                                if (DataStore.PedList[iBe].AirAttack == 1)
                                    FlyAway(DataStore.PedList[iBe].ThisPed, ReturnValues.YoPoza(), 250.00f, 0.00f);
                                else if (DataStore.PedList[iBe].AirAttack == 2)
                                {
                                    int iFlight = 6;
                                    if (DataStore.MySettings.iAggression > 8)
                                        iFlight = 2;

                                    Function.Call(Hash.TASK_PLANE_MISSION, DataStore.PedList[iBe].ThisPed.Handle, DataStore.PedList[iBe].ThisVeh.Handle, Game.Player.Character.CurrentVehicle.Handle, 0, 0, 0, 0, iFlight, 0.0f, 0.0f, DataStore.PedList[iBe].AirDirect, 1000.0f, -5000.0f);
                                }
                                else if (DataStore.PedList[iBe].AirAttack == 3)
                                    Function.Call(Hash.TASK_PLANE_MISSION, DataStore.PedList[iBe].ThisPed.Handle, DataStore.PedList[iBe].ThisVeh.Handle, Game.Player.Character.CurrentVehicle.Handle, 0, 0, 0, 0, 6, 0.0f, 0.0f, DataStore.PedList[iBe].AirDirect, 300.0f, -5000.0f);
                            }
                            else
                            {
                                if (DataStore.PedList[iBe].AirAttack == 1)
                                    FlyAway(DataStore.PedList[iBe].ThisPed, ReturnValues.YoPoza(), 250.00f, 0.00f);
                                else if (DataStore.PedList[iBe].AirAttack == 2)
                                {
                                    int iFlight = 6;
                                    if (DataStore.MySettings.iAggression > 8)
                                        iFlight = 2;

                                    Function.Call(Hash.TASK_PLANE_MISSION, DataStore.PedList[iBe].ThisPed.Handle, DataStore.PedList[iBe].ThisVeh.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, iFlight, 0.0f, 0.0f, DataStore.PedList[iBe].AirDirect, 1000.0f, -5000.0f);
                                }
                                else if (DataStore.PedList[iBe].AirAttack == 3)
                                    Function.Call(Hash.TASK_PLANE_MISSION, DataStore.PedList[iBe].ThisPed.Handle, DataStore.PedList[iBe].ThisVeh.Handle, 0, Game.Player.Character.Handle, 0, 0, 0, 6, 0.0f, 0.0f, DataStore.PedList[iBe].AirDirect, 300.0f, -5000.0f);
                            }
                        }
                    }
                    else if (DataStore.PedList[iBe].Driver)
                    {
                        if (DataStore.PedList[iBe].ThisVeh != null)
                        {
                            if (DataStore.PedList[iBe].ThisPed.IsInVehicle())
                            {

                                if (DataStore.PedList[iBe].DirBlip != null)
                                {
                                    ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                    DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, VehActions.OhMyBlip(DataStore.PedList[iBe].ThisVeh), DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                }
                                else if (DataStore.PedList[iBe].Follower)
                                {
                                    if (Game.Player.Character.IsInVehicle(DataStore.PedList[iBe].ThisPed.CurrentVehicle))
                                    {
                                        if (Game.IsWaypointActive)
                                        {
                                            if (World.GetWaypointPosition() != DataStore.LetsGoHere)
                                            {
                                                DataStore.LetsGoHere = World.GetWaypointPosition();
                                                DriveToooDest(DataStore.PedList[iBe].ThisPed, DataStore.LetsGoHere);
                                            }
                                        }
                                    }
                                    else if (Game.Player.Character.IsInVehicle())
                                    {
                                        if (DataStore.PedList[iBe].ThisBlip == null)
                                            DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 225, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);

                                        if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iBe].ThisPed.Position) > 25.00f)
                                        {
                                            if (DataStore.PedList[iBe].FindPlayer < Game.GameTime)
                                            {
                                                DataStore.PedList[iBe].FindPlayer = Game.GameTime + 5000;
                                                DriveTooo(DataStore.PedList[iBe].ThisPed, false);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        GetOutVehicle(DataStore.PedList[iBe].ThisPed, DataStore.PedList[iBe].Level);
                                        if (DataStore.PedList[iBe].ThisVeh != null)
                                        {
                                            DataStore.PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                            DataStore.PedList[iBe].ThisVeh = null;
                                        }
                                        DataStore.PedList[iBe].Passenger = false;
                                        DataStore.PedList[iBe].Driver = false;
                                        OhDoKeepUp(DataStore.PedList[iBe].ThisPed);
                                    }
                                }
                                else if (DataStore.PedList[iBe].ApprochPlayer)
                                {
                                    if (DataStore.MySettings.iAggression < 9 && DataStore.PedList[iBe].Friendly && DataStore.iFollow < 7)
                                    {
                                        if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iBe].ThisPed.Position) < 5.00f)
                                        {
                                            if (!DataStore.PedList[iBe].Horny)
                                            {
                                                DataStore.PedList[iBe].Horny = true;
                                                DataStore.PedList[iBe].ThisVeh.SoundHorn(3000);
                                                ScaleDisp.TopLeftUI("Press" + DataStore.ControlSym[23] + "to enter vehicle");
                                            }
                                            else if (!Game.Player.Character.IsInVehicle())
                                            {
                                                if (ReturnValues.ButtonDown(23, true))
                                                {
                                                    DataStore.PedList[iBe].TimeOn = Game.GameTime + 600000;
                                                    DataStore.PedList[iBe].ApprochPlayer = false;
                                                    DataStore.PedList[iBe].Colours = 38;
                                                    DataStore.PedList[iBe].Follower = true;
                                                    FolllowTheLeader(DataStore.PedList[iBe].ThisPed);
                                                    DataStore.iFollow += 1;
                                                    PlayerEnterVeh(DataStore.PedList[iBe].ThisVeh);
                                                    DriveAround(DataStore.PedList[iBe].ThisPed);
                                                }
                                            }
                                            else
                                            {
                                                DataStore.PedList[iBe].ApprochPlayer = false;
                                                DriveAround(DataStore.PedList[iBe].ThisPed);
                                            }
                                        }
                                        else if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iBe].ThisPed.Position) > 25.00f)
                                        {
                                            if (DataStore.PedList[iBe].FindPlayer < Game.GameTime)
                                            {
                                                DataStore.PedList[iBe].FindPlayer = Game.GameTime + 5000;
                                                DriveTooo(DataStore.PedList[iBe].ThisPed, false);
                                            }
                                        }
                                    }
                                    else if (DataStore.MySettings.iAggression > 8)
                                    {
                                        if (DataStore.PedList[iBe].FindPlayer < Game.GameTime)
                                        {
                                            DataStore.PedList[iBe].FindPlayer = Game.GameTime + 5000;
                                            DriveBye(DataStore.PedList[iBe].ThisPed);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (DataStore.PedList[iBe].Follower)
                                {
                                    if (DataStore.PedList[iBe].ThisPed.IsInCombat)
                                    {
                                        if (DataStore.PedList[iBe].DirBlip == null)
                                        {
                                            ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                            DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                            DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                        }
                                    }
                                    else
                                    {
                                        if (Game.Player.Character.IsInVehicle())
                                        {
                                            if (DataStore.PedList[iBe].ThisVeh != null)
                                            {
                                                DataStore.PedList[iBe].EnterVehQue = true;
                                                SearchSeats(DataStore.PedList[iBe].ThisPed, DataStore.PedList[iBe].ThisVeh, DataStore.PedList[iBe].Level);
                                            }
                                            else
                                                DataStore.PedList[iBe].Driver = false;
                                        }
                                        else
                                        {
                                            if (DataStore.PedList[iBe].ThisVeh != null)
                                            {
                                                DataStore.PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                                DataStore.PedList[iBe].ThisVeh = null;
                                            }
                                            DataStore.PedList[iBe].Driver = false;

                                            if (DataStore.PedList[iBe].DirBlip == null)
                                            {
                                                ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                                DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                                DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    EmptyVeh(DataStore.PedList[iBe].ThisVeh);
                                    DataStore.PedList[iBe].ThisVeh.MarkAsNoLongerNeeded();
                                    DataStore.PedList[iBe].ThisVeh = null;

                                    if (DataStore.PedList[iBe].DirBlip == null)
                                    {
                                        ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                        DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                        DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                    }
                                }
                            }
                        }
                        else
                            DataStore.PedList[iBe].Driver = false;
                    }
                    else if (DataStore.PedList[iBe].Passenger)
                    {
                        if (DataStore.PedList[iBe].Follower)
                        {
                            if (!Game.Player.Character.IsInVehicle())
                            {
                                DataStore.PedList[iBe].Passenger = false;
                                ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                DataStore.PedList[iBe].ThisPed.Task.LeaveVehicle();
                                OhDoKeepUp(DataStore.PedList[iBe].ThisPed);
                            }
                        }
                        else
                        {
                            if (!DataStore.PedList[iBe].ThisPed.IsInVehicle())
                            {
                                DataStore.PedList[iBe].Passenger = false;
                                ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                            }
                            else if (Game.Player.Character.IsInVehicle(DataStore.PedList[iBe].ThisPed.CurrentVehicle) && DataStore.PedList[iBe].Friendly && DataStore.iFollow < 7)
                            {
                                FolllowTheLeader(DataStore.PedList[iBe].ThisPed);
                                DataStore.PedList[iBe].Colours = 38;
                                DataStore.PedList[iBe].Follower = true;
                                DataStore.iFollow += 1;
                            }
                        }
                    }
                    else if (DataStore.PedList[iBe].Follower)
                    {
                        if (Game.Player.Character.IsInVehicle())
                        {
                            DataStore.PedList[iBe].Passenger = true;
                            Vehicle DisVeh = Game.Player.Character.CurrentVehicle;
                            SearchSeats(DataStore.PedList[iBe].ThisPed, DisVeh, DataStore.PedList[iBe].Level);
                            DataStore.PedList[iBe].EnterVehQue = true;
                            DataStore.PedList[iBe].TimeOn += 60000;
                        }
                        else if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iBe].ThisPed.Position) > 150.00f)
                        {
                            DataStore.PedList[iBe].ThisPed.Position = ReturnValues.YoPoza() + (Game.Player.Character.RightVector * 2);
                            OhDoKeepUp(DataStore.PedList[iBe].ThisPed);
                        }
                    }
                    else if (DataStore.PedList[iBe].Friendly)
                    {
                        if (DataStore.PedList[iBe].ThisPed.HasBeenDamagedBy(Game.Player.Character) || DataStore.PedList[iBe].ThisPed.IsInCombatAgainst(Game.Player.Character) && DataStore.MySettings.iAggression > 2)
                        {
                            ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                            DataStore.PedList[iBe].Colours = 1;
                            if (DataStore.MySettings.iAggression < 5)
                                DataStore.PedList[iBe].TimeOn = Game.GameTime + 120000;
                            else
                                DataStore.PedList[iBe].TimeOn += 120000;

                            FightPlayer(DataStore.PedList[iBe].ThisPed, false);
                            DataStore.PedList[iBe].Friendly = false;
                            if (DataStore.PedList[iBe].Follower)
                            {
                                Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iBe].ThisPed.Handle);
                                DataStore.PedList[iBe].Follower = false;
                                DataStore.iFollow -= 1;
                            }
                            DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                            DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                        }
                        else
                        {
                            if (ReturnValues.YoPoza().DistanceTo(DataStore.PedList[iBe].ThisPed.Position) < 7.00f && !DataStore.PedList[iBe].ThisPed.IsInVehicle() && DataStore.iFollow < 7)
                            {
                                if (Game.Player.Character.IsInVehicle())
                                {
                                    if (Game.Player.Character.SeatIndex == VehicleSeat.Driver)
                                    {
                                        if (!DataStore.PedList[iBe].Horny2)
                                        {
                                            ScaleDisp.TopLeftUI("Press" + DataStore.ControlSym[86] + "to attract the players attention");
                                            DataStore.PedList[iBe].Horny2 = true;
                                        }
                                        else if (ReturnValues.ButtonDown(86, false))
                                        {
                                            if (DataStore.MySettings.iAggression < 9)
                                            {
                                                ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);

                                                DataStore.PedList[iBe].Colours = 38;
                                                DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                                DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                                FolllowTheLeader(DataStore.PedList[iBe].ThisPed);
                                                OhDoKeepUp(DataStore.PedList[iBe].ThisPed);
                                                DataStore.PedList[iBe].TimeOn = Game.GameTime + 600000;
                                                DataStore.iFollow += 1;

                                                DataStore.PedList[iBe].Follower = true;
                                            }
                                            else
                                            {
                                                ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                                FightPlayer(DataStore.PedList[iBe].ThisPed, false);
                                                DataStore.PedList[iBe].Colours = 3;
                                                DataStore.PedList[iBe].Friendly = false;
                                                DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                                DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!DataStore.PedList[iBe].Horny2)
                                    {
                                        ScaleDisp.TopLeftUI("Press" + DataStore.ControlSym[46] + "to invte this player");
                                        DataStore.PedList[iBe].Horny2 = true;
                                    }
                                    else if (ReturnValues.ButtonDown(46, false))
                                    {
                                        if (DataStore.MySettings.iAggression < 9)
                                        {
                                            ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);

                                            DataStore.PedList[iBe].Colours = 38;
                                            DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                            DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                            FolllowTheLeader(DataStore.PedList[iBe].ThisPed);
                                            OhDoKeepUp(DataStore.PedList[iBe].ThisPed);
                                            DataStore.PedList[iBe].TimeOn = Game.GameTime + 600000;
                                            DataStore.iFollow += 1;

                                            DataStore.PedList[iBe].Follower = true;
                                        }
                                        else
                                        {
                                            ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                            FightPlayer(DataStore.PedList[iBe].ThisPed, false);
                                            DataStore.PedList[iBe].Colours = 3;
                                            DataStore.PedList[iBe].Friendly = false;
                                            DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                            DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!DataStore.PedList[iBe].OffRadarBool && DataStore.PedList[iBe].OffRadar == -1)
                        {

                            DataStore.PedList[iBe].OffRadar = Game.GameTime + 300000;
                            ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                            UI.Notify("~h~" + DataStore.PedList[iBe].MyName + "~s~ has gone off radar");
                        }
                        else if (DataStore.PedList[iBe].OffRadarBool)
                        {
                            if (DataStore.PedList[iBe].OffRadar < Game.GameTime)
                            {
                                DataStore.PedList[iBe].OffRadarBool = false;
                                ClearUp.ClearPedBlips(DataStore.PedList[iBe].Level);
                                DataStore.PedList[iBe].DirBlip = BlipActions.DirectionalBlimp(DataStore.PedList[iBe].ThisPed);
                                DataStore.PedList[iBe].ThisBlip = BlipActions.PedBlimp(DataStore.PedList[iBe].ThisPed, 1, DataStore.PedList[iBe].MyName, DataStore.PedList[iBe].Colours);
                            }
                        }
                        else if (DataStore.PedList[iBe].ThisPed.Position.DistanceTo(ReturnValues.YoPoza()) > 350.00f && DataStore.MySettings.iAggression > 6 && DataStore.PedList[iBe].DeathSequence == 0)
                        {
                            if (DataStore.PedList[iBe].ThisVeh == null)
                                AirAttack(DataStore.PedList[iBe].Level);
                        }
                        else if (DataStore.bPiggyBack)
                        {
                            if (DataStore.PedList[iBe].ThisPed.IsInCombatAgainst(Game.Player.Character))
                            {
                                if (DataStore.iOrbBurnOut < Game.GameTime)
                                {
                                    DataStore.iOrbBurnOut = Game.GameTime + 25000;

                                    FireOrb(-1, DataStore.PedList[iBe].ThisPed);
                                }
                            }
                        }
                    }

                    DataStore.iNpcList += 1;
                }
                else
                    DataStore.iNpcList = 0;

                if (ThisAFKer(DataStore.iBlpList) != null)
                {
                    AfkPlayer HouseBlip = ThisAFKer(DataStore.iBlpList);

                    if (HouseBlip.TimeOn < Game.GameTime)
                    {
                        ClearUp.DeListingBrains(false, DataStore.iBlpList, true);
                        DataStore.iCurrentPlayerz -= 1;
                    }
                    DataStore.iBlpList += 1;
                }
                else
                    DataStore.iBlpList = 0;

                if (ReturnValues.BTimer(1))
                    NewPlayer();
            }
        }
        private static void WhileYouDead(string Kellar, int iKills, int iKilled, Ped Peddy)
        {
            LoggerLight.GetLogging("PedActions.WhileYouDead, string == " + Kellar + ", iKills == " + iKills + ", iKilled == " + iKilled);

            while (Game.Player.Character.GetKiller() == Peddy)
                Script.Wait(1);
            Script.Wait(1000);
            UI.Notify("You  " + iKills + " - " + iKilled + " " + Kellar);
        }
    }
}
