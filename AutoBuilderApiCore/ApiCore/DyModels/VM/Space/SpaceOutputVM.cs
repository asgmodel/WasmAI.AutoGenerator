using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Space  property for VM Output.
    /// </summary>
    public class SpaceOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? Name { get; set; }
        ///
        public String? Description { get; set; }
        ///
        public Nullable<Int32> Ram { get; set; }
        ///
        public Nullable<Int32> CpuCores { get; set; }
        ///
        public Nullable<Single> DiskSpace { get; set; }
        ///
        public Nullable<Boolean> IsGpu { get; set; }
        ///
        public Nullable<Boolean> IsGlobal { get; set; }
        ///
        public Nullable<Single> Bandwidth { get; set; }
        ///
        public String? Token { get; set; }
        ///
        public String? SubscriptionId { get; set; }
        public SubscriptionOutputVM? Subscription { get; set; }
        //
        public List<RequestOutputVM>? Requests { get; set; }
    }
}