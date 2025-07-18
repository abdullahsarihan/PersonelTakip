using Microsoft.AspNetCore.Mvc;
using PersonelTakipMVC.Models;
using PersonelTakipMVC.Models.DTOs;
using PersonelTakipMVC.Models.Entities;

namespace PersonelTakipMVC.Controllers
{
    public class DepartmanController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmanController(AppDbContext context)
        {
            _context = context;
        }
        //GET: Departman
        public IActionResult Index()
        {
            var departmanlar = _context.Departmanlar
                .Select(d => new DepartmanDto
                {
                    Id = d.Id,
                    DepartmanAdi = d.DepartmanAdi
                }).ToList();

            return View(departmanlar);
        }
        //GET: Departman/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Departman/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmanDto dto)
        {
            if (ModelState.IsValid)
            {
                var departman = new Departman
                {
                    DepartmanAdi = dto.DepartmanAdi,
                };

                _context.Departmanlar.Add(departman);
                _context.SaveChanges();
                TempData["Success"] = "Yeni departman başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }
        // GET: Departman/Edit
        public IActionResult Edit(int id)
        {
            var departman = _context.Departmanlar.Find(id);
            if (departman == null) return NotFound();

            var dto = new DepartmanDto
            {
                Id = departman.Id,
                DepartmanAdi = departman.DepartmanAdi
            };
            return View(dto);  
        }



        //POST: Departman/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DepartmanDto dto) 
        {
            if (id != dto.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var departman = _context.Departmanlar.Find(id);
                if (departman == null) return NotFound();

                departman.DepartmanAdi = dto.DepartmanAdi;
                _context.SaveChanges();
                TempData["Success"] = "Departman başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }
        //GET: Departman/Delete
        public IActionResult Delete(int id)
        {
            var departman = _context.Departmanlar.Find(id);
            if(departman == null) return NotFound();

            var dto = new DepartmanDto
            {
                Id = departman.Id,
                DepartmanAdi = departman.DepartmanAdi
            };
            return View(dto);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var departman = _context.Departmanlar.Find(id);
            if (departman == null) return NotFound();

            _context.Departmanlar.Remove(departman);
            _context.SaveChanges();
            TempData["Success"] = "Departman başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
