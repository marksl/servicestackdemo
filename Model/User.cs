using System.Collections.Generic;

namespace Model
{
    public class User
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}