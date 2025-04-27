using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// ServiceMethod  property for VM Output.
    /// </summary>
    public class ServiceMethodOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? Method { get; set; }
        ///
        public String? InputParameters { get; set; }
        ///
        public String? OutputParameters { get; set; }
        ///
        public String? ServiceId { get; set; }
        public ServiceOutputVM? Service { get; set; }
    }
}