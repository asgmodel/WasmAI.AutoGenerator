using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Service  property for VM Update.
    /// </summary>
    public class ServiceUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public ServiceCreateVM? Body { get; set; }
    }
}