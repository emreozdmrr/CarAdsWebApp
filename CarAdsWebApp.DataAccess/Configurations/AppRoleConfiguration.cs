﻿using CarAdsWebApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAdsWebApp.DataAccess.Configurations
{
    public class AppRoleConfiguration:IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(50).IsRequired();
            builder.HasData(new AppRole[]
            {
                new AppRole
                {
                    Id=1,
                    Definition="Admin"
                },
                new AppRole
                {
                    Id = 2,
                    Definition = "User"
                }
            });
        }
    }
}
