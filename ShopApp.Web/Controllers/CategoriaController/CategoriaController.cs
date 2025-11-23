using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interface.Categoria;
using ShopApp.Domain.Models.Categoria;

namespace ShopApp.Web.Controllers.CategoriaController
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: CategoriaController
        public async Task<ActionResult> Index()
        {
            var result = await _categoriaService.GetAllCategoriaAsync();

            if (!result.IsSucces) 
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CategoriaController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var result = await _categoriaService.GetCategoriaByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // GET: CategoriaController/Create
        public async Task<ActionResult> Create()
        {
                return View();
        }

        // POST: CategoriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoriaCreateModel model)
        {
            try
            {
                model.creation_user = 1;
                var result = await _categoriaService.CreateCategoriaAsync(model);

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

        // GET: CategoriaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _categoriaService.GetCategoriaByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: CategoriaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoriaUpdateModel model)
        {
            try
            {
                model.modify_user = 1;
                var result = await _categoriaService.UpdateCategoria(model);

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

        // GET: CategoriaController/Delete/5
        public async Task<ActionResult> Desactivate(int id)
        {
            var result = await _categoriaService.GetCategoriaByIdAsync(id);

            if (!result.IsSucces)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Desactivate(int id, int delete_user)
        {
            try
            {
                delete_user = 1;
                var result = await _categoriaService.DeleteCategoriaByIdAsync(id, delete_user);

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
