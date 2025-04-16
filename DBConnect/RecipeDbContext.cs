using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Npgsql;

namespace TechRecipeAX
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<BaseParameter> BaseParameters { get; set; }
        public DbSet<BaseRecipeStage> BaseRecipeStages { get; set; }
        public DbSet<RecipeParameter> RecipeParameters { get; set; }
        public DbSet<RecipeStage> RecipeStages { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public RecipeDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<RecipeDbContext>());            
            Database.Initialize(true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //Убирает добавление буквы s в конец названия таблицы
            base.OnModelCreating(modelBuilder);
        }

    }
}
