using AutoGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAHJAAPI.Models
{
    public class UserModelAi : ITModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public required string ModelAiId { get; set; }
        public ModelAi? ModelAi { get; set; }

    }
}
