using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configuration
{
    public class StageReviewConfiguration : IEntityTypeConfiguration<StageReview>
    {
        public void Configure(EntityTypeBuilder<StageReview> builder)
        {
            builder.HasOne(sr => sr.Stage)
                .WithMany(s => s.StageReviews)
                .HasForeignKey(sr => sr.StageId)
                .HasConstraintName("stage_review_stage_FK")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sr => sr.Review)
                .WithMany(r => r.StageReviews)
                .HasForeignKey(sr => sr.ReviewId)
                .HasConstraintName("stage_review_review_FK")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
