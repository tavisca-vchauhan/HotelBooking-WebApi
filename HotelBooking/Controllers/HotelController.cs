using System;
using HotelBooking.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    public class HotelController : ApiController
    {
        private static int _index = 5;

        private static List<Hotel> _hotels = new List<Hotel>()
        {
        new Hotel(){Name="Novotel" ,Id=1 , RoomsAvailable=20,Address="Viman Nagar ,Pune , Maharashtra",Pincode=411014,RentPerDay=5500},
        new Hotel(){Name="Hayatt Regency" ,Id=2 , RoomsAvailable=17,Address="Weikfield IT Park, Pune Nagar Road, Pune, Maharashtra ",Pincode=411014,RentPerDay=8500 },
        new Hotel(){Name="The Gateway Hotel " ,Id=3 , RoomsAvailable=24,Address="Rajiv Gandhi Infotech Park in Hinjewadi,Pune ",Pincode=411057,RentPerDay=3800},
        new Hotel(){Name="The Orchid Hotel" ,Id=4 , RoomsAvailable=18,Address="Historic Aga Khan Palace,Pune",Pincode=411045,RentPerDay=4000 }
        };

        public HotelResponse GetHotels()
        {
            try
            {
                return new HotelResponse()
                {
                    Hotel = _hotels,
                    Status = new Status()
                    {
                        HotelStatus = HotelStatus.Success,
                        ErrorMessage = string.Empty,
                        StatusCode = 200
                    }
                };
            }
            catch (Exception e)
            {
                return new HotelResponse()
                {
                    Hotel = null,
                    Status = new Status()
                    {
                        HotelStatus = HotelStatus.Failure,
                        ErrorMessage = "Server Not Responding. Please try agin later...",
                        StatusCode = 500
                    }
                };
            }
        }

        [HttpGet]
        public HotelResponse GetHotelById(int id)
        {
            try
            {

                var WantedHoted = _hotels.Find(x => x.Id == id);
                if (WantedHoted == null)
                {
                    throw new Exception("There is no Hotel found with this Id.");
                }
                else
                {
                    return new HotelResponse()
                    {
                        Hotel = new List<Hotel>() { _hotels.Find(i => i.Id == id) },
                        Status = new Status()
                        {
                            HotelStatus = HotelStatus.Success,
                            StatusCode = 200,
                        }
                    };
                }

            }
            catch(Exception e)
            {
                return new HotelResponse()
                {
                    Hotel = null,
                    Status = new Status()
                    {
                        HotelStatus = HotelStatus.Failure,
                        StatusCode = 404,
                        ErrorMessage = "" + e.Message
                    }
                };
            }
        }

        [HttpPut]
        public string BookRooms(int id, [FromBody] int numberOfRoomsToBeBooked)
        {
            try
            {

                var hotelToBeBooked = _hotels.Find(i => i.Id == id);
                if (hotelToBeBooked != null)
                {
                        if (hotelToBeBooked.RoomsAvailable < numberOfRoomsToBeBooked)
                        return "Only " + hotelToBeBooked.RoomsAvailable + " rooms available, Can't book " + numberOfRoomsToBeBooked + " room";
                        else
                        hotelToBeBooked.RoomsAvailable = hotelToBeBooked.RoomsAvailable - numberOfRoomsToBeBooked;

                    return "Room successfully booked";
                }
                else
                {
                    return "No hotel found with this Id";
                }
            }
            catch(Exception e)
            {
                return ""+e.Message;
            }
        }

        [HttpPost]
        public HotelResponse AddAHotel(Hotel hoteltoBeAdded)
        {
            try
            {

                hoteltoBeAdded.Id = _index;
                _index++;
                _hotels.Add(hoteltoBeAdded);
                return new HotelResponse()
                {
                    Status = new Status()
                    {
                        HotelStatus = HotelStatus.Success,
                        StatusCode = 200
                    }
                };
            }
            catch (Exception e)
            {
                return new HotelResponse()
                {
                    Hotel = null,
                    Status = new Status()
                    {
                        HotelStatus = HotelStatus.Failure,
                        ErrorMessage = "Internal Server Error",
                        StatusCode = 500
                    }
                };
            }
        }

        [HttpDelete]
        public HotelResponse DeleteHotel(int id)
        {
            try
            {

                var hotelToBeDeleted = _hotels.Find(i => i.Id == id);
                if (hotelToBeDeleted != null)
                {

                    _hotels.Remove(hotelToBeDeleted);
                    return new HotelResponse()
                    {
                        Status = new Status()
                        {
                            HotelStatus = HotelStatus.Success,
                            StatusCode = 200
                        }

                    };
                }
                else
                {
                    return new HotelResponse()
                    {
                        Hotel = null,
                        Status = new Status()
                        {
                            HotelStatus = HotelStatus.Failure,
                            StatusCode = 404,
                            ErrorMessage = "The Hotel to be deleted is Not Found"

                        }
                    };
                }
            }
            catch(Exception e)
            {
                return new HotelResponse()
                {
                    Hotel = null,
                    Status = new Status()
                    {
                        HotelStatus = HotelStatus.Failure,
                        StatusCode = 404,
                        ErrorMessage = "" + e.Message

                    }
                };
            }
        }

        [HttpPut]
        public string Update(Hotel update)
        {
            try
            {

                var hotelToBeUpdated = _hotels.Find(i => i.Id == update.Id);
                if (hotelToBeUpdated != null)
                {
                    if (update.Name != null)
                        hotelToBeUpdated.Name = update.Name;
                    if (update.Pincode != 0)
                        hotelToBeUpdated.Pincode = update.Pincode;
                    if (update.RentPerDay != 0)
                        hotelToBeUpdated.RentPerDay = update.RentPerDay;
                    if (update.RoomsAvailable != 0)
                        hotelToBeUpdated.RoomsAvailable = update.RoomsAvailable;
                    if (update.Address != null)
                        hotelToBeUpdated.Address = update.Address;

                    return "Hotel successfully updated ";

                }
                else
                {
                    return " No hotel found with this Id";
                }
            }
            catch(Exception e)
            {
                return ""+e.Message;
            }
        }

    }
}
