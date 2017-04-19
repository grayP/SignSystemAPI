using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SignSystem.API.Entities;

namespace SignSystem.API.Migrations
{
    [DbContext(typeof(SignSystemInfoContext))]
    [Migration("20170417102603_Sign")]
    partial class Sign
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SignSystem.API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("SignSystem.API.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointsOfInterest");
                });

            modelBuilder.Entity("SignSystem.API.Entities.Sign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Height");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Width");

                    b.HasKey("Id");

                    b.ToTable("Signs");
                });

            modelBuilder.Entity("SignSystem.API.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IpAddress");

                    b.Property<string>("Manager")
                        .HasMaxLength(100);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Port");

                    b.Property<int?>("SignId");

                    b.Property<string>("SubMask");

                    b.Property<string>("Suburb")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("SignId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("SignSystem.API.Entities.PointOfInterest", b =>
                {
                    b.HasOne("SignSystem.API.Entities.City", "City")
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SignSystem.API.Entities.Store", b =>
                {
                    b.HasOne("SignSystem.API.Entities.Sign", "Sign")
                        .WithMany()
                        .HasForeignKey("SignId");
                });
        }
    }
}
