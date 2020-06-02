﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZBot.DbModels;

namespace ZBot.Migrations
{
    [DbContext(typeof(SummonerContext))]
    partial class SummonerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4");

            modelBuilder.Entity("ZBot.DbModels.RankHistoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rank")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SummonerModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SummonerModelId");

                    b.ToTable("RankHistoryModels");
                });

            modelBuilder.Entity("ZBot.DbModels.SummonerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SummonerName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SummonerModels");
                });

            modelBuilder.Entity("ZBot.DbModels.RankHistoryModel", b =>
                {
                    b.HasOne("ZBot.DbModels.SummonerModel", "SummonerModel")
                        .WithMany()
                        .HasForeignKey("SummonerModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
