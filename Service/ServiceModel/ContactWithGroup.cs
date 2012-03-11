using System.Collections.Generic;

namespace Service.ServiceModel
{
    public class ContactAndGroups
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public List<string> Groups { get; set; }
    }
}