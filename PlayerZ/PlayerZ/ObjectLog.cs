using GTA;
using GTA.Native;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace PlayerZero
{
    public class ObjectLog
    {
        private static readonly string sAssSave = "" + Directory.GetCurrentDirectory() + "/Scripts/PlayerZero/MyAssets.txt";

        public static void WriteObj(string sLogs, TextWriter tEx)
        {
            try
            {
                tEx.WriteLine(sLogs);
            }
            catch
            {

            }
        }
        public static void BackUpAss(string myAss)
        {
            using (StreamWriter tEx = File.AppendText(sAssSave))
                WriteObj(myAss, tEx);
        }
        public static void ClearThis()
        {
            if (File.Exists(sAssSave))
            {
                LoggerLight.GetLogging("ClearThis.HasFile");

                List<Prop> prop = new List<Prop>();
                List<Ped> peds = new List<Ped>();
                List<Blip> blips = new List<Blip>();
                List<Vehicle> vehicles = new List<Vehicle>();
                string[] readNote = File.ReadAllLines(sAssSave);

                for (int i = 0; i < readNote.Count(); i++)
                {
                    if (readNote[i].Contains("Prop-"))
                    {
                        readNote[i].Remove(4);
                        int MyHand = IniSettings.ReadMyInt(readNote[i]);
                        prop.Add(new Prop(MyHand));
                    }
                    else if (readNote[i].Contains("Blip-"))
                    {
                        readNote[i].Remove(4);
                        int MyHand = IniSettings.ReadMyInt(readNote[i]);
                        blips.Add(new Blip(MyHand));
                    }
                    else if (readNote[i].Contains("PedX-"))
                    {
                        readNote[i].Remove(4);
                        int MyHand = IniSettings.ReadMyInt(readNote[i]);
                        peds.Add(new Ped(MyHand));
                    }
                    else if (readNote[i].Contains("Vehs-"))
                    {
                        readNote[i].Remove(4);
                        int MyHand = IniSettings.ReadMyInt(readNote[i]);
                        vehicles.Add(new Vehicle(MyHand));
                    }
                }

                for (int i = 0; i < vehicles.Count; i++)
                    FlushBrains(vehicles[i], null, null, null);

                for (int i = 0; i < peds.Count; i++)
                    FlushBrains(null, peds[i], null, null);

                for (int i = 0; i < blips.Count; i++)
                    FlushBrains(null, null, blips[i], null);

                for (int i = 0; i < prop.Count; i++)
                    FlushBrains(null, null, null, prop[i]);

                File.Delete(sAssSave);
            }
        }
        private static void FlushBrains(Vehicle Vhick, Ped Peddy, Blip Blippy, Prop Proper)
        {
            try
            {
                if (Vhick != null)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, Vhick.Handle))
                        Vhick.Delete();
                }
                else if (Peddy != null)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, Peddy.Handle))
                        Peddy.Delete();
                }
                else if (Proper != null)
                {
                    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, Proper.Handle))
                        Proper.Delete();
                }
                else if (Blippy != null)
                {
                    if (Function.Call<bool>(Hash.DOES_BLIP_EXIST, Blippy.Handle))
                        Blippy.Remove();
                }
            }
            catch
            {

            }
        }
    }
}
