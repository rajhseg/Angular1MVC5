using Angular1MVC.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular1MVC.Validators
{
    public class LoginModelValidator:AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Invalid username");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Invalid password");
        }
    }
}