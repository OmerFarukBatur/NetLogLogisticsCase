# Task Distribution System

Bir yazılım birimindeki task'ların zorluk seviyesine göre adil ve otomatik biçimde developer'lara atanmasını sağlayan ASP.NET Core MVC web uygulamasıdır.

---

## 📋 Proje Hakkında

Yazılım biriminde açılan her task; kullanıcı tarafından oluşturulur, analist tarafından analiz edilir ve sistem tarafından belirlenen uygun developer'a otomatik olarak atanır. Atama algoritması, zorluk seviyesi bazında adil dağılımı ve ardışık atama kuralını gözetir.

---

## 🏗️ Mimari

Proje dört katmanlı bir yapıda geliştirilmiştir.

```
TaskDistribution/
├── NetLogLogisticsCase.Core/            # Entity'ler, Enum'lar, ISoftDeletable, Interface'ler, DTO'lar, Helper'lar
├── NetLogLogisticsCase.Data/  # Repository implementasyonu, AppDbContext
├── NetLogLogisticsCase.Business/        # Servis implementasyonları, Mapping, Validator'lar
└── NetLogLogisticsCase.Web(DistributionCaseStudy)/             # Controller'lar, View'lar, ViewComponent'lar
```

---

## 👥 Kullanıcı Rolleri

| Rol | Yetki |
|---|---|
| **Admin** | Kullanıcı oluşturma, güncelleme, silme ve personel durum yönetimi |
| **Kullanıcı (Opener)** | Task açma, kendi task'larını yönetme ve ilerlemeyi takip etme |
| **Analist** | Bekleyen task'ları analiz etme, zorluk/öncelik belirleme, developer'a yönlendirme |
| **Developer** | Atanan task'ları geliştirme, durum güncelleme ve tamamlama |

---

## 🔄 İş Akışı

```
Kullanıcı task açar (Stage: Open)
        ↓
Analist "Analiz Et" butonuna basar → TaskAnalysis kaydı oluşur (Stage: InAnalysis)
        ↓
Analist zorluk, öncelik ve gereksinim bilgilerini girer
        ↓
Analist "Geliştirmeye Al" butonuna basar → Sistem algoritması devreye girer
        ↓
Sistem uygun developer'ı seçer → TaskDevelopment kaydı oluşur (Stage: InDevelopment)
        ↓
Developer task'ı tamamlar (Stage: Completed) veya iptal eder (Stage: Rejected)
```

---

## ⚙️ Developer Atama Algoritması

Sistem, developer seçiminde iki kuralı birlikte uygular.

**Adil Dağılım:** Her zorluk seviyesi için en az görev almış developer öncelikli olarak seçilir.

**Ardışık Atama Kuralı:** Bir developer'ın son aldığı task ile aynı zorluk seviyesinde yeni görev alması engellenir. Tüm developer'lar bloke durumdaysa (edge case) kural geçici olarak kaldırılır ve en az görevli developer seçilir.

Algoritma akışı için `task_distribution_flowchart.jpg` dosyasına bakınız.

---

## 🛠️ Teknoloji Stack

| Kategori | Teknoloji |
|---|---|
| Framework | ASP.NET Core MVC (.NET 9.0) |
| ORM | Entity Framework Core 9.0 — Code First |
| Veritabanı | Microsoft SQL Server |
| Kimlik Doğrulama | Cookie Authentication |
| Şifreleme | BCrypt.Net-Next |
| Validasyon | FluentValidation 12 |
| Nesne Eşleme | AutoMapper 16 |
| Mimari | Katmanlı mimari + Repository Pattern |

---

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler

- .NET 9.0 SDK
- Microsoft SQL Server (LocalDB dahil)
- Visual Studio 2022 veya VS Code

### Adımlar

**1. Repoyu klonlayın**
```bash
git clone https://github.com/OmerFarukBatur/NetLogLogisticsCase.git
cd NetLogLogisticsCase
```

**2. Bağlantı dizesini ayarlayın**

`NetLogLogisticsCase.Web(DistributionCaseStudy)/appsettings.json` dosyasındaki `ConnectionStrings` bölümünü kendi SQL Server bağlantınıza göre güncelleyin.

```json
{
  "ConnectionStrings": {
    "SqlServer": "Data Source=BATUR;Initial Catalog=DistributionCaseStudyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }
}
```

**3. Migration'ları uygulayın**
```bash
cd NetLogLogisticsCase.Data
dotnet ef database update
```

**4. Uygulamayı başlatın**
```bash
dotnet run
```

Uygulama `https://localhost:5001` adresinde çalışmaya başlar.

---

## 🔑 Varsayılan Giriş Bilgileri

Uygulama ilk çalıştırıldığında seed data ile aşağıdaki veriler oluşturulur.

