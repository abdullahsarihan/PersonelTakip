using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PersonelTakipMVC.Models.Entities
{
    public class Departman
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string DepartmanAdi { get; set; }

        public ICollection<Personel> Personeller { get; set; }
    }
}
