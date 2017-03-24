using Angular1MVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Angular1MVC.Models
{
    public class UserModel:IValidatableObject
    {
        public int? id { set; get; }
        public string displayname { set; get; }

        public string username { set; get; }

        public string email { set; get; }
        
        public int? phone { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new UserModelValidator();
            var result =validator.Validate(this);
            return result.Errors.Select(x => new ValidationResult(x.ErrorMessage, new[] { x.PropertyName }));
        }
    }
}