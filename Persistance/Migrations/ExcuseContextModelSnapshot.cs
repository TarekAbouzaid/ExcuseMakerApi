﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance.Context;

#nullable disable

namespace Persistance.Migrations
{
    [DbContext(typeof(ExcuseContext))]
    partial class ExcuseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DTO.Excuse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Excuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 0,
                            Text = "I promised my aunts that I would meet them this reunion."
                        },
                        new
                        {
                            Id = 2,
                            Category = 0,
                            Text = "I got run over by a cyclist"
                        },
                        new
                        {
                            Id = 3,
                            Category = 0,
                            Text = "I fell over in the shower and knocked myself out."
                        },
                        new
                        {
                            Id = 4,
                            Category = 0,
                            Text = "Had a morning action rolling. It was too good to leave."
                        },
                        new
                        {
                            Id = 5,
                            Category = 0,
                            Text = "My goldfish is ill. Have to take care of it."
                        },
                        new
                        {
                            Id = 6,
                            Category = 1,
                            Text = "My fortune teller advised against going home."
                        },
                        new
                        {
                            Id = 7,
                            Category = 1,
                            Text = "My plot to take over the presidency of the book club is thickening, and I must stay at the office to make sure everything is working out smoothly."
                        },
                        new
                        {
                            Id = 8,
                            Category = 1,
                            Text = "I have to go to the post office to see if I am still wanted."
                        },
                        new
                        {
                            Id = 9,
                            Category = 1,
                            Text = "I am currently working on my bucket list and, unfortunately, going home is not on my list."
                        },
                        new
                        {
                            Id = 10,
                            Category = 1,
                            Text = "I need to plant my watermelon seeds in the office. Yes, I know it is the middle of the winter. I am starting ahead of the game this year!."
                        },
                        new
                        {
                            Id = 11,
                            Category = 2,
                            Text = "I am trying to be less popular. Someone has got to do it!"
                        },
                        new
                        {
                            Id = 12,
                            Category = 2,
                            Text = "I have lost my lucky rat's tail. Sorry, but I never go out without it!\r\n"
                        },
                        new
                        {
                            Id = 13,
                            Category = 2,
                            Text = "I am being deported Friday, sorry I will not be able to make it."
                        },
                        new
                        {
                            Id = 14,
                            Category = 2,
                            Text = "My socks are matching! This is a natural disaster, an emergency!. I can't leave now."
                        },
                        new
                        {
                            Id = 15,
                            Category = 2,
                            Text = "I don't like to leave my comfort zone."
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
