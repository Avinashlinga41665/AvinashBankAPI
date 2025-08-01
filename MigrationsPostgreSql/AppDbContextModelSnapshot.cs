﻿// <auto-generated />
using System;
using AvinashBackEndAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AvinashBackEndAPI.MigrationsPostgreSql
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AvinashBackEndAPI.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.AccountTable", b =>
                {
                    b.Property<int>("tmsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("tmsID"));

                    b.Property<int>("Credit")
                        .HasColumnType("integer");

                    b.Property<int>("Debit")
                        .HasColumnType("integer");

                    b.Property<string>("fromAccountName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("status")
                        .HasColumnType("boolean");

                    b.Property<string>("toAccountName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("transferType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("tmsID");

                    b.ToTable("TransferTable");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.Transaction", b =>
                {
                    b.Property<int>("TransId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TransId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("CreditAccountId")
                        .HasColumnType("integer");

                    b.Property<int>("DebitAccountId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeOfTransfer")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("TransactionStatus")
                        .HasColumnType("boolean");

                    b.Property<string>("TransferID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TypeOfPayment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("TransId");

                    b.HasIndex("CreditAccountId");

                    b.HasIndex("DebitAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeOfTransfer")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransferID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.Property<string>("fromAccount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("toAccount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("ScheduleTransfers");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AadharNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.Account", b =>
                {
                    b.HasOne("AvinashBackEndAPI.Models.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.Transaction", b =>
                {
                    b.HasOne("AvinashBackEndAPI.Models.Account", "CreditAccount")
                        .WithMany("CreditTransactions")
                        .HasForeignKey("CreditAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AvinashBackEndAPI.Models.Account", "DebitAccount")
                        .WithMany("DebitTransactions")
                        .HasForeignKey("DebitAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CreditAccount");

                    b.Navigation("DebitAccount");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.Transfer", b =>
                {
                    b.HasOne("AvinashBackEndAPI.Models.User", "User")
                        .WithMany("ScheduleTransfers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.Account", b =>
                {
                    b.Navigation("CreditTransactions");

                    b.Navigation("DebitTransactions");
                });

            modelBuilder.Entity("AvinashBackEndAPI.Models.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("ScheduleTransfers");
                });
#pragma warning restore 612, 618
        }
    }
}
