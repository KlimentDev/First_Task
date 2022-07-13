using First_Task.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace First_Task.Controllers
{
    public class FileController : Controller
    {
        readonly IFileUploadService _fileUploadService;
        readonly IFileRepository _fileRepository;
        readonly IFileLineRepository _fileLineRepository;

        public FileController(IFileUploadService streamFileUploadService, IFileRepository fileRepository, IFileLineRepository fileLineRepository)
        {
            _fileUploadService = streamFileUploadService;
            _fileRepository = fileRepository;
            _fileLineRepository = fileLineRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Files"] = FileControllerUtility.GetFilesFromDirectory();
            var lastFile = _fileRepository.GetLastFile();
            if (lastFile != null)
            {
                var fileLines = _fileLineRepository.GetRowsById(lastFile.Id);

                if (fileLines.Count > 0)
                {
                    var chart = FileControllerUtility.CreateChart(fileLines, lastFile.Name);
                    ViewData["chart"] = chart;
                }
            }

            return View();
        }

        [ActionName("Index")]
        [HttpPost]
        public async Task<IActionResult> SaveFileToPhysicalFolder()
        {
            var boundary = HeaderUtilities.RemoveQuotes(
                MediaTypeHeaderValue.Parse(Request.ContentType).Boundary
            ).Value;
            var reader = new MultipartReader(boundary, Request.Body);
            var section = await reader.ReadNextSectionAsync();
            try
            {
                if (await _fileUploadService.UploadFile(reader, section))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }

            return RedirectToAction("Index");
        }
    }
}
