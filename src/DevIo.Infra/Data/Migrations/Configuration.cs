using DevIo.Infra.Data.Context;
using System.Data.Entity.Migrations;

namespace DevIo.Infra.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MeusProdutosDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MeusProdutosDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
