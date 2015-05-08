using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace BitTech.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Courses",
                routeTemplate: "api/courses/{id}",
                defaults: new { controller="courses",id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(

                name: "StudentsList",
                routeTemplate: "api/students/{username}",
                defaults: new { controller="students",username = RouteParameter.Optional}

                );
            config.Routes.MapHttpRoute(

                name: "Students",
                routeTemplate: "api/courses/{courseID}/students/{username}",
                defaults: new { controller="enrollments",username= RouteParameter.Optional}

                );

            // camel casing formatters
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
