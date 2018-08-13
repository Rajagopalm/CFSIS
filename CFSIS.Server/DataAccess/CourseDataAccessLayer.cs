using CFSIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFSIS.Server.DataAccess
{
    public class CourseDataAccessLayer
    {
        CFSISContext db = new CFSISContext();

        //To Get all courses details   
        public IEnumerable<Course> GetAllCourses()
        {
            try
            {
                return db.Course.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new course record     
        public void AddCourse(Course course)
        {
            try
            {
                db.Course.Add(course);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar course    
        public void UpdateCourse(Course course)
        {
            try
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular course    
        public Course GetCourseData(int id)
        {
            try
            {
                Course course = db.Course.Find(id);
                return course;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular course    
        public void DeleteCourse(int id)
        {
            try
            {
                Course emp = db.Course.Find(id);
                db.Course.Remove(emp);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
