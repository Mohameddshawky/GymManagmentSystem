using GymManagmentDAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagmentDAL.Data.Configurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(o =>
            {
                o.HasCheckConstraint("CK_Session_Capacity_NonNegative", "Capacity between 1 and 25");
                o.HasCheckConstraint("CK_Session_EndDate", "EndDate >StartDate");
            });

            builder.HasOne(o => o.SessionCategory)
                .WithMany(o => o.Sessions)
                .HasForeignKey(o => o.CategoryId);
            builder.HasOne(o => o.SessionTrainer)
                .WithMany(o => o.TrainerSessions)
                .HasForeignKey(o => o.TrainerId);


        }
    }
}
