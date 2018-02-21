namespace bieszczadyapp.API.Models
{
    public class Room
    {
         public int RoomId { get; set; }
        public RoomType Type { get; set; }
        public int PerAmount { get; set; }
        public string Description { get; set; }
        public bool Internet { get; set; }
        public bool tv { get; set; }
        public bool bathroom { get; set; }  
        public bool bussy { get; set; }

        
        
    }
    public enum RoomType{
        apart ,
        normal  
    }
    }
