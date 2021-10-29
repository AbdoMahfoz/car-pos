using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;
using Models.DataModels.ItemModels;
using Models.Helpers;
using Newtonsoft.Json;

namespace Models
{
    public class ApplicationDbContext : DbContext
    {
        public static string LocalDatabaseName { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ActionRole> ActionRoles { get; set; }
        public DbSet<ActionPermission> ActionPermissions { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCarModel> CarModels { get; set; }
        public DbSet<ItemCarModelIcon> CarModelIcons { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ItemPicture> ItemPictures { get; set; }
        public DbSet<Receit> Receits { get; set; }
        public DbSet<ReceitItem> ReceitItems { get; set; }


        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            Configure(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(u => u.Category)
                .WithMany(u => u.Items)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static AppSettings loadAppSettings()
        {
            string[] Paths = { Path.Combine("..", "WebAPI", "appsettings.json"), "appsettings.json" };
            foreach (var path in Paths)
            {
                try
                {
                    return JsonConvert.DeserializeAnonymousType(
                        File.ReadAllText(path),
                        new { AppSettings = new AppSettings() })!.AppSettings;
                }
                catch (Exception ex)
                {
                    if (ex is DirectoryNotFoundException or FileNotFoundException or UnauthorizedAccessException)
                    {
                        continue;
                    }

                    throw;
                }
            }

            return null;
        }

        public static void Configure(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            var appSettings = loadAppSettings();
            if (appSettings.UseSQLServer)
            {
                options.UseSqlServer(appSettings.ConnectionString);
            }
            else
            {
                string url = Environment.GetEnvironmentVariable("DATABASE_URL");
                if (string.IsNullOrWhiteSpace(url))
                {
                    if (string.IsNullOrWhiteSpace(LocalDatabaseName))
                        LocalDatabaseName = appSettings.LocalDatabaseName;
                    options.UseNpgsql(
                        $"Host=localhost;Port=5432;Database={LocalDatabaseName};Username=user;Password=123");
                }
                else
                {
                    url = url[(url.IndexOf("//", StringComparison.Ordinal) + 2)..];
                    string userName = url[..url.IndexOf(':')];
                    url = url[(url.IndexOf(':') + 1)..];
                    string password = url[..url.IndexOf('@')];
                    url = url[(url.IndexOf('@') + 1)..];
                    string host = url[..url.IndexOf(':')];
                    url = url[(url.IndexOf(':') + 1)..];
                    string port = url[..url.IndexOf('/')];
                    string database = url[(url.IndexOf('/') + 1)..];
                    options.UseNpgsql(
                        $"Host={host};Port={port};Database={database};Username={userName};Password={password};SSLMode=Require;TrustServerCertificate=true");
                }
            }
        }
    }
}