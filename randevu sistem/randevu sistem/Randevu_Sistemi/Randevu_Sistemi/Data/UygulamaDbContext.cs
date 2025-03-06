using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Randevu_Sistemi.Models;

namespace Randevu_Sistemi.Data
{
	public class UygulamaDbContext : DbContext
	{
		public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

		public DbSet<HastalikTuru> HastalikTurleri { get; set; }
		public DbSet<Hasta>Hastalar {  get; set; }
		public DbSet<Hekim>Hekimler {  get; set; }
		public DbSet<Role> Roles { get; set; }
		
		public DbSet<Kullanici> Kullanicilar { get; set; }
		
		public DbSet<RolePage> RolePages { get; set; }

		public DbSet<UserLog> UserLogs { get; set; }
	}
}
