﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using core_api.Database;

namespace core_api.Migrations
{
    [DbContext(typeof(BlogContext))]
    partial class BlogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("core_api.Model.Post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("body")
                        .HasColumnType("TEXT");

                    b.Property<string>("title")
                        .HasColumnType("TEXT");

                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("userId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("core_api.Model.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<string>("profile")
                        .HasColumnType("TEXT");

                    b.Property<string>("salt")
                        .HasColumnType("TEXT");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("core_api.Model.Post", b =>
                {
                    b.HasOne("core_api.Model.User", null)
                        .WithMany("Posts")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
