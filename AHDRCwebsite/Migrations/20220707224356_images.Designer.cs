﻿//// <auto-generated />
//using System;
//using AHDRCwebsite.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//#nullable disable

//namespace AHDRCwebsite.Migrations
//{
//    [DbContext(typeof(ArtworkContext))]
//    [Migration("20220707224356_images")]
//    partial class images
//    {
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "6.0.6")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

//            modelBuilder.Entity("AHDRCwebsite.Models.Artwork", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<string>("Acquiredfrom")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Acquisitiondate")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Additionalfeatures")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Aquisitiondate")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Artist")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Artistgender")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Artistsg")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Associatefeatures")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Associatfeatures")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Auctions")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Calabashinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Certificate")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Chefferie")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Clan")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Collectedby")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Collectedwhen")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Collection")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Commanditaire")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Comments")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Commgender")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Commonfeatures")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Commsg")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Condition")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Confidential")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Country")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Createdate")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Createdatemax")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Createdatemin")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Creditline")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Depth")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Diameter")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Donationfrom")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Ethnicgroup")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Exhibition")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Features")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Groups")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Hairinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Height")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Identifier")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Inventory")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Ispublic")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Kingdom")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Langgroup")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Langsubgroup")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Length")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medbeinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medbkinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medboinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medceinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medclinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medfeinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medfiinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medglinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medhoinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medirinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medium")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medivinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medmainfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medotinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medrainfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medreinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medseedpodsinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medshinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medskinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medstinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medwoinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Medwoodinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Multiline")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Needbetter")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Objectgender")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Objectjanus")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Objectname")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Objectnameex")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Objectnamegn")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Objectposture")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Photocopy")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Photographer")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Photoinvnr")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Photoprov")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Pigmentinfo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Provenance")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Publication")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Raaiid")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Reacttmp")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Region")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Restoration")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Ritualassoc")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Sitearcheo")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Structuralfeatures")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Tms")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Unit")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Usage")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Village")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Web")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Weight")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Width")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Workshop")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Workshoplist")
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Yaleid")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Artworks");
//                });

//            modelBuilder.Entity("AHDRCwebsite.Models.ArtworkImage", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

//                    b.Property<int?>("ArtworkId")
//                        .HasColumnType("int");

//                    b.Property<string>("ImageURL")
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("ArtworkId");

//                    b.ToTable("ArtworkImages");
//                });

//            modelBuilder.Entity("AHDRCwebsite.Models.ArtworkImage", b =>
//                {
//                    b.HasOne("AHDRCwebsite.Models.Artwork", "Artwork")
//                        .WithMany("ArtworkImage")
//                        .HasForeignKey("ArtworkId");

//                    b.Navigation("Artwork");
//                });

//            modelBuilder.Entity("AHDRCwebsite.Models.Artwork", b =>
//                {
//                    b.Navigation("ArtworkImage");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
