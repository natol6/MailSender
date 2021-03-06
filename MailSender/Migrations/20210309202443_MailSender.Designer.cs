﻿// <auto-generated />
using System;
using MailSender.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MailSender.Migrations
{
    [DbContext(typeof(MailSenderDB))]
    [Migration("20210309202443_MailSender")]
    partial class MailSender
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MailSender.lib.Entities.EmailAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Person_Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailAddresses");
                });

            modelBuilder.Entity("MailSender.lib.Entities.MessagePattern", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MessagePatterns");
                });

            modelBuilder.Entity("MailSender.lib.Entities.MessageSendContainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddressesTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PortUse")
                        .HasColumnType("int");

                    b.Property<bool>("SSLUse")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("SendDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("SmtpAccountEmailUse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmtpAccountPasswordUse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmtpAccountPerson_CompanyUse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SmtpServerUse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MessageSendContainers");
                });

            modelBuilder.Entity("MailSender.lib.Entities.SmtpAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Person_Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Smtp_ServerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Smtp_ServerId");

                    b.ToTable("SmtpAccounts");
                });

            modelBuilder.Entity("MailSender.lib.Entities.SmtpServer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("SmtpServ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("UseSSL")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SmtpServers");
                });

            modelBuilder.Entity("MailSender.lib.Entities.SmtpAccount", b =>
                {
                    b.HasOne("MailSender.lib.Entities.SmtpServer", "Smtp_Server")
                        .WithMany("SmtpAccounts")
                        .HasForeignKey("Smtp_ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Smtp_Server");
                });

            modelBuilder.Entity("MailSender.lib.Entities.SmtpServer", b =>
                {
                    b.Navigation("SmtpAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
