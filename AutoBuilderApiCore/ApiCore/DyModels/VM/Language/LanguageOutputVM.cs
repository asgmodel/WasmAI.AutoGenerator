using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// Language  property for VM Output.
    /// </summary>
    public class LanguageOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? Name { get; set; }
        ///
        public String? Code { get; set; }
    }
}