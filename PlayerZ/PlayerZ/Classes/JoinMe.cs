using GTA;

namespace PlayerZero.Classes
{
    public class JoinMe
    {
        public string MyIdentity { get; set; }
        public Ped ThisPed { get; set; }
        public Vehicle ThisVeh { get; set; }
        public bool AddToFollow { get; set; }
        public int InCarOutCarFoot { get; set; }
        public JoinMe(string myId, Ped thisPed, Vehicle thisVeh, bool bFollow, int inCarOUtCarFoot)
        {
            MyIdentity = myId; ThisPed = thisPed; ThisVeh = thisVeh; AddToFollow = bFollow; InCarOutCarFoot = inCarOUtCarFoot;
        }
    
    }
}
