using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Payment  property for VM Update.
    /// </summary>
    public class PaymentUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public PaymentCreateVM? Body { get; set; }
    }
}