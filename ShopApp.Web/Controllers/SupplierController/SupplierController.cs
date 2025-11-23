using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Suppliers;
using ShopApp.Domain.Models.Suppliers;

namespace ShopApp.Web.Controllers.SupplierController
{
    public class SupplierController : Controller
    {
        private readonly ISuppliersService _suppliersService;

        public SupplierController(ISuppliersService suppliersService)
        {
            _suppliersService = suppliersService;
        }
        // GET: SupplierController
        public async Task<IActionResult> Index()
        {
            var result = await _suppliersService.GetAllSupplierAsync();

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: SupplierController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var result = await _suppliersService.GetSupplierByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: SupplierController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuppliersCreateModel model)
        {
            try
            {
                model.creartion_user = 1;
                var result = await _suppliersService.CreateSupplierAsync(model);

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

        // GET: SupplierController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _suppliersService.GetSupplierByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SuppliersUpdateModel model)
        {
            try
            {
                model.modify_user = 1;
                var result = await _suppliersService.UpdateSupplier(model);

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

        // GET: SupplierController/Delete/5
        public async Task<ActionResult> Desactivate(int id)
        {
            var result = await _suppliersService.GetSupplierByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: SupplierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id, int delete_user)
        {
            try
            {
                delete_user = 1;
                var result = await _suppliersService.DeleteSupplierByIdAsync(id, delete_user);

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
