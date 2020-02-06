using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PDF_Creator.Models;

namespace PDF_Creator.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        } 

        [HttpPost]
        public FileResult Index(string editor1)
        { 
            // create a unique file name
            string fileName = Guid.NewGuid() + ".pdf";

            // convert HTML text to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(editor1);

            // generate PDF from the HTML
            MemoryStream stream = new MemoryStream(byteArray);
            HtmlLoadOptions options = new HtmlLoadOptions();
            Document pdfDocument = new Document(stream, options);

            // create memory stream for the PDF file
            Stream outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);

            // return generated PDF file
            return File(outputStream, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
        } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
