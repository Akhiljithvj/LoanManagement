using System.ComponentModel.DataAnnotations;

namespace LoanManagement.Models
{
    public class UpdateProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Required")]
        [MaxLength(10, ErrorMessage = "Max 10 Characters Allowed")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50, ErrorMessage = "Max 50 Characters Allowed")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Max 50 Characters Allowed")]
        public string? Description { get; set; }

        public decimal InterestRate { get; set; }
        public int? TermInDays { get; set; }
    }
}
