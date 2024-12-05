using System;

namespace HotelSol.Data.ViewModels
{
    public class PaymentVM
    {
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class PaymentWithReservationDetailsVM
    {
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public int ReservationID { get; set; }
        public string GuestFullName { get; set; } // Nombre del huésped asociado a la reserva
    }
}
