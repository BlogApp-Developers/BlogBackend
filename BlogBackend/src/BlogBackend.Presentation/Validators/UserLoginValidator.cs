using BlogBackend.Core.Dtos.Models;
using FluentValidation;

namespace BlogBackend.Presentation.Validators;

public class UserLoginValidator: AbstractValidator<LoginDto>
{
     public UserLoginValidator()
    {


        base.RuleFor(u => u.Email)
                        .NotEmpty()
                        .EmailAddress();

        


        base.RuleFor(u => u.Name)
            .NotEmpty();

        


    }

        
}

