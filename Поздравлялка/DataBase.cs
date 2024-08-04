using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulations
{
	internal class DataBase:DbContext
	{
		public DbSet<Birthday> birthdays { get; set; } = null!;
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=DESKTOP-AJJ8D56;DataBase=BirthdayBD;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Birthday>().ToTable(t => t.HasCheckConstraint("ValidDate", "[date] BETWEEN '1900-01-01' AND '2025-01-01'"));
		}
	}
}
