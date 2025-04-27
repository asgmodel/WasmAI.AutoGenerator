using AutoGenerator;
using System.ComponentModel.DataAnnotations;

namespace LAHJAAPI.Models
{
    public class Payment : ITModel
    {
        [Key]
        public required string Id { get; set; }
        public required string CustomerId { get; set; }
        public required string InvoiceId { get; set; }
        public required string Status { get; set; }
        public required string Amount { get; set; }
        public required string Currency { get; set; }
        public required DateOnly Date { get; set; }

        public Invoice Invoice { get; set; }
    }
}
