using System.Runtime.Serialization;
using ServiceStack.DesignPatterns.Model;

namespace Service.Data.Model
{
    [DataContract(Namespace = Configuration.DefaultNamespace)]
    public class Contact : IHasId<int>
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int UserId { get; set; }

        #region IHasId<int> Members

        [DataMember]
        public int Id { get; set; }

        #endregion
    }
}