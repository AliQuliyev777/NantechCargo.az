using System;
using System.Collections.Generic;

#nullable disable

namespace NantechCargo.az.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public int? UserStatusId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoto { get; set; }
        public DateTime? UserBirthDay { get; set; }
        public string UserAddress { get; set; }
        public string UserFin { get; set; }
    
        public virtual Status UserStatus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
