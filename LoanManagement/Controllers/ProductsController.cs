using LoanManagement.Entity;
using LoanManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Entity;

namespace LoanManagement.Controllers
{
    public class ProductsController : Controller
    {
        private AppDbContext _appDbContext { get; }
        public ProductsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var products = await _appDbContext.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductModel addProductModel)
        {
            var product = new Product()
            {
                //Id = Guid.NewGuid(),
                Code = addProductModel.Code,
                Name = addProductModel.Name,
                Description = addProductModel.Description,
                InterestRate = addProductModel.InterestRate,
                TermInDays = addProductModel.TermInDays,
            };
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var products = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (products != null)
            {
                var viewModel = new UpdateProductModel()
                {
                    Id = products.Id,
                    Code = products.Code,
                    Name = products.Name,
                    Description = products.Description,
                    InterestRate = products.InterestRate,
                    TermInDays = products.TermInDays,
                };
                return await Task.Run(() => View("Update", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateProductModel model)
        {
            var products = await _appDbContext.Products.FindAsync(model.Id);
            if (products != null)
            {
                products.Code = model.Code;
                products.Name = model.Name;
                products.Description = model.Description;
                products.InterestRate = model.InterestRate;
                products.TermInDays = model.TermInDays;

                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateProductModel model)
        {
            var products = await _appDbContext.Products.FindAsync(model.Id);
            if (products != null)
            {
                _appDbContext.Products.Remove(products);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
