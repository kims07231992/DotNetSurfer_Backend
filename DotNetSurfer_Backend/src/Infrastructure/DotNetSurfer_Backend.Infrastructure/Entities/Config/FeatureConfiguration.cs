using DotNetSurfer_Backend.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DotNetSurfer_Backend.Infrastructure.Entities.Config
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features");
            builder.HasKey(f => f.FeatureId);
            builder.Property(f => f.FeatureType)
                .HasConversion(
                f => f.ToString(),
                f => (FeatureType)Enum.Parse(typeof(FeatureType), f));
        }
    }
}
