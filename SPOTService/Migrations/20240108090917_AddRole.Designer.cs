﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SPOTService.DataStorage;

#nullable disable

namespace SPOTService.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20240108090917_AddRole")]
    partial class AddRole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AnswerVariantId")
                        .HasColumnType("integer");

                    b.Property<string>("OpenAnswer")
                        .HasColumnType("text");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int>("RespondentId")
                        .HasColumnType("integer");

                    b.Property<int>("SurveyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnswerVariantId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("RespondentId");

                    b.HasIndex("SurveyId");

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.AnswerVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AnswerVariant", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.QuestionAnswerVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerVariantId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AnswerVariantId");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionAnswerVariants");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Respondent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Patronomic")
                        .HasColumnType("text");

                    b.Property<int?>("StateId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<long>("TelegramChatId")
                        .HasColumnType("bigint");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Respondent", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "admin"
                        });
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.RoleRules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("RulesId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RulesId");

                    b.ToTable("RoleRules");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Rules", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rules", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Survey", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int>("SurveyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestions");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronomyc")
                        .HasColumnType("text");

                    b.Property<string>("RefreshTokenHash")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Answer", b =>
                {
                    b.HasOne("SPOTService.DataStorage.Entities.AnswerVariant", "AnswerVariant")
                        .WithMany("Answers")
                        .HasForeignKey("AnswerVariantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SPOTService.DataStorage.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPOTService.DataStorage.Entities.Respondent", "Respondent")
                        .WithMany("Answers")
                        .HasForeignKey("RespondentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPOTService.DataStorage.Entities.Survey", "Survey")
                        .WithMany("Answers")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerVariant");

                    b.Navigation("Question");

                    b.Navigation("Respondent");

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.QuestionAnswerVariant", b =>
                {
                    b.HasOne("SPOTService.DataStorage.Entities.AnswerVariant", "AnswerVariant")
                        .WithMany("QuestionAnswerVariants")
                        .HasForeignKey("AnswerVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPOTService.DataStorage.Entities.Question", "Question")
                        .WithMany("QuestionAnswerVariants")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnswerVariant");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Respondent", b =>
                {
                    b.HasOne("SPOTService.DataStorage.Entities.Group", "Group")
                        .WithMany("Respondents")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.RoleRules", b =>
                {
                    b.HasOne("SPOTService.DataStorage.Entities.Role", "Role")
                        .WithMany("RoleRules")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPOTService.DataStorage.Entities.Rules", "Rule")
                        .WithMany("RoleRules")
                        .HasForeignKey("RulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Rule");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Survey", b =>
                {
                    b.HasOne("SPOTService.DataStorage.Entities.Group", "Group")
                        .WithMany("Surveys")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPOTService.DataStorage.Entities.User", "User")
                        .WithMany("Surveys")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.SurveyQuestion", b =>
                {
                    b.HasOne("SPOTService.DataStorage.Entities.Question", "Question")
                        .WithMany("SurveyQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPOTService.DataStorage.Entities.Survey", "Survey")
                        .WithMany("SurveyQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.User", b =>
                {
                    b.HasOne("SPOTService.DataStorage.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.AnswerVariant", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("QuestionAnswerVariants");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Group", b =>
                {
                    b.Navigation("Respondents");

                    b.Navigation("Surveys");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("QuestionAnswerVariants");

                    b.Navigation("SurveyQuestions");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Respondent", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Role", b =>
                {
                    b.Navigation("RoleRules");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Rules", b =>
                {
                    b.Navigation("RoleRules");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.Survey", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("SurveyQuestions");
                });

            modelBuilder.Entity("SPOTService.DataStorage.Entities.User", b =>
                {
                    b.Navigation("Surveys");
                });
#pragma warning restore 612, 618
        }
    }
}
