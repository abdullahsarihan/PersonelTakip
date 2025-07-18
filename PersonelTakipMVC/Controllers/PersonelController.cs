using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipMVC.Models;
using PersonelTakipMVC.Models.DTOs;
using PersonelTakipMVC.Models.Entities;

namespace PersonelTakipMVC.Controllers
{
    public class PersonelController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PersonelController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var personeller = _context.Personeller
                .Include(p => p.Departman)
                .Select(p => new PersonelDto
                {
                    Id = p.Id,
                    Ad = p.Ad,
                    Soyad = p.Soyad,
                    Email = p.Email,
                    DogumTarihi = p.DogumTarihi,
                    DepartmanAdi = p.Departman.DepartmanAdi,
                }).ToList();

            return View(personeller);
        }
        public IActionResult Create()
        {
            var dto = new PersonelDto
            {
                Departmanlar = _context.Departmanlar
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.DepartmanAdi
                    }).ToList()
            };
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PersonelDto dto)
        {
            if (dto.DogumTarihi > DateTime.Today.AddYears(-18))
                ModelState.AddModelError("DogumTarihi", "Personel en az 18 yaşında olmalıdır.");

            //if (ModelState.IsValid)
            //{
            //    var personel = new Personel
            //    {
            //        Ad = dto.Ad,
            //        Soyad = dto.Soyad,
            //        Email = dto.Email,
            //        DogumTarihi = dto.DogumTarihi,
            //        DepartmanId = dto.DepartmanId
            //    };

            //    _context.Personeller.Add(personel);
            //    _context.SaveChanges();
            //    TempData["Success"] = "Yeni personel başarıyla eklendi.";
            //    return RedirectToAction(nameof(Index));
            //}

            //AutoMapper ile
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Personel>(dto);
                _context.Personeller.Add(entity);
                _context.SaveChanges();
                TempData["Success"] = "Personel başarıyla eklendi";
                return RedirectToAction("Index");
            }

            dto.Departmanlar = _context.Departmanlar
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DepartmanAdi
                }).ToList();

            return View(dto);
        }

        public IActionResult Edit(int id)
        {
            var personel = _context.Personeller.Find(id);
            if (personel == null) return NotFound();

            var dto = new PersonelDto
            {
                Id = personel.Id,
                Ad = personel.Ad,
                Soyad = personel.Soyad,
                Email = personel.Email,
                DogumTarihi = personel.DogumTarihi,
                DepartmanId = personel.DepartmanId,
                Departmanlar = _context.Departmanlar
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.DepartmanAdi
                    }).ToList()
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PersonelDto dto)
        {
            if (id != dto.Id) return BadRequest();

            if (dto.DogumTarihi > DateTime.Today.AddYears(-18))
                ModelState.AddModelError("DogumTarihi", "Personel en az 18 yaşında olmalıdır.");

            if (ModelState.IsValid)
            {
                var personel = _context.Personeller.Find(id);
                if (personel == null) return NotFound();

                personel.Ad = dto.Ad;
                personel.Soyad = dto.Soyad;
                personel.Email = dto.Email;
                personel.DogumTarihi = dto.DogumTarihi;
                personel.DepartmanId = dto.DepartmanId;

                _context.SaveChanges();
                TempData["Success"] = "Personel başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }

            dto.Departmanlar = _context.Departmanlar
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DepartmanAdi
                }).ToList();

            return View(dto);
        }

        //GET: Personel/Delete
        public IActionResult Delete(int id)
        {
            var personel = _context.Personeller.Include(p => p.Departman).FirstOrDefault(p => p.Id == id);
            if (personel == null) return NotFound();

            var dto = new PersonelDto
            {
                Id = personel.Id,
                Ad = personel.Ad,
                Soyad = personel.Soyad,
                Email = personel.Email,
                DogumTarihi = personel.DogumTarihi,
                DepartmanAdi = personel.Departman.DepartmanAdi
            };
            return View(dto);
        }
        //POST Personel/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var personel = _context.Personeller.Find(id);
            //if(personel == null) return NotFound();

            if (personel == null)
            {
                TempData["Error"] = "Silinmek istenen personel bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            _context.Personeller.Remove(personel);
            _context.SaveChanges();
            TempData["Success"] = "Personel başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        //En Yasli Personel
        public IActionResult EnYasli()
        {
            //Manuel Mapping

            //var enYasli = _context.Personeller
            //    .FromSqlRaw("EXEC sp_EnYasliPersonelGetir")
            //    .Include(p => p.Departman)
            //    .FirstOrDefault();

            //if (enYasli == null) return NotFound();

            //var dto = new PersonelDto
            //{
            //    Id = enYasli.Id,
            //    Ad = enYasli.Ad,
            //    Soyad = enYasli.Soyad,
            //    Email = enYasli.Email,
            //    DogumTarihi = enYasli.DogumTarihi,
            //    DepartmanAdi = enYasli.Departman.DepartmanAdi
            //};

            var enYasli = _context.Personeller
            .FromSqlRaw("EXEC sp_EnYasliPersonelGetir")
            .AsEnumerable()
            .FirstOrDefault();

            var dto = _mapper.Map<PersonelDto>(enYasli);
            return View(dto);
        }

        //Bugun Dogan Personeller

        public IActionResult BugunDogan()
        {
            var bugunDoganlar = _context.Personeller
                .FromSqlRaw("EXEC sp_BugunDogmusPersoneller")
                .ToList();

            var dtoList = _mapper.Map<List<PersonelDto>>(bugunDoganlar);
            return View(dtoList); 
        }
    }
}
