using BitTech.Data.Entities;
using BitTech.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BitTech.Web.Controllers
{
    public class EnrollmentsController : BaseApiController
    {

        public EnrollmentsController(ILearningRepository repo)
            : base(repo)
        { 
        }

        public IHttpActionResult Get(int courseID, int page = 0, int pageSize = 10)
        {
            IQueryable<Student> query;

            query = TheRepository.GetEnrolledStudentsInCourse(courseID).OrderBy(s=>s.FirstName);

            var totalCount = query.Count();

            var result = query.Skip(page * pageSize)
                .Take(pageSize).ToList().Select(s => TheModelFactory.CreateSummary(s));

            return Ok(result);
        
        }

        public HttpResponseMessage Post(int courseID,[FromUri] string username,[FromBody] Enrollment enrollment)
        {
            if(!TheRepository.CourseExist(courseID))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not find course");
            }

           var student = TheRepository.GetStudent(username);

            if(student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified, "Could not find student");
            }

            var result = TheRepository.EnrollStudentInCourse(student.Id, courseID, enrollment);

            if (result == 1)
            {
                return Request.CreateResponse(HttpStatusCode.Created,"Succesfully created");
            }
            else if (result == 2)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Already enrolled in this course");

            }

            else {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Try later");
            }
        
        }

    }
}
