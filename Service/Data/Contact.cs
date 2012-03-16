using ServiceStack.DesignPatterns.Model;

namespace Service.Data
{
    internal class Contact : IHasId<long>
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public long UserId { get; set; }

        public long Id { get; set; }
    }
}