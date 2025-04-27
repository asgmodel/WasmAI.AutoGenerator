using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{
    public class Service : ITModel
    {
        [Key]
        public string Id { get; set; } = $"serv_{Guid.NewGuid():N}";
        public required string Name { get; set; }
        public required string AbsolutePath { get; set; }
        public required string Token { get; set; }

        public string? ModelAiId { get; set; }
        public ModelAi? ModelAi { get; set; }
        public ICollection<ServiceMethod> ServiceMethods { get; set; } = [];
        public ICollection<UserService> UserServices { get; set; } = [];
        public ICollection<Request> Requests { get; set; } = [];
        public ICollection<PlanServices> PlanServices { get; set; } = new List<PlanServices>();
    }


}
