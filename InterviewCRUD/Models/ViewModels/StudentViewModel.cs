using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewCRUD.Models.ViewModels
{
    public class StudentViewModel : IValidatableObject
    {
        [DisplayName("學號")]
        public string Number { get; set; }
        [DisplayName("生日")]
        public string Birthday { get; set; }
        [DisplayName("學生名稱")]
        [StringLength(20,ErrorMessage = "{0}不可超過20個字")]
        public string Name { get; set; }
        [DisplayName("信箱")]
        [StringLength(50, ErrorMessage = "{0}不可超過50個字")]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(Number) || !Number.StartsWith("S") || Number.Length != 5 || !int.TryParse(Number.Substring(1,4), out var number))
            {
                yield return new ValidationResult("學號格式錯誤", new[] { nameof(Number) });
            }
            else if(!DateTime.TryParse(Birthday, out var birthday))
            {
                yield return new ValidationResult("生日格式錯誤", new[] { nameof(Birthday) });
            }
        }
    }
}