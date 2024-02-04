﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScoreCraftApi.Data;

#nullable disable

namespace ScoreCraftApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScoreCraftApi.Enities.Match", b =>
                {
                    b.Property<int>("RefMatch")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefMatch"));

                    b.Property<int>("BestOf")
                        .HasColumnType("int");

                    b.Property<string>("Format")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RefGuestTeam")
                        .HasColumnType("int");

                    b.Property<int?>("RefHomeTeam")
                        .HasColumnType("int");

                    b.Property<int?>("RefMatchWinner")
                        .HasColumnType("int");

                    b.Property<int?>("TeamRefTeam")
                        .HasColumnType("int");

                    b.HasKey("RefMatch");

                    b.HasIndex("RefGuestTeam");

                    b.HasIndex("RefHomeTeam");

                    b.HasIndex("RefMatchWinner");

                    b.HasIndex("TeamRefTeam");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.MatchResult", b =>
                {
                    b.Property<int>("RefMatchResult")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefMatchResult"));

                    b.Property<int?>("GuestResult")
                        .HasColumnType("int");

                    b.Property<int?>("HomeResult")
                        .HasColumnType("int");

                    b.Property<int?>("RefMatch")
                        .HasColumnType("int");

                    b.HasKey("RefMatchResult");

                    b.HasIndex("RefMatch");

                    b.ToTable("MatchResults");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.Team", b =>
                {
                    b.Property<int?>("RefTeam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("RefTeam"));

                    b.Property<string>("TeamName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefTeam");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.User", b =>
                {
                    b.Property<Guid>("RefUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTeamCaptain")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.UserTeam", b =>
                {
                    b.Property<Guid>("RefUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RefTeam")
                        .HasColumnType("int");

                    b.HasKey("RefUser", "RefTeam");

                    b.HasIndex("RefTeam");

                    b.ToTable("UserTeams");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.Match", b =>
                {
                    b.HasOne("ScoreCraftApi.Enities.Team", "GuestTeam")
                        .WithMany()
                        .HasForeignKey("RefGuestTeam")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ScoreCraftApi.Enities.Team", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("RefHomeTeam")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ScoreCraftApi.Enities.Team", "WinningTeam")
                        .WithMany()
                        .HasForeignKey("RefMatchWinner")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ScoreCraftApi.Enities.Team", null)
                        .WithMany("Matches")
                        .HasForeignKey("TeamRefTeam");

                    b.Navigation("GuestTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("WinningTeam");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.MatchResult", b =>
                {
                    b.HasOne("ScoreCraftApi.Enities.Match", "Match")
                        .WithMany("MatchResults")
                        .HasForeignKey("RefMatch");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.UserTeam", b =>
                {
                    b.HasOne("ScoreCraftApi.Enities.Team", "Team")
                        .WithMany("UserTeams")
                        .HasForeignKey("RefTeam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScoreCraftApi.Enities.User", "User")
                        .WithMany("UserTeams")
                        .HasForeignKey("RefUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.Match", b =>
                {
                    b.Navigation("MatchResults");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.Team", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("UserTeams");
                });

            modelBuilder.Entity("ScoreCraftApi.Enities.User", b =>
                {
                    b.Navigation("UserTeams");
                });
#pragma warning restore 612, 618
        }
    }
}
