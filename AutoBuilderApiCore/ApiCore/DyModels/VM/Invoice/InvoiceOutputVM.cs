using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Invoice  property for VM Output.
    /// </summary>
    public class InvoiceOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? CustomerId { get; set; }
        ///
        public String? Status { get; set; }
        ///
        public String? Url { get; set; }
        ///
        public Nullable<DateTime> InvoiceDate { get; set; }
    }
}