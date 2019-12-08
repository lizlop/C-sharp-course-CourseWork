using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CourseWork.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public bool SetAlphabet { get; set; }
        [BindProperty]
        public Models.Encoder Encoder { get; set; }
        [BindProperty]
        public string Result { get; set; }
        public IActionResult OnPostDownloadFile()
        {
            if (ModelState.IsValid)
            {
                var mode = Request.Form["mode"];
                if (Encoder.File != null && Encoder.File.Length != 0)
                {
                    Encoder.Text = null;
                    Result = Encoder.Vigener(mode == "0" ? Models.Encoder.Mode.ENCRYPT : Models.Encoder.Mode.DECRYPT);
                    string filePath = "uploads\\document" + (Encoder.File.FileName.EndsWith(".docx") ? ".docx" : ".txt");
                    string fileType = "application/octet-stream";
                    string fileName = Encoder.File.FileName;
                    Encoder.File = null;
                    return File(filePath, fileType, fileName);
                }
                Result = Encoder.Vigener(mode == "0" ? Models.Encoder.Mode.ENCRYPT : Models.Encoder.Mode.DECRYPT);
            }
            else { Result = null; }
            return null;
        }
        public void OnGet()
        {

        }
    }
}
