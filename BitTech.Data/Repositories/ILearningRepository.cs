using BitTech.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitTech.Data.Repositories
{
   public interface ILearningRepository
    {
       //SUBJECT OPERATIONS
       IQueryable<Subject> GetAllSubjects();
       Subject GetSubject(int subjectId);

       //COURSES OPERATIONS
       IQueryable<Course> GetAllCourses();
       IQueryable<Course> GetCoursesBySubject(int subjectID);
       Course GetCourse(int courseID, bool includeEnrollments = true);
       bool CourseExists(int courseId);


       IQueryable<Student> GetAllStudentsWithEnrollments();
       IQueryable<Student> GetAllStudentsSummary();

       IQueryable<Student> GetEnrolledStudentsInCourse(int courseId);
       Student GetStudentEnrollments(string userName);
       Student GetStudent(string userName);

       Tutor GetTutor(int tutorId);

       bool LoginStudent(string userName, string password);

       bool Insert(Student student);
       bool Update(Student originalStudent, Student updatedStudent);
       bool DeleteStudent(int id);

       int EnrollStudentInCourse(int studentId, int courseId, Enrollment enrollment);

       bool Insert(Course course);
       bool Update(Course originalCourse, Course updatedCourse);
       bool DeleteCourse(int id);

       bool CourseExist(int courseID);

       bool SaveAll();





    }
}
