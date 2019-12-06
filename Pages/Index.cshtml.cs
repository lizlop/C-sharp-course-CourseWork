using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                var mode = Request.Form["mode"];
                Result = Encoder.Vigener(mode == "0" ? Models.Encoder.Mode.ENCRYPT : Models.Encoder.Mode.DECRYPT);
                if (Encoder.File != null && Encoder.File.Length != 0)
                {
                    /*Response.Clear();
                    Response.Headers.Clear();
                    Response.Headers.Add("Content-Disposition", "attachment; filename=" + Encoder.File.FileName);
                    Response.Headers.Add("Content-Length", Encoder.File.Length.ToString());
                    Response.ContentType = "multipart/form-data";
                    await Response.SendFileAsync("wwwroot\\uploads\\document" + (Encoder.File.FileName.EndsWith(".docx") ? ".docx" : ".txt"));*/
                }
            }
        }
        public void OnGet()
        {

        }
    }
}
