using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{
    public class Language : ITModel
    {
        [Key]
        public string Id { get; set; } = $"lang_{Guid.NewGuid():N}";

        [Required]
        public string? Name { get; set; } // مثل "Arabic", "English"

        [Required]
        public string? Code { get; set; } // مثل "ar", "en"
    }

}
