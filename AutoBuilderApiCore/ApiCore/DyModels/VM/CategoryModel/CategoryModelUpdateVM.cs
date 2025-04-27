using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// CategoryModel  property for VM Update.
    /// </summary>
    public class CategoryModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public CategoryModelCreateVM? Body { get; set; }
    }
}