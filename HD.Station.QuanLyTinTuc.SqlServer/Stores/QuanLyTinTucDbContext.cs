using HD.Station.QuanLyTinTuc.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace HD.Station.QuanLyTinTuc.SqlServer.Stores;

public partial class QuanLyTinTucDbContext : DbContext
{
    public QuanLyTinTucDbContext()
    {
    }

    public QuanLyTinTucDbContext(DbContextOptions<QuanLyTinTucDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; } = null!;

    public virtual DbSet<ArticleTag> ArticleTags { get; set; } = null!;

    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public virtual DbSet<Tag> Tags { get; set; } = null!;

    public virtual DbSet<Topic> Topics { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Server=localhost;Database=QuanLyTinTuc;User Id=SA;Password=Sql_password;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.ArticleId);

            // entity.Property(e => e.ArticleId).ValueGeneratedNever();
            entity.Property(e => e.BannerImage).HasMaxLength(255);
            entity.Property(e => e.Body).HasColumnType("ntext");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Slug).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ArticleTag>(entity =>
        {
            entity.HasKey(e => e.ArticleTagId);

            entity.ToTable("ArticleTag");

            entity.Property(e => e.ArticleTagId)
                // .ValueGeneratedNever()
                .HasColumnName("ArticleTagId");

            entity.HasOne(d => d.Article).WithMany(p => p.Tags)
                .HasForeignKey(d => d.ArticleId);

            entity.HasOne(d => d.Tag).WithMany(p => p.ArticleTags)
                .HasForeignKey(d => d.TagId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("Comment");

            // entity.Property(e => e.CommentId).ValueGeneratedNever();
            entity.Property(e => e.CommentBody).HasColumnType("ntext");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId);

            entity.ToTable("Tag");

            // entity.Property(e => e.TagId).ValueGeneratedNever();
            entity.Property(e => e.TagName).HasMaxLength(255);
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId);

            entity.ToTable("Topic");

            // entity.Property(e => e.TopicId).ValueGeneratedNever();
            entity.Property(e => e.TopicName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            // entity.Property(e => e.Id).ValueGeneratedNever(); // use ValueGeneratedNever result in IDENTITY_INSERT error
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
