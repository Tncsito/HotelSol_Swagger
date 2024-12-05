using System;

namespace HotelSol.Data.ViewModels
{
    public class GuestVM
    {
        public string FullName { get; set; }
        public string ContactInfo { get; set; }
    }

    public class GuestWithReservationsVM
    {
        public string FullName { get; set; }
        public string ContactInfo { get; set; }
        public int TotalReservations { get; set; } // Cantidad de reservas realizadas por el huésped
    }
}
