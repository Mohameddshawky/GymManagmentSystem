using GymManagmentDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Data.Configurations
{
    public class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Property(o=>o.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()");   
            builder.HasKey(ms => new { ms.MemberId, ms.SessionId });
            builder.Ignore(o => o.Id);
        }
    }
}
