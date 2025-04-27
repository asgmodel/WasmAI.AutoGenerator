using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// TypeModel  property for VM Create.
    /// </summary>
    public class TypeModelCreateVM : ITVM
    {
        //
        public TranslationData? Name { get; set; }
        //
        public TranslationData? Description { get; set; }
        ///
        public Boolean Active { get; set; }
        ///
        public String? Image { get; set; }
    }
}