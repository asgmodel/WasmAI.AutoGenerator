using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Plan  property for VM Create.
    /// </summary>
    public class PlanCreateVM : ITVM
    {
        ///
        public String? ProductId { get; set; }
        //
        public TranslationData? ProductName { get; set; }
        //
        public TranslationData? Description { get; set; }
        //
        public List<String>? Images { get; set; }
        ///
        public String? BillingPeriod { get; set; }
        ///
        public Double Amount { get; set; }
        ///
        public Boolean Active { get; set; }
        ///
        public DateTime UpdatedAt { get; set; }
        ///
        public DateTime CreatedAt { get; set; }
        //
        public List<SubscriptionCreateVM>? Subscriptions { get; set; }
        //
        public List<PlanServices>? PlanServices { get; set; }
        //
        public List<PlanFeatureCreateVM>? PlanFeatures { get; set; }
    }
}