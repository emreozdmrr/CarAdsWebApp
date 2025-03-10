using CarAdsWebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.DataAccess.Configurations
{
    public class AdvertisementConfiguration:IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasColumnType("ntext").IsRequired();
            builder.Property(x => x.City).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.BodyType).HasMaxLength(50).IsRequired();
            builder.Property(x => x.GearBox).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.Make).WithMany(x => x.Advertisements).HasForeignKey(x => x.MakeId);
            builder.HasOne(x => x.Model).WithMany(x => x.Advertisements).HasForeignKey(x => x.ModelId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Advertisements).HasForeignKey(x => x.AppUserId);
            builder.HasOne(x => x.BodyType).WithMany(x => x.Advertisements).HasForeignKey(x => x.BodyTypeId);
            builder.HasOne(x => x.GearBox).WithMany(x => x.Advertisements).HasForeignKey(x => x.GearBoxId);
        }
    }
}
