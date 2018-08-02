using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class Status
    {

        public HotelStatus HotelStatus { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }

        public enum HotelStatus
        {
            Success,
            Failure,
            Warning
        }
    
}