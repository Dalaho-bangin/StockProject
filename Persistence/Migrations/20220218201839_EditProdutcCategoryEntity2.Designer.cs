﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

namespace Persistence.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220218201839_EditProdutcCategoryEntity2")]
    partial class EditProdutcCategoryEntity2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.ProductCategories.ProductCategory", b =>
                {
                    b.Property<long>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 2, 18, 23, 48, 38, 75, DateTimeKind.Local).AddTicks(2114));

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<long?>("ParentProductCategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("ParentProductCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProductCategoryId1")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductCategoryName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductCategoryId");

                    b.HasIndex("ProductCategoryId1");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Domain.Roles.Permission", b =>
                {
                    b.Property<long>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ParentPermissionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PermissionId1")
                        .HasColumnType("bigint");

                    b.Property<string>("PermissionTitle")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("PermissionId");

                    b.HasIndex("PermissionId1");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            PermissionId = 1L,
                            PermissionTitle = "مدیر سایت"
                        },
                        new
                        {
                            PermissionId = 2L,
                            ParentPermissionId = 1L,
                            PermissionTitle = "مدیریت کاربران"
                        },
                        new
                        {
                            PermissionId = 3L,
                            ParentPermissionId = 2L,
                            PermissionTitle = "افزودن کاربر"
                        },
                        new
                        {
                            PermissionId = 4L,
                            ParentPermissionId = 2L,
                            PermissionTitle = "ویرایش کاربر"
                        },
                        new
                        {
                            PermissionId = 5L,
                            ParentPermissionId = 2L,
                            PermissionTitle = "حذف کاربر"
                        },
                        new
                        {
                            PermissionId = 6L,
                            ParentPermissionId = 2L,
                            PermissionTitle = "افزودن نقش به کاربر"
                        },
                        new
                        {
                            PermissionId = 7L,
                            ParentPermissionId = 1L,
                            PermissionTitle = "مدیریت نقش ها"
                        },
                        new
                        {
                            PermissionId = 8L,
                            ParentPermissionId = 7L,
                            PermissionTitle = "افزودن نقش"
                        },
                        new
                        {
                            PermissionId = 9L,
                            ParentPermissionId = 7L,
                            PermissionTitle = "ویرایش نقش"
                        },
                        new
                        {
                            PermissionId = 10L,
                            ParentPermissionId = 7L,
                            PermissionTitle = "حذف نقش"
                        });
                });

            modelBuilder.Entity("Domain.Roles.PermissionsRole", b =>
                {
                    b.Property<long>("PermissionsRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("PermissionsRoleId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("PermissionsRoles");
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 2, 18, 23, 48, 38, 92, DateTimeKind.Local).AddTicks(4639));

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Roles.RolesUser", b =>
                {
                    b.Property<long>("RolesUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("RolesUserId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("RolesUsers");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("EmailActiveCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime>("InsertTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 2, 18, 23, 48, 38, 92, DateTimeKind.Local).AddTicks(8295));

                    b.Property<bool>("IsEmailActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMobileActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("MobileActiveCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.ProductCategories.ProductCategory", b =>
                {
                    b.HasOne("Domain.ProductCategories.ProductCategory", null)
                        .WithMany("ParentProductCategories")
                        .HasForeignKey("ProductCategoryId1");
                });

            modelBuilder.Entity("Domain.Roles.Permission", b =>
                {
                    b.HasOne("Domain.Roles.Permission", null)
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionId1");
                });

            modelBuilder.Entity("Domain.Roles.PermissionsRole", b =>
                {
                    b.HasOne("Domain.Roles.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Roles.Role", "Role")
                        .WithMany("PermissionsRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Roles.RolesUser", b =>
                {
                    b.HasOne("Domain.Roles.Role", "Role")
                        .WithMany("RolesUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Users.User", "User")
                        .WithMany("RolesUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.ProductCategories.ProductCategory", b =>
                {
                    b.Navigation("ParentProductCategories");
                });

            modelBuilder.Entity("Domain.Roles.Permission", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Domain.Roles.Role", b =>
                {
                    b.Navigation("PermissionsRoles");

                    b.Navigation("RolesUsers");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Navigation("RolesUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
