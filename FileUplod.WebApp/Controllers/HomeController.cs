using FileUplod.WebApp.Models;
using FileUplod.WebApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileUplod.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileRepository _fileRepository;

        public HomeController(ILogger<HomeController> logger, IFileRepository fileRepository)
        {
            _logger = logger;
            _fileRepository = fileRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(FileUploadViewModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                ModelState.AddModelError("File", "Please select a file");
                return View(model);
            }
            using (var memoryStream = new MemoryStream())
            {
                await model.File.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                var fileName = model.File.FileName;
                await _fileRepository.UplodFile(fileBytes, fileName);
            }
            return RedirectToAction("UploadSuccess");
        }
        public IActionResult UploadSuccess()
        {
            return View();
        }
    }
    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
}