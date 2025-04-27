using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{
    public class EventRequest : ITModel
    {
        [Key]
        public string Id { get; set; } = $"event_{Guid.NewGuid():N}";

        [Required]
        public string Status { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Details { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string RequestId { get; set; }
        public Request Request { get; set; }

    }
}
