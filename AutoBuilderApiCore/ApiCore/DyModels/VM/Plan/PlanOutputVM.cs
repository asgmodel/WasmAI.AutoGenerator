using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Plan  property for VM Output.
    /// </summary>
    public class PlanOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? ProductId { get; set; }
        //
        public string? ProductName { get; set; }
        //
        public string? Description { get; set; }
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
        public List<SubscriptionOutputVM>? Subscriptions { get; set; }
        //
        public List<PlanServices>? PlanServices { get; set; }
        //
        public List<PlanFeatureOutputVM>? PlanFeatures { get; set; }
    }
}