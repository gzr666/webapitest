using BitTech.Data.Entities;
using BitTech.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BitTech.Web.Controllers
{
    public class StudentsController : BaseApiController
    {
        public StudentsController(ILearningRepository repo)
            : base(repo)
        { 
        
        }

        public IHttpActionResult Get(int page = 0,int pageSize = 10)
        {
            IQueryable<Student> query;

            query = TheRepository.GetAllStudentsWithEnrollments().OrderBy(s => s.UserName);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("StudentsList", new { page = page - 1 }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("StudentsList", new { page = page + 1 }) : "";

            var result = query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(s => TheModelFactory.CreateSummary(s));


            return Ok(new {

                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink,
                Results = result
            });
        
        }


    }
}
