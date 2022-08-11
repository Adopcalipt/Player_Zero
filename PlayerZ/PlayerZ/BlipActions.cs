using GTA;
using GTA.Math;
using GTA.Native;

namespace PlayerZero
{
    public class BlipActions
    {
        public static Blip DirectionalBlimp(Ped pEdd)
        {
            LoggerLight.GetLogging("DirectionalBlimp");

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, 399);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);
            Function.Call(Hash.SET_BLIP_PRIORITY, MyBlip.Handle, 1);
            Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, 85);
            Function.Call(Hash.SET_BLIP_DISPLAY, MyBlip.Handle, 8);

            return MyBlip;
        }
        public static Blip PedBlimp(Ped pEdd, int iBlippy, string sName, int iColour)
        {
            LoggerLight.GetLogging("PedBlimp, iBlippy == " + iBlippy + ", sName == " + sName + ", iColour" + iColour);

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_ENTITY, pEdd.Handle);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

            Function.Call(Hash.SET_BLIP_COLOUR, MyBlip.Handle, iColour);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
            Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);

            return MyBlip;
        }
        public static Blip LocalBlip(Vector3 Vlocal, int iBlippy, string sName)
        {
            LoggerLight.GetLogging("LocalBlip, iBlippy == " + iBlippy + ", sName == " + sName);

            Blip MyBlip = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, Vlocal.X, Vlocal.Y, Vlocal.Z);
            Function.Call(Hash.SET_BLIP_SPRITE, MyBlip.Handle, iBlippy);
            Function.Call(Hash.SET_BLIP_AS_SHORT_RANGE, MyBlip.Handle, true);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_SET_BLIP_NAME, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
            Function.Call(Hash.END_TEXT_COMMAND_SET_BLIP_NAME, MyBlip.Handle);
            Function.Call((Hash)0xF9113A30DE5C6670, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, " Player: " + sName);
            Function.Call((Hash)0xBC38B49BCB83BC9B, MyBlip.Handle);

            return MyBlip;
        }
        public static void BlipDirect(Blip Blippy, float fDir)
        {
            int iHead = (int)fDir;
            if (Blippy.Exists())
                Function.Call(Hash.SET_BLIP_ROTATION, Blippy.Handle, iHead);
        }
    }
}
