using System.Collections.Generic;
using System.Data;
using Model;
using ServiceStack.OrmLite;
using ServiceStack.ServiceHost;
using Contact = Service.Data.Contact;
using User = Service.Data.User;

namespace Service.Service
{
    internal class UserService : IService<UserRequest>
    {
        public IDbConnectionFactory ConnectionFactory { get; set; }

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

                    Dictionary<long, List<string>> contactGroups = dbCmd.GetLookup<long, string>(
                        "select cg.ContactId, cg.Name from ContactGroup cg join Contact c on cg.ContactId = c.Id where c.UserId = {0}",
                        user.Id);

                    response.User = user.ToModel(contacts, contactGroups);
                }
            }

            return response;
        }
    }
}