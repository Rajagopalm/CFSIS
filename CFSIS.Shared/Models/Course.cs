using System;
using System.Collections.Generic;

namespace CFSIS.Shared.Models
{
    public partial class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDesc { get; set; }
    }
}
