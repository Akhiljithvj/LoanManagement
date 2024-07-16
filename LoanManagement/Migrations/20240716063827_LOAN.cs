using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanManagement.Migrations
{
    /// <inheritdoc />
    public partial class LOAN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanGenerations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanAmount = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    DisbursementAmount = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    NumberOfInstalments = table.Column<int>(type: "int", nullable: false),
                    PrincipalAmount = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    InterestAmount = table.Column<decimal>(type: "decimal(16,3)", nullable: false),
                    CustomerOnboardingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanGenerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanGenerations_CustomerOnboardings_CustomerOnboardingId",
                        column: x => x.CustomerOnboardingId,
                        principalTable: "CustomerOnboardings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanGenerations_CustomerOnboardingId",
                table: "LoanGenerations",
                column: "CustomerOnboardingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanGenerations");
        }
    }
}
