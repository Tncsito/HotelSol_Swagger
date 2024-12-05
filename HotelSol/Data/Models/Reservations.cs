using System;
using System.Collections.Generic;

namespace HotelSol.Data.Models
{
    public class Reservations
    {
        public int ReservationID { get; set; }
        public int GuestID { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guests Guests { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<ReservationRoom> ReservationRooms { get; set; }

    }
}
