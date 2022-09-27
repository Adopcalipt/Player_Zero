using GTA;
using GTA.Math;
using GTA.Native;
using PlayerZero.Classes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlayerZero
{
    public class ReturnValues
    {
        public static Vector3 YoPoza()
        {
            Ped XPed = Game.Player.Character;
            return XPed.Position;
        }
        public static bool NotTheSame(Vector4 V1, Vector4 V2)
        {
            bool bTrue = false;

            if (V1.X != V2.X)
                bTrue = true;

            if (V1.Y != V2.Y)
                bTrue = true;

            if (V1.Z != V2.Z)
                bTrue = true;

            if (V1.R != V2.R)
                bTrue = true;

            return bTrue;
        }
        public static string RandomId()
        {
            string sNewName = Path.GetRandomFileName();
            while (!IdScan(sNewName))
            {
                sNewName = Path.GetRandomFileName();
                Script.Wait(100);
            }
            return sNewName;
        }
        private static bool IdScan(string sThisName)
        {
            bool IsNew = true;

            for (int i = 0; i < DataStore.AFKList.Count; i++)
            {
                if (DataStore.AFKList[i].MyIdentity == sThisName)
                    IsNew = false;
            }
            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].MyIdentity == sThisName)
                    IsNew = false;
            }
            for (int i = 0; i < DataStore.PlayContList.PedBrain.Count; i++)
            {
                if (DataStore.PlayContList.PedBrain[i].MyIdentity == sThisName)
                    IsNew = false;
            }
            return IsNew;
        }
        public static string SillyNameList()
        {
            LoggerLight.GetLogging("ReturnValues.SillyNameList");

            string name = FindNewName();

            while (DataStore.PlayerNames.Contains(name))
            {
                name = FindNewName();
                Script.Wait(1);
            }
            DataStore.PlayerNames.Add(name);
            return name;
        }
        private static string FindNewName()
        {
            LoggerLight.GetLogging("ReturnValues.SillyNameList");

            List<string> sListNumbers = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            List<string> sListVowls = new List<string> { "ay", "ee", "igh", "ow", "oo", "or", "air", "ir", "ou", "oy", "ai", "ea", "ie", "ew", "ur", "ow", "oi", "ire", "ear", "ure", "tion", "ey", "ore", "ere", "oor" };
            List<string> sListPadding = new List<string> { "TyHrd", "Luzz", "Killz" };
            List<string> sListPostfix = new List<string> { "X", "-", "^", "*", "#", "$" };
            List<string> sListOpeniLet = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            int iS = RandomNum.FindRandom(3, 0, sListOpeniLet.Count - 1);
            string sName = sListOpeniLet[iS];

            iS = RandomNum.FindRandom(4, 0, sListVowls.Count - 1);
            sName = sName + sListVowls[iS];
            if (RandomNum.FindRandom(5, 0, 10) < 5)
            {
                iS = RandomNum.FindRandom(4, 0, sListVowls.Count - 1);
                sName = sName + sListVowls[iS];
            }
            iS = RandomNum.FindRandom(6, 0, 20);

            if (iS < 3)
            {
                if (DataStore.MySettings.Aggression > 7)
                    sName = sName + sListPostfix[RandomNum.FindRandom(8, 0, sListPostfix.Count - 1)] + sListPadding[RandomNum.FindRandom(7, 0, sListPadding.Count - 1)];
            }
            else if (iS < 6)
            {
                string s = "";
                for (int i = 0; i < 3; i++)
                    s = s + sListPostfix[RandomNum.FindRandom(8, 0, sListPostfix.Count - 1)];

                string sR = "";
                int iRev = s.Count() - 1;
                while (iRev > -1)
                {
                    sR = sR + s[iRev];
                    iRev--;
                }
                sName = s + sName + sR;
            }
            else if (iS < 10)
            {
                sName = sName + sListNumbers[RandomNum.FindRandom(9, 0, sListNumbers.Count - 1)] + sListNumbers[RandomNum.FindRandom(9, 0, sListNumbers.Count - 1)] + sListNumbers[RandomNum.FindRandom(9, 0, sListNumbers.Count - 1)];
            }
            else if (iS < 14)
            {
                sName = sName + sListNumbers[RandomNum.FindRandom(9, 0, sListNumbers.Count - 1)] + sListNumbers[RandomNum.FindRandom(9, 0, sListNumbers.Count - 1)];
            }
            else if (iS < 16)
            {
                sName = sName + sListPostfix[RandomNum.FindRandom(8, 0, sListPostfix.Count - 1)] + sListNumbers[RandomNum.FindRandom(9, 0, sListNumbers.Count - 1)];
            }

            return sName;
        }
        public static float RandFlow(float fMin, float fMax)
        {
            return Function.Call<float>(Hash.GET_RANDOM_INT_IN_RANGE, fMin, fMax);
        }
        public static bool BeInAngle(float fRange, float fValue_01, float fValue_02)
        {
            LoggerLight.GetLogging("ReturnValues.BeInAngle, fRange == " + fRange + ", fValue_01 == " + fValue_01 + ", fValue_02 == " + fValue_02);

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
        public static bool WhileButtonDown(int CButt, bool bDisable)
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
        public static bool ButtonDown(int CButt, bool bDisable)
        {
            if (bDisable)
                ButtonDisabler(CButt);
            return Function.Call<bool>(Hash.IS_DISABLED_CONTROL_PRESSED, 0, CButt);
        }
        private static void ButtonDisabler(int LButt)
        {
            Function.Call(Hash.DISABLE_CONTROL_ACTION, 0, LButt, true);
        }
        public static int PlayerZinSesh()
        {
            return DataStore.PedList.Count + DataStore.AFKList.Count + FindStuff.MakeCarz.Count + FindStuff.MakeFrenz.Count;
        }
        public static int UniqueLevels()
        {
            LoggerLight.GetLogging("PedActions.UniqueLevels");

            int iNumber = RandomNum.RandInt(1, 1000);

            return iNumber;
        }
        public static bool InSameVeh(Ped Me)
        {
            bool bIn = false;
            if (Me.IsInVehicle())
            {
                Vehicle Vic = Me.CurrentVehicle;
                if (Game.Player.Character.IsInVehicle(Vic))
                    bIn = true;
            }
            return bIn;
        }
        public static bool HasASeat(Vehicle vMe)
        {
            bool bIn = false;
            if (vMe != null)
            {
                if (FindUSeat(vMe, false) > -1)
                    bIn = true;
            }
            return bIn;
        }
        public static int FindUSeat(Vehicle vMe, bool bPass)
        {
            int iSeats;
            if (bPass)
            {
                iSeats = 0;
                while (iSeats < Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, vMe.Handle))
                {
                    if (Function.Call<bool>(Hash.IS_VEHICLE_SEAT_FREE, vMe.Handle, iSeats))
                        break;
                    else
                        iSeats++;
                }
            }
            else
            {
                iSeats = Function.Call<int>(Hash.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS, vMe.Handle);
                while (iSeats > -1)
                {
                    if (Function.Call<bool>(Hash.IS_VEHICLE_SEAT_FREE, vMe.Handle, iSeats))
                        break;
                    else
                        iSeats--;
                }
            }

            return iSeats;
        }
    }
}
