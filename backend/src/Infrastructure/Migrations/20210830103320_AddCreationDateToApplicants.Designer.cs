﻿// <auto-generated />
using System;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210830103320_AddCreationDateToApplicants")]
    partial class AddCreationDateToApplicants
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.Action", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ActionType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("Domain.Entities.Applicant", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CvFileInfoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Experience")
                        .HasColumnType("float");

                    b.Property<string>("ExperienceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSelfApplied")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ToBeContacted")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CvFileInfoId");

                    b.ToTable("Applicants");
                });

            modelBuilder.Entity("Domain.Entities.CandidateComment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CandidateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("StageId");

                    b.ToTable("CandidateComments");
                });

            modelBuilder.Entity("Domain.Entities.CandidateReview", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CandidateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Mark")
                        .HasColumnType("float");

                    b.Property<string>("ReviewId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("StageId");

                    b.ToTable("CandidateReviews");
                });

            modelBuilder.Entity("Domain.Entities.CandidateToStage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CandidateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRemoved")
                        .HasColumnType("datetime2");

                    b.Property<string>("MoverId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("MoverId");

                    b.HasIndex("StageId");

                    b.ToTable("CandidateToStages");
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Domain.Entities.CvParsingJob", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AWSJobId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TriggerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("CvParsingJobs");
                });

            modelBuilder.Entity("Domain.Entities.EmailToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("EmailToken");
                });

            modelBuilder.Entity("Domain.Entities.FileInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileInfos");
                });

            modelBuilder.Entity("Domain.Entities.Pool", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Pools");
                });

            modelBuilder.Entity("Domain.Entities.PoolToApplicant", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PoolId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("PoolId");

                    b.ToTable("PoolToApplicants");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Domain.Entities.RegisterPermission", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("RegisterPermissions");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Domain.Entities.ReviewToStage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReviewId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StageId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.HasIndex("StageId");

                    b.ToTable("ReviewToStages");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Key")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.SkillsParsingJob", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OutputPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TriggerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TriggerId");

                    b.ToTable("SkillsParsingJobs");
                });

            modelBuilder.Entity("Domain.Entities.Stage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsReviewable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("VacancyId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VacancyId");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skype")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.UserFollowedEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EntityId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EntityType")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFollowedEntities");
                });

            modelBuilder.Entity("Domain.Entities.UserToRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserToRoles");
                });

            modelBuilder.Entity("Domain.Entities.Vacancy", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfOpening")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHot")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemote")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlannedCompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Requirements")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponsibleHrId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SalaryFrom")
                        .HasColumnType("int");

                    b.Property<int>("SalaryTo")
                        .HasColumnType("int");

                    b.Property<string>("Sources")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TierFrom")
                        .HasColumnType("int");

                    b.Property<int>("TierTo")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ResponsibleHrId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("Domain.Entities.VacancyCandidate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<double>("Experience")
                        .HasColumnType("float");

                    b.Property<DateTime?>("FirstContactDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HrWhoAddedId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsSelfApplied")
                        .HasColumnType("bit");

                    b.Property<bool>("IsViewed")
                        .HasColumnType("bit");

                    b.Property<int>("SalaryExpectation")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SecondContactDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThirdContactDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.HasIndex("HrWhoAddedId");

                    b.HasIndex("Id");

                    b.ToTable("VacancyCandidates");
                });

            modelBuilder.Entity("Domain.Entities.Action", b =>
                {
                    b.HasOne("Domain.Entities.Stage", "Stage")
                        .WithMany("Actions")
                        .HasForeignKey("StageId")
                        .HasConstraintName("action_stage_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("Domain.Entities.Applicant", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Company")
                        .WithMany("Applicants")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("applicant_company_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.FileInfo", "CvFileInfo")
                        .WithMany()
                        .HasForeignKey("CvFileInfoId");

                    b.Navigation("Company");

                    b.Navigation("CvFileInfo");
                });

            modelBuilder.Entity("Domain.Entities.CandidateComment", b =>
                {
                    b.HasOne("Domain.Entities.VacancyCandidate", "Candidate")
                        .WithMany("CandidateComments")
                        .HasForeignKey("CandidateId")
                        .HasConstraintName("candidate_comment_candidate_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Stage", "Stage")
                        .WithMany("CandidateComments")
                        .HasForeignKey("StageId")
                        .HasConstraintName("candidate_comment_stage_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Candidate");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("Domain.Entities.CandidateReview", b =>
                {
                    b.HasOne("Domain.Entities.VacancyCandidate", "Candidate")
                        .WithMany("Reviews")
                        .HasForeignKey("CandidateId")
                        .HasConstraintName("candidate_review_candidate_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Review", "Review")
                        .WithMany("CandidateReviews")
                        .HasForeignKey("ReviewId")
                        .HasConstraintName("candidate_review_review_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Stage", "Stage")
                        .WithMany("Reviews")
                        .HasForeignKey("StageId")
                        .HasConstraintName("candidate_review_stage_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Candidate");

                    b.Navigation("Review");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("Domain.Entities.CandidateToStage", b =>
                {
                    b.HasOne("Domain.Entities.VacancyCandidate", "Candidate")
                        .WithMany("CandidateToStages")
                        .HasForeignKey("CandidateId")
                        .HasConstraintName("candidate_to_stage_candidate_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.User", "Mover")
                        .WithMany("MovedCandidateToStages")
                        .HasForeignKey("MoverId")
                        .HasConstraintName("candidate_to_stage_mover_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Stage", "Stage")
                        .WithMany("CandidateToStages")
                        .HasForeignKey("StageId")
                        .HasConstraintName("candidate_to_stage_stage_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Candidate");

                    b.Navigation("Mover");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("Domain.Entities.CvParsingJob", b =>
                {
                    b.HasOne("Domain.Entities.User", "Trigger")
                        .WithMany("CvParsingJobs")
                        .HasForeignKey("TriggerId")
                        .HasConstraintName("cv_parsing_job_user_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("Domain.Entities.EmailToken", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("EmailToken")
                        .HasForeignKey("Domain.Entities.EmailToken", "UserId")
                        .HasConstraintName("email_token__user_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Pool", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Company")
                        .WithMany("Pools")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("pool_company_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("Company");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Domain.Entities.PoolToApplicant", b =>
                {
                    b.HasOne("Domain.Entities.Applicant", "Applicant")
                        .WithMany("ApplicantPools")
                        .HasForeignKey("ApplicantId")
                        .HasConstraintName("pool_applicant__applicant_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Pool", "Pool")
                        .WithMany("PoolApplicants")
                        .HasForeignKey("PoolId")
                        .HasConstraintName("pool_applicant__pool_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Applicant");

                    b.Navigation("Pool");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Company")
                        .WithMany("Projects")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("project_company_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .HasConstraintName("refresh_token__user_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.RegisterPermission", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("register_permission__company_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Domain.Entities.ReviewToStage", b =>
                {
                    b.HasOne("Domain.Entities.Review", "Review")
                        .WithMany("ReviewToStages")
                        .HasForeignKey("ReviewId")
                        .HasConstraintName("review_to_stage_review_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Stage", "Stage")
                        .WithMany("ReviewToStages")
                        .HasForeignKey("StageId")
                        .HasConstraintName("review_to_stage_stage_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Review");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("Domain.Entities.SkillsParsingJob", b =>
                {
                    b.HasOne("Domain.Entities.User", "Trigger")
                        .WithMany("SkillsParsingJobs")
                        .HasForeignKey("TriggerId");

                    b.Navigation("Trigger");
                });

            modelBuilder.Entity("Domain.Entities.Stage", b =>
                {
                    b.HasOne("Domain.Entities.Vacancy", "Vacancy")
                        .WithMany("Stages")
                        .HasForeignKey("VacancyId")
                        .HasConstraintName("stage_vacancy_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Company")
                        .WithMany("Recruiters")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("user_company_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Domain.Entities.UserFollowedEntity", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.UserToRole", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("user_role__role_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("user_role__user_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Vacancy", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Company")
                        .WithMany("Vacancies")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("vacancy_company_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany("Vacancies")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("vacancy_project_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.User", "ResponsibleHr")
                        .WithMany("Vacancies")
                        .HasForeignKey("ResponsibleHrId")
                        .HasConstraintName("vacancy_user_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Company");

                    b.Navigation("Project");

                    b.Navigation("ResponsibleHr");
                });

            modelBuilder.Entity("Domain.Entities.VacancyCandidate", b =>
                {
                    b.HasOne("Domain.Entities.Applicant", "Applicant")
                        .WithMany("Candidates")
                        .HasForeignKey("ApplicantId")
                        .HasConstraintName("candidate_applicant_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.User", "HrWhoAdded")
                        .WithMany("AddedCandidates")
                        .HasForeignKey("HrWhoAddedId")
                        .HasConstraintName("candidate_hr_who_added_FK")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Applicant");

                    b.Navigation("HrWhoAdded");
                });

            modelBuilder.Entity("Domain.Entities.Applicant", b =>
                {
                    b.Navigation("ApplicantPools");

                    b.Navigation("Candidates");
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.Navigation("Applicants");

                    b.Navigation("Pools");

                    b.Navigation("Projects");

                    b.Navigation("Recruiters");

                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("Domain.Entities.Pool", b =>
                {
                    b.Navigation("PoolApplicants");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Navigation("CandidateReviews");

                    b.Navigation("ReviewToStages");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("Domain.Entities.Stage", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("CandidateComments");

                    b.Navigation("CandidateToStages");

                    b.Navigation("Reviews");

                    b.Navigation("ReviewToStages");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("AddedCandidates");

                    b.Navigation("CvParsingJobs");

                    b.Navigation("EmailToken");

                    b.Navigation("MovedCandidateToStages");

                    b.Navigation("RefreshTokens");

                    b.Navigation("SkillsParsingJobs");

                    b.Navigation("UserRoles");

                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("Domain.Entities.Vacancy", b =>
                {
                    b.Navigation("Stages");
                });

            modelBuilder.Entity("Domain.Entities.VacancyCandidate", b =>
                {
                    b.Navigation("CandidateComments");

                    b.Navigation("CandidateToStages");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
