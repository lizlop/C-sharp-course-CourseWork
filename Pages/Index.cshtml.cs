using System;
using System.Collections.Generic;
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
        public Models.Encoder Encoder { get; set; }
        [BindProperty]
        public string Result { get; set; }
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                var mode = Request.Form["mode"];
                Result = Encoder.Vigener(mode == "0" ? Models.Encoder.Mode.ENCRYPT : Models.Encoder.Mode.DECRYPT, false);
            }
        }
        public void OnGet()
        {

        }
    }
}
