using GTA.Math;

namespace PlayerZero.Classes
{
    public class Tattoo
    {
        public string BaseName { get; set; }
        public string TatName { get; set; }
        public string Name { get; set; }

        public Tattoo(string baseName, string tatName, string name)
        {
            BaseName = BaseName; TatName = tatName; Name = name;
        }
    }
}
