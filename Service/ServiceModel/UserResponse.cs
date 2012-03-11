using System.Collections.Generic;
using Service.Data.Model;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Service.ServiceModel
{
    public class UserResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public User User { get; set; }

        public List<ContactAndGroups> Contacts { get; set; }
    }
}