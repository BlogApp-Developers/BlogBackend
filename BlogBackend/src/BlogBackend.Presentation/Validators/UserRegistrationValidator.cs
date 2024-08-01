
using BlogBackend.Core.Dtos;
using BlogBackend.Core.Dtos.Models;
using FluentValidation;


namespace BlogBackend.Presentation.Validators;


public class UserRegistrationValidator : AbstractValidator<RegistrationDto>
{
     public UserRegistrationValidator()
    {


        base.RuleFor(u => u.Email)
                        .NotEmpty()
                        .EmailAddress();

        


        base.RuleFor(u => u.Name)
            .NotEmpty();

        


    }

        
}
