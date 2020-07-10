using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ITICommunity.Models
{
    public partial class ITICommunityContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole,IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ITICommunityContext()
        {
        }

        public ITICommunityContext(DbContextOptions<ITICommunityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<Intake> Intakes { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<OnlineUser> OnlineUsers { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        //deleted the users table because it alreay gets added by Identity class
        public virtual DbSet<UserContacts> UserContacts { get; set; }
        public virtual DbSet<UserEducation> UserEducations { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<UserWorkExperience> UserWorkExperiences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=ITICommunity;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                userRole.HasOne(ur => ur.Role).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();
                userRole.HasOne(ur => ur.User).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.BranchLocation)
                    .HasColumnName("branchLocation")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Comment_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FollowingId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FollowingId).HasColumnName("FollowingID");

                entity.HasOne(d => d.Following)
                    .WithMany(p => p.FollowFollowing)
                    .HasForeignKey(d => d.FollowingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Follow_User1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollowUser)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Follow_User");
            });

            modelBuilder.Entity<Intake>(entity =>
            {
                entity.Property(e => e.Intake1)
                    .HasColumnName("Intake")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_Like_Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Like_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Like)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Like_User");
            });

            modelBuilder.Entity<OnlineUser>(entity =>
            {
                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.IntakeId).HasColumnName("IntakeID");

                entity.Property(e => e.Track1)
                    .HasColumnName("Track")
                    .HasMaxLength(200);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Track)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Track_Branch");

                entity.HasOne(d => d.Intake)
                    .WithMany(p => p.Track)
                    .HasForeignKey(d => d.IntakeId)
                    .HasConstraintName("FK_Track_Intake");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.BgPic).HasColumnName("bgPic");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Cvfile).HasColumnName("CVFile");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IntakeId).HasColumnName("IntakeID");

                entity.Property(e => e.JobTitle).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NationalId).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.TrackId).HasColumnName("trackID");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_User_Branch");

                entity.HasOne(d => d.Intake)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IntakeId)
                    .HasConstraintName("FK_User_Intake");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.TrackId)
                    .HasConstraintName("FK_User_Track");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserTypeId)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserContacts>(entity =>
            {
                entity.Property(e => e.ContactDetails).HasMaxLength(200);

                entity.Property(e => e.ContactTypeId).HasColumnName("ContactTypeID");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.UserContacts)
                    .HasForeignKey(d => d.ContactTypeId)
                    .HasConstraintName("FK_UserContacts_ContactType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserContacts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserContacts_User");
            });

            modelBuilder.Entity<UserEducation>(entity =>
            {
                entity.Property(e => e.Degree).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FieldOfStudy).HasMaxLength(50);

                entity.Property(e => e.Grade).HasMaxLength(50);

                entity.Property(e => e.SchoolName).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserEducation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserEducation_User");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<UserWorkExperience>(entity =>
            {
                entity.Property(e => e.CompanyLocation).HasMaxLength(100);

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.EmploymentType).HasMaxLength(100);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserWorkExperience)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserWorkExperience_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
