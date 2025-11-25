using GymManagmentDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Data.Configurations
{
    internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(o => o.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(o => o.Description)
                .HasColumnType("varchar")
                .HasMaxLength(200);
            builder.Property(o => o.Price)
                .HasPrecision(10, 2);

            builder.ToTable(o =>
            {
                o.HasCheckConstraint("CK_Plan_DurationDays_Check", "DurationDays between 1 and 365");
            }) ;
                


        }
    }
}
