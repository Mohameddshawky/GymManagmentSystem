using GymManagmentDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Data.Configurations
{
    internal class MemberConfiguration :GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {

            builder.Property(o => o.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GetDate()");

            base.Configure(builder);
           
        }
    }
}
