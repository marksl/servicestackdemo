using System.Runtime.Serialization;
using ServiceStack.DesignPatterns.Model;

namespace Service.Data.Model
{
    [DataContract(Namespace = Configuration.DefaultNamespace)]
    public class ContactGroup : IHasId<int>
    {
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string Name { get; set; }

        #region IHasId<int> Members

        [DataMember]
        public int Id { get; set; }

        #endregion
    }
}