# BlueMediCore API

BlueMediCore, .NET 9 ve Entity Framework Core kullanılarak geliştirilmiş
bir " Hastane Otomasyon Sistemi Backend API " projesidir.

Proje; doktor, hasta, randevu ve kullanıcı yönetimini kapsayan,
JWT tabanlı kimlik doğrulama içeren modern bir Web API mimarisi sunar.

---

## Kullanılan Teknolojiler

- .NET 9 (ASP.NET Core Web API)
- Entity Framework Core (Code First)
- MS SQL Server
- JWT Authentication & Authorization
- Swagger (OpenAPI)
- Dependency Injection
- RESTful API yaklaşımı

---

## Mimari Yapı

Proje " Katmanlı Mimari " prensibine göre tasarlanmıştır:

Controllers
↓
Services (Business Logic)
↓
Data (DbContext)
↓
Entities (Database Models)

## Katmanların Görevleri

- Entities 
  Veritabanı tablolarını temsil eder.

- DTOs
  API’nin client ile haberleştiği veri modelleridir.
  Hassas alanların dışarı açılmasını engeller.

- Services
  İş kurallarının ve veritabanı işlemlerinin yapıldığı katmandır.

- Controllers 
  HTTP isteklerini karşılar, Service katmanını çağırır.

---
## Kimlik Doğrulama (JWT)

- JWT tabanlı authentication kullanılır
- Swagger üzerinden `Authorize` butonu ile token girilebilir
- Rol bazlı yetkilendirme desteklenir

---

## API Response Yapısı

Tüm endpoint’ler standart bir response yapısı döner:

```json
{
  "data": {},
  "success": true,
  "message": "İşlem başarılı."
}
```
Örnek – Doktor Listesi
```json
{
  "data": [
    {
      "id": 1,
      "name": "Dr. Ahmet Yılmaz",
      "branch": "Kardiyoloji"
    }
  ],
  "success": true,
  "message": "Doktorlar listelendi."
}

```
⚙️ Kurulum ve Çalıştırma
1️- Projeyi Klonla:
git clone https://github.com/aysenurtkyn/BlueMediCore.git
cd BlueMediCore

2️- Veritabanı Bağlantısını Ayarla:

appsettings.json içinde:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=BlueMediCoreDb;Trusted_Connection=True;"
}

3️- Migration ve DB Oluştur:
dotnet ef database update

4️- Uygulamayı Çalıştır:
dotnet run

Entities (Database Models)

