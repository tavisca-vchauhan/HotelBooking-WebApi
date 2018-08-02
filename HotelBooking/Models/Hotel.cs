using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class Hotel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int RoomsAvailable { get; set; }
        public string Address { get; set; }
        public int Pincode{ get; set; }
        public int RentPerDay { get; set; }

    }
}