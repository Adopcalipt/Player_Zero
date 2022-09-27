using GTA.Math;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace PlayerZero.Classes
{
    public struct VehBlips
    {
        public string VehicleKey;
        public int BlipNo;

        public VehBlips(string vehicle, int blipNo)
        {
            VehicleKey = vehicle; BlipNo = blipNo;
        }
    }
}
