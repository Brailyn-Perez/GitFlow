using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Shippers;
using ShopApp.Domain.Models.Shippers;

namespace ShopApp.Web.Controllers.ShipperController
{
    public class ShipperController : Controller
    {
        private readonly IShippersService _shippersService;

        public ShipperController(IShippersService shippersService)
        {
            _shippersService = shippersService;
        }
        // GET: ShippersController
        public async Task<ActionResult> Index()
        {
            var result = await _shippersService.GetAllShippersAsync();

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: ShippersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _shippersService.GetShippersByIdAsync(id); 

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: ShippersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShippersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ShippersCreateModel model)
        {
            try
            {
                model.creation_user = 1;
                var result = await _shippersService.CreateShippersAsync(model);

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

        // GET: ShippersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _shippersService.GetShippersByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: ShippersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ShippersUpdateModel model)
        {
            try
            {
                model.modify_user = 1;
                var result = await _shippersService.UpdateShippers(model);

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

        // GET: ShippersController/Delete/5
        public async Task<ActionResult> Desactivate(int id)
        {
            var result = await _shippersService.GetShippersByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: ShippersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id, int delete_user)
        {
            try
            {
                delete_user = 1;
                var result = await _shippersService.DeleteShippersByIdAsync(id, delete_user);

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
