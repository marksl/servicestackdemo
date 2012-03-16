using System.Collections.Generic;
using System.Linq;
using ServiceStack.DesignPatterns.Model;

namespace Service.Data
{
    internal class User : IHasId<long>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long Id { get; set; }

        public Model.User ToModel(List<Contact> contacts, Dictionary<long, List<string>> contactGroups)
        {
            var modelUser = new Model.User
                                {
                                    Email = Email,
                                    FirstName = FirstName,
                                    LastName = LastName,
                                    Contacts = GetContacts(contacts, contactGroups)
                                };

            return modelUser;
        }

        private static List<Model.Contact> GetContacts(IEnumerable<Contact> contacts,
                                                       Dictionary<long, List<string>> contactGroups)
        {
            return contacts.Select(c => new Model.Contact
                                            {
                                                Email = c.Email,
                                                Name = c.Name,
                                                Groups = contactGroups.ContainsKey(c.Id) ? contactGroups[c.Id] : null
                                            }).ToList();
        }
    }
}