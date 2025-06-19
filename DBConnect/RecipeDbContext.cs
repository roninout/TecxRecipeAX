using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Npgsql;
using TecxRecipeAX;

namespace TechRecipeAX
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]
    public class RecipeDbContext : DbContext
    {
        public DbSet<BaseParameter> BaseParameters { get; set; }
        public DbSet<BaseRecipeStage> BaseRecipeStages { get; set; }
        public DbSet<RecipeParameter> RecipeParameters { get; set; }
        public DbSet<RecipeStage> RecipeStages { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipePage> RecipePages { get; set; }

        public RecipeDbContext(string connectionString)
            : base(new NpgsqlConnection(connectionString), contextOwnsConnection: true)
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

     //провайдер Npgsql сервер
    class NpgsqlConfiguration : DbConfiguration
    {
        public NpgsqlConfiguration()
        {
            SetProviderServices("Npgsql", NpgsqlServices.Instance);
            SetProviderFactory("Npgsql", NpgsqlFactory.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
        }
    }
}
