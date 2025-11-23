using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Employees;
using ShopApp.Domain.Models.Employees;

namespace ShopApp.Web.Controllers.EmployeesController
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }
        // GET: EmployeesController
        public async Task<ActionResult> Index()
        {
            var result = await _employeesService.GetAllEmployeesAsync();

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: EmployeesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _employeesService.GetEmployeesByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: EmployeesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeesCreateModel model)
        {
            try
            {
                var result = await _employeesService.CreateEmployeesAsync(model);

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

        // GET: EmployeesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _employeesService.GetEmployeesByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: EmployeesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeesUpdateModel model)
        {
            try
            {
                model.modiffy_user = 1;
                var result = await _employeesService.UpdateEmployees(model);

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

        // GET: EmployeesController/Delete/5
        public async Task<ActionResult> Desactivate(int id)
        {
            var result = await _employeesService.GetEmployeesByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: EmployeesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id, int delete_user)
        {
            try
            {
                delete_user = 1;
                var result = await _employeesService.DeleteEmployeesByIdAsync(id, delete_user);

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
