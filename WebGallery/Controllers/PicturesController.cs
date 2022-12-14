using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebGallery.Data;
using WebGallery.Models;

namespace WebGallery.Controllers
{
    public class PicturesController : Controller
    {
        private readonly WebGalleryContext _context;
        Picture picture = new Picture();
        public PicturesController(WebGalleryContext context)
        {
            _context = context;
        }

        // GET: Pictures
        public async Task<IActionResult> Index()
        {

            ViewBag.Pictures = new SelectList(await _context.Picture.ToListAsync(), "Id", "FileName");
            return View(picture);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Picture pic)
        {
            if (pic.Id == 0) picture = new Picture() {  FileName = "*/", LoadDate = DateTime.Now };
            picture = (await _context.Picture.ToListAsync()).FirstOrDefault(p => p.Id == pic.Id);
            return await Index();
        }


        // GET: Pictures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        
        [HttpPost]
        public async Task<IActionResult> Create( IFormFile LoadDate)
        {
           
            using (MemoryStream MS = new MemoryStream())
            {
                LoadDate.CopyTo(MS);
                MS.Seek(0, SeekOrigin.Begin);
                await _context.AddAsync(new Picture { FileName = LoadDate.FileName, File = MS.ToArray(), LoadDate= DateTime.Now, FileSize =(int) LoadDate.Length });
                await _context.SaveChangesAsync();
            }  
            
              
                return RedirectToAction(nameof(Index));
          
           
        }

       

    }
}
