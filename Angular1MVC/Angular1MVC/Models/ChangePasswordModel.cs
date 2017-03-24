using Angular1MVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Angular1MVC.Models
{
    public class ChangePasswordModel:IValidatableObject
    {
        public string OldPassword { set; get; }

        public string NewPassword { set; get; }

        public string ReNewPassword { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validate = new ChangePasswordValidator();
            var result = validate.Validate(this);
            return result.Errors.Select(x => new ValidationResult(x.ErrorMessage, new[] { x.PropertyName }));
        }

    }
}