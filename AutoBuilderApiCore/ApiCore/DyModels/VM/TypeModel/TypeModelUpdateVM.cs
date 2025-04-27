using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// TypeModel  property for VM Update.
    /// </summary>
    public class TypeModelUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public TypeModelCreateVM? Body { get; set; }
    }
}