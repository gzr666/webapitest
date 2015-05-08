using BitTech.Data.Context;
using BitTech.Data.Entities;
using BitTech.Data.Repositories;
using BitTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BitTech.Web.Controllers
{
    public class CoursesController : BaseApiController
    {
        public CoursesController(ILearningRepository repo)
            : base(repo)
        {
 
        }

        public IHttpActionResult Get(int page = 0,int pageSize = 10)
        {
            IQueryable<Course> query;

            //kreiranje paging objekta

            //result
            query = TheRepository.GetAllCourses().OrderBy(c => c.CourseSubject.Id);

            //broj stranica i ukupni broj
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            //prev i next page

            var urlHelper = new UrlHelper(Request);

            var prevPage = page > 0 ? urlHelper.Link("Courses", new { page = page - 1 }) : "";

            var nextPage = page < totalPages - 1 ? urlHelper.Link("Courses", new { page = page + 1 }) : "";

            var results = query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(s => TheModelFactory.Create(s));







            return Ok(new {

                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevPage,
                NextPageLink = nextPage,
                Results = results
            
            });

        }

        public IHttpActionResult Get(int id)
        {
            try
            {

                var result = TheRepository.GetCourse(id);

                if (result != null)
                {
                    return Ok(TheModelFactory.Create(result));
                }

                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Post(CourseModel courseModel)
        {

            var entity = TheModelFactory.ParseFromCourseModel(courseModel);

            if (entity == null)
            {
                return BadRequest("Please send valid data");
            }

            if (TheRepository.Insert(entity) && TheRepository.SaveAll())
            {
                return CreatedAtRoute("Courses", new { id = entity.Id }, TheModelFactory.Create(entity));
            }

            else {
                return BadRequest("Could not save to database");
            }
        }


        [HttpPatch]
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] CourseModel courseModel)
        {
            var updatedCourse = TheModelFactory.ParseFromCourseModel(courseModel);

            if (updatedCourse == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Could not read posted values");
            }

            var originalCourse = TheRepository.GetCourse(id, false);

            if (originalCourse == null || originalCourse.Id != id)
            {
                return Request.CreateResponse(HttpStatusCode.NotModified,"Wrong id posted in body");
            }

            else {

                updatedCourse.Id = id;
            }

            if(TheRepository.Update(originalCourse,updatedCourse) && TheRepository.SaveAll())
            {
                return Request.CreateResponse(HttpStatusCode.OK,TheModelFactory.Create(updatedCourse));
            
            }

            else{

                return Request.CreateResponse(HttpStatusCode.NotModified);
            }

        
        }


        public IHttpActionResult Delete(int id)
        {

            var course = TheRepository.GetCourse(id);

            if (course == null)
            {
                return NotFound();
            
            }

            if (course.Enrollments.Count > 0)
            {
                return BadRequest("Could not delete course with enrolled students");
            
            }

            if (TheRepository.DeleteCourse(id) && TheRepository.SaveAll())
            {
                return Ok();

            }
            else {
                return BadRequest();
            }
        
        }

    }
}
