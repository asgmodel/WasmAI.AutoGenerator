using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{


    public class ModelAi : ITModel
    {
        [Key]
        public string Id { get; set; } = $"mod_{Guid.NewGuid():N}";
        [ToTranslation]
        public required string Name { get; set; }

        public string? Token { get; set; }

        public string? AbsolutePath { get; set; }
        [ToTranslation]
        public string? Category { get; set; }
        public string? Language { get; set; }

        public bool? IsStandard { get; set; }

        public string? Gender { get; set; }

        [ToTranslation]
        public string? Dialect { get; set; }
        public string? Type { get; set; }

        public string? ModelGatewayId { get; set; }
        public ModelGateway ModelGateway { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<UserModelAi> UserModelAis { get; set; } = [];
    }
}
