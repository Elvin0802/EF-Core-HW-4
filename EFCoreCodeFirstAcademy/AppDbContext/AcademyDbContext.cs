using EFCoreCodeFirstAcademy.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstAcademy.AppDbContext;

public class AcademyDbContext : DbContext
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Faculty> Facultys { get; set; }
    public DbSet<Group> Groups { get; set; }

    public AcademyDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		/*
			modelBuilder.Entity<Faculty>(faculty => { });

			modelBuilder.Entity<Group>(group => 
			{
				group.Property(x => x.GroupId)
					 .IsRequired()
					 .HasColumnName("Id")
					 .ValueGeneratedOnAdd();

				group.HasIndex(x => x.GroupId)
					 .IsUnique();

				group.HasKey(x => x.GroupId);

				group.Property(x => x.Name)
					 .IsRequired()
					 .HasMaxLength(10);

				group.HasIndex(x => x.Name)
					 .IsUnique();

				group.ToTable(x => x
					 .HasCheckConstraint("CK_Group_RatingRange", "Rating >= 0 AND Rating <= 5"));

				group.ToTable(x => x
					 .HasCheckConstraint("CK_Group_YearRange", "Year >= 1 AND Year =< 5"));

			});

			modelBuilder.Entity<Department>(department => { });

			modelBuilder.Entity<Student>(student => { });

			modelBuilder.Entity<Teacher>(teacher => { });
		*/



		modelBuilder.Entity<Group>(entity =>
		{
			entity.HasKey(e => e.GroupId);
			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(10);
			entity.HasIndex(e => e.Name)
				.IsUnique();
			entity.Property(e => e.Rating)
				.IsRequired();
			entity.Property(e => e.Year)
				.IsRequired();
		});

		modelBuilder.Entity<Department>(entity =>
		{
			entity.HasKey(e => e.DepartmentId);
			entity.Property(e => e.Financing)
				.HasColumnType("money")
				.IsRequired()
				.HasDefaultValue(0);
			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(100);
			entity.HasIndex(e => e.Name)
				.IsUnique();
		});

		modelBuilder.Entity<Faculty>(entity =>
		{
			entity.HasKey(e => e.FacultyId);
			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(100);
			entity.HasIndex(e => e.Name)
				.IsUnique();
		});

		modelBuilder.Entity<Teacher>(entity =>
		{
			entity.HasKey(e => e.TeacherId);
			entity.Property(e => e.EmploymentDate)
				.IsRequired()
				.HasDefaultValueSql("GETDATE()")
				.HasColumnType("date");
			entity.Property(e => e.FirstName)
				.IsRequired();
			entity.Property(e => e.LastName)
				.IsRequired();
			entity.Property(e => e.Salary)
				.HasColumnType("money")
				.IsRequired();
			entity.Property(e => e.Premium)
				.HasColumnType("money")
				.IsRequired()
				.HasDefaultValue(0);

			entity.HasOne(d => d.Department)
				.WithMany(p => p.Teachers)
				.HasForeignKey(d => d.DepartmentId);
		});

		modelBuilder.Entity<Student>(entity =>
		{
			entity.HasKey(e => e.StudentId);
			entity.Property(e => e.FirstName)
				.IsRequired();
			entity.Property(e => e.LastName)
				.IsRequired();

			entity.HasOne(d => d.Group)
				.WithMany(p => p.Students)
				.HasForeignKey(d => d.GroupId);

			entity.HasOne(d => d.Faculty)
				.WithMany(p => p.Students)
				.HasForeignKey(d => d.FacultyId);
		});

		modelBuilder.Entity<Group>()
			.HasMany(g => g.Teachers)
			.WithMany(t => t.Groups)
			.UsingEntity(j => j.ToTable("GroupTeachers"));
	}


}


/*

public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public int Year { get; set; }

    public ICollection<Student> Students { get; set; }
    public ICollection<Teacher> Teachers { get; set; }
}

public class Department
{
    public int DepartmentId { get; set; }
    public decimal Financing { get; set; }
    public string Name { get; set; }

    public ICollection<Teacher> Teachers { get; set; }
}

public class Faculty
{
    public int FacultyId { get; set; }
    public string Name { get; set; }

    public ICollection<Student> Students { get; set; }
}

public class Teacher
{
    public int TeacherId { get; set; }
    public DateTime EmploymentDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    public decimal Premium { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }

    public ICollection<Group> Groups { get; set; }
}

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
}

public class AcademyContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(10);
            entity.HasIndex(e => e.Name)
                .IsUnique();
            entity.Property(e => e.Rating)
                .IsRequired();
            entity.Property(e => e.Year)
                .IsRequired();
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);
            entity.Property(e => e.Financing)
                .HasColumnType("money")
                .IsRequired()
                .HasDefaultValue(0);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasIndex(e => e.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasIndex(e => e.Name)
                .IsUnique();
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId);
            entity.Property(e => e.EmploymentDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .HasColumnType("date");
            entity.Property(e => e.FirstName)
                .IsRequired();
            entity.Property(e => e.LastName)
                .IsRequired();
            entity.Property(e => e.Salary)
                .HasColumnType("money")
                .IsRequired();
            entity.Property(e => e.Premium)
                .HasColumnType("money")
                .IsRequired()
                .HasDefaultValue(0);

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Teachers)
                .HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId);
            entity.Property(e => e.FirstName)
                .IsRequired();
            entity.Property(e => e.LastName)
                .IsRequired();

            entity.HasOne(d => d.Group)
                .WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId);

            entity.HasOne(d => d.Faculty)
                .WithMany(p => p.Students)
                .HasForeignKey(d => d.FacultyId);
        });

        modelBuilder.Entity<Group>()
            .HasMany(g => g.Teachers)
            .WithMany(t => t.Groups)
            .UsingEntity(j => j.ToTable("GroupTeachers"));
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AcademyContext>();
        optionsBuilder.UseSqlServer("YourConnectionStringHere");

        using (var context = new AcademyContext(optionsBuilder.Options))
        {
            // Verilənlər bazası migrasiyasını tətbiq etmək üçün
            context.Database.Migrate();
        }
    }
}



*/