```c#

// Tüm kullanıcıların default şifresi =>  Pass1234!
const string defaultHash = "$2a$11$HnCznl6xKGRssPPf/FNqo.vT7GErG/fxi4qVavbdEGVyK7EZgWp2e";

modelBuilder.Entity<Role>().HasData(
    new Role { Id = 1, Name = "Admin", CreatedAt = now },
    new Role { Id = 2, Name = "Opener", CreatedAt = now },
    new Role { Id = 3, Name = "Analyst", CreatedAt = now },
    new Role { Id = 4, Name = "Developer", CreatedAt = now }
);

// Kullanıcılar
modelBuilder.Entity<User>().HasData(
    new User { Id = 1, RoleId = 1, Email = "admin@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 2, RoleId = 2, Email = "ahmet.yilmaz@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 3, RoleId = 3, Email = "ayse.kaya@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 4, RoleId = 4, Email = "mehmet.demir@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 5, RoleId = 4, Email = "fatma.celik@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 6, RoleId = 4, Email = "ali.sahin@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 7, RoleId = 4, Email = "zeynep.arslan@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 8, RoleId = 4, Email = "mustafa.koc@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 9, RoleId = 4, Email = "elif.erdogan@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 10, RoleId = 4, Email = "hasan.dogan@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
    new User { Id = 11, RoleId = 4, Email = "merve.yildiz@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now }
);

// Personel
modelBuilder.Entity<Personnel>().HasData(
    new Personnel { Id = 1, UserId = 1, FirstName = "Sistem", LastName = "Admin", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 2, UserId = 2, FirstName = "Ahmet", LastName = "Yılmaz", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 3, UserId = 3, FirstName = "Ayşe", LastName = "Kaya", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 4, UserId = 4, FirstName = "Mehmet", LastName = "Demir", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 5, UserId = 5, FirstName = "Fatma", LastName = "Çelik", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 6, UserId = 6, FirstName = "Ali", LastName = "Şahin", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 7, UserId = 7, FirstName = "Zeynep", LastName = "Arslan", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 8, UserId = 8, FirstName = "Mustafa", LastName = "Koç", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 9, UserId = 9, FirstName = "Elif", LastName = "Erdoğan", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 10, UserId = 10, FirstName = "Hasan", LastName = "Doğan", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
    new Personnel { Id = 11, UserId = 11, FirstName = "Merve", LastName = "Yıldız", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 }
);

---

## 📦 Kullanılan Paketler

```xml
<!-- Business -->
<PackageReference Include="AutoMapper" Version="16.1.1" />
<PackageReference Include="BCrypt.Net-Next" Version="4.1.0" />
<PackageReference Include="FluentValidation" Version="12.1.1" />
<PackageReference Include="FluentValidation.AspNetCore" Version="11.3.1" />
<PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="12.1.1" />

<!-- Data -->
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
```

---

## 📐 Tasarım Prensipleri

- **SOLID** prensipleri gözetilerek geliştirilmiştir.
- **Repository Pattern** ile veri erişim katmanı soyutlanmıştır.
- **Loosely Coupled** yapı; servisler interface'ler üzerinden birbirine bağlıdır.
- **Soft Delete** uygulanmıştır; kayıtlar fiziksel olarak silinmez.
- **Code First** yaklaşımı ile veritabanı şeması migration'lar üzerinden yönetilir.
- **ISoftDeletable** interface'i ile yalnızca silinebilir entity'ler soft delete destekler.

---

## 📁 Proje Yapısı

```
docs/
└── task_distribution_flowchart.jpg          # Developer atama algoritması akış diyagramı


NetLogLogisticsCase.Core/
├── DTOs/
│   ├── AuthDtos/
│   ├── AdminDtos/
│   ├── OpenerDtos/
│   ├── AnalystDtos/
│   └── DeveloperDtos/
├── Entities/
    ├── BaseEntity.cs
│   ├── ISoftDeletable.cs
│   ├── User.cs
│   ├── Personnel.cs
│   ├── Role.cs
│   ├── Task.cs
│   ├── TaskAnalysis.cs
│   ├── TaskDevelopment.cs
│   └── AssignmentHistory.cs
├── Enums/
    ├── UserRole.cs
    ├── PersonnelStatus.cs
    ├── TaskStage.cs
    ├── AnalysisStatus.cs
    ├── DevelopmentStatus.cs
    ├── DifficultyLevel.cs
    ├── TaskPriority.cs
    └── StageType.cs
├── Helpers/
    ├── PaginatedList.cs
    └── EnumHelper.cs
├── Interfaces/
│   ├── IRepositories/
│   └── IServices/

NetLogLogisticsCase.Data/
├── Context/
│   └── AppDbContext.cs
└── Repositories/
    └── Repository.cs
└── Migration    

NetLogLogisticsCase.Business/
├── Services/
├── Mapping/
└── Validators/

NetLogLogisticsCase.Web(DistributionCaseStudy)/
├── Controllers/
├── Views/
├── ViewComponents/
└── Program.cs
```

---