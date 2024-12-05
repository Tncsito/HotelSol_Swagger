using System.Collections.Generic;

namespace HotelSol.Data.Models
{
    public class ReservationRoom
    {
        public int ReservationRoomID { get; set; }
        public int ReservationID { get; set; }
        public Reservations Reservations { get; set; }
        public int RoomID { get; set; }
        public Rooms Rooms { get; set; }
        public int TotalPrice { get; set; }
    }
}
