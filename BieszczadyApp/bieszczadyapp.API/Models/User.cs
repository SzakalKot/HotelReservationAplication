using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace bieszczadyapp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[]  PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string name { get; set; }
        public string Surname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        public ICollection<Photo> Photos {get; set;}

        public User(){
                Photos = new Collection<Photo>();
        }


    }
}