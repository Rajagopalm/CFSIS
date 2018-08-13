using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFSIS.Server.DataAccess;
using CFSIS.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CFSIS.Server.Controllers
{
    public class CourseController : Controller
    {
        CourseDataAccessLayer objcourse = new CourseDataAccessLayer();

        // To Fetch all course records
        [HttpGet]
        [Route("api/Course/Index")]
        public IEnumerable<Course> Index()
        {
            return objcourse.GetAllCourses();
        }

        // To Create a new course record
        [HttpPost]
        [Route("api/Course/Create")]
        public void Create([FromBody] Course course)
        {
            if (ModelState.IsValid)
                objcourse.AddCourse(course);
        }

        // To fetch the details of a particular course
        [HttpGet]
        [Route("api/Course/Details/{id}")]
        public Course Details(int id)
        {

            return objcourse.GetCourseData(id);
        }

        // Edit an existing course records
        [HttpPut]
        [Route("api/Course/Edit")]
        public void Edit([FromBody]Course course)
        {
            if (ModelState.IsValid)
                objcourse.UpdateCourse(course);
        }

        [HttpDelete]
        [Route("api/Course/Delete/{id}")]
        public void Delete(int id)
        {
            objcourse.DeleteCourse(id);
        }

    }
}
