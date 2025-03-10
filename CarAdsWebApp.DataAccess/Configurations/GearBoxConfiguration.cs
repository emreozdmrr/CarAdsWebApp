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
        }
    }
}
