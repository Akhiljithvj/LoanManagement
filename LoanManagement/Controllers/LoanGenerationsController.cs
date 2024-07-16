using LoanManagement.Entity;
using LoanManagement.Migrations;
using LoanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Entity;

namespace LoanManagement.Controllers
{
    public class LoanGenerationsController : Controller
    {
        private AppDbContext _appDbContext { get; }
        public LoanGenerationsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var loanGenerations = await _appDbContext.LoanGenerations.ToListAsync();
            return View(loanGenerations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            PopulateCustomerOnboardingsDropDownList();
            //GenerateLoan();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLoanGenerationModel addLoanGenerationModel)
        {
            var loanGeneration = new Entity.LoanGeneration()
            {
                LoanAmount = addLoanGenerationModel.LoanAmount,
                DisbursementAmount = addLoanGenerationModel.DisbursementAmount,
                InterestRate = addLoanGenerationModel.InterestRate,
                NumberOfInstalments = addLoanGenerationModel.NumberOfInstalments,
                PrincipalAmount = addLoanGenerationModel.PrincipalAmount,
                InterestAmount = addLoanGenerationModel.InterestAmount,
                CustomerOnboardingId = addLoanGenerationModel.CustomerOnboardingId,
            };
            await _appDbContext.LoanGenerations.AddAsync(loanGeneration);
            await _appDbContext.SaveChangesAsync();
            PopulateCustomerOnboardingsDropDownList(addLoanGenerationModel.CustomerOnboardingId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var loanGenerations = await _appDbContext.LoanGenerations.FirstOrDefaultAsync(x => x.Id == id);
            if (loanGenerations != null)
            {
                var viewModel = new UpdateLoanGenerationModel()
                {
                    Id = loanGenerations.Id,
                    LoanAmount = loanGenerations.LoanAmount,
                    DisbursementAmount = loanGenerations.DisbursementAmount,
                    InterestRate = loanGenerations.InterestRate,
                    NumberOfInstalments = loanGenerations.NumberOfInstalments,
                    PrincipalAmount = loanGenerations.PrincipalAmount,
                    InterestAmount = loanGenerations.InterestAmount,
                    CustomerOnboardingId = loanGenerations.CustomerOnboardingId,
                };
                PopulateCustomerOnboardingsDropDownList(loanGenerations.CustomerOnboardingId);
                return await Task.Run(() => View("Update", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateLoanGenerationModel model)
        {
            var loanGenerations = await _appDbContext.LoanGenerations.FindAsync(model.Id);
            if (loanGenerations != null)
            {
                loanGenerations.LoanAmount = model.LoanAmount;
                loanGenerations.DisbursementAmount = model.DisbursementAmount;
                loanGenerations.InterestRate = model.InterestRate;
                loanGenerations.NumberOfInstalments = model.NumberOfInstalments;
                loanGenerations.PrincipalAmount = model.PrincipalAmount;
                loanGenerations.InterestAmount = model.InterestAmount;
                loanGenerations.CustomerOnboardingId = model.CustomerOnboardingId;
                PopulateCustomerOnboardingsDropDownList(model.CustomerOnboardingId);

                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateLoanGenerationModel model)
        {
            var loanGenerations = await _appDbContext.LoanGenerations.FindAsync(model.Id);
            if (loanGenerations != null)
            {
                _appDbContext.LoanGenerations.Remove(loanGenerations);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        private void PopulateCustomerOnboardingsDropDownList(object selectedCustomerOnboarding = null)
        {
            var customerOnboardingsQuery = from p in _appDbContext.CustomerOnboardings
                                orderby p.Name
                                select p;

            ViewBag.CustomerOnboardings = new SelectList(customerOnboardingsQuery.AsNoTracking(), "Id", "Name", selectedCustomerOnboarding);
        }

        private void GenerateLoan()
        {
            var loan = new AddLoanGenerationModel
            {
                LoanAmount = 10000,
                DisbursementAmount = 9000,
                InterestRate = 1000,
                NumberOfInstalments = 10,
                PrincipalAmount = 900,
                InterestAmount = 100
            };
        }
    }
}
