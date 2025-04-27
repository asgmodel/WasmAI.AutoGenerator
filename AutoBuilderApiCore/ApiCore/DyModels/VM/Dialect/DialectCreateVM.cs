using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Dialect  property for VM Create.
    /// </summary>
    public class DialectCreateVM : ITVM
    {
        //
        public TranslationData? Name { get; set; }
        //
        public TranslationData? Description { get; set; }
        ///
        public String? LanguageId { get; set; }
        public LanguageCreateVM? Language { get; set; }
    }
}