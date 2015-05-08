using BitTech.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTech.Data.Repositories
{
    public class LearningRepository : ILearningRepository
    {
        private LearningContext context;
        public LearningRepository(LearningContext _ctx)
        {

            context = _ctx;

        
        }

        public IQueryable<Entities.Subject> GetAllSubjects()
        {
            return context.Subjects.AsQueryable();
        }

        public Entities.Subject GetSubject(int subjectId)
        {
            return context.Subjects.Find(subjectId);
        }

        public IQueryable<Entities.Course> GetAllCourses()
        {
            return context.Courses
                .Include("CourseTutor")
                .Include("CourseSubject")
                .AsQueryable();
        }

        public IQueryable<Entities.Course> GetCoursesBySubject(int subjectID)
        {
            return context.Courses
                .Include("CourseSubject")
                .Include("CourseTutor")
                .Where(c => c.CourseSubject.Id == subjectID).AsQueryable();
        }

        public Entities.Course GetCourse(int courseID, bool includeEnrollments = true)
        {
            if (includeEnrollments)
            {
                return context.Courses
                    .Include("Enrollments")
                   .Include("CourseSubject")
                   .Include("CourseTutor")
                   .Where(c => c.Id == courseID)
                   .SingleOrDefault();
            }
            else {

                return context.Courses
                    .Include("CourseSubject")
                    .Include("CourseTutor")
                    .Where(c => c.Id == courseID)
                    .SingleOrDefault();
            
            }
        }

        public bool CourseExists(int courseId)
        {
            return context.Courses.Any(c=>c.Id==courseId);
        }

        public IQueryable<Entities.Student> GetAllStudentsWithEnrollments()
        {
            return context.Students
                .Include("Enrollments")
                .Include("Enrollments.Course")
                .Include("Enrollments.Course.CourseSubject")
                .Include("Enrollments.Course.CourseTutor")
                .AsQueryable();
        }

        public IQueryable<Entities.Student> GetAllStudentsSummary()
        {
            return context.Students.AsQueryable();
        }

        public IQueryable<Entities.Student> GetEnrolledStudentsInCourse(int courseId)
        {
            return context.Students
                .Include("Enrollments")
                .Where(s => s.Enrollments.Any(e => e.Course.Id == courseId))
                .AsQueryable();
        }

        public Entities.Student GetStudentEnrollments(string userName)
        {
            return context.Students
                .Include("Enrollments")
                .Where(s => s.UserName == userName).SingleOrDefault();
        }

        public Entities.Student GetStudent(string userName)
        {
            return context.Students.Where(s=>s.UserName==userName).SingleOrDefault();
        }

        public Entities.Tutor GetTutor(int tutorId)
        {
            return context.Tutors.Find(tutorId);
        }

        public bool LoginStudent(string userName, string password)
        {
            var student = context.Students.Where(s => s.UserName == userName).SingleOrDefault();

            if (student != null)
            {
                if (student.Password == password)
                {
                    return true;
                }
            }

            return false;
        }

        public bool Insert(Entities.Student student)
        {
            try
            {
                context.Students.Add(student);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Entities.Student originalStudent, Entities.Student updatedStudent)
        {
            context.Entry(originalStudent).CurrentValues.SetValues(updatedStudent);
            return true;
        }

        public bool DeleteStudent(int id)
        {
            try
            {
                var entity = context.Students.Find(id);
                if (entity != null)
                {
                    context.Students.Remove(entity);
                    return true;
                }
            }
            catch
            {
                // TODO Logging
            }

            return false;
        }

        public int EnrollStudentInCourse(int studentId, int courseId, Entities.Enrollment enrollment)
        {
           

            

            try
            {
                if (context.Enrollments.Any(e => e.Course.Id == courseId && e.Student.Id == studentId))
                { 
                    return 2;
                }

                context.Database.ExecuteSqlCommand
                    ("INSERT INTO Enrollments VALUES (@p0,@p1,@p2)",
                    enrollment.EnrollmentDate,
                    courseId.ToString(),
                    studentId.ToString());

                return 1;

            }
            catch (DbEntityValidationException dbex){

                foreach (var eve in dbex.EntityValidationErrors)
                {
                    string line = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        line = string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);

                    }
                }
                return 0;

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public bool Insert(Entities.Course course)
        {
            try
            {
                context.Courses.Add(course);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Entities.Course originalCourse, Entities.Course updatedCourse)
        {
            context.Entry(originalCourse).CurrentValues.SetValues(updatedCourse);

            //update child entities

            originalCourse.CourseSubject = updatedCourse.CourseSubject;
            originalCourse.CourseTutor = updatedCourse.CourseTutor;

            return true;


        }

        public bool DeleteCourse(int id)
        {
            try
            {
                var entity = context.Courses.Find(id);
                if (entity != null)
                {
                    context.Courses.Remove(entity);
                    return true;
                }
            }
            catch
            {
                //ToDo: Logging
            }

            return false;
        }

        public bool CourseExist(int courseID)
        {
            return context.Courses.Any(c => c.Id == courseID);
        
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }
    }
}
