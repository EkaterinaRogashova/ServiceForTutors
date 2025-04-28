using Microsoft.EntityFrameworkCore;
using ServiceForTutorDatabaseImplements.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = ServiceForTutorDatabaseImplements.Models.Task;

namespace ServiceForTutorDatabaseImplements
{
    public class ServiceForTutorDatabase: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ServiceForTutorBd;Username=postgres;Password=1234");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<AssignedTask> AssignedTasks { get; set; }
        public virtual DbSet<InvitationCode> InvitationCodes { get; set; }
        public virtual DbSet<PurchasedTariffPlan> PurchasedTariffPlans { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }
        public virtual DbSet<TariffPlan> TariffPlans { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TutorStudent> TutorStudents { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<StudentWhiteboard> StudentWhiteboards { get; set; }

    }
}
