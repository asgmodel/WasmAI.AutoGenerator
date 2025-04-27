using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ServiceMethod  property for VM Create.
    /// </summary>
    public class ServiceMethodCreateVM : ITVM
    {
        ///
        public String? Method { get; set; }
        ///
        public String? InputParameters { get; set; }
        ///
        public String? OutputParameters { get; set; }
        ///
        public String? ServiceId { get; set; }
        public ServiceCreateVM? Service { get; set; }
    }
}