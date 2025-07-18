using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonelTakipMVC.Models.DTOs
{
    public class PersonelDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "Departman seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir departman seçiniz.")]
        public int DepartmanId { get; set; }
        [ValidateNever]
        public string DepartmanAdi { get; set; } // Listeleme için
        [ValidateNever]
        public List<SelectListItem> Departmanlar { get; set; } // Dropdown için
    }
}
