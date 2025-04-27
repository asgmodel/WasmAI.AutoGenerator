using AutoGenerator;

namespace LAHJAAPI.Models
{
    public class UserService : ITModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public required string ServiceId { get; set; }
        public Service? Service { get; set; }
    }
}
