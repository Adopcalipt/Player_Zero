using GTA.Math;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace PlayerZero.Classes
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float R;

        public Vector4(float x, float y, float z, float r)
        {
            X = x; Y = y; Z = z; R = r;
        }
        public float DistanceFrom(Vector3 V3)
        {
            return V3.DistanceTo(new Vector3(X, Y, Z));
        }
    }
}
