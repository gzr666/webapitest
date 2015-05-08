using BitTech.Data.Entities;
using BitTech.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTech.Data.Context
{
    public class LearningContext : DbContext

    {
        public LearningContext()
            : base("eLearning")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Tutor> Tutors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseMapper());
            modelBuilder.Configurations.Add(new EnrollmentMapper());
            modelBuilder.Configurations.Add(new StudentMapper());
            modelBuilder.Configurations.Add(new SubjectMapper());
            modelBuilder.Configurations.Add(new TutorMapper());



            base.OnModelCreating(modelBuilder);
        }

    }
}
