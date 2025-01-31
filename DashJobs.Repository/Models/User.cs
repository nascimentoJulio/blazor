using Dashclass.Constants;

namespace Dashclass.Model
{
    public class User
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public DateTime CreatedAt { get; set; }

        public Roles Roles { get; init; }
    }
}
