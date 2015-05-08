namespace BitTech.Data.Migrations
{
    using BitTech.Data.DBInitializer;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BitTech.Data.Context.LearningContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

#if DEBUG
        protected override void Seed(BitTech.Data.Context.LearningContext context)
        {
            new LearningDataSeeder(context).Seed();

        }
#endif
    }
}
