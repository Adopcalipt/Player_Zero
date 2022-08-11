using GTA;
using GTA.Native;
using System.Linq;

namespace PlayerZero
{
    public class ScaleDisp
    {
        private static void Scaleform_PLAYER_LIST()
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

            DataStore.bPlayerList = true;
        }
        private static void CloseBaseHelpBar()
        {
            LoggerLight.GetLogging("ScaleDisp.CloseBaseHelpBar");

            unsafe
            {
                int SF = DataStore.iScale;
                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &SF);
            }

            DataStore.bPlayerList = false;
        }
        public static void PlayScales()
        {
            LoggerLight.GetLogging("ScaleDisp.PlayScales");
            bool bLoadMenu = false;
            Scaleform_PLAYER_LIST();

            TopLeftUI("Press" + DataStore.ControlSym[DataStore.MySettings.iDisableMod] + "for hackers menu mod");
            ///if (DataStore.bDisabled)
            //    TopLeftUI("Press" + DataStore.ControlSym[DataStore.MySettings.iDisableMod] + " to enable mod");
            //else
            //    TopLeftUI("Press" + DataStore.ControlSym[DataStore.MySettings.iClearPlayList] + "to clear the session and " + DataStore.ControlSym[DataStore.MySettings.iDisableMod] + " to disable mod");

            int iStick = Game.GameTime + 8000;
            while (!bLoadMenu && iStick > Game.GameTime && !ReturnValues.ButtonDown(DataStore.MySettings.iClearPlayList, false) && !ReturnValues.ButtonDown(DataStore.MySettings.iDisableMod, false))
            {
                Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, DataStore.iScale, 255, 255, 255, 255);
                Script.Wait(1);
                //if (ReturnValues.ButtonDown(DataStore.MySettings.iGetlayList, true) && ReturnValues.WhileButtonDown(DataStore.MySettings.iClearPlayList, true) && !DataStore.bDisabled)
                //{
                //    Script.Wait(500);
                //    PedActions.LaggOut();
                //}
                if (ReturnValues.ButtonDown(DataStore.MySettings.iGetlayList, true) && ReturnValues.WhileButtonDown(DataStore.MySettings.iDisableMod, true))
                    bLoadMenu = true;
            }
            CloseBaseHelpBar();
            if (bLoadMenu)
                HackazMenu.HackerZMenu();
        }
        public static void TopLeftUI(string sText)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, sText);
            Function.Call(Hash._0x238FFE5C7B0498A6, false, false, false, 5000);
        }
    }
}
