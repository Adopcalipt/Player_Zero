using GTA;

namespace PlayerZero.Classes
{
    public class GetInAveh
    {
        public Ped Peddy { get; set; }
        public Vehicle Vhic { get; set; }
        public int Seats { get; set; }
        public string PedId { get; set; }

        public GetInAveh(Ped peddy, Vehicle vhic, string pedId)
        {
            Peddy = peddy; Vhic = vhic; Seats = -1; PedId = pedId; 
        }
    }
}
