using AutoGenerator;
using AutoGenerator.Helper.Translation;
using LAHJAAPI.Models;
using AutoGenerator.Config;
using System;

namespace ApiCore.DyModels.Dto.Build.Responses
{
    public class InvoiceResponseBuildDto : ITBuildDto
    {
        /// <summary>
        /// Id property for DTO.
        /// </summary>
        public String? Id { get; set; }
        /// <summary>
        /// CustomerId property for DTO.
        /// </summary>
        public String? CustomerId { get; set; }
        /// <summary>
        /// Status property for DTO.
        /// </summary>
        public String? Status { get; set; }
        /// <summary>
        /// Url property for DTO.
        /// </summary>
        public String? Url { get; set; }
        /// <summary>
        /// InvoiceDate property for DTO.
        /// </summary>
        public Nullable<DateTime> InvoiceDate { get; set; }
    }
}