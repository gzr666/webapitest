using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitTech.Data.Entities
{
    public class Course
    {

        public Course()
        {
            CourseSubject = new Subject();
            CourseTutor = new Tutor();
            Enrollments = new List<Enrollment>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        public string Description { get; set; }
    
        //navigational
        public Tutor CourseTutor { get; set; }
        public Subject CourseSubject { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }







    }
}
