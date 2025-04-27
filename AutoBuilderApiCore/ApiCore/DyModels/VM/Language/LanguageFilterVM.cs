using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Language  property for VM Filter.
    /// </summary>
    public class LanguageFilterVM : ITVM
    {
        ///
        public string? Id { get; set; }
        ///
        public string? Lg { get; set; }
    }
}