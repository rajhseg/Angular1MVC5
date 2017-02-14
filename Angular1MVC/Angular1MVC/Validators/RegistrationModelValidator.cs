using Angular1MVC.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular1MVC.Validators
{
    public class RegistrationModelValidator:AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Invalid User name");
            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("Invalid Display name");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid Email address");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Invalid Phone Numner");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password should not empty");
        }
    }
}