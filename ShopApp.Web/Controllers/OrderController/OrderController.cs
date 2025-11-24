using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Order;
using ShopApp.Domain.Models.Order.OrderBaseModel;

namespace ShopApp.Web.Controllers.OrderController
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            var result = await _orderService.GetAllOrderAsync();

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: OrderController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);

            if (!result.IsSucces)
            { 
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderModel model)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(model);

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

        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrderModel model)
        {
            try
            {
                var result = await _orderService.UpdateOrder(model);

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

        // GET: OrderController/Delete/5
        public async Task<ActionResult> Desactivate(int id, int s)
        {
            var result = await _orderService.GetOrderByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id)
        {
            try
            {
                var result = await _orderService.DeleteOrderByIdAsync(id);

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
