using GTA;
using GTA.Native;
using PlayerZero.Classes;
using System.IO;
using System.Linq;

namespace PlayerZero
{
    public class ClearUp
    {
        public static bool VehExists(Vehicle[] Vehlist, int iPos)
        {
            LoggerLight.GetLogging("VehExists iPos == " + iPos);
            bool b = false;
            if (iPos < Vehlist.Count())
            {
                try
                {
                    if (Vehlist[iPos].Exists())
                        b = true;
                }
                catch
                {

                }
            }

            return b;
        }
        public static bool PedExists(Ped[] Peddylist, int iPos)
        {
            LoggerLight.GetLogging("PedExists iPos == " + iPos);
            bool b = false;
            if (iPos < Peddylist.Count())
            {
                try
                {
                    if (Peddylist[iPos].Exists())
                        b = true;
                }
                catch
                {

                }
            }

            return b;
        }
        public static void RedundentAssets()
        {
            LoggerLight.GetLogging("RedundentAssets");

            if (File.Exists(DataStore.sMemory))
            {
                BackUpBrain B = XmlReadWrite.LoadPlayerBrain(DataStore.sMemory);

                for (int i = 0; i < B.BigBrain.Count; i++)
                    FlushBrains(B.BigBrain[i], 1);

                for (int i = 0; i < B.BigVeh.Count; i++)
                    FlushBrains(B.BigVeh[i], 2);

                for (int i = 0; i < B.BigBlip.Count; i++)
                    FlushBrains(B.BigBlip[i], 0);

                for (int i = 0; i < B.BigDirBlip.Count; i++)
                    FlushBrains(B.BigDirBlip[i], 0);

                for (int i = 0; i < B.BigAFKBlip.Count; i++)
                    FlushBrains(B.BigAFKBlip[i], 0);
            }
        }
        private static void FlushBrains(int iHandles, int iType)
        {
            LoggerLight.GetLogging("FlushBrains, iHandles == " + iHandles + ", iType == " + iType);

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
        public static void PedCleaning(int iPed, string sOff, bool bGone)
        {
            LoggerLight.GetLogging("PedCleaning, iPed == " + iPed);

            int iBe = PedActions.ReteaveBrain(iPed);
            UI.Notify("~h~" + DataStore.PedList[iBe].MyName + "~s~ " + sOff, false);

            DeListingBrains(true, iPed, bGone);
            DataStore.iCurrentPlayerz -= 1;
        }
        public static void DeListingBrains(bool bPed, int iPos, bool bGone)
        {
            LoggerLight.GetLogging("DeListingBrains, bPed == " + bPed + ", iPos == " + iPos);

            if (bPed)
            {
                iPos = PedActions.ReteaveBrain(iPos);

                if (DataStore.PedList[iPos].ThisPed == null)
                {
                    if (DataStore.PedList[iPos].ThisBlip != null)
                        DataStore.PedList[iPos].ThisBlip.Remove();

                    if (DataStore.PedList[iPos].DirBlip != null)
                        DataStore.PedList[iPos].DirBlip.Remove();

                    if (DataStore.PedList[iPos].ThisVeh != null)
                        DataStore.PedList[iPos].ThisVeh.MarkAsNoLongerNeeded();

                    DataStore.PedList.RemoveAt(iPos);
                }
                else
                {
                    if (DataStore.PedList[iPos].Hacker)
                    {
                        for (int i = 0; i < DataStore.Plops.Count; i++)
                        {
                            DataStore.Plops[i].MarkAsNoLongerNeeded();
                            DataStore.bEclipceWind = false;
                        }

                        for (int i = 0; i < DataStore.Vicks.Count; i++)
                            DataStore.Vicks[i].MarkAsNoLongerNeeded();
                        DataStore.Plops.Clear();
                        DataStore.Vicks.Clear();
                        DataStore.PedList[iPos].ThisPed.Detach();
                        DataStore.bHackerIn = false;
                        DataStore.bPiggyBack = false;
                        PedActions.FireOrb(DataStore.PedList[iPos].Level, DataStore.PedList[iPos].ThisPed, true);
                    }

                    if (DataStore.PedList[iPos].Follower)
                        DataStore.iFollow -= 1;

                    if (DataStore.PedList[iPos].ThisBlip != null)
                        DataStore.PedList[iPos].ThisBlip.Remove();

                    if (DataStore.PedList[iPos].DirBlip != null)
                        DataStore.PedList[iPos].DirBlip.Remove();

                    if (DataStore.PedList[iPos].ThisOppress != null)
                    {
                        PedActions.EmptyVeh(DataStore.PedList[iPos].ThisOppress);
                        DataStore.PedList[iPos].ThisOppress.Explode();
                        DataStore.PedList[iPos].ThisOppress.MarkAsNoLongerNeeded();
                        DataStore.PedList[iPos].ThisOppress = null;

                        if (DataStore.PedList[iPos].ThisVeh != null)
                        {
                            PedActions.EmptyVeh(DataStore.PedList[iPos].ThisVeh);
                            DataStore.PedList[iPos].ThisVeh.Delete();
                            DataStore.PedList[iPos].ThisVeh = null;
                        }
                    }
                    else if (DataStore.PedList[iPos].ThisVeh != null)
                    {
                        PedActions.EmptyVeh(DataStore.PedList[iPos].ThisVeh);
                        DataStore.PedList[iPos].ThisVeh.MarkAsNoLongerNeeded();
                        DataStore.PedList[iPos].ThisVeh = null;
                    }

                    if (bGone)
                        DataStore.PedList[iPos].ThisPed.Delete();
                    else
                        DataStore.PedList[iPos].ThisPed.MarkAsNoLongerNeeded();
                    Function.Call(Hash.REMOVE_PED_FROM_GROUP, DataStore.PedList[iPos].ThisPed.Handle);
                    DataStore.PedList.RemoveAt(iPos);
                }
            }
            else
            {
                DataStore.AFKList[iPos].ThisBlip.Remove();
                UI.Notify("~h~" + DataStore.AFKList[iPos].MyName + "~s~ left");
                DataStore.AFKList.RemoveAt(iPos);
            }

            BackItUpBrain();
        }
        public static void ClearPedBlips(int iPed)
        {
            LoggerLight.GetLogging("ClearPedBlips, iPed == " + iPed);

            iPed = PedActions.ReteaveBrain(iPed);

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
        public static void BackItUpBrain()
        {
            LoggerLight.GetLogging("BackItUpBrain");
            BackUpBrain BB = new BackUpBrain();

            for (int i = 0; i < DataStore.PedList.Count; i++)
            {
                if (DataStore.PedList[i].ThisPed != null)
                    BB.BigBrain.Add(DataStore.PedList[i].ThisPed.Handle);

                if (DataStore.PedList[i].ThisVeh != null)
                    BB.BigVeh.Add(DataStore.PedList[i].ThisVeh.Handle);

                if (DataStore.PedList[i].ThisBlip != null)
                    BB.BigBlip.Add(DataStore.PedList[i].ThisBlip.Handle);

                if (DataStore.PedList[i].DirBlip != null)
                    BB.BigDirBlip.Add(DataStore.PedList[i].DirBlip.Handle);
            }

            for (int i = 0; i < DataStore.AFKList.Count; i++)
                BB.BigAFKBlip.Add(DataStore.AFKList[i].ThisBlip.Handle);

            XmlReadWrite.SavePlayerBrain(BB, DataStore.sMemory);
        }
    }
}
