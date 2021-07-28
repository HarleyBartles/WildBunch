using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WildBunch.Business.Entities;
using WildBunch.Shared.Enums;

namespace WildBunch.Data.DbContexts
{
    public class WildBunchContext : ApiAuthorizationDbContext<WildBunchUser>
    {
        private IOptions<OperationalStoreOptions> _operationalStoreOptions;
        public WildBunchContext(DbContextOptions<WildBunchContext> options, IOptions<OperationalStoreOptions> storeOptions)
            : base(options, storeOptions)
        {
            _operationalStoreOptions = storeOptions;
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterBag> CharacterBags { get; set; }
        public DbSet<InventoryObject> InventoryObjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);

            builder.Entity<WildBunchUser>(user =>
            {
                user.ToTable("tblUser");
            });

            builder.Entity<Game>(game =>
            {
                game.HasKey(g => g.GameId);
                game.Property(g => g.GameId)
                    .ValueGeneratedOnAdd();
                game.ToTable("tblGame");

                game.HasOne(g => g.CurrentTown)
                    .WithMany();
                
            });

            builder.Entity<Town>(town =>
            {
                town.HasKey(t => t.TownId);
                town.ToTable("tblTown");

                town.HasData(
                    new Town { TownId = 1, Name = "Dry Gulch" },
                    new Town { TownId = 2, Name = "Dodge City" },
                    new Town { TownId = 3, Name = "Nugget City" },
                    new Town { TownId = 4, Name = "BulletVille" },
                    new Town { TownId = 5, Name = "Deadman's Creek" }
                );
            });

            builder.Entity<Character>(character =>
            {
                character.HasKey(c => c.CharacterId);
                character.Property(c => c.CharacterId)
                    .ValueGeneratedOnAdd();
                character.ToTable("tblCharacter");
                character.HasOne(c => c.Bag)
                    .WithOne(cb => cb.Character);
            });

            builder.Entity<CharacterBag>(bag =>
            {
                bag.HasKey(b => b.CharacterBagId);
                bag.Property(b => b.CharacterBagId)
                    .ValueGeneratedOnAdd();
                bag.ToTable("tblCharacterBag");
                bag.HasOne(b => b.Character)
                    .WithOne(c => c.Bag);
                bag.HasMany(b => b.Items)
                    .WithOne(i => i.Bag);
            });

            builder.Entity<BagItem>(item =>
            {
                item.HasKey(i => i.BagItemId);
                item.Property(i => i.BagItemId)
                    .ValueGeneratedOnAdd();
                item.ToTable("tblBagItem");
                item.HasOne(i => i.Bag)
                    .WithMany(b => b.Items);
                item.HasOne(bi => bi.InventoryObject)
                    .WithMany(o => o.Instances);
            });

            builder.Entity<InventoryObject>(obj =>
            {
                obj.HasKey(o => o.InventoryObjectId);
                obj.ToTable("tblInventoryObject");
                obj.HasMany(ob => ob.Instances)
                    .WithOne(i => i.InventoryObject);
                obj.HasData(
                    new InventoryObject { InventoryObjectId = 1, Name = "Horse", BasePrice = 55 },
                    new InventoryObject { InventoryObjectId = 2, Name = "Saddle", BasePrice = 28 },
                    new InventoryObject { InventoryObjectId = 3, Name = "Colt .44", BasePrice = 35 },
                    new InventoryObject { InventoryObjectId = 4, Name = "Colt .44 Bullets", BasePrice = 5 },
                    new InventoryObject { InventoryObjectId = 5, Name = "Colt .45", BasePrice = 35 },
                    new InventoryObject { InventoryObjectId = 6, Name = "Colt .45 Bullets", BasePrice = 10 },
                    new InventoryObject { InventoryObjectId = 7, Name = "Winchester Rifle", BasePrice = 40 },
                    new InventoryObject { InventoryObjectId = 8, Name = "Winchester Bullets", BasePrice = 15 },
                    new InventoryObject { InventoryObjectId = 9, Name = "Knife", BasePrice = 10 },
                    new InventoryObject { InventoryObjectId = 10, Name = "Food for 1 day", BasePrice = 2 },
                    new InventoryObject { InventoryObjectId = 11, Name = "Horse Food for 1 day", BasePrice = 3 },
                    new InventoryObject { InventoryObjectId = 12, Name = "Canteen of Water", BasePrice = 3 },
                    new InventoryObject { InventoryObjectId = 13, Name = "Great Coat", BasePrice = 15 },
                    new InventoryObject { InventoryObjectId = 14, Name = "Whiskey", BasePrice = 2 },
                    new InventoryObject { InventoryObjectId = 15, Name = "Blanket", BasePrice = 8 },
                    new InventoryObject { InventoryObjectId = 16, Name = "Snakebite Medicine", BasePrice = 4 }
                );
            });
        }
    }
}
