using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HotelSol.Data.Models
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public int ReservationID { get; set; }
        [JsonIgnore]  // Ignora el campo 'reservation' en la serialización JSON
        public Reservations Reservation { get; set; }
        public DateTime? PaymentDate { get; set; } //Acepta null
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
    }
    public class PaymentRequest
    {
        public int ReservationID { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
    }
}
