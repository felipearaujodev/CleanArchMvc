using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        public ProductsController(IProductService productService, 
            ICategoryService categoryService, IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId = 
                new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product) 
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId =
                           new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
                return View(product);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null) return NotFound();
            var productDto = await _productService.GetByIdAsync(id);
            if(productDto == null) return NotFound();
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetByIdAsync(id); 
            if(productDto == null) return NotFound();
            return View(productDto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null) return NotFound();
            var productDto = await _productService.GetByIdAsync(id);
            if (productDto == null) return NotFound();
            var wwwroot = _environment.WebRootPath;
            var image = Path.Combine(wwwroot, "Images\\" + productDto.Image);
            var exists = System.IO.File.Exists(image);
            ViewBag.ImageExist = exists;
            return View(productDto);
        }
    }
}
