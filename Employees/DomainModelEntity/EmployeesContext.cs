using System.Data.Entity;
using Employees.DomainModel;

namespace Employees.DomainModelEntity
{
    public class EmployeesContext : DbContext
    {
        public EmployeesContext() : base("BaseOfEmployees") { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<CertificationsOfEmployee> CertificationsOfEmployees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Cities
            // устанавливаем длину строкового поля и хар-ку NOT NULL
            modelBuilder.Entity<City>().Property(c => c.Name).HasMaxLength(50);
            modelBuilder.Entity<City>().Property(c => c.Name).IsRequired();
            // устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Cities и Addresses и внешний ключ в таблице Addresses
            modelBuilder.Entity<City>().HasMany(c => c.Addresses)
                .WithRequired(a => a.City).HasForeignKey(a => a.CityFk);

            // Streets
            // устанавливаем длину строкового поля и хар-ку NOT NULL
            modelBuilder.Entity<Street>().Property(s => s.Name).HasMaxLength(50);
            modelBuilder.Entity<Street>().Property(s => s.Name).IsRequired();
            // устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Streets и Addresses и внешний ключ в таблице Addresses
            modelBuilder.Entity<Street>().HasMany(c => c.Addresses)
                .WithRequired(a => a.Street).HasForeignKey(a => a.StreetFk);

            // Positions
            // устанавливаем длину строкового поля и хар-ку NOT NULL
            modelBuilder.Entity<Position>().Property(p => p.Name).HasMaxLength(50);
            modelBuilder.Entity<Position>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Position>().Property(p => p.Salary).IsRequired();
            // устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Positions и Employees и внешний ключ в таблице Employees
            modelBuilder.Entity<Position>().HasMany(p => p.Employees)
                .WithRequired(e => e.Position).HasForeignKey(e => e.PositionFk);

            // Addresses
            // устанавливаем длину строкового поля и хар-ку NOT NULL
            modelBuilder.Entity<Address>().Property(a => a.CityFk).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.StreetFk).IsRequired();
            modelBuilder.Entity<Address>().Property(a => a.House).HasMaxLength(20);
            modelBuilder.Entity<Address>().Property(a => a.House).IsRequired();
            // устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Addresses и Employees и внешний ключ в таблице Employees
            modelBuilder.Entity<Address>().HasMany(a => a.Employees)
                .WithRequired(e => e.Address).HasForeignKey(e => e.AddressFk);

            // Certifications
            // устанавливаем длину строкового поля и хар-ку NOT NULL
            modelBuilder.Entity<Certification>().Property(c => c.Name).HasMaxLength(50);
            modelBuilder.Entity<Certification>().Property(c => c.Name).IsRequired();
            // устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Certifications и CertificationsOfEmployees и внешний ключ в таблице CertificationsOfEmployees
            modelBuilder.Entity<Certification>().HasMany(c => c.CertificationsOfEmployees)
                .WithRequired(coe => coe.Certification).HasForeignKey(coe => coe.CertificationFk);

            // Employees
            // устанавливаем длину строкового поля и хар-ку NOT NULL
            modelBuilder.Entity<Employee>().Property(e => e.Surname).HasMaxLength(20);
            modelBuilder.Entity<Employee>().Property(e => e.Surname).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(20);
            modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Patronymic).HasMaxLength(20);
            modelBuilder.Entity<Employee>().Property(e => e.Patronymic).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Sex).HasMaxLength(20);
            modelBuilder.Entity<Employee>().Property(e => e.Sex).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Phone).HasMaxLength(20);
            modelBuilder.Entity<Employee>().Property(e => e.Phone).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.DateOfBirth).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.AddressFk).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.PositionFk).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Photo).HasMaxLength(100);
            modelBuilder.Entity<Employee>().Property(e => e.Photo).IsRequired();
            // устанавливаем ссылочную целостность для связи "один-ко-многим"
            // между таблицами Employees и CertificationsOfEmployees и внешний ключ в таблице CertificationsOfEmployees
            modelBuilder.Entity<Employee>().HasMany(e => e.CertificationsOfEmployees)
                .WithRequired(coe => coe.Employee).HasForeignKey(coe => coe.EmployeeFk);

            // CertificationsOfEmployees
            // устанавливаем хар-ку NOT NULL
            modelBuilder.Entity<CertificationsOfEmployee>().Property(c => c.EmployeeFk).IsRequired();
            modelBuilder.Entity<CertificationsOfEmployee>().Property(c => c.CertificationFk).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}