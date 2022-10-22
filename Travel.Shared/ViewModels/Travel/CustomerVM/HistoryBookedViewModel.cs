using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel.CustomerVM
{
    public class HistoryBookedViewModel
    {
        private string bookingNo ;
        private string idSchedule ;
        private long dateBooking ;
        private float totalPrice ;
        private int valuePromotion ;
        private string thumbsnail ;

        private long departureDate ;
        private long returnDate ;

        public long DepartureDate { get => departureDate; set => departureDate = value; }
        public long ReturnDate { get => returnDate; set => returnDate = value; }
        public int ValuePromotion { get => valuePromotion; set => valuePromotion = value; }
        public string Thumbsnail { get => thumbsnail; set => thumbsnail = value; }
        public string BookingNo { get => bookingNo; set => bookingNo = value; }
        public string IdSchedule { get => idSchedule; set => idSchedule = value; }
        public long DateBooking { get => dateBooking; set => dateBooking = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
