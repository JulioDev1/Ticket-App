﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ticket_App.Context;

#nullable disable

namespace Ticket_App.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20240718185401_ModifyDataType")]
    partial class ModifyDataType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ticket_App.Model.Events", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateInit")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Ticket_App.Model.Tickets", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("EventId")
                        .IsUnique();

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Ticket_App.Model.UserTickets", b =>
                {
                    b.Property<Guid>("UsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("UsersId");

                    b.HasIndex("TicketId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTickets");
                });

            modelBuilder.Entity("Ticket_App.Model.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TicketsUsers", b =>
                {
                    b.Property<Guid>("TicketsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("TicketsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TicketsUsers");
                });

            modelBuilder.Entity("Ticket_App.Model.Events", b =>
                {
                    b.HasOne("Ticket_App.Model.Users", "User")
                        .WithMany("Event")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ticket_App.Model.Tickets", b =>
                {
                    b.HasOne("Ticket_App.Model.Events", "Event")
                        .WithOne("Ticket")
                        .HasForeignKey("Ticket_App.Model.Tickets", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Ticket_App.Model.UserTickets", b =>
                {
                    b.HasOne("Ticket_App.Model.Tickets", "Ticket")
                        .WithMany("userTickets")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ticket_App.Model.Users", "User")
                        .WithMany("userTickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ticket");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TicketsUsers", b =>
                {
                    b.HasOne("Ticket_App.Model.Tickets", null)
                        .WithMany()
                        .HasForeignKey("TicketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ticket_App.Model.Users", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ticket_App.Model.Events", b =>
                {
                    b.Navigation("Ticket")
                        .IsRequired();
                });

            modelBuilder.Entity("Ticket_App.Model.Tickets", b =>
                {
                    b.Navigation("userTickets");
                });

            modelBuilder.Entity("Ticket_App.Model.Users", b =>
                {
                    b.Navigation("Event");

                    b.Navigation("userTickets");
                });
#pragma warning restore 612, 618
        }
    }
}
