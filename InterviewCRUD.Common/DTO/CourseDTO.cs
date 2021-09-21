using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewCRUD.Common.DTO
{
    public class CourseDTO
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Credit { get; set; }
        public string Place { get; set; }
        public string TeacherName { get; set; }
        public bool IsSeleted { get; set; }
    }
}
