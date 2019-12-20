using DotNetSurfer_Backend.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DotNetSurfer_Backend.Infrastructure.Entities.Config
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(p => p.PermissionId);
            builder.Property(p => p.PermissionType)
                .HasConversion(
                    p => p.ToString(),
                    p => (PermissionType)Enum.Parse(typeof(PermissionType), p));
        }
    }
}
