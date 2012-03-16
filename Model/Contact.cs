using System.Collections.Generic;

namespace Model
{
    public class Contact
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public List<string> Groups { get; set; }
    }
}