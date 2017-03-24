using Angular1MVC.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular1MVC.Validators
{
    public class ChangePasswordValidator:AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Old Password is required");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New Password is required");
            RuleFor(x => x.ReNewPassword).NotEmpty().WithMessage("Re Enter new password is required");
        }
    }
}