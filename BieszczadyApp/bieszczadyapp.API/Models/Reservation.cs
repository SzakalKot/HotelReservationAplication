using System;

namespace bieszczadyapp.API.Models
{
    public class Reservation
    {
         public int id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        
        public virtual Room Room {get;set;}
        public virtual User User{get; set;}
    }
}