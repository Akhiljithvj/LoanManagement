using LoanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Entity;

namespace LoanManagement.Controllers
{
    public class CustomerOnboardingsController : Controller
    {
        private AppDbContext _appDbContext { get; }
        public CustomerOnboardingsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var customerOnboardings = await _appDbContext.CustomerOnboardings.ToListAsync();
            return View(customerOnboardings);
        }

        [HttpGet]
        public IActionResult Add()
        {
            PopulateProductsDropDownList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerOnboardingModel addCustomerOnboardingModel)
        {
            var customerOnboarding = new Entity.CustomerOnboarding()
            {
                Code = addCustomerOnboardingModel.Code,
                Name = addCustomerOnboardingModel.Name,
                Address = addCustomerOnboardingModel.Address,
                City = addCustomerOnboardingModel.City,
                State = addCustomerOnboardingModel.State,
                ZipCode = addCustomerOnboardingModel.ZipCode,
                Aadhaar = addCustomerOnboardingModel.Aadhaar,
                ProductId = addCustomerOnboardingModel.ProductId,
            };
            await _appDbContext.CustomerOnboardings.AddAsync(customerOnboarding);
            await _appDbContext.SaveChangesAsync();
            PopulateProductsDropDownList(addCustomerOnboardingModel.ProductId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var customerOnboardings = await _appDbContext.CustomerOnboardings.FirstOrDefaultAsync(x => x.Id == id);
            if (customerOnboardings != null)
            {
                var viewModel = new UpdateCustomerOnboardingModel()
                {
                    Id = customerOnboardings.Id,
                    Code = customerOnboardings.Code,
                    Name = customerOnboardings.Name,
                    Address = customerOnboardings.Address,
                    City = customerOnboardings.City,
                    State = customerOnboardings.State,
                    ZipCode = customerOnboardings.ZipCode,
                    Aadhaar = customerOnboardings.Aadhaar,
                    ProductId = customerOnboardings.ProductId,
                };
                PopulateProductsDropDownList(customerOnboardings.ProductId);
                return await Task.Run(() => View("Update", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateCustomerOnboardingModel model)
        {
            var customerOnboardings = await _appDbContext.CustomerOnboardings.FindAsync(model.Id);
            if (customerOnboardings != null)
            {
                customerOnboardings.Code = model.Code;
                customerOnboardings.Name = model.Name;
                customerOnboardings.Address = model.Address;
                customerOnboardings.City = model.City;
                customerOnboardings.State = model.State;
                customerOnboardings.ZipCode = model.ZipCode;
                customerOnboardings.Aadhaar = model.Aadhaar;
                customerOnboardings.ProductId = model.ProductId;
                PopulateProductsDropDownList(model.ProductId);

                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCustomerOnboardingModel model)
        {
            var customerOnboardings = await _appDbContext.CustomerOnboardings.FindAsync(model.Id);
            if (customerOnboardings != null)
            {
                _appDbContext.CustomerOnboardings.Remove(customerOnboardings);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        private void PopulateProductsDropDownList(object selectedProduct = null)
        {
            var productsQuery = from p in _appDbContext.Products
                                orderby p.Name
                                select p;

            ViewBag.Products = new SelectList(productsQuery.AsNoTracking(), "Id", "Name", selectedProduct);
        }
    }
}
