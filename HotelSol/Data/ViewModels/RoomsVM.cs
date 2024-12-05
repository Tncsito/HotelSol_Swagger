using System;

namespace HotelSol.Data.ViewModels
{
    public class RoomVM
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }

    public class RoomWithReservationsVM
    {
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public int TotalReservations { get; set; } // Cantidad de veces reservada
    }
}
