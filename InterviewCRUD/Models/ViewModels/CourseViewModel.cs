using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewCRUD.Models.ViewModels
{
    public class CourseViewModel : IValidatableObject
    {
        [Required]
        [DisplayName("課號")]
        public string Number { get; set; }
        [DisplayName("課程名稱")]
        [StringLength(20, ErrorMessage = "{0}不可超過20個字")]
        public string Name { get; set; }
        [DisplayName("學分數")]
        public string Credit { get; set; }
        [DisplayName("上課地點")]
        [StringLength(20, ErrorMessage = "{0}不可超過20個字")]
        public string Place { get; set; }
        [DisplayName("教師名稱")]
        [StringLength(20, ErrorMessage = "{0}不可超過20個字")]
        public string TeacherName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Number) || !Number.StartsWith("C") || Number.Length != 4 || !int.TryParse(Number.Substring(1, 3), out var number))
            {
                yield return new ValidationResult("課號格式錯誤", new[] { nameof(Number) });
            }

            if(string.IsNullOrEmpty(Credit) || Credit.Contains('.') || !int.TryParse(Credit,out var credit))
            {
                yield return new ValidationResult("學分格式錯誤，限制為整數", new[] { nameof(Number) });
            }
        }
    }
}