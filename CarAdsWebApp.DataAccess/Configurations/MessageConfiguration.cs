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
    public class MessageConfiguration:IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(x => x.Sender).WithMany(x => x.SentMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Receiver).WithMany(x => x.ReceivedMessages).HasForeignKey(x => x.ReceiverId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();
        }
    }
}
