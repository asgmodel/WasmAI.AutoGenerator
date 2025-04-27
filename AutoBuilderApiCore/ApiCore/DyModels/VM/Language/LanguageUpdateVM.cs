using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Language  property for VM Update.
    /// </summary>
    public class LanguageUpdateVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public LanguageCreateVM? Body { get; set; }
    }
}