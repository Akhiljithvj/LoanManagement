using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagement.Models
{
    public class AddLoanGenerationModel
    {
        public decimal LoanAmount { get; set; }

        public decimal DisbursementAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int NumberOfInstalments { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal InterestAmount { get; set; }

        public int CustomerOnboardingId { get; set; }
    }
}
