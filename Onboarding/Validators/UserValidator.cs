using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onboarding.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.firstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Characters");

            RuleFor(p => p.lastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid")
                .Must(BeAValidName).WithMessage("{PropertyName} Contains Invalid Characters");

            RuleFor(p => p.email)
                .EmailAddress();

            RuleFor(p => p.phoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid");

            RuleFor(p => p.password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(2, 50).WithMessage("Length ({TotalLength}) of {PropertyName} Invalid");

        }

        protected bool BeAValidName(string name)
        {
            name = name.Trim();
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }
    }
}
