using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Setting  property for VM Output.
    /// </summary>
    public class SettingOutputVM : ITVM
    {
        ///
        public String? Name { get; set; }
        ///
        public String? Value { get; set; }
    }
}