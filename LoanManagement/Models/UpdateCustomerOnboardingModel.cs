using LoanManagement.Entity;
using System.ComponentModel.DataAnnotations;

namespace LoanManagement.Models
{
    public class UpdateCustomerOnboardingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Required")]
        [MaxLength(10, ErrorMessage = "Max 10 Characters Allowed")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50, ErrorMessage = "Max 50 Characters Allowed")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Max 50 Characters Allowed")]
        public string? Address { get; set; }

        [MaxLength(50, ErrorMessage = "Max 50 Characters Allowed")]
        public string? City { get; set; }

        [MaxLength(50, ErrorMessage = "Max 50 Characters Allowed")]
        public string? State { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "Only 6 Characters Allowed")]
        public string? ZipCode { get; set; }

        [StringLength(12, MinimumLength = 12, ErrorMessage = "Only 12 Characters Allowed")]
        public string? Aadhaar { get; set; }

        public int ProductId { get; set; } 
        public Product Product { get; set; }
    }
}
