using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Customers;
using ShopApp.Domain.Models.Customers;

namespace ShopApp.Web.Controllers.CustomerController
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        // GET: CCustomerController
        public async Task<ActionResult> Index()
        {
            var result = await _customerService.GetAllCustmersAsync();

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CCustomerController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _customerService.GetCustmersByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CCustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CCustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomersCreateModel model)
        {
            try
            {
                model.creation_user = 1;
                var result = await _customerService.CreateCustmersAsync(model);

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

        // GET: CCustomerController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _customerService.GetCustmersByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: CCustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomersUpdateModel model)
        {
            try
            {
                model.modify_user = 1;
                var result = await _customerService.UpdateCustmersAsync(model);

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

        // GET: CCustomerController/Delete/5
        public async Task<ActionResult> Desactivate(int id)
        {
            var result = await _customerService.GetCustmersByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: CCustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id, int delete_user)
        {
            try
            {
                delete_user = 1;
                var result = await _customerService.DeleteCustmersByIdAsync(id, delete_user);

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
