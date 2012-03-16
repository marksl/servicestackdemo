using ServiceStack.ServiceInterface.ServiceModel;

namespace Model
{
    public class UserResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public User User { get; set; }
    }
}