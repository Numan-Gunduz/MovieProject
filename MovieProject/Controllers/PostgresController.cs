using Microsoft.AspNetCore.Mvc;
using MovieProject.Services;

namespace MovieProject.Controllers
{
    public class PostgresController : Controller
    {
        private readonly PostgresFunctionService _functionService;

        public PostgresController(PostgresFunctionService functionService)
        {
            _functionService = functionService;
        }

        public IActionResult TestFunction()
        {
            //var functionName = "example_function";

            //// Fonksiyon var mı kontrol et
            //if (!_functionService.FunctionExists(functionName))
            //{
            //    _functionService.CreateFunction();
            //    ViewBag.Message = "Fonksiyon oluşturuldu.";
            //}
            //else
            //{
            //    ViewBag.Message = "Fonksiyon zaten mevcut.";
            //}

            //// Fonksiyonu çağır
            //_functionService.CallFunction(functionName);

            return View();
        }
    }
}