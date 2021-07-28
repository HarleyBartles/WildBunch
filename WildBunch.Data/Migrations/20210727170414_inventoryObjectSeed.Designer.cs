﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WildBunch.Data.DbContexts;

namespace WildBunch.Data.Migrations
{
    [DbContext(typeof(WildBunchContext))]
    [Migration("20210727170414_inventoryObjectSeed")]
    partial class inventoryObjectSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.HasIndex("SubjectId", "SessionId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.BagItem", b =>
                {
                    b.Property<string>("BagItemId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CharacterBagId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("InventoryObjectId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityCarried")
                        .HasColumnType("int");

                    b.HasKey("BagItemId");

                    b.HasIndex("CharacterBagId");

                    b.HasIndex("InventoryObjectId");

                    b.ToTable("tblBagItem");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.Character", b =>
                {
                    b.Property<string>("CharacterId")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Dollars")
                        .HasColumnType("double");

                    b.Property<int>("HealthPoints")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("CharacterId");

                    b.HasIndex("UserId");

                    b.ToTable("tblCharacter");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.CharacterBag", b =>
                {
                    b.Property<string>("CharacterBagId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CharacterId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("CharacterBagId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("tblCharacterBag");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.Game", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CharacterId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("GameId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("UserId");

                    b.ToTable("tblGame");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.InventoryObject", b =>
                {
                    b.Property<int>("InventoryObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("BasePrice")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("InventoryObjectId");

                    b.ToTable("tblInventoryObject");

                    b.HasData(
                        new
                        {
                            InventoryObjectId = 1,
                            BasePrice = 55.0,
                            Name = "Horse"
                        },
                        new
                        {
                            InventoryObjectId = 2,
                            BasePrice = 28.0,
                            Name = "Saddle"
                        },
                        new
                        {
                            InventoryObjectId = 3,
                            BasePrice = 35.0,
                            Name = "Colt .44"
                        },
                        new
                        {
                            InventoryObjectId = 4,
                            BasePrice = 5.0,
                            Name = "Colt .44 Bullets"
                        },
                        new
                        {
                            InventoryObjectId = 5,
                            BasePrice = 35.0,
                            Name = "Colt .45"
                        },
                        new
                        {
                            InventoryObjectId = 6,
                            BasePrice = 10.0,
                            Name = "Colt .45 Bullets"
                        },
                        new
                        {
                            InventoryObjectId = 7,
                            BasePrice = 40.0,
                            Name = "Winchester Rifle"
                        },
                        new
                        {
                            InventoryObjectId = 8,
                            BasePrice = 15.0,
                            Name = "Winchester Bullets"
                        },
                        new
                        {
                            InventoryObjectId = 9,
                            BasePrice = 10.0,
                            Name = "Knife"
                        },
                        new
                        {
                            InventoryObjectId = 10,
                            BasePrice = 2.0,
                            Name = "Food for 1 day"
                        },
                        new
                        {
                            InventoryObjectId = 11,
                            BasePrice = 3.0,
                            Name = "Horse Food for 1 day"
                        },
                        new
                        {
                            InventoryObjectId = 12,
                            BasePrice = 3.0,
                            Name = "Canteen of Water"
                        },
                        new
                        {
                            InventoryObjectId = 13,
                            BasePrice = 15.0,
                            Name = "Great Coat"
                        },
                        new
                        {
                            InventoryObjectId = 14,
                            BasePrice = 2.0,
                            Name = "Whiskey"
                        },
                        new
                        {
                            InventoryObjectId = 15,
                            BasePrice = 8.0,
                            Name = "Blanket"
                        },
                        new
                        {
                            InventoryObjectId = 16,
                            BasePrice = 4.0,
                            Name = "Snakebite Medicine"
                        });
                });

            modelBuilder.Entity("WildBunch.Business.Entities.WildBunchUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("tblUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WildBunch.Business.Entities.WildBunchUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WildBunch.Business.Entities.WildBunchUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WildBunch.Business.Entities.WildBunchUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WildBunch.Business.Entities.WildBunchUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WildBunch.Business.Entities.BagItem", b =>
                {
                    b.HasOne("WildBunch.Business.Entities.CharacterBag", "Bag")
                        .WithMany("Items")
                        .HasForeignKey("CharacterBagId");

                    b.HasOne("WildBunch.Business.Entities.InventoryObject", "InventoryObject")
                        .WithMany("Instances")
                        .HasForeignKey("InventoryObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bag");

                    b.Navigation("InventoryObject");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.Character", b =>
                {
                    b.HasOne("WildBunch.Business.Entities.WildBunchUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.CharacterBag", b =>
                {
                    b.HasOne("WildBunch.Business.Entities.Character", "Character")
                        .WithOne("Bag")
                        .HasForeignKey("WildBunch.Business.Entities.CharacterBag", "CharacterId");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.Game", b =>
                {
                    b.HasOne("WildBunch.Business.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("WildBunch.Business.Entities.WildBunchUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Character");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.Character", b =>
                {
                    b.Navigation("Bag");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.CharacterBag", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("WildBunch.Business.Entities.InventoryObject", b =>
                {
                    b.Navigation("Instances");
                });
#pragma warning restore 612, 618
        }
    }
}
