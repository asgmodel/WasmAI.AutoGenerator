using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Subscription  property for VM Output.
    /// </summary>
    public class SubscriptionOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? CustomerId { get; set; }
        ///
        public DateTime StartDate { get; set; }
        ///
        public DateTime CurrentPeriodStart { get; set; }
        ///
        public DateTime CurrentPeriodEnd { get; set; }
        ///
        public String? Status { get; set; }
        ///
        public Boolean CancelAtPeriodEnd { get; set; }
        ///
        public Int32 NumberRequests { get; set; }
        ///
        public Int32 AllowedRequests { get; set; }
        ///
        public Int32 AllowedSpaces { get; set; }
        ///
        public Nullable<DateTime> CancelAt { get; set; }
        ///
        public Nullable<DateTime> CanceledAt { get; set; }
        ///
        public String? PlanId { get; set; }
        public PlanOutputVM? Plan { get; set; }
        ///
        public String? UserId { get; set; }
        public ApplicationUserOutputVM? User { get; set; }
        //
        public List<RequestOutputVM>? Requests { get; set; }
        //
        public List<SpaceOutputVM>? Spaces { get; set; }
    }
}