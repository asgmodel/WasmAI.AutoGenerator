using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// CategoryTab  property for VM Create.
    /// </summary>
    public class CategoryTabCreateVM : ITVM
    {
        //
        public TranslationData? Name { get; set; }
        //
        public TranslationData? Description { get; set; }
        ///
        public Boolean Active { get; set; }
        ///
        public String? Image { get; set; }
        ///
        public String? UrlUsed { get; set; }
        ///
        public Int32 CountFalvet { get; set; }
        ///
        public Int32 Rateing { get; set; }
    }
}