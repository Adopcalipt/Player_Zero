using System.Collections.Generic;

namespace PlayerZero.Classes
{
    public class ContactList
    {
        public List<MakeContact> PedBrain { get; set; }

        public ContactList()
        {
            PedBrain = new List<MakeContact>();
        }
    }
}
