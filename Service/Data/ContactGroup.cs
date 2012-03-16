using ServiceStack.DesignPatterns.Model;

namespace Service.Data
{
    internal class ContactGroup : IHasId<long>
    {
        public long ContactId { get; set; }

        public string Name { get; set; }

        public long Id { get; set; }
    }
}