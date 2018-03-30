﻿// <auto-generated />
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CoreApp.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreApp.Models.Curator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CuratedGroups");

                    b.Property<int>("FacultyId");

                    b.Property<int>("Mark");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("UserId");

                    b.ToTable("Curators");
                });

            modelBuilder.Entity("CoreApp.Models.ElderCurator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FacultyId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("UserId");

                    b.ToTable("ElderCurators");
                });

            modelBuilder.Entity("CoreApp.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeadline");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("CoreApp.Models.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Mark");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("CoreApp.Models.Feedback", b =>
                {
                    b.Property<int>("ScheduledEventId");

                    b.Property<DateTime>("DateOfWriting");

                    b.Property<int>("Mark");

                    b.HasKey("ScheduledEventId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("CoreApp.Models.FeedbackAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<int>("FeedbackAnswerFormId");

                    b.Property<string>("Question");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackAnswerFormId");

                    b.ToTable("FeedbackAnswers");
                });

            modelBuilder.Entity("CoreApp.Models.FeedbackAnswerForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DeadlineDate");

                    b.Property<int>("FeedbackId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId")
                        .IsUnique();

                    b.ToTable("FeedbackAnswerForms");
                });

            modelBuilder.Entity("CoreApp.Models.FeedbackForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DeadlineDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("FeedbackForms");
                });

            modelBuilder.Entity("CoreApp.Models.FeedbackQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FeedbackFormId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackFormId");

                    b.ToTable("FeedbackQuestions");
                });

            modelBuilder.Entity("CoreApp.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CoreApp.Models.ScheduledEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorId");

                    b.Property<int>("EventId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("ScheduledEvents");
                });

            modelBuilder.Entity("CoreApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FacultyId");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.Property<string>("SurName");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoreApp.Models.Curator", b =>
                {
                    b.HasOne("CoreApp.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApp.Models.ElderCurator", b =>
                {
                    b.HasOne("CoreApp.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApp.Models.Feedback", b =>
                {
                    b.HasOne("CoreApp.Models.ScheduledEvent", "ScheduledEvent")
                        .WithOne("Feedback")
                        .HasForeignKey("CoreApp.Models.Feedback", "ScheduledEventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApp.Models.FeedbackAnswer", b =>
                {
                    b.HasOne("CoreApp.Models.FeedbackAnswerForm", "FeedbackAnswerForm")
                        .WithMany("FeedbackAnswers")
                        .HasForeignKey("FeedbackAnswerFormId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApp.Models.FeedbackAnswerForm", b =>
                {
                    b.HasOne("CoreApp.Models.Feedback", "Feedback")
                        .WithOne("FeedbackAnswerForm")
                        .HasForeignKey("CoreApp.Models.FeedbackAnswerForm", "FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApp.Models.FeedbackQuestion", b =>
                {
                    b.HasOne("CoreApp.Models.FeedbackForm", "FeedbackForm")
                        .WithMany("FeedbackQuestions")
                        .HasForeignKey("FeedbackFormId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreApp.Models.ScheduledEvent", b =>
                {
                    b.HasOne("CoreApp.Models.User", "Author")
                        .WithMany("AuthoredEvents")
                        .HasForeignKey("AuthorId");

                    b.HasOne("CoreApp.Models.Event", "Event")
                        .WithMany("ScheduledEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreApp.Models.User", "User")
                        .WithMany("ScheduledEvents")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CoreApp.Models.User", b =>
                {
                    b.HasOne("CoreApp.Models.Faculty")
                        .WithMany("Users")
                        .HasForeignKey("FacultyId");

                    b.HasOne("CoreApp.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
