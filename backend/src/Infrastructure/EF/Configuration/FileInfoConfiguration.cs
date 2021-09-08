using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Configuration
{
    public class FileInfoConfiguration : IEntityTypeConfiguration<FileInfo>
    {
        public void Configure(EntityTypeBuilder<FileInfo> builder)
        {
            builder.ToTable("FileInfos");

            builder.HasOne(f => f.VacancyCandidate)
                .WithOne(c => c.CvFileInfo)
                .HasForeignKey<VacancyCandidate>(c => c.CvFileInfoId)
                .HasConstraintName("candidate_fileInfo_FK")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}