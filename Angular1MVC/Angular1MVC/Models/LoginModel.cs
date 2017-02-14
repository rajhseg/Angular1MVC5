using Angular1MVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Angular1MVC.Models
{
    public class LoginModel:IValidatableObject
    {
        public string UserName { set; get; }

        public string Password { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new LoginModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(x => new ValidationResult(x.ErrorMessage, new[] { x.PropertyName }));
        }
    }
}