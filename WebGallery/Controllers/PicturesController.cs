using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        Picture picture = new Picture() { FilePath = "/", FileName = "*/", LoadDate = DateTime.Now };
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
            if (pic.Id == 0) picture = new Picture() { FilePath = "/", FileName = "*/", LoadDate = DateTime.Now };
            picture = (await _context.Picture.ToListAsync()).FirstOrDefault(p => p.Id == pic.Id);
            return await Index();
        }


        // GET: Pictures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,FileSize,LoadDate,FilePath")] Picture picture)
        {

           
            picture.LoadDate = DateTime.Now;
            picture.FilePath = "C:\\Users\\Bavovna\\source\\repos\\WebGallery\\WebGallery\\images\\" + picture.FileName;
            picture.FileName = picture.FileName.Split('.')[0];
            if (ModelState.IsValid)
            {
                _context.Add(picture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(picture);
        }

       

    }
}
