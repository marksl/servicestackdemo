using System.Runtime.Serialization;
using ServiceStack.DesignPatterns.Model;

namespace Service.Data.Model
{
    [DataContract(Namespace = Configuration.DefaultNamespace)]
    public class User : IHasId<int>
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        #region IHasId<int> Members

        [DataMember]
        public int Id { get; set; }

        #endregion
    }
}