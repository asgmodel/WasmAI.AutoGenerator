using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using System;

namespace ApiCore.DyModels.VMs
{
    /// <summary>
    /// AdvertisementTab  property for VM Output.
    /// </summary>
    public class AdvertisementTabOutputVM : ITVM
    {
        ///
        public String? Id { get; set; }
        ///
        public String? AdvertisementId { get; set; }
        //
        public string? Title { get; set; }
        //
        public string? Description { get; set; }
        ///
        public String? ImageAlt { get; set; }
        public AdvertisementOutputVM? Advertisement { get; set; }
    }
}