using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class HotelResponse
    {

        public List<Hotel> Hotel { get; set; }
        public Status Status { get; set; }
    }
}