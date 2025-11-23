using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.OrderDetails;
using ShopApp.Domain.Models.OrderDetails.OrderDetailsBaseModel;

namespace ShopApp.Web.Controllers.OrderDetailsController
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }
        // GET: OrderDetailsController
        public async Task<ActionResult> Index()
        {
            var result = await _orderDetailsService.GetAllOrderDetailsAsync();

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: OrderDetailsController/Details/5 
        public async Task<ActionResult> Details(int id)
        {
            var result = await _orderDetailsService.GetOrderDetailsByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: OrderDetailsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Create(OrderDetailsModel model)
        {
            try
            {
                var result = await _orderDetailsService.CreateOrderDetailsAsync(model);

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

        // GET: OrderDetailsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _orderDetailsService.GetOrderDetailsByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: OrderDetailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderDetailsModel model)
        {
            try
            {
                var result = await _orderDetailsService.UpdateOrderDetails(model);

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

        // GET: OrderDetailsController/Delete/5
        public async Task<ActionResult> Desactivate(int id, int s)
        {
            var result = await _orderDetailsService.GetOrderDetailsByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: OrderDetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id)
        {
            try
            {
                var result = await _orderDetailsService.DeleteOrderDetailsByIdAsync(id);

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
