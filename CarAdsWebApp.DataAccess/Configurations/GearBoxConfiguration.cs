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
    public class GearBoxConfiguration:IEntityTypeConfiguration<GearBox>
    {
        public void Configure(EntityTypeBuilder<GearBox> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(50).IsRequired();
            builder.HasData(new GearBox[]
            {
                new GearBox
                {
                    Id=1,
                    Definition="Manuel"
                },
                new GearBox
                {
                    Id=2,
                    Definition="Otomatik"
                },
                new GearBox
                {
                    Id =3,
                    Definition="Yarı Otomatik"
                }
            });
        }
    }
}
