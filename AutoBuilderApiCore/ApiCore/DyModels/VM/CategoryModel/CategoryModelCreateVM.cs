using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// CategoryModel  property for VM Create.
    /// </summary>
    public class CategoryModelCreateVM : ITVM
    {
        //
        public TranslationData? Name { get; set; }
        //
        public TranslationData? Description { get; set; }
    }
}