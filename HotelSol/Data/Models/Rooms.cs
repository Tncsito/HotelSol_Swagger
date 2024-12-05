using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelSol.Data.Models
{
    public class Rooms
    {
        public int RoomID { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int PricePerNight { get; set; }
        [JsonIgnore]
        public ICollection<ReservationRoom> ReservationRooms { get; set; }

    }
}
