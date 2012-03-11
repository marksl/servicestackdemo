using System.Collections.Generic;
using System.Data;
using System.Linq;
using Service.Data.Model;
using Service.ServiceModel;
using ServiceStack.OrmLite;
using ServiceStack.ServiceHost;

namespace Service.Service
{
    public class UserRequest
    {
        public int Id { get; set; }
    }

    public class UserService : IService<UserRequest>
    {
        public IDbConnectionFactory ConnectionFactory { get; set; }

        #region IService<UserRequest> Members

        public object Execute(UserRequest request)
        {
            var response = new UserResponse();

            using (IDbConnection dbConn = ConnectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = dbConn.CreateCommand())
            {
                var user = dbCmd.GetById<User>(request.Id);
                if (user != null)
                {
                    List<Contact> contacts = dbCmd.Select<Contact>(x => x.UserId == user.Id);

                    Dictionary<int, List<string>> contactGroups = dbCmd.GetLookup<int, string>(
                        "select cg.ContactId, cg.Name from ContactGroup cg join Contact c on cg.ContactId = c.Id where c.UserId = {0}",
                        user.Id);

                    response.User = user;
                    response.Contacts = GetContacts(contacts, contactGroups);
                }
            }

            return response;
        }

        #endregion

        // Perhaps Automapper?
        // http://lostechies.com/jimmybogard/2009/01/23/automapper-the-object-object-mapper/
        private List<ContactAndGroups> GetContacts(IEnumerable<Contact> contacts,
                                                   Dictionary<int, List<string>> contactGroups)
        {
            return
                contacts.Select(c => new ContactAndGroups {Email = c.Email, Name = c.Name, Groups = contactGroups[c.Id]})
                    .ToList();
        }
    }
}