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
    public class BodyTypeConfiguration:IEntityTypeConfiguration<BodyType>
    {
        public void Configure(EntityTypeBuilder<BodyType> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(50).IsRequired();
            builder.HasData(new BodyType[]
            {
                new BodyType
                {
                    Id=1,
                    Definition="Cabrio"
                },
                new BodyType
                {
                    Id = 2,
                    Definition ="Coupe"
                },
                new BodyType
                {
                    Id=3,
                    Definition="Hatchback 3 kapı"
                },
                new BodyType
                {
                    Id=4,
                    Definition="Hatchback 5 kapı"
                },
                new BodyType
                {
                    Id=5,
                    Definition="Sedan"
                },
                new BodyType
                {
                    Id=6,
                    Definition="MPV"
                },
                new BodyType
                {
                    Id=7,
                    Definition="Roadster"
                }
            });
        }
    }
}
