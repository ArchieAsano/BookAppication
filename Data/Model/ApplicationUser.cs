using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class ApplicationUser 
    {
        
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Chat> ChatsAsUser1 { get; set; }
        public virtual ICollection<Chat> ChatsAsUser2 { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
