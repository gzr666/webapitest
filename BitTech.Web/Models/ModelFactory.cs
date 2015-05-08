using BitTech.Data.Entities;
using BitTech.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace BitTech.Web.Models
{
    public class ModelFactory
    {
        private UrlHelper urlHelper;
        private ILearningRepository repository;

        public ModelFactory(HttpRequestMessage request,ILearningRepository repo)
        {
            urlHelper = new UrlHelper(request);
            repository = repo;

        }
        public SubjectModel Create(Subject subject)
        {
            return new SubjectModel
            {
                Id = subject.Id,
                Name = subject.Name
            };

        }

        public CourseModel Create(Course course)
        {

            return new CourseModel
            {
                Id = course.Id,
                Name = course.Name,
                Url = urlHelper.Link("Courses",new {id=course.Id}),
                Duration = course.Duration,
                Description = course.Description,
                Tutor = Create(course.CourseTutor),
                Subject = Create(course.CourseSubject)
            };
        }

        public TutorModel Create(Tutor tutor)
        {
            return new TutorModel()
            {
                Id = tutor.Id,
                Email = tutor.Email,
                UserName = tutor.UserName,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Gender = tutor.Gender
            };
        }

        public EnrollmentModel Create(Enrollment enrollment)
        {
            return new EnrollmentModel()
            {
                EnrollmentDate = enrollment.EnrollmentDate,
                Course = Create(enrollment.Course)
            };
        }

        public Course ParseFromCourseModel(CourseModel courseModel)
        {
            try {

                var course = new Course 
                {
                Name = courseModel.Name,
                Duration = courseModel.Duration,
                Description = courseModel.Description,
                CourseSubject = repository.GetSubject(courseModel.Subject.Id),
                CourseTutor = repository.GetTutor(courseModel.Tutor.Id)

                };

                return course;
            }
            catch (Exception)
            {
                return null;
            }


        }

        public StudentBaseModel CreateSummary(Student student)
        {
            return new StudentBaseModel()
            {
                Url = urlHelper.Link("StudentsList", new { userName = student.UserName }),
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Gender = student.Gender,
                EnrollmentsCount = student.Enrollments.Count(),
            };
        
        }
    }
}