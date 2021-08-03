using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewCRUD.Models.ViewModels
{
    public class StudentViewModel
    {
        public string Number { get; set; }
        public string Birthday { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}