using GTA;
using GTA.Native;
using PlayerZero.Classes;
using System.IO;

namespace PlayerZero
{
    public class ClearUp
    {
        public static bool VehExists(Vehicle vehX, int iPlace, int iListTotal)
        {
            LoggerLight.GetLogging("ClearUp.VehExists iPos == " + iPlace + " , from == " + iListTotal);
            bool b = false;
            if (iPlace < iListTotal && vehX != null)
            {
                try
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, vehX.Handle))
                        b = true;
                }
                catch
                {

                }
            }

            return b;
        }
        public static bool PedExists(Ped pedX,int iPlace, int iListTotal)
        {
            LoggerLight.GetLogging("ClearUp.PedExists iPos == " + iPlace +" , from == "+ iListTotal);
            bool b = false;
            if (iPlace < iListTotal && pedX != null)
            {
                try
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, pedX.Handle))
                        b = true;
                }
                catch
                {
                    LoggerLight.GetLogging("ClearUp.PedExists == does not exist");
                }
            }

            return b;
        }
        private static void FlushBrains(int iHandles, int iType)
        {
            LoggerLight.GetLogging("ClearUp.FlushBrains, iHandles == " + iHandles + ", iType == " + iType);

            try
            {
                if (iType == 1)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, iHandles))
                    {
                        Ped Peed = new Ped(iHandles);
                        Peed.Delete();
                    }
                }
                else if (iType == 2)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, iHandles))
                    {
                        Vehicle Peed = new Vehicle(iHandles);
                        Peed.Delete();
                    }
                }
                else
                {
                    if (Function.Call<bool>(Hash.DOES_BLIP_EXIST, iHandles))
                    {
                        Blip BeeLip = new Blip(iHandles);
                        BeeLip.Remove();
                    }
                }
            }
            catch
            {

            }
        }
        public static void PedCleaning(PlayerBrain played, string sOff, bool bGone)
        {
            LoggerLight.GetLogging("ClearUp.PedCleaning, MyName == " + played.MyName + ", MyIdentity == " + played.MyIdentity);

            ScaleDisp.BottomLeft("~h~" + played.MyName + "~s~ " + sOff);
            DeListingBrains(played, bGone);
        }
        public static void PedCleaning(AfkPlayer afkErr, string sOff)
        {
            LoggerLight.GetLogging("ClearUp.PedCleaning, MyName == " + afkErr.MyName + ", MyIdentity == " + afkErr.MyIdentity);

            ScaleDisp.BottomLeft("~h~" + afkErr.MyName + "~s~ " + sOff);
            DeListingBrains(afkErr);
        }
        public static void DeListingBrains(AfkPlayer played)
        {
            LoggerLight.GetLogging("ClearUp.DeListingBrainsAfk, MyName == " + played.MyName + ", MyIdentity == " + played.MyIdentity);

            int iPos = PlayerAI.ReteaveAfk(played.MyIdentity);

            if (iPos != -1 && iPos < DataStore.AFKList.Count)
            {
                played.ThisBlip.Remove();
                ScaleDisp.BottomLeft("~h~" + played.MyName + "~s~ left");
                PlayerAI.RemoveFromAFKList(iPos);
            }
        }
        public static void DeListingBrains(PlayerBrain played, bool bGone)
        {
            LoggerLight.GetLogging("ClearUp.DeListingBrainsPlayer, MyName == " + played.MyName + ", MyIdentity == " + played.MyIdentity);
            int iPos = PlayerAI.ReteaveBrain(played.MyIdentity);

            if (iPos != -1 && iPos < DataStore.PedList.Count)
                PlayerAI.RemoveFromPedList(iPos);
            else
                DataStore.AddtoPedList.Remove(played);


            if (played.Hacker)
            {
                for (int i = 0; i < DataStore.Vicks.Count; i++)
                    DataStore.Vicks[i].MarkAsNoLongerNeeded();
                DataStore.Plops.Clear();
                DataStore.Vicks.Clear();
                DataStore.bHackerIn = false;
                DataStore.bPiggyBack = false;
            }

            if (played.Follower)
                DataStore.iFollow -= 1;

            if (played.ThisBlip != null)
                played.ThisBlip.Remove();

            if (played.DirBlip != null)
                played.DirBlip.Remove();

            if (played.ThisOppress != null)
            {
                PedActions.EmptyVeh(played.ThisOppress);
                played.ThisOppress.Explode();
                played.ThisOppress.MarkAsNoLongerNeeded();
            }

            if (played.ThisVeh != null)
            {
                PedActions.EmptyVeh(played.ThisVeh);
                played.ThisVeh.MarkAsNoLongerNeeded();
            }

            if (played.ThisPed != null)
            {
                played.ThisPed.Detach();
                Function.Call(Hash.REMOVE_PED_FROM_GROUP, played.ThisPed.Handle);

                if (bGone)
                    played.ThisPed.Delete();
                else
                    played.ThisPed.MarkAsNoLongerNeeded();
            }
        }
        public static void ClearPedBlips(string sId)
        {
            LoggerLight.GetLogging("ClearUp.ClearPedBlips, sId == " + sId);

            int iPed = PlayerAI.ReteaveBrain(sId);

            PlayerBrain PeBrain = DataStore.PedList[iPed];
            if (PeBrain.ThisBlip != null)
            {
                PeBrain.ThisBlip.Remove();
                PeBrain.ThisBlip = null;
            }
            if (PeBrain.DirBlip != null)
            {
                PeBrain.DirBlip.Remove();
                PeBrain.DirBlip = null;
            }
        }
        public static void ClearProps()
        {
            for (int i = 0; i < DataStore.Plops.Count; i++)
                DataStore.Plops[i].Delete();
            DataStore.Plops.Clear();
        }
    }
}
