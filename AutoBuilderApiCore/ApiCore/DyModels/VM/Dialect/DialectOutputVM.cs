using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Dialect  property for VM Output.
    /// </summary>
    public class DialectOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        //
        public string? Name { get; set; }
        //
        public string? Description { get; set; }
        ///
        public String? LanguageId { get; set; }
        public LanguageOutputVM? Language { get; set; }
    }
}