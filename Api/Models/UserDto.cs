using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class UserDto :IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string UserName {get; set; }
        [Required]
        [MaxLength(500)]
        public string PassWord { get; set; }
        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Email{ get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
                yield return new ValidationResult("نام کاربری نمی تواند test باشد", new string[]{nameof(UserName) });
            if (PassWord.Equals("1231456"))
                yield return new ValidationResult("کلمه عبور نمی تواند 123456 باشد",new string[] {nameof(PassWord) });
            if (Gender.Equals(1) && Age > 30)
            {
                yield return new ValidationResult("آقایان نمی توانند بالاتر تر از 30 سال سن داشته باشن",new string[] {nameof(Gender),nameof(Age) });
            }
        }
    }
}
