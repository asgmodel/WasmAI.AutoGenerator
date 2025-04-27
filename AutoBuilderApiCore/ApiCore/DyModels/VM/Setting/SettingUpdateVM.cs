using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Setting  property for VM Update.
    /// </summary>
    public class SettingUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public SettingCreateVM? Body { get; set; }
    }
}