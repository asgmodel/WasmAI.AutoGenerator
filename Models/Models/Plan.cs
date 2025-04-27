using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{

    public class Plan : ITModel
    {
        [Key]
        public string Id { get; set; } = $"plan_{Guid.NewGuid():N}";
        public string ProductId { get; set; } = "price_1Pst3IKMQ7LabgRTZV9VgPex";
        [ToTranslation]
        public required string ProductName { get; set; }
        [ToTranslation]
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
        public string BillingPeriod { get; set; } = "monthly";       // daily , weekly , monthly ,.. 
        public required double Amount { get; set; }
        public bool Active { get; set; } = true;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<PlanServices> PlanServices { get; set; } = new List<PlanServices>();

        public ICollection<PlanFeature> PlanFeatures { get; set; } = new List<PlanFeature>();

    }
}
