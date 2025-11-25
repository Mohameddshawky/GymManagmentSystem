using GymManagmentDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Data.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                .HasKey(x => x.Id);
            builder.HasOne<Member>()
                   .WithOne(o=>o.healthRecord)
                   .HasForeignKey<HealthRecord>(fk => fk.Id);

            builder.Ignore(e => e.CreatedAt);
            builder.Ignore(e => e.UbdatedAt);
        }
    }
}
