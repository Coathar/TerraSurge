using Microsoft.EntityFrameworkCore;
using TerraSurgeShared.Loaders;
using TerraSurgeShared.Models;

namespace TerraSurgeShared
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(local);Database=TerraSurge;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set default value for Guids
            modelBuilder.Entity<Game>(g =>
            {
                g.ToTable("Game");

                g.Property(x => x.GameGuid).HasDefaultValueSql("NEWID()");
            });

            // Add conversion for enum
            modelBuilder.Entity<HeroAbility>(ha =>
            {
                ha.Property(p => p.AbilitySlot).HasConversion(x => (int)x, x => (HeroAbility.AbilitySlots)x);
            });

            modelBuilder.Entity<Hero>(h =>
            {
                h.Property(p => p.Role).HasConversion(x => (int)x, x => (Hero.Roles)x);
            });

            modelBuilder.Entity<HeroSwapActivity>(hsa =>
            {
                hsa.HasOne(h => h.FromHero).WithMany(x => x.FromHeroSwapActivities).HasForeignKey(x => x.FromHeroID).OnDelete(DeleteBehavior.Restrict);
                hsa.HasOne(h => h.ToHero).WithMany(x => x.ToHeroSwapActivities).HasForeignKey(x => x.ToHeroID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EliminationActivity>(ea =>
            {
                ea.HasOne(h => h.Player).WithMany(x => x.PlayerEliminationActivities).HasForeignKey(x => x.PlayerID).OnDelete(DeleteBehavior.Restrict);
                ea.HasOne(h => h.PlayerHero).WithMany(x => x.PlayerEliminationActivities).HasForeignKey(x => x.PlayerHeroID).OnDelete(DeleteBehavior.Restrict);

                ea.HasOne(h => h.Victim).WithMany(x => x.VictimEliminationActivities).HasForeignKey(x => x.VictimID).OnDelete(DeleteBehavior.Restrict);
                ea.HasOne(h => h.VictimHero).WithMany(x => x.VictimEliminationActivities).HasForeignKey(x => x.VictimHeroID).OnDelete(DeleteBehavior.Restrict);
            });
        }

        /// <summary>
        /// Runs loaders
        /// This is somewhat messy because I don't feel like making a proper depenancy tree system to handle
        /// some loaded tables needing to run before others. The simple solution which would get horrendously worse with more tables
        /// is to just skip ones that haven't had their dependency run yet.
        /// </summary>
        public void RunLoaders()
        {
            List<Type> hasRun = new List<Type>();

            List<Type> toRun = GetType().Assembly.GetTypes().Where(t => string.Equals(t.Namespace, "TerraSurgeShared.Loaders") && t.GetCustomAttributes(typeof(Attributes.Loader), false).Count() > 0).ToList();

            RunLoaders(toRun, hasRun);
        }

        private void RunLoaders(List<Type> toRun, List<Type> hasRun)
        {
            bool mustRerun = false;

            foreach (Type loader in toRun)
            {
                // Skip already ran loaders
                if (hasRun.Contains(loader))
                {
                    continue;
                }

                Attributes.Loader attribute = (Attributes.Loader)loader.GetCustomAttributes(typeof(Attributes.Loader), false)[0];

                if (attribute.DependsOn != null && !hasRun.Contains(attribute.DependsOn))
                {
                    mustRerun = true;
                    continue;
                }

                ((Loader)loader.GetConstructor(new Type[0]).Invoke(new object[0])).Start();

                hasRun.Add(loader);
            }

            if (mustRerun)
            {
                RunLoaders(toRun, hasRun);
            }
        }

        public DbSet<Game> Game { get; set; }

        public DbSet<BNetPlayer> BNetPlayer { get; set; }

        public DbSet<Map> Map { get; set; }

        public DbSet<MapType> MapType { get; set; }

        public DbSet<Hero> Hero { get; set; }

        public DbSet<HeroAbility> HeroAbility { get; set; }

        public DbSet<GameActivity> GameActivity { get; set; }

        public DbSet<HeroSwapActivity> HeroSwapActivity { get; set; }

        public DbSet<EliminationActivity> EliminationActivity { get; set; }

        public DbSet<AbilityActivity> AbilityActivitiy { get; set; }
    }
}
