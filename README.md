# 🧩 Personel Takip Uygulaması (ASP.NET Core MVC + EF Core + MSSQL)

Bu proje, ASP.NET Core MVC kullanılarak geliştirilmiş **katmansız mimari** yapısında bir **personel takip sistemi** örneğidir. Uygulamada temel olarak **CRUD işlemleri**, **DTO kullanımı**, **MSSQL bağlantısı**, **veri doğrulama (validation)** ve **bootstrap tabanlı UI düzenlemeleri** yapılmıştır.

---

## 📌 Teknolojiler

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- MSSQL Server
- Bootstrap 4
- AutoMapper (isteğe bağlı)
- Visual Studio 2022

---

## 🧱 Uygulama Katmanları

Proje **katmansız** olarak yapılandırılmıştır ancak **DTO mantığı** aktif şekilde kullanılmıştır.

```bash
/Models
    /Entities         -> Personel.cs, Departman.cs
    /DTOs             -> PersonelDto.cs, DepartmanDto.cs
AppDbContext.cs       -> DbContext konfigürasyonu
/Controllers          -> PersonelController.cs, DepartmanController.cs
/Views                -> Razor view dosyaları

