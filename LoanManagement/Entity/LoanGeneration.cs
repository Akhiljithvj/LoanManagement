using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagement.Entity
{
    public class LoanGeneration
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal LoanAmount { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal DisbursementAmount { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal InterestRate { get; set; }

        public int NumberOfInstalments { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal PrincipalAmount { get; set; }

        [Column(TypeName = "decimal(16,3)")]
        public decimal InterestAmount { get; set; }

        public int CustomerOnboardingId { get; set; }
        public CustomerOnboarding CustomerOnboarding { get; set; }

    }
}
