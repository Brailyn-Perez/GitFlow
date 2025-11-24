using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Products;
using ShopApp.Domain.Models.Products;

namespace ShopApp.Web.Controllers.ProductsController
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var result = await _productsService.GetAllProductsAsync();

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }
             
            return View(result.Data);
        }

        // GET: ProductsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _productsService.GetProductsByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductsCreateModel model)
        {
            try
            {
                model.creation_user = 1;
                var result = await _productsService.CreateProductsAsync(model);

                if (!result.IsSucces)
                {
                    ViewBag.Error = result.Message;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _productsService.GetProductsByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductsUpdateModel model)
        {
            try
            {
                model.modify_user = 1;
                var result = await _productsService.UpdateProducts(model);

                if (!result.IsSucces)
                {
                    ViewBag.Error = result.Message;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> Desactivate(int id)
        {
            var result = await _productsService.GetProductsByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id, int delete_user)
        {
            try
            {
                delete_user = 1;
                var result = await _productsService.DeleteProductsByIdAsync(id, delete_user);

                if (!result.IsSucces)
                {
                    ViewBag.Error = result.Message;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
