using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{
   

    public class Space : ITModel
    {
        [Key]
        public string Id { get; set; } = $"space_{Guid.NewGuid():N}";

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? Ram { get; set; }  // RAM in MB

        public int? CpuCores { get; set; }  // Number of CPU cores

        public float? DiskSpace { get; set; }  // Disk space in GB

        public bool? IsGpu { get; set; }  // GPU model or details
        public bool? IsGlobal { get; set; }  // GPU model or details

        public float? Bandwidth { get; set; }  // Bandwidth in Mbps

        public required string Token { get; set; }

        public required string SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public ICollection<Request> Requests { get; set; } = [];


      


    }
}
