namespace LoanManagement.Models
{
    public class UpdateLoanGenerationModel
    {
        public int Id { get; set; }

        public decimal LoanAmount { get; set; }

        public decimal DisbursementAmount { get; set; }

        public decimal InterestRate { get; set; }

        public int NumberOfInstalments { get; set; }

        public decimal PrincipalAmount { get; set; }

        public decimal InterestAmount { get; set; }

        public int CustomerOnboardingId { get; set; }
    }
}
