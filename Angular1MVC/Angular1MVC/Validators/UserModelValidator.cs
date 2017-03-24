using Angular1MVC.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular1MVC.Validators
{
    public class UserModelValidator:AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.username).NotEmpty().WithMessage("Invalid User name");
            RuleFor(x => x.displayname).NotEmpty().WithMessage("Invalid Display name");
            RuleFor(x => x.email).NotEmpty().EmailAddress().WithMessage("Invalid Email address");
            RuleFor(x => x.phone).NotEmpty().WithMessage("Invalid Phone Numner");          
        }
    }
}