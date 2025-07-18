using System.ComponentModel.DataAnnotations;

namespace PersonelTakipMVC.Models.Entities
{
    public class Personel
    {
        public int Id { get; set; }
        [Required]
        public string Ad {  get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }
        public int DepartmanId { get; set; }
        public Departman Departman { get; set; }
    }
}